package com.shvil.project.sync;

import java.io.BufferedInputStream;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.lang.reflect.Type;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.ArrayList;
import java.util.Collection;
import java.util.List;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.content.Context;
import android.content.SharedPreferences;
import android.content.SharedPreferences.Editor;
import android.database.Cursor;
import android.os.Build;
import android.preference.PreferenceManager;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import com.shvil.project.MyApplication;
import com.shvil.project.db.Angel;
import com.shvil.project.db.AngelDao;
import com.shvil.project.db.GeneralItem;
import com.shvil.project.db.GeneralItem.GeneralItemType;
import com.shvil.project.db.GeneralItemDao;
import com.shvil.project.db.ServerMessage;
import com.shvil.project.db.User;
import com.shvil.project.map.MyLocation;
import com.shvil.project.menu.MyProfileActivity;

import de.greenrobot.dao.AbstractDao;
import de.greenrobot.dao.QueryBuilder;

@SuppressLint({ "UseValueOf", "UseValueOf" })
public class SyncDataBase {
	
	final String SERVER_NAME = "http://10.0.2.2:8888/shvilserver";
	MyApplication myApplication;
	Activity mContext;
	Long mUserId;
	
	public SyncDataBase(Context urls, Context urls2) {
		myApplication = (MyApplication) urls;
		mContext = (Activity) urls2;
		disableConnectionReuseIfNecessary();
		mUserId = 0L;
		
        SharedPreferences sharedPreferences = PreferenceManager.getDefaultSharedPreferences(myApplication.getApplicationContext());
		if (sharedPreferences.contains(MyProfileActivity.MY_PROFILE_PREF_USERNAME)){
			mUserId = sharedPreferences.getLong(MyProfileActivity.MY_PROFILE_PREF_USERNAME, 0);
		}
	}
	
	private void disableConnectionReuseIfNecessary() {
		if (Integer.parseInt(Build.VERSION.SDK) < Build.VERSION_CODES.FROYO) {
			System.setProperty("http.keepAlive", "false");
		}
	}
	
	public void SyncDBWithServer() {
		
		try {
			if (0 == mUserId){
				getUniqueUserId();
			}
			sendUserLocation();
			SyncDB();
		}
		catch(Exception e)
		{
			
		}
	}
	

	@SuppressWarnings({ "unchecked", "rawtypes" })
	private void SyncDB() {
		
		// for i in table name, send requests and update sever
		List<Class> classes = new ArrayList();
		classes.add(Angel.class);
		classes.add(GeneralItem.class);
		classes.add(User.class);
		classes.add(ServerMessage.class);
		
		
		for (Class db_class:  classes){
			
			// send local deleted, added, changed records from local DB to server
			String jsonRecordsToUpdate = postLocalUpdatedDbRecords(db_class);
			
			// retrieve the diff between the local and the server DB and update the local DB
			retrieveSeverUpdatedDbRecords(db_class, jsonRecordsToUpdate);				
		}
	}
	
	@SuppressWarnings({ "rawtypes", "unchecked" })
	private String postLocalUpdatedDbRecords(Class db_class) {

		AbstractDao dao = myApplication.daoSession.getDao(db_class);
		long max_server_time = getLastServerTimestamp(dao);
		byte[] payload = getJsonOfRecordsToUpdate(db_class).getBytes();
		
		// send post request
		HttpURLConnection urlConnection = null;
		URL url = null;
		InputStream in = null;
		OutputStream out = null;
		int httpStatus = 0;
		String response = null;
		
		try
		{
			url = new URL(SERVER_NAME);
			urlConnection = (HttpURLConnection)url.openConnection();
			urlConnection.setDoOutput(true);
			urlConnection.setFixedLengthStreamingMode(payload.length);
			urlConnection.setRequestProperty("action", "sync");
			urlConnection.setRequestProperty("record_type", dao.getTablename());
		    urlConnection.setRequestProperty("user_id", new Long(mUserId).toString());
		    urlConnection.setRequestProperty("max_server_time", new Long(max_server_time).toString());
			
			// this opens a connection, then sends POST & headers.
		    out = urlConnection.getOutputStream();
		    out.write(payload);

		    httpStatus = urlConnection.getResponseCode();
		    if (httpStatus / 100 != 2) {
		      // Dear me, dear me
		    }
		    
		    in = urlConnection.getInputStream(); 
		    response = readResponse(new BufferedInputStream(in));    
		}
		catch (IOException e)
		{
		}
		finally
		{
			urlConnection.disconnect();
		}
		
		return response;
	}
	
