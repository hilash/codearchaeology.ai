package com.shvil.project.menu;

import com.shvil.project.R;

import android.os.Bundle;
import android.preference.PreferenceFragment;

public class HelpFragment extends PreferenceFragment {
	@Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        // Load the preferences from an XML resource
        addPreferencesFromResource(R.xml.help_preferences);   
    }
}
