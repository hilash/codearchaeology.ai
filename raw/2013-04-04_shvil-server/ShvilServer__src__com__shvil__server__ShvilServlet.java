package com.shvil.server;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.lang.reflect.Type;
import java.util.Collection;
import java.util.List;

import javax.jdo.PersistenceManager;
import javax.jdo.Query;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import com.shvil.server.db.Angel;
import com.shvil.server.db.GeneralItem;
import com.shvil.server.db.ServerMessage;
import com.shvil.server.db.SyncEntity;
import com.shvil.server.db.User;

@SuppressWarnings("serial")
public class ShvilServlet extends HttpServlet {

	
	public void doGet(HttpServletRequest req, HttpServletResponse resp) throws IOException {
		
		String action = req.getHeader("action");

		if(action == null)
		{
			action="";
        }
        else if(action.equals("getUserID"))
        {
	        try
	        {

	        	Gson gson = new Gson();
	        	String useridstring = gson.toJson(System.currentTimeMillis());
	        	
	        	// send
	        	PrintWriter out = resp.getWriter();
	        	resp.setHeader("Access-Control-Allow-Origin", "*"); // Everone may process the response.
	        	resp.setHeader("Access-Control-Allow-Methods", "GET, POST"); // Commaseparated string of allowed request methods.				
	        	resp.setHeader("Access-Control-Allow-Headers", "action, record_type, user_id, max_server_time"); // Commaseparated string of allowed request methods.				
	        	out.write(useridstring);
				out.close();
	        	
	        }
	        finally{
	        	
	        }
        }
        else if(action.equals("getUserLocation"))
        {
    		PersistenceManager pm = PMF.get().getPersistenceManager();

	        try
	        {
	        	String user_id = req.getHeader("user_id");
	        	String lat = req.getHeader("lat");
	        	String lon = req.getHeader("lon");
	        	
	        	// insert/replace user location
	        	if (!user_id.equals("0")){
	        		User user = new User(Long.parseLong(user_id), "", "",  Double.parseDouble(lon), Double.parseDouble(lat),
	        	    		(byte) 0, SyncState.RECORD_STATE_IDLE, System.currentTimeMillis());
	        		pm.makePersistent(user);
	        		pm.close();
	        	}
	        }
	        finally{
	        	 pm.close();
	        	 
	        	// send empty response
		        PrintWriter out = resp.getWriter();
				out.write("");
	        }
        }
  
	}
	
