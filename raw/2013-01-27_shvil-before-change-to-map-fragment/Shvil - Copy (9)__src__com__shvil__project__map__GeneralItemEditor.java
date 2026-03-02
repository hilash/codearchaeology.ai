package com.shvil.project.map;

import android.app.ActionBar;
import android.app.Activity;
import android.app.AlertDialog;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.widget.EditText;

import com.shvil.project.MyApplication;
import com.shvil.project.R;
import com.shvil.project.db.GeneralItem;
import com.shvil.project.db.GeneralItemDao;
import com.shvil.project.sync.SyncState;

public class GeneralItemEditor extends Activity {
	
    // The activity incoming intent data
    public static final String INTENT_IN_DATA_GENERAL_ID = "general_in_id";
    public static final String INTENT_IN_DATA_GENERAL_TYPE = "general_in_type";
    public static final String INTENT_IN_DATA_GENERAL_LAT = "general_in_lat";
    public static final String INTENT_IN_DATA_GENERAL_LON = "general_in_lon";
    public static final String INTENT_IN_DATA_GENERAL_POSITION = "general_int_position";

    
    // The Activity return Intent data
    public static final String INTENT_OUT_DATA_GENERAL_ID = "general_out_id";
    public static final String INTENT_OUT_DATA_GENERAL_TYPE = "general_out_type";
    public static final String INTENT_OUT_DATA_GENERAL_LAT = "general_out_lat";
    public static final String INTENT_OUT_DATA_GENERAL_LON = "general_out_lon";
    public static final String INTENT_OUT_DATA_GENERAL_POSITION = "general_out_position";
    
    private MyApplication mApplication;
    private GeneralItemDao mGeneralItemDao;
    private long mGeneralItemID;
    private byte mGeneralItemType;
    private double mGeneralItemLatitude;
    private double mGeneralItemLongitude;
    private long mGeneralItemPosition;
    private GeneralItem mGeneralItem;
    ViewHolder viewHolder;
    
    static class ViewHolder {
		public EditText title;
		public EditText text;
	  }
    
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        
        ActionBar actionBar = getActionBar();
        actionBar.setDisplayHomeAsUpEnabled(true);
        actionBar.setHomeButtonEnabled(true);

        final Intent intent = getIntent();
        final String action = intent.getAction();
        mApplication = (MyApplication) this.getApplication();
        mGeneralItemDao = mApplication.daoSession.getGeneralItemDao();
        
        mGeneralItemID = intent.getLongExtra(INTENT_IN_DATA_GENERAL_ID, 0);
        mGeneralItemType = intent.getByteExtra(INTENT_IN_DATA_GENERAL_TYPE, (byte) 0);
        mGeneralItemLatitude = intent.getDoubleExtra(INTENT_IN_DATA_GENERAL_LAT, 0);
        mGeneralItemLongitude = intent.getDoubleExtra(INTENT_IN_DATA_GENERAL_LON, 0);
        mGeneralItemPosition = intent.getLongExtra(INTENT_IN_DATA_GENERAL_POSITION, 0);
        
        if (Intent.ACTION_EDIT.equals(action))
        {
            mGeneralItem = mGeneralItemDao.load(mGeneralItemID);
            mGeneralItem.setRecord_state(SyncState.RECORD_STATE_EDIT);
        }
        else if (Intent.ACTION_INSERT.equals(action))
        {
        	long server_time = 0;
            mGeneralItem = new GeneralItem(null, mGeneralItemType, "", "", mGeneralItemLongitude, mGeneralItemLatitude, (byte) 0, SyncState.RECORD_STATE_ADD, server_time);
        }
        else
        {
            Log.e("GeneralItemEditor", "Unknown action, exiting");
            finish();
            return;
        }

        infalte_GeneralItem_layout();
    }
    
    private void infalte_GeneralItem_layout()
    {
    	setContentView(R.layout.general_item_editor);
	    viewHolder = new ViewHolder();
		    
	    viewHolder.title = (EditText)findViewById(R.id.general_item_editor_title);
	    viewHolder.text = (EditText)findViewById(R.id.general_item_editor_text);
	    
	    // get data
        viewHolder.text.setText(mGeneralItem.getTitle());
        viewHolder.title.setText(mGeneralItem.getText());
    }
    
    ///////////////// OPTIONS MENU /////////////////////////////////////////////////////////
    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
    	super.onCreateOptionsMenu(menu);
        MenuInflater inflater = getMenuInflater();
        inflater.inflate(R.menu.general_item_editor_menu, menu);
        return true;
    }
    
    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {
            case R.id.general_item_editor_menu_save:
        		if (saveGeneralItem() == true){
        			exitActivity();
        		}
                return true;
            default:
                return super.onOptionsItemSelected(item);
        }
    }
    
    private boolean saveGeneralItem() {
    	
    	if ((viewHolder.title.getText().toString().length() == 0) ||
    			(viewHolder.text.getText().toString().length() == 0)){
    		
    		AlertDialog.Builder builder = new AlertDialog.Builder(this);
    		builder.setIcon(android.R.drawable.ic_menu_info_details);
    		builder.setTitle(R.string.error);
    		builder.setMessage(R.string.general_item_editor_dialog_error);
    		builder.show();
    		return false;
    	}
    	
    	mGeneralItem.setTitle(viewHolder.title.getText().toString());
    	mGeneralItem.setText(viewHolder.text.getText().toString());
    	mGeneralItemID = mGeneralItemDao.insertOrReplace(mGeneralItem);
    	return true;
    }
    
    private void exitActivity()
    {
    	Intent resultIntent = new Intent();  
    	resultIntent.putExtra(INTENT_OUT_DATA_GENERAL_ID, mGeneralItemID);
    	resultIntent.putExtra(INTENT_OUT_DATA_GENERAL_TYPE, mGeneralItemType);
    	resultIntent.putExtra(INTENT_OUT_DATA_GENERAL_LAT, mGeneralItemLatitude);
    	resultIntent.putExtra(INTENT_OUT_DATA_GENERAL_LON, mGeneralItemLongitude);
    	resultIntent.putExtra(INTENT_OUT_DATA_GENERAL_POSITION, mGeneralItemPosition);
    	setResult(Activity.RESULT_OK, resultIntent);
    	finish();
    }
}