	@SuppressWarnings("rawtypes")
	private long getLastServerTimestamp(AbstractDao dao){
		long max_server_time = 0;
		
		String query = "SELECT max(server_time) FROM " + dao.getTablename();    		
		Cursor cursor = dao.getDatabase().rawQuery(query, null);
		cursor.moveToFirst();
	    while (!cursor.isAfterLast()) {
	    	max_server_time = cursor.isNull(0) ? 0 : cursor.getLong(0);
	    	cursor.moveToNext();
	    }
		cursor.close();
		return max_server_time;
	}

	@SuppressWarnings("rawtypes")
	private String getJsonOfRecordsToUpdate(Class db_class) {
		String json = "";
		Collection<?> records = null;
		Gson gson = new Gson();
		
		// TODO - sync access, don't add new overlays
		if (db_class == Angel.class){
			AngelDao dao = myApplication.daoSession.getAngelDao();
			records = dao.queryBuilder().where(AngelDao.Properties.Record_state.notEq(SyncState.RECORD_STATE_IDLE)).list();
		}
		else if (db_class == GeneralItem.class){
			GeneralItemDao dao = myApplication.daoSession.getGeneralItemDao();
			
			QueryBuilder qb = dao.queryBuilder();
			qb.where(GeneralItemDao.Properties.Record_state.notEq(SyncState.RECORD_STATE_IDLE),
					GeneralItemDao.Properties.Type.notEq((byte)GeneralItemType.GENERAL_ITEM_TYPE_WATER_HIDE.ordinal()));
			records = qb.list();
		}	
		if (records != null) {
			Type collectionType = new TypeToken<Collection<?>>(){}.getType();
			json = gson.toJson(records, collectionType);
		}
		return json;
	}

	@SuppressWarnings({ "rawtypes", "unchecked" })
	private void retrieveSeverUpdatedDbRecords(Class db_class, String jsonRecordsToUpdate) {
		// each time we retrieve all the people, and server messages
		// get diff of angels, genetalItems

		Type collectionType = null;
		Gson gson = null;
		List<?> records = null;
		AbstractDao dao = null;
		
		if (db_class == Angel.class){
			collectionType = new TypeToken<List<Angel>>(){}.getType();
		}
		else if (db_class == GeneralItem.class){
			collectionType = new TypeToken<List<GeneralItem>>(){}.getType();
		}
		else if (db_class == User.class){
			collectionType = new TypeToken<List<User>>(){}.getType();
		}
		
		
		gson = new Gson();
		records = gson.fromJson(jsonRecordsToUpdate, collectionType);
		// TODO - sync accsees to DB - don't add new overlays
		
		if (records == null){
			return;
		}
		
		dao = myApplication.daoSession.getDao(db_class);
		// each time we retrieve all the people, and server messages
		if ((db_class == User.class) || (db_class == ServerMessage.class)){
			dao.deleteAll();
		}

		// for each item, update it - insert/update the record to the db
		// the state should be idle or delete
		dao.insertOrReplaceInTx(records);
		
		// if record_state is to delete, then delete this records
		for (Object record: records){
			byte syncState = SyncState.RECORD_STATE_IDLE;
			if (db_class == Angel.class){
				syncState = ((Angel)record).getRecord_state();
			}
			else if (db_class == GeneralItem.class){
				syncState = ((GeneralItem)record).getRecord_state();
			}
			else {
				//TODO error!
			}
			
			if (syncState == SyncState.RECORD_STATE_DELETE){
				dao.delete(record);
			}
		}
		
		// delete all records with sever stamp 0 - they probably already resended by server
		if ((db_class == Angel.class) || (db_class == GeneralItem.class)){
			String query = "DELETE FROM " + dao.getTablename() + " WHERE SERVER_TIME=0";    		
			dao.getDatabase().rawQuery(query, null);
		}
	}

