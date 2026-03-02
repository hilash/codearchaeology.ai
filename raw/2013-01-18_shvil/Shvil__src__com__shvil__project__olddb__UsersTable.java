package com.shvil.project.olddb;

public class UsersTable {
	
	private UsersTable() {
        throw new AssertionError();
    }
	
	public static final String TABLE_NAME = "users";
    public static final String COLUMN_ID = "_id";
    public static final String COLUMN_NAME = "name";
    public static final String COLUMN_IMAGE = "image";
    
    // Database creation SQL statement
    public static final String TABLE_CREATE =
    	  "create table " + TABLE_NAME
        + "("
        + COLUMN_ID + " integer primary key autoincrement, "
        + COLUMN_NAME + " text not null, "
        + COLUMN_IMAGE + " text"
        + ");";
    
    public static final String TABLE_DROP =
    		"DROP TABLE IF EXISTS " + TABLE_NAME;
}
