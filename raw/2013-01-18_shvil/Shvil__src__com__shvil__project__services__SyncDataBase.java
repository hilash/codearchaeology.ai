package com.shvil.project.services;

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

import android.content.Intent;
import android.os.Build;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import com.shvil.project.MyApplication;
import com.shvil.project.db.Angel;
import com.shvil.project.db.GeneralItem;

import de.greenrobot.dao.AbstractDao;

public class SyncDataBase {
	
	final String SERVER_NAME = "http://10.0.2.2:8080/ShvilServer/";
	MyApplication myApplication;
	
	public SyncDataBase(MyApplication app) {
		myApplication = app;
		//getApplicationContext();
		//myApplication = (MyApplication) this.getApplicationContext();
		disableConnectionReuseIfNecessary();
	}
	
	@SuppressWarnings("deprecation")
	private void disableConnectionReuseIfNecessary() {
		if (Integer.parseInt(Build.VERSION.SDK) < Build.VERSION_CODES.FROYO) {
			System.setProperty("http.keepAlive", "false");
		}
	}
	
	public void SyncDBWithServer() {
		
		try {
		SyncDB();
		}
		catch(Exception e)
		{
			
		}
	}
	
	private void SyncDB() {
		
		// for i in table name, send requests and upate sever
		List<Class> classes = new ArrayList();
		classes.add(Angel.class);
		classes.add(GeneralItem.class);
		
		for (Class db_class:  classes){
			
			// send local deleted, added, changed records from local DB to server
			String jsonRecordsToUpdate = postLocalUpdatedDbRecords(db_class);
			
			// retrieve the diff between the local and the server DB
			// and update the local DB
			retrieveSeverUpdatedDbRecords(db_class, jsonRecordsToUpdate);				
		}
	}
	
	private String postLocalUpdatedDbRecords(Class db_class) {
		
		String json = getJsonOfRecordsToUpdate(db_class);
		byte[] payload = json.getBytes();
		
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
			//urlConnection.setRequestProperty("action", "getUserID");
			// TODO - add property with table name
			urlConnection.setDoOutput(true);
			urlConnection.setFixedLengthStreamingMode(payload.length);
			
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

	private String getJsonOfRecordsToUpdate(Class db_class) {
		String json;
		Gson gson = new Gson();
		
		// TODO - sync accsees to DB
		AbstractDao dao = myApplication.daoSession.getDao(db_class);
		Collection<?> records = dao.loadAll();
		Type collectionType = new TypeToken<Collection<?>>(){}.getType();
		json = gson.toJson(records, collectionType);
		return json;
	}

	private void retrieveSeverUpdatedDbRecords(Class db_class, String jsonRecordsToUpdate) {

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
		
		gson = new Gson();
		records = gson.fromJson(jsonRecordsToUpdate, collectionType);
		// TODO - sync accsees to DB - don't add new overlays
		
		// for each item, update it. 
		dao = myApplication.daoSession.getDao(db_class);
		dao.insertOrReplaceInTx(records);
		
		// delete records without changes time
	}


	private void getUniqueUserId() {		
		
		String response = askServerForUniqueUserId();
		retrieveUniqueUserId(response);
	}

	private void retrieveUniqueUserId(String response) {
		
		// parse to JSON
		
		// update userID in preferences
		
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
			/*
			//urlConnection.setRequestProperty("action", "getUserID");
			//urlConnection.setUseCaches(false);
			//??? urlConnection.setAllowUserInteraction(false);
			//urlConnection.setDoOutput(true);
			//??urlConnection.setChunkedStreamingMode(0);
			//urlConnection.setConnectTimeout(20000);
			//urlConnection.setReadTimeout(10000);
			 */
			
			// this opens a connection, then sends GET & headers 
		    in = urlConnection.getInputStream();
		    
		    // can't get status before getInputStream (else, got exception)
		    httpStatus = urlConnection.getResponseCode();
		    if (httpStatus / 100 != 2) {
		      // redirects, server errors, lions and tigers and bears! Oh my!
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
			response = null;
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
