/**
 * 
 */
package com.shvil.project.diary;

import com.shvil.project.R;

import android.annotation.SuppressLint;
import android.app.ActionBar;
import android.app.Activity;
import android.os.Build;
import android.os.Bundle;

/**
 * @author Purple Fire
 *
 */
public class DiaryPicsViewer extends Activity {

	
	public DiaryPicsViewer() {
		// TODO Auto-generated constructor stub
	}
	
	 @SuppressLint("NewApi")
	protected void onCreate(Bundle savedInstanceState) {
	        super.onCreate(savedInstanceState);
	        ActionBar actionBar = getActionBar();
	        actionBar.setDisplayHomeAsUpEnabled(true);
	        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.ICE_CREAM_SANDWICH) {
	        	actionBar.setHomeButtonEnabled(true);
			}
	        setContentView(R.layout.note_editor);
	 }

}
