package com.shvil.project.menu.angels;

import java.util.List;

import android.annotation.TargetApi;
import android.app.ActionBar;
import android.app.ListActivity;
import android.content.Intent;
import android.graphics.drawable.BitmapDrawable;
import android.os.Build;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.PopupWindow;

import com.shvil.project.MainActivity;
import com.shvil.project.MyApplication;
import com.shvil.project.R;
import com.shvil.project.db.Angel;
import com.shvil.project.db.AngelDao;

public class AngelsActivity<ConfirmActivity> extends ListActivity {

	private AngelDao angelDao;
	private AngelsArrayAdapter adapter;
	
	@TargetApi(Build.VERSION_CODES.ICE_CREAM_SANDWICH)
	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
	    setContentView(R.layout.angels_activity);
	    ActionBar actionBar = getActionBar();
	    actionBar.setDisplayHomeAsUpEnabled(true);
	    if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.ICE_CREAM_SANDWICH) {
        	actionBar.setHomeButtonEnabled(false);
		}
    
	    
	    
 		MyApplication mApplication = (MyApplication)getApplicationContext();
 		angelDao = mApplication.daoSession.getAngelDao();
 		
 		//addAngels();
	    
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
	
	private PopupWindow pw;
	private static View rowView;
	private static ViewHolder viewHolder = null;
	private void initiatePopupWindow(Angel angel, View view) {
	    try {
	        //We need to get the instance of the LayoutInflater, use the context of this activity
	        LayoutInflater inflater = this.getLayoutInflater();
	        rowView = inflater.inflate(R.layout.angel_editor, null);
	        if (viewHolder == null){
		        viewHolder = new ViewHolder();
			    
			    viewHolder.name = (EditText)rowView.findViewById(R.id.angel_editor_name);
			    viewHolder.address = (EditText)rowView.findViewById(R.id.angel_editor_address);
			    viewHolder.phone1 = (EditText)rowView.findViewById(R.id.angel_editor_phone1);
			    viewHolder.phone2 = (EditText)rowView.findViewById(R.id.angel_editor_phone2);
			    viewHolder.information = (EditText)rowView.findViewById(R.id.angel_editor_information);
			    viewHolder.passport = (CheckBox)rowView.findViewById(R.id.angel_editor_passport);
			    viewHolder.stamp = (CheckBox)rowView.findViewById(R.id.angel_editor_stamp);
			    viewHolder.religious = (CheckBox)rowView.findViewById(R.id.angel_editor_religious);
			    
			    viewHolder.name.setEnabled(false);
		        viewHolder.address.setEnabled(false);
		        viewHolder.phone1.setEnabled(false);
		        viewHolder.phone2.setEnabled(false);
		        viewHolder.information.setEnabled(false);
		        viewHolder.passport.setEnabled(false);
		        viewHolder.stamp.setEnabled(false);
			    rowView.setTag(viewHolder);
	        }
	        
	        // get data
	        rowView.setBackgroundColor(0xff444444);
	        viewHolder = (ViewHolder) rowView.getTag();
	        viewHolder.name.setText(angel.getName());
	        viewHolder.address.setText(angel.getCity());
	        viewHolder.phone1.setText(angel.getPhone1());
	        viewHolder.phone2.setText(angel.getPhone2());
	        viewHolder.information.setText(angel.getInformation());
	        viewHolder.passport.setChecked(angel.getPassport());
	        viewHolder.stamp.setChecked(angel.getStamp());
	        viewHolder.religious.setChecked(angel.getReligious());
	        
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
        
	        // create a 300px width and 470px height PopupWindow
	        pw = new PopupWindow(rowView);
	        //pw.setIgnoreCheekPress();
	        //pw.setOutsideTouchable(false);
	        pw.setTouchable(false);
	        pw.setBackgroundDrawable(new BitmapDrawable());
	        pw.setOutsideTouchable(true);
	        pw.setWindowLayoutMode(ViewGroup.LayoutParams.MATCH_PARENT, ViewGroup.LayoutParams.WRAP_CONTENT);
	        // display the popup in the center
	        //pw.showAtLocation(layout, Gravity.CENTER, 0, 0);
	        pw.showAsDropDown(view);
	 
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
