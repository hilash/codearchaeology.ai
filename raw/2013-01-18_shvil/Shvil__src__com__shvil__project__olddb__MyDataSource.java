package com.shvil.project.olddb;

import java.util.ArrayList;
import java.util.List;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.SQLException;
import android.database.sqlite.SQLiteDatabase;

public class MyDataSource {
	
	private SQLiteDatabase database;
	private MySQLiteHelper dbHelper;

	public MyDataSource(Context context) {
		dbHelper = new MySQLiteHelper(context);
	}

	public void open() throws SQLException {
	    database = dbHelper.getWritableDatabase();
	  }
	
	public void close() {
	    dbHelper.close();
	  }
	
	 /*** User *******************************************************************************/
	
	public User createUser(String name, String image) {   
	    ContentValues values = new ContentValues();
	    values.put(UsersTable.COLUMN_NAME, name);
	    values.put(UsersTable.COLUMN_IMAGE, image);
	    long insertId = database.insert(UsersTable.TABLE_NAME, null, values);
	    Cursor cursor = database.query(UsersTable.TABLE_NAME,
	        null, UsersTable.COLUMN_ID + " = " + insertId, null,
	        null, null, null);
	    cursor.moveToFirst();
	    User newUser = cursorToUser(cursor);
	    cursor.close();
	    return newUser;
	  }
	
	  public void deleteUser(User user) {
	    long id = user.getId();
	    database.delete(UsersTable.TABLE_NAME, UsersTable.COLUMN_ID
	        + " = " + id, null);
	  }
		  
	  private User cursorToUser(Cursor cursor) {
		  User user = new User();
		  user.setId(cursor.getLong(0));
		  user.setName(cursor.getString(1));
		  user.setImage(cursor.getString(1));
		  return user;
	  }
		  
	
	/*** Message *******************************************************************************/
	
	public Message createMessage(String message, long time, long coversation_id, long sender_id,
			long receiver_id, long group_id) {   
	    ContentValues values = new ContentValues();
	    values.put(MessagesTable.COLUMN_MESSAGE, message);
	    values.put(MessagesTable.COLUMN_TIME, time);
	    values.put(MessagesTable.COLUMN_CONVERSATION_ID, coversation_id);
	    values.put(MessagesTable.COLUMN_SENDER_ID, sender_id);
	    values.put(MessagesTable.COLUMN_RECEIVER_ID, receiver_id);
	    values.put(MessagesTable.COLUMN_GROUP_ID, group_id);
	    long insertId = database.insert(MessagesTable.TABLE_NAME, null, values);
	    Cursor cursor = database.query(MessagesTable.TABLE_NAME,
	        null, MessagesTable.COLUMN_ID + " = " + insertId, null,
	        null, null, null);
	    cursor.moveToFirst();
	    Message newMessage = cursorToMessage(cursor);
	    cursor.close();
	    return newMessage;
	  }

	  public void deleteMessage(Message message) {
	    long id = message.getId();
	    database.delete(MessagesTable.TABLE_NAME, MessagesTable.COLUMN_ID
	        + " = " + id, null);
	  }
	  
	  private Message cursorToMessage(Cursor cursor) {
		  Message message = new Message();
		  message.setId(cursor.getLong(0));
		  message.setMessage(cursor.getString(1));
		  message.setTime(cursor.getLong(2));
		  message.setConverationId(cursor.getLong(3));
		  message.setSenderId(cursor.getLong(4));
		  message.setReceiverId(cursor.getLong(5));
		  message.setGroupId(cursor.getLong(6));
		  return message;
	  }
	  
	  /****** ConversationPreview *************************************************************/
	  
	  private ConversationPreview cursorToConversationPreview(Cursor cursor) {
		  // TODO fix this it's shit
		  ConversationPreview conv = new ConversationPreview();
		  conv.message = cursorToMessage(cursor);
		  conv.name = cursor.getString(7);
		  conv.image = cursor.getString(8);
		  return conv;
	  }
	  
	  /****** Angels *************************************************************************/

	  public Angel createAngel(
				 String city,
				 String name,
				 String phone1,
				 String phone2,
				 String information,
				 long latitude,
				 long longitude,
				 short passport,
				 short stamp,
				 short religious,
				 short distance
		) {
						
		    ContentValues values = new ContentValues();
		    values.put(AngelsTable.COLUMN_NAME, name);
		    values.put(AngelsTable.COLUMN_PHONE1, phone1);
		    values.put(AngelsTable.COLUMN_PHONE2, phone2);
		    values.put(AngelsTable.COLUMN_INFORMATION, information);
		    values.put(AngelsTable.COLUMN_LATITUDE, latitude);
		    values.put(AngelsTable.COLUMN_LONGITUDE, longitude);
		    values.put(AngelsTable.COLUMN_PASSPORT, passport);
		    values.put(AngelsTable.COLUMN_STAMP, stamp);
		    values.put(AngelsTable.COLUMN_RELIGIOUS, religious);
		    values.put(AngelsTable.COLUMN_DISTANCE, distance);

		    long insertId = database.insert(AngelsTable.TABLE_NAME, null, values);
		    Cursor cursor = database.query(AngelsTable.TABLE_NAME,
		        null, AngelsTable.COLUMN_ID + " = " + insertId, null,
		        null, null, null);
		    cursor.moveToFirst();
		    Angel newAngel = cursorToAngel(cursor);
		    cursor.close();
		    return newAngel;
		  }
	  
