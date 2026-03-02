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
import android.view.View;
import android.widget.CheckBox;
import android.widget.EditText;

import com.shvil.project.MyApplication;
import com.shvil.project.R;
import com.shvil.project.db.Angel;
import com.shvil.project.db.AngelDao;
import com.shvil.project.sync.SyncState;

public class AngelEditor extends Activity {
	
	// This Activity can be started by more than one action
	// Each action is represented as a "state" constant
    private static final int STATE_EDIT = 0;
    private static final int STATE_INSERT = 1;
    private static final int STATE_VIEW = 2;
    
    // The activity incoming intent data
    public static final String INTENT_IN_DATA_ANGEL_ID = "angel_in_id";
    public static final String INTENT_IN_DATA_ANGEL_LAT = "angel_in_lat";
    public static final String INTENT_IN_DATA_ANGEL_LON = "angel_in_lon";
    public static final String INTENT_IN_DATA_ANGEL_POSITION = "angel_in_position";
    
    // The Activity return Intent data
    public static final String INTENT_OUT_DATA_ANGEL_ID = "angel_out_id";
    public static final String INTENT_OUT_DATA_ANGEL_LAT = "angel_out_lat";
    public static final String INTENT_OUT_DATA_ANGEL_LON = "angel_out_lon";
    public static final String INTENT_OUT_DATA_ANGEL_POSITION = "angel_out_position";
    
    private MyApplication mApplication;
    private AngelDao mAngelDao;
    private int mState;
    private long mAngelID;
    private double mAngelLatitude;
    private double mAngelLongitude;
    private long mAngelPosition;
    private Angel mAngel;
    ViewHolder viewHolder;
    
    static class ViewHolder {
		public EditText name;
	    public EditText address;
		public EditText phone1;
		public EditText phone2;
		public EditText information;
		public CheckBox passport;
		public CheckBox stamp;
		public CheckBox religious;
	  }
    
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        
        ActionBar actionBar = getActionBar();
        if (actionBar != null){
	        actionBar.setDisplayHomeAsUpEnabled(true);
	        actionBar.setHomeButtonEnabled(true);
        }

        final Intent intent = getIntent();
        final String action = intent.getAction();
        mApplication = (MyApplication) this.getApplication();
        mAngelDao = mApplication.daoSession.getAngelDao();
        
        mAngelID = intent.getLongExtra(INTENT_IN_DATA_ANGEL_ID, 0);
        mAngelLatitude = intent.getDoubleExtra(INTENT_IN_DATA_ANGEL_LAT, 0);
        mAngelLongitude = intent.getDoubleExtra(INTENT_IN_DATA_ANGEL_LON, 0);
        mAngelPosition = intent.getLongExtra(INTENT_IN_DATA_ANGEL_POSITION, 0);
        
        if (Intent.ACTION_EDIT.equals(action))
        {
            mState = STATE_EDIT;
            mAngel = mAngelDao.load(mAngelID);
            mAngel.setRecord_state(SyncState.RECORD_STATE_EDIT);
        }
        else if (Intent.ACTION_INSERT.equals(action))
        {
            mState = STATE_INSERT;
            long server_time = 0;
            mAngel = new Angel(null, "", "", "", "", "", mAngelLatitude, mAngelLongitude, false, false, false, (byte) 0, SyncState.RECORD_STATE_ADD, server_time);
	    }
        else if (Intent.ACTION_VIEW.equals(action))
        {
        	mState = STATE_VIEW;
            mAngel = mAngelDao.load(mAngelID);
	    }
        else
        {
            Log.e("AngelEditor", "Unknown action, exiting");
            finish();
            return;
        }

