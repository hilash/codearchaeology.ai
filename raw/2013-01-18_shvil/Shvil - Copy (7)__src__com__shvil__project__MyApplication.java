package com.shvil.project;

import android.app.Application;
import android.content.res.Configuration;
import android.database.sqlite.SQLiteDatabase;

import com.shvil.project.db.DaoMaster;
import com.shvil.project.db.DaoMaster.DevOpenHelper;
import com.shvil.project.db.DaoSession;

public class MyApplication extends Application
{	
	private static MyApplication singleton;
	//	private SharedPreferences mPrefs;
	
    public DaoSession daoSession;

	private SQLiteDatabase db;
	private DaoMaster daoMaster;
	
	public MyApplication getInstance(){
		return singleton;
	}

	@Override
	public void onConfigurationChanged(Configuration newConfig) {
		super.onConfigurationChanged(newConfig);
	}

	@Override
	public void onCreate() {
		super.onCreate();
		singleton = this;
		
		//PreferenceManager.getDefaultSharedPreferences(this);
		DevOpenHelper helper = new DaoMaster.DevOpenHelper(this, "shvil-db", null);
        db = helper.getWritableDatabase();
        daoMaster = new DaoMaster(db);
        daoSession = daoMaster.newSession();
	}

	@Override
	public void onLowMemory() {
		super.onLowMemory();
	}

	@Override
	public void onTerminate() {
		super.onTerminate();
	}
}
