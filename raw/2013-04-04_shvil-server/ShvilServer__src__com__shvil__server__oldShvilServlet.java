package com.shvil.server;
//
//
//import java.io.BufferedReader;
//import java.io.IOException;
//import java.io.InputStream;
//import java.io.InputStreamReader;
//import java.io.PrintWriter;
//import java.lang.reflect.Type;
//import java.util.Collection;
//import java.util.List;
//
//import javax.servlet.RequestDispatcher;
//import javax.servlet.ServletException;
//import javax.servlet.annotation.WebServlet;
//import javax.servlet.http.HttpServlet;
//import javax.servlet.http.HttpServletRequest;
//import javax.servlet.http.HttpServletResponse;
//
//import com.google.gson.Gson;
//import com.google.gson.reflect.TypeToken;
//
///**
// * Servlet implementation class ShvilServlet
// */
//@WebServlet(description = "server side of the shvil android application", urlPatterns = { "" })
//public class ShvilServlet extends HttpServlet {
//	private static final long serialVersionUID = 1L;
//	
//       
//    /**
//     * @see HttpServlet#HttpServlet()
//     */
//    public ShvilServlet() {
//        super();
//        // TODO Auto-generated constructor stub
//    }
//
//	/**
//	 * @see HttpServlet#doGet(HttpServletRequest request, HttpServletResponse response)
//	 */
//	protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
//		// TODO Auto-generated method stub
//		PrintWriter out = response.getWriter();
//		out.println("Yo Yo Yo!!!");
//		out.println("This is so cool!!!!!!");
//		out.println("Yo Yo Yo!!!");
//		out.close();
//		
//		String action=request.getParameter("action");
//        //EmployeeManager eMgr=new EmployeeManager();
//		if(action == null)
//		{
//			action="";
//        }
//        else if(action.equals("getChangedRecords"))
//        {
//        	//Employee emp=eMgr.Populate(request);
//	        try
//	        {
//	        	//eMgr.create(emp);
//	        	System.out.println("Bla");
//	        }
//	        //catch(SQLException e)
//	        //{
//	        //	System.out.println("Exception="+e);
//	        //}
//	 
//	        finally
//	        {
//	        	RequestDispatcher disp=request.getRequestDispatcher("/jsp/confirmation.jsp");
//	        	disp.forward(request,response);
//	        }
//        }
//        else if(action.equals("getUserID"))
//        {
//        	//Employee emp=eMgr.Populate(request);
//	        try
//	        {
//	        	//eMgr.create(emp);
//	        	System.out.println("Bla");
//	        }
//	        //catch(SQLException e)
//	        //{
//	        //	System.out.println("Exception="+e);
//	        //}
//	 
//	        finally
//	        {
//	        	RequestDispatcher disp=request.getRequestDispatcher("/jsp/confirmation.jsp");
//	        	disp.forward(request,response);
//	        }
//        }
//	}
//
//	/**
//	 * @see HttpServlet#doPost(HttpServletRequest request, HttpServletResponse response)
//	 */
//	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException{
//		
//		try {
//			String record_type = request.getParameter("record_type");
//			String str_user_id = request.getParameter("user_id");
//			String str_max_server_time = request.getParameter("max_server_time");
//			long user_id = Long.parseLong(str_user_id);
//			long max_server_time = Long.parseLong( str_max_server_time);
//			String requestBody = null;
//			String responseBody = null;
//			
//			requestBody = readInputStream(request.getInputStream());
//			insertClientRecordsToDB(record_type, user_id, max_server_time, requestBody);
//			responseBody = getNewRecordsForClient(record_type, user_id, max_server_time);
//			
//			PrintWriter out = response.getWriter();
//			out.write(responseBody);
//			out.close(); 
//			
//		} catch (IOException e) {
//			// TODO Auto-generated catch block
//			e.printStackTrace();
//		}
//	}
//	
//	private void insertClientRecordsToDB(String record_type, long user_id, long max_server_time, String requestBody)
//	{
//		
//		if ((record_type != "ANGEL") && (record_type != "GENERAL_ITEM")) {
//			return;
//		}
//		
//		// convert json to records
//		Type collectionType = null;
//		Gson gson = null;
//		List<?> records = null;
//		/*
//		AbstractDao dao = null;	
//		if (db_class == Angel.class){
//			collectionType = new TypeToken<List<Angel>>(){}.getType();
//		}
//		else if (db_class == GeneralItem.class){
//			collectionType = new TypeToken<List<GeneralItem>>(){}.getType();
//		}
//		gson = new Gson();
//		records = gson.fromJson(jsonRecordsToUpdate, collectionType);
//		*/
//		
//		// we got all the records who don't have the sync state idle
//		// for each item, update it - insert/update the record to the db
//		for (Object record: records){
//			byte syncState = SyncState.RECORD_STATE_IDLE;
//			
//			// get records sync state
//			switch (syncState){
//			case SyncState.RECORD_STATE_ADD:
//				// insert and change server_time and assign new id
//				// change state to idle
//				break;
//			case SyncState.RECORD_STATE_EDIT:
//				// update and change server_time
//				// + or - the record counter
//				// change state to idle
//				break;
//			case SyncState.RECORD_STATE_IDLE:
//			case SyncState.RECORD_STATE_DELETE:
//				// currently we don't want to enable delete
//				break;
//			default:
//				break;
//			}
//		}		
//	}
//	
//	private String getNewRecordsForClient(String record_type, long user_id, long max_server_time)
//	{
//		if ((record_type != "ANGEL") && (record_type != "GENERAL_ITEM") && (record_type != "SERVER_MESSAGE") && (record_type != "USER")) {
//			return "";
//		}
//		
//		String json = "";
//		Collection<?> records = null;
//		Gson gson = new Gson();
//		
//		// get all records with time starting from max_server_time
//		// state should be idle or delete
//		
//		// convert records to json
//		if (records != null) {
//			Type collectionType = new TypeToken<Collection<?>>(){}.getType();
//			json = gson.toJson(records, collectionType);
//		}
//		return json;
//	}
//	
//	private String readInputStream(InputStream in){
//		BufferedReader bufferedReader = null;
//		try 
//		{
//	        InputStreamReader reader = new InputStreamReader(in);
//	        bufferedReader = new BufferedReader(reader);
//	        StringBuilder builder = new StringBuilder();
//	        String line = bufferedReader.readLine();
//	        while (line != null) {
//	            builder.append(line);
//	            line = bufferedReader.readLine();
//	        }
//	        return builder.toString();
//		} catch (Exception e) {
//			e.printStackTrace();
//		}
//		finally
//		{
//			if (bufferedReader != null)
//			{
//				try
//				{
//					bufferedReader.close();
//				}
//				catch (IOException e)
//				{
//					e.printStackTrace();
//				}
//			}
//		}
//		return null;
//    }
//
//}
