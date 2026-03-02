package com.shvil.project.menu;

import com.shvil.project.R;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;

public class AboutActivity extends MenuItemActivity {
	   @Override
	    public void onCreate(Bundle savedInstanceState) {
	        super.onCreate(savedInstanceState);
	        setContentView(R.layout.about_activity_layout);
	    }
	   
	   public void sendContactMail(View v){
		   Intent intent = new Intent(Intent.ACTION_SEND);
		   intent.setType("message/rfc822");
		   
		   intent.putExtra(Intent.EXTRA_EMAIL, this.getResources().getString(R.string.support_email));
		   intent.putExtra(Intent.EXTRA_SUBJECT, "Subject");
		   // TODO - add user details
		   intent.putExtra(Intent.EXTRA_TEXT, "I'm email body.");

		   startActivity(Intent.createChooser(intent, "Send Email"));
	   }
}