	/**
	 * @see HttpServlet#doPost(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException{
		
		try {
			String action = request.getHeader("action");
			if (action == null){
				return;
			}
			if (action.equals("sync")){
				doPostSyncData(request, response);
				return;
			}
			String record_type = request.getHeader("record_type");
			String str_user_id = request.getHeader("user_id");
			String str_max_server_time = request.getHeader("max_server_time");
			long user_id = Long.parseLong(str_user_id);
			long max_server_time = Long.parseLong( str_max_server_time);
			String requestBody = null;
			String responseBody = null;
			
			requestBody = readInputStream(request.getInputStream());
			insertClientRecordsToDB(record_type, user_id, max_server_time, requestBody);
			responseBody = getNewRecordsForClient(record_type, user_id, max_server_time);
			
			response.setContentType("text/plain; charset=UTF-8");
			response.setCharacterEncoding("UTF-8");
			PrintWriter out = response.getWriter();
			response.setHeader("Access-Control-Allow-Origin", "*"); // Everone may process the response.
			response.setHeader("Access-Control-Allow-Methods", "GET, POST"); // Commaseparated string of allowed request methods.		
        	response.setHeader("Access-Control-Allow-Headers", "action, record_type, user_id, max_server_time"); // Commaseparated string of allowed request methods.				
			out.write(new String(responseBody.getBytes("UTF-8"),"UTF-8"));
			out.close(); 
			
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	
	protected void doPostSyncData(HttpServletRequest request, HttpServletResponse response) throws ServletException{
		try {
			String record_type = request.getHeader("record_type");
			String str_user_id = request.getHeader("user_id");
			String str_max_server_time = request.getHeader("max_server_time");
			long user_id = Long.parseLong(str_user_id);
			long max_server_time = Long.parseLong( str_max_server_time);
			String requestBody = null;
			String responseBody = null;
			
			try {
				requestBody = readInputStream(request.getInputStream());
				insertClientRecordsToDB(record_type, user_id, max_server_time, requestBody);
				responseBody = getNewRecordsForClient(record_type, user_id, max_server_time);
			}
			catch (Exception e)
			{
				responseBody = "[]";
			}
			
			response.setContentType("text/plain; charset=UTF-8");
			response.setCharacterEncoding("UTF-8");
			response.setHeader("Access-Control-Allow-Origin", "*"); // Everone may process the response.
			response.setHeader("Access-Control-Allow-Methods", "GET, POST"); // Commaseparated string of allowed request methods.		
        	response.setHeader("Access-Control-Allow-Headers", "action, record_type, user_id, max_server_time"); // Commaseparated string of allowed request methods.				
			
			PrintWriter out = response.getWriter();
			//out.write(new String(responseBody.getBytes("UTF-8"),"UTF-8"));

			out.write(responseBody);
			out.close(); 
			
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	
	private void insertClientRecordsToDB(String record_type, long user_id, long max_server_time, String requestBody)
	{
		
		if ((!record_type.equals("ANGEL")) && (!record_type.equals("GENERAL_ITEM"))) {
			return;
		}
		
		Type collectionType = null;
		Gson gson = new Gson();
		List<SyncEntity> records = null;
		long server_time = 0;
		PersistenceManager pm = PMF.get().getPersistenceManager();
		
		// convert json string to records
		if (record_type.equals("ANGEL")){
			collectionType = new TypeToken<List<Angel>>(){}.getType();
		}
		else if (record_type.equals("GENERAL_ITEM")){
			collectionType = new TypeToken<List<GeneralItem>>(){}.getType();
		}
		records = gson.fromJson(requestBody, collectionType);	
		
		// we got all the records who don't have the sync state idle
		// for each item, update it - insert/update the record to the db
		for (SyncEntity record: records){
			byte syncState = record.getRecord_state();
			switch (syncState){
			case SyncState.RECORD_STATE_ADD:
				// insert and change server_time and assign new id
				// change state to idle
				server_time = System.currentTimeMillis();
				record.setServer_time(server_time);
				record.setId(null);
				record.setRecord_state(SyncState.RECORD_STATE_IDLE);
				try {
		            pm.makePersistent(record);
		        } finally {
		            pm.close();
		        }
				break;
			case SyncState.RECORD_STATE_EDIT:
				// TODO - block this option
				// or enable only for counter
				// update and change server_time
				// + or - the record counter
				// change state to idle
				server_time = System.currentTimeMillis();
				record.setServer_time(server_time);
				record.setRecord_state(SyncState.RECORD_STATE_IDLE);
				try {
		            pm.makePersistent(record);
		        } finally {
		            pm.close();
		        }
				break;
			case SyncState.RECORD_STATE_IDLE:
			case SyncState.RECORD_STATE_DELETE:
				// currently we don't want to enable delete
				break;
			default:
				break;
			}
		}		
	}
	
	private String getNewRecordsForClient(String record_type, long user_id, long max_server_time)
	{
		if ((!record_type.equals("ANGEL")) && (!record_type.equals("GENERAL_ITEM"))
				&& (!record_type.equals("SERVER_MESSAGE")) && (!record_type.equals("USER"))) {
			return "";
		}
		
		String json = "[]";
		Collection<?> records = null;
		Gson gson = new Gson();
		Query q = null;
		Type collectionType = new TypeToken<Collection<?>>(){}.getType();
		PersistenceManager pm = PMF.get().getPersistenceManager();
		
		// get all records with time starting from max_server_time
		// state should be idle or delete
		if (record_type.equals("ANGEL")){
			q = pm.newQuery(Angel.class);
			collectionType = new TypeToken<Collection<Angel>>(){}.getType();	
		} else if (record_type.equals("GENERAL_ITEM")){
			q = pm.newQuery(GeneralItem.class);
			collectionType = new TypeToken<Collection<GeneralItem>>(){}.getType();
		} else if (record_type.equals("SERVER_MESSAGE")){
			q = pm.newQuery(ServerMessage.class);
			collectionType = new TypeToken<Collection<ServerMessage>>(){}.getType();
		} else if (record_type.equals("USER")){
			q = pm.newQuery(User.class);
			collectionType = new TypeToken<Collection<User>>(){}.getType();
		}

		if (record_type.equals("USER")){
			// get all records but the current user record
			q.setFilter("id != user_id");
			// get only last records from last 24 hours
			q.setFilter("server_time > yesterday");
			q.declareParameters("Long user_id, Long yesterday");
			records = (Collection<?>) q.execute(user_id, System.currentTimeMillis() - 86400000);
		}
		else {
			// If record_state is SyncState.RECORD_STATE_IDLE or SyncState.RECORD_STATE_DELETE
			q.setFilter("record_state == 0 || record_state == 2");
			// get latest updates
			q.setFilter("server_time > max_server_time");
			q.declareParameters("Long max_server_time");
			records = (Collection<?>) q.execute(max_server_time);
		}
		
		// convert records to json
		if (records != null) {
			json = gson.toJson(records, collectionType);
		}
		return json;
	}
	
	private String readInputStream(InputStream in){
		BufferedReader bufferedReader = null;
		try 
		{
	        InputStreamReader reader = new InputStreamReader(in, "UTF-8");
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
