package com.shvil.project.menu;

import android.content.Context;
import android.os.AsyncTask;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.ProgressBar;

import com.shvil.project.MyApplication;
import com.shvil.project.R;
import com.shvil.project.db.GeneralItem.GeneralItemType;
import com.shvil.project.db.GeneralItemDao;
import com.shvil.project.sync.SyncDataBase;

public class SyncDbActivity extends MenuItemActivity {
	
	MyApplication myApplication;
	private ProgressBar progressBar;
    private Button startSync;

	
	@Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.sync_db_activity_layout);
        myApplication = (MyApplication) this.getApplication();
        progressBar = (ProgressBar) findViewById(R.id.sync_db_activity_progress_bar);
        startSync = (Button) findViewById(R.id.sync_db_activity_button);
        progressBar.setVisibility(View.GONE);
    }
	
	public void StartSync(View v){
		
		// sync as regular
		new SyncDatabaseTask().execute((MyApplication)this.getApplicationContext(), this);
	}
	
	// remove all public records
	private void deleteDB(){
		myApplication.daoSession.getAngelDao().deleteAll();
		myApplication.daoSession.getUserDao().deleteAll();
		myApplication.daoSession.getServerMessageDao().deleteAll();

		// delete all records with type != water hide
		GeneralItemDao generalItemDao = myApplication.daoSession.getGeneralItemDao();
		String query = "DELETE FROM " + generalItemDao.getTablename() + " WHERE " +
				GeneralItemDao.Properties.Type.columnName + "!= " + (byte)GeneralItemType.GENERAL_ITEM_TYPE_WATER_HIDE.ordinal();    		
		generalItemDao.getDatabase().rawQuery(query, null);
	}
	
	private class SyncDatabaseTask extends AsyncTask<Context, Integer, Long> {
        protected Long doInBackground(Context... urls) {
    		deleteDB();
        	new SyncDataBase(urls[0], urls[1]).SyncDBWithServer();
        	long totalSize = 0;
        	return totalSize;
        }

        protected void onProgressUpdate(Integer... progress) {
        }
        
        protected void onPreExecute() {
        	progressBar.setVisibility(View.VISIBLE);
        	startSync.setVisibility(View.GONE);
        }

        protected void onPostExecute(Long result) {
        	progressBar.setVisibility(View.GONE);
        	startSync.setVisibility(View.VISIBLE);
        }
    }
}
