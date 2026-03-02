package com.shvil.project.menu;

import android.content.SharedPreferences;
import android.os.Bundle;
import android.preference.PreferenceManager;
import android.widget.EditText;

import com.shvil.project.R;

public class MyProfileActivity extends MenuItemActivity {
	
	public static final String MY_PROFILE_PREF_USERNAME = "username";
	static final String USERNAME_PREFIX = "Shvilist";

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.my_profile_activity);
        
        String username_string = " ";
        SharedPreferences sharedPreferences = PreferenceManager.getDefaultSharedPreferences(this.getApplicationContext());
		if (sharedPreferences.contains(MY_PROFILE_PREF_USERNAME)){
			long userID = sharedPreferences.getLong(MY_PROFILE_PREF_USERNAME, 0);
			username_string = USERNAME_PREFIX + Long.toString(userID);
		}
		
		EditText username_view = (EditText) findViewById(R.id.profile_username);
		username_view.setText(username_string);
    }
}