        infalte_angel_layout();
        if (mState == STATE_VIEW){
        	change_to_view_mode();
        }
    }
    
    private void infalte_angel_layout()
    {
    	setContentView(R.layout.angel_editor);
	    viewHolder = new ViewHolder();
		    
	    viewHolder.name = (EditText)findViewById(R.id.angel_editor_name);
	    viewHolder.address = (EditText)findViewById(R.id.angel_editor_address);
	    viewHolder.phone1 = (EditText)findViewById(R.id.angel_editor_phone1);
	    viewHolder.phone2 = (EditText)findViewById(R.id.angel_editor_phone2);
	    viewHolder.information = (EditText)findViewById(R.id.angel_editor_information);
	    viewHolder.passport = (CheckBox)findViewById(R.id.angel_editor_passport);
	    viewHolder.stamp = (CheckBox)findViewById(R.id.angel_editor_stamp);
	    viewHolder.religious = (CheckBox)findViewById(R.id.angel_editor_religious);
	    
	    // get data
        viewHolder.name.setText(mAngel.getName());
        viewHolder.address.setText(mAngel.getCity());
        viewHolder.phone1.setText(mAngel.getPhone1());
        viewHolder.phone2.setText(mAngel.getPhone2());
        viewHolder.information.setText(mAngel.getInformation());
        viewHolder.passport.setChecked(mAngel.getPassport());
        viewHolder.stamp.setChecked(mAngel.getStamp());
        viewHolder.religious.setChecked(mAngel.getReligious());
    }
    
    private void change_to_view_mode()
    {
	    viewHolder.name.setEnabled(false);
        viewHolder.address.setEnabled(false);
        viewHolder.phone1.setEnabled(false);
        viewHolder.phone2.setEnabled(false);
        viewHolder.information.setEnabled(false);
        viewHolder.passport.setEnabled(false);
        viewHolder.stamp.setEnabled(false);
        viewHolder.religious.setEnabled(false);
        
        // remove empty fields
        if (viewHolder.phone2.getText().length() == 0){
        	viewHolder.phone2.setVisibility(View.GONE);
        }
        else {
        	viewHolder.phone2.setVisibility(View.VISIBLE);
        }
        if (viewHolder.information.getText().length() == 0){
        	viewHolder.information.setVisibility(View.GONE);
        }
        else {
        	viewHolder.information.setVisibility(View.VISIBLE);
        }
        if (!viewHolder.passport.isChecked()){
        	viewHolder.passport.setVisibility(View.GONE);
        }
        else {
        	viewHolder.passport.setVisibility(View.VISIBLE);
        }	
        if (!viewHolder.stamp.isChecked()){
        	viewHolder.stamp.setVisibility(View.GONE);
        }
        else {
        	viewHolder.stamp.setVisibility(View.VISIBLE);
        }
        if (!viewHolder.religious.isChecked()){
        	viewHolder.religious.setVisibility(View.GONE);
        }
        else {
        	viewHolder.religious.setVisibility(View.VISIBLE);
        }
    }
    
    ///////////////// OPTIONS MENU /////////////////////////////////////////////////////////
    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
    	super.onCreateOptionsMenu(menu);
        MenuInflater inflater = getMenuInflater();
        inflater.inflate(R.menu.angel_editor_menu, menu);
        return true;
    }
    
    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {
            case R.id.angel_editor_menu_save:
            	if ((mState == STATE_EDIT) || (mState == STATE_INSERT)){
            		if (saveAngel() == true){
            			exitActivity();
            		}
            		
            	}
            	else if (mState == STATE_VIEW){
            		exitActivity();
            	}
                return true;
            default:
                return super.onOptionsItemSelected(item);
        }
    }
    
    private boolean saveAngel() {
    	
    	if ((viewHolder.name.getText().toString().length() == 0) ||
    			(viewHolder.address.getText().toString().length() == 0) ||
    			(viewHolder.phone1.getText().toString().length() == 0)){
    		
    		AlertDialog.Builder builder = new AlertDialog.Builder(this);
    		builder.setIcon(android.R.drawable.ic_menu_info_details);
    		builder.setTitle(R.string.error);
    		builder.setMessage(R.string.angel_editor_dialog_error);
    		builder.show();
    		return false;
    	}
    	
    	mAngel.setName(viewHolder.name.getText().toString());
    	mAngel.setCity(viewHolder.address.getText().toString());
    	mAngel.setPhone1(viewHolder.phone1.getText().toString());
    	mAngel.setPhone2(viewHolder.phone2.getText().toString());
    	mAngel.setInformation(viewHolder.information.getText().toString());
    	mAngel.setPassport(viewHolder.passport.isChecked());
    	mAngel.setStamp(viewHolder.stamp.isChecked());
        mAngel.setReligious(viewHolder.religious.isChecked());
        
    	mAngelID = mAngelDao.insertOrReplace(mAngel);
    	return true;
    }
    
    private void exitActivity()
    {
    	Intent resultIntent = new Intent();  
    	resultIntent.putExtra(INTENT_OUT_DATA_ANGEL_ID, mAngelID);
    	resultIntent.putExtra(INTENT_OUT_DATA_ANGEL_LAT, mAngelLatitude);
    	resultIntent.putExtra(INTENT_OUT_DATA_ANGEL_LON, mAngelLongitude);
    	resultIntent.putExtra(INTENT_OUT_DATA_ANGEL_POSITION, mAngelPosition);
    	setResult(Activity.RESULT_OK, resultIntent);
    	finish();
    }
}
