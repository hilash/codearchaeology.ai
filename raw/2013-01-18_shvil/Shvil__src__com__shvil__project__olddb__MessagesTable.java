package com.shvil.project.olddb;

public class MessagesTable {
	
	private MessagesTable() {
        throw new AssertionError();
    }
	
	public static final String TABLE_NAME = "messages";
    public static final String COLUMN_ID = "_id";
    public static final String COLUMN_MESSAGE = "message";
    public static final String COLUMN_TIME = "time";
    public static final String COLUMN_CONVERSATION_ID = "conversation_id";
    public static final String COLUMN_SENDER_ID = "sender_id";
    public static final String COLUMN_RECEIVER_ID = "receiver_id";
    public static final String COLUMN_GROUP_ID = "group_id";
    
    // Database creation SQL statement
    public static final String TABLE_CREATE =
    	  "create table " + TABLE_NAME
        + "("
        + COLUMN_ID + " integer primary key autoincrement, "
        + COLUMN_MESSAGE + " text not null, "
        + COLUMN_TIME + " integer not null, "
        + COLUMN_CONVERSATION_ID + " integer not null, "
        + COLUMN_SENDER_ID + " integer not null, "
        + COLUMN_RECEIVER_ID + " integer, "
        + COLUMN_GROUP_ID + " integer"
        + ");";
    
    public static final String TABLE_DROP =
    		"DROP TABLE IF EXISTS " + TABLE_NAME;
}