	private void sendUserLocation() {
		boolean share_user_location = true;
		String sLat = "";
		String sLong = "";
		
		SharedPreferences sharedPreferences = PreferenceManager.getDefaultSharedPreferences(myApplication.getApplicationContext());
		if (sharedPreferences.contains("pref_id_share_location")){
			share_user_location = sharedPreferences.getBoolean("pref_id_share_location", true);
		}
		
		if (!share_user_location){
			return;
		}
		
		// get user location
		if (sharedPreferences.contains(MyLocation.LOCATION_PREF_LAT) 
				&& sharedPreferences.contains(MyLocation.LOCATION_PREF_LONG)){
			sLat = sharedPreferences.getString(MyLocation.LOCATION_PREF_LAT, "0");
			sLong = sharedPreferences.getString(MyLocation.LOCATION_PREF_LONG, "0");
		}
		else {
			return;
		}
		
		// send user location
		HttpURLConnection urlConnection = null;
		URL url = null;
		InputStream in = null;
		int httpStatus = 0;
		
		try {
			url = new URL(SERVER_NAME);
			urlConnection = (HttpURLConnection)url.openConnection();
			urlConnection.setRequestMethod("GET");
			urlConnection.addRequestProperty("action", "getUserLocation");
			urlConnection.addRequestProperty("user_id", mUserId.toString());
			urlConnection.addRequestProperty("lat", sLat);
			urlConnection.addRequestProperty("lon", sLong);

			urlConnection.connect();

			// this opens a connection, then sends GET & headers 
		    in = urlConnection.getInputStream();
		    
		    // can't get status before getInputStream (else, got exception)
		    httpStatus = urlConnection.getResponseCode();
		    if (httpStatus / 100 != 2) {
		      // redirects, server errors, lions and tigers and bears! Oh my!
		    	urlConnection.disconnect();
		    }
		    
		    readResponse(new BufferedInputStream(in));

		} catch (MalformedURLException e) {
			e.printStackTrace();
		} catch (IOException e) {
			e.printStackTrace();
		}
		finally
		{
			urlConnection.disconnect();
	    }
	}
	
	private void getUniqueUserId() {		
		
        SharedPreferences sharedPreferences = PreferenceManager.getDefaultSharedPreferences(myApplication.getApplicationContext());
		if (sharedPreferences.contains(MyProfileActivity.MY_PROFILE_PREF_USERNAME)){
			mUserId = sharedPreferences.getLong(MyProfileActivity.MY_PROFILE_PREF_USERNAME, 0);
			return;
		}
		
		String response = askServerForUniqueUserId();
		retrieveUniqueUserId(response);
		return;
	}

	private void retrieveUniqueUserId(String response) {
		
		try {
			Gson gson = new Gson();
			Long userid = gson.fromJson(response, Long.class);
			
			if (0 == userid){
				// :( try again
				return;
			}
			
	        SharedPreferences sharedPreferences = PreferenceManager.getDefaultSharedPreferences(myApplication.getApplicationContext());
			Editor preferencesEditor = sharedPreferences.edit();
			preferencesEditor.putLong(MyProfileActivity.MY_PROFILE_PREF_USERNAME, userid);
			preferencesEditor.commit();
			mUserId = userid;
		}
		catch (Exception e)
		{
		}
		finally
		{
		}
	}

	private String askServerForUniqueUserId() {
		
		HttpURLConnection urlConnection = null;
		URL url = null;
		InputStream in = null;
		int httpStatus = 0;
		String response = null;
		
		try {
			url = new URL(SERVER_NAME);
			urlConnection = (HttpURLConnection)url.openConnection();
			urlConnection.setRequestMethod("GET");
			urlConnection.addRequestProperty("action", "getUserID");
			urlConnection.connect();

			// this opens a connection, then sends GET & headers 
		    in = urlConnection.getInputStream();
		    
		    // can't get status before getInputStream (else, got exception)
		    httpStatus = urlConnection.getResponseCode();
		    if (httpStatus / 100 != 2) {
		      // redirects, server errors, lions and tigers and bears! Oh my!
		    	urlConnection.disconnect();
		    	return null;
		    }
		    
		    response = readResponse(new BufferedInputStream(in));

		} catch (MalformedURLException e) {
			e.printStackTrace();
		} catch (IOException e) {
			e.printStackTrace();
		}
		finally
		{
			urlConnection.disconnect();
	    }
		return response;
	}

	private String readResponse(InputStream in){
		BufferedReader bufferedReader = null;
		try 
		{
	        InputStreamReader reader = new InputStreamReader(in);
	        bufferedReader = new BufferedReader(reader);
	        StringBuilder builder = new StringBuilder();
	        String line = bufferedReader.readLine();
	        while (line != null) {
	            builder.append(line);
	            line = bufferedReader.readLine();
	        }
	        return builder.toString();
		} catch (Exception e) {
			e.printStackTrace();
		}
		finally
		{
			if (bufferedReader != null)
			{
				try
				{
					bufferedReader.close();
				}
				catch (IOException e)
				{
					e.printStackTrace();
				}
			}
		}
		return null;
    }
}
