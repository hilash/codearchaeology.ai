package com.shvil.project.olddb;

public class AngelsTable {
	
	private AngelsTable() {
        throw new AssertionError();
    }
	
	public static final String TABLE_NAME = "angels";
    public static final String COLUMN_ID = "_id";
    public static final String COLUMN_NAME = "name";
    public static final String COLUMN_PHONE1 = "phone1";
    public static final String COLUMN_PHONE2 = "phone2";
    public static final String COLUMN_INFORMATION = "information";
    public static final String COLUMN_LATITUDE = "latitude";
    public static final String COLUMN_LONGITUDE = "longitude";
    public static final String COLUMN_PASSPORT = "passport";
    public static final String COLUMN_STAMP = "stamp";
    public static final String COLUMN_RELIGIOUS = "religious";
    public static final String COLUMN_DISTANCE = "distance";
    
    // Database creation SQL statement
    public static final String TABLE_CREATE =
    	  "create table " + TABLE_NAME
        + "("
        + COLUMN_ID + " integer primary key autoincrement, "
        + COLUMN_NAME + " text not null, "
        + COLUMN_PHONE1 + " text not null, "
        + COLUMN_PHONE2 + " text, "
        + COLUMN_INFORMATION + " text, "
        + COLUMN_LATITUDE + " integer not null, "
        + COLUMN_LONGITUDE + " integer not null, "
        + COLUMN_PASSPORT + " integer, "
        + COLUMN_STAMP + " integer , "
        + COLUMN_RELIGIOUS + " integer, "
        + COLUMN_DISTANCE + " integer"
        + ");";
    
    public static final String TABLE_DROP =
    		"DROP TABLE IF EXISTS " + TABLE_NAME;
}