	  public void deleteAngel(Angel angel) {
		    long id = angel.id;
		    database.delete(AngelsTable.TABLE_NAME, AngelsTable.COLUMN_ID
		        + " = " + id, null);
		  }
		  
	  private Angel cursorToAngel(Cursor cursor) {
		  Angel angel = new Angel();
		  angel.id = cursor.getLong(0);
		  angel.city = cursor.getString(1);
		  angel.name = cursor.getString(2);
		  angel.phone1 = cursor.getString(3);
		  angel.phone2 = cursor.getString(4);
		  angel.information = cursor.getString(5);
		  angel.latitude = cursor.getLong(6);
		  angel.longitude = cursor.getLong(7);
		  angel.passport = cursor.getShort(8);
		  angel.stamp = cursor.getShort(9);
		  angel.religious = cursor.getShort(10);
		  angel.distance = cursor.getShort(11);
		  return angel;
	  }
	  
	  /****************************************************************************************/

	  public List<Message> getAllMessages() {
	    List<Message> messages = new ArrayList<Message>();

	    Cursor cursor = database.query(MessagesTable.TABLE_NAME,
	        null, null, null, null, null, null);

	    cursor.moveToFirst();
	    while (!cursor.isAfterLast()) {
	    	Message message = cursorToMessage(cursor);
	    	messages.add(message);
	    	cursor.moveToNext();
	    }
	    // Make sure to close the cursor
	    cursor.close();
	    return messages;
	  }
	  
	  public List<Message> getMessagesByConversation(long converation_id, int limit) {
		    List<Message> messages = new ArrayList<Message>();

		    String WHERE = MessagesTable.COLUMN_CONVERSATION_ID + " = " + converation_id;
		    String ORDER_BY = MessagesTable.COLUMN_TIME;
		    String LIMIT = String.valueOf(limit);
		    Cursor cursor = database.query(MessagesTable.TABLE_NAME,
		    		null, WHERE, null, null, null, ORDER_BY, LIMIT);

		    cursor.moveToFirst();
		    while (!cursor.isAfterLast()) {
		    	Message message = cursorToMessage(cursor);
		    	messages.add(message);
		    	cursor.moveToNext();
		    }
		    // Make sure to close the cursor
		    cursor.close();
		    return messages;
		  }
	  
	  public List<ConversationPreview> getLastMessageFromEachConversation() {
		    List<ConversationPreview> conversations = new ArrayList<ConversationPreview>();

		    /*
		    String query = "SELECT m1.* FROM "
		    			+ MessagesTable.TABLE_NAME + " m1"
		    			+ " LEFT JOIN "
		    			+ MessagesTable.TABLE_NAME + " m2"
		    			+ " ON (m1." + MessagesTable.COLUMN_CONVERSATION_ID
		    			+ " = m2." + MessagesTable.COLUMN_CONVERSATION_ID
		    			+ " AND m1." + MessagesTable.COLUMN_TIME
		    			+ " < m2." + MessagesTable.COLUMN_TIME + ")"
		    			+ " WHERE m2." + MessagesTable.COLUMN_TIME
		    			+ " IS NULL;";
		    */
		    
		    String query = "SELECT m1.*, "
		    		+ "u1." + UsersTable.COLUMN_NAME + ", "
		    		+ "u1." + UsersTable.COLUMN_IMAGE

		    		+ " FROM "
	    			+ MessagesTable.TABLE_NAME + " m1"
	    			+ " LEFT JOIN "
	    			+ UsersTable.TABLE_NAME + " u1"
	    			
	    			+ " ON ("
		    		+ "m1." + MessagesTable.COLUMN_SENDER_ID
		    		+ " = "
		    		+ "u1." + UsersTable.COLUMN_ID
		    		+ ")"
		    
	    			+ " LEFT JOIN "
	    			+ MessagesTable.TABLE_NAME + " m2"
	    			+ " ON (m1." + MessagesTable.COLUMN_CONVERSATION_ID
	    			+ " = m2." + MessagesTable.COLUMN_CONVERSATION_ID
	    			+ " AND m1." + MessagesTable.COLUMN_TIME
	    			+ " < m2." + MessagesTable.COLUMN_TIME + ")"
	    			+ " WHERE m2." + MessagesTable.COLUMN_TIME
	    			+ " IS NULL";
		    		
		    		
		    
		    Cursor cursor = database.rawQuery(query, null);

		    cursor.moveToFirst();
		    while (!cursor.isAfterLast()) {
		    	ConversationPreview conv = cursorToConversationPreview(cursor);
		    	conversations.add(conv);
		    	cursor.moveToNext();
		    }
		    // Make sure to close the cursor
		    cursor.close();
		    return conversations;
		  }

	  
}
