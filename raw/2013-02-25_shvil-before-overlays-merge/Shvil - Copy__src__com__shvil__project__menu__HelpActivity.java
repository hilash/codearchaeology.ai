package com.shvil.project.menu;

import android.os.Bundle;

public class HelpActivity extends MenuItemActivity {
	
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        
        // Display the fragment as the main content.
        getFragmentManager().beginTransaction()
                .replace(android.R.id.content, new HelpFragment())
                .commit();
    }
}
