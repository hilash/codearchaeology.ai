package com.shvil.project.angels;

import java.util.List;

import android.app.ActionBar;
import android.app.ListActivity;
import android.content.Context;
import android.content.Intent;
import android.graphics.drawable.BitmapDrawable;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ListView;
import android.widget.PopupWindow;
import android.widget.TextView;

import com.shvil.project.MainActivity;
import com.shvil.project.MyApplication;
import com.shvil.project.R;
import com.shvil.project.db.Angel;
import com.shvil.project.db.AngelDao;

public class AngelsActivity<ConfirmActivity> extends ListActivity {

	private AngelDao angelDao;
	private AngelsArrayAdapter adapter;
	
	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
	    setContentView(R.layout.angels_activity);
	    ActionBar actionBar = getActionBar();
	    actionBar.setDisplayHomeAsUpEnabled(true);
	    actionBar.setHomeButtonEnabled(true);
    
	    
	    
 		MyApplication mApplication = (MyApplication)getApplicationContext();
 		angelDao = mApplication.daoSession.getAngelDao();
 		
 		addAngels();
	    
 		List<Angel> angles = angelDao.loadAll();
	    
	    adapter = new AngelsArrayAdapter(this, angles);
	    setListAdapter(adapter);
	  }
	
	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		switch (item.getItemId()) {
			case android.R.id.home:
				// app icon in action bar clicked; go home
				Intent intent = new Intent(this, MainActivity.class);
				//intent.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
				startActivity(intent);
	  
				//finish();
				return true;
			default:
				return super.onOptionsItemSelected(item);
	  }
  }
	
	private void addAngels()
	{

		Long id = null;
	    String city = "Haifa";
	    String name = "Joe Johnson";
	    String phone1 = "03423";
	    String phone2 = null;
	    String information = null;
	    long latitude = 10;
	    long longitude = 20;
	    Boolean passport = false;
	    Boolean stamp = false;
	    Boolean religious = false;
	    Long distance = (long) 209;
		Angel angel = new Angel(id, city, name, phone1, phone2,
				information, latitude, longitude, passport,
				stamp, religious, distance);
		Angel angel2 = new Angel(id, city, "Joshhhh", phone1, phone2,
				information, latitude, longitude, passport,
				stamp, religious, distance);
	    angelDao.insert(angel);
	    angelDao.insert(angel2);
	}
	
	static class ViewHolder {
	    public TextView city;
		public TextView name;
		public TextView phone1;
		//public TextView phone2;
		//public TextView information;
		//public TextView latitude;
		//public TextView longitude;
		//public ImageView passport;
		//public ImageView stamp;
		//public ImageView religious;
		//public ImageView distance; // from shvil
	  }
	
	private PopupWindow pw;
	@SuppressWarnings("deprecation")
	private void initiatePopupWindow(Angel angel, View view) {
	    try {
	        //We need to get the instance of the LayoutInflater, use the context of this activity
	        LayoutInflater inflater = (LayoutInflater) AngelsActivity.this
	                .getSystemService(Context.LAYOUT_INFLATER_SERVICE);
	        //Inflate the view from a predefined XML layout
	        View layout = inflater.inflate(R.layout.angel_popup,
	        		(ViewGroup) findViewById(R.id.popup_element));
	        // create a 300px width and 470px height PopupWindow
	        pw = new PopupWindow(layout);
	        //pw.setIgnoreCheekPress();
	        //pw.setOutsideTouchable(false);
	        pw.setTouchable(false);
	        pw.setBackgroundDrawable(new BitmapDrawable());
	        pw.setOutsideTouchable(true);
	        pw.setWindowLayoutMode(ViewGroup.LayoutParams.MATCH_PARENT, ViewGroup.LayoutParams.WRAP_CONTENT);
	        // display the popup in the center
	        //pw.showAtLocation(layout, Gravity.CENTER, 0, 0);
	        pw.showAsDropDown(view);

	 
	        //TextView mResultText = (TextView) layout.findViewById(R.id.city);
	 
	    } catch (Exception e) {
	        e.printStackTrace();
	    }
	}
	
	 @Override
	    public void onListItemClick(ListView l, View v, int position, long id) {
	        super.onListItemClick(l, v, position, id);
	        {
	        	initiatePopupWindow(adapter.getItem(position), v);
	        }
	    }

}
