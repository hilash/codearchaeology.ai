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
public class ShvilServerServlet extends HttpServlet {
	public void doGet(HttpServletRequest req, HttpServletResponse resp) throws IOException {
		resp.setContentType("text/plain");
		resp.getWriter().println("Hello, world");
		
		
		PersistenceManager pm = PMF.get().getPersistenceManager();

		User e = new User(new Long(0), "Hila", "Bka Bka", new Double(32.0), new Double(34.0));
        try {
            pm.makePersistent(e);
        } finally {
            pm.close();
        }
	}
	
	/**
	 * @see HttpServlet#doPost(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException{
		
		try {
			String record_type = request.getParameter("record_type");
			String str_user_id = request.getParameter("user_id");
			String str_max_server_time = request.getParameter("max_server_time");
			long user_id = Long.parseLong(str_user_id);
			long max_server_time = Long.parseLong( str_max_server_time);
			String requestBody = null;
			String responseBody = null;
			
			requestBody = readInputStream(request.getInputStream());
			insertClientRecordsToDB(record_type, user_id, max_server_time, requestBody);
			responseBody = getNewRecordsForClient(record_type, user_id, max_server_time);
			
			PrintWriter out = response.getWriter();
			out.write(responseBody);
			out.close(); 
			
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	
	private void insertClientRecordsToDB(String record_type, long user_id, long max_server_time, String requestBody)
	{
		
		if ((record_type != "ANGEL") && (record_type != "GENERAL_ITEM")) {
			return;
		}
		
		Type collectionType = null;
		Gson gson = new Gson();
		List<SyncEntity> records = null;
		long server_time = 0;
		PersistenceManager pm = PMF.get().getPersistenceManager();
		
		// convert json string to records
		if (record_type == "ANGEL"){
			collectionType = new TypeToken<List<Angel>>(){}.getType();
		}
		else if (record_type == "GENERAL_ITEM"){
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
		if ((record_type != "ANGEL") && (record_type != "GENERAL_ITEM") && (record_type != "SERVER_MESSAGE") && (record_type != "USER")) {
			return "";
		}
		
		String json = "";
		Collection<?> records = null;
		Gson gson = new Gson();
		Query q = null;
		PersistenceManager pm = PMF.get().getPersistenceManager();
		
		// get all records with time starting from max_server_time
		// state should be idle or delete
		if ((record_type == "ANGEL") || (record_type == "GENERAL_ITEM")){
			if (record_type == "ANGEL"){
				q = pm.newQuery(Angel.class);
			} else if (record_type == "GENERAL_ITEM"){
				q = pm.newQuery(GeneralItem.class);
			}
			q.setFilter("max_server_time < server_time");
			q.declareParameters("Long max_server_time");
			records = (Collection<?>) q.execute(max_server_time);
		}
		else if ((record_type == "SERVER_MESSAGE") || (record_type == "USER")){
			if (record_type == "SERVER_MESSAGE"){
				q = pm.newQuery(ServerMessage.class);
			} else if (record_type == "USER"){
				q = pm.newQuery(User.class);
			}
			records = (Collection<?>) q.execute();
		}
		
		// convert records to json
		if (records != null) {
			Type collectionType = new TypeToken<Collection<?>>(){}.getType();
			json = gson.toJson(records, collectionType);
		}
		return json;
	}
	
	private String readInputStream(InputStream in){
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
