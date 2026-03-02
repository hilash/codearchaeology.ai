package com.shvil.project.menu;

import android.content.Intent;
import android.os.Bundle;
import android.support.v4.app.ListFragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ListView;

import com.shvil.project.R;
import com.shvil.project.angels.AngelsActivity;
import com.shvil.project.messages.RecentMessagesActivity;


public class MenuFragment extends ListFragment {
	
    String[] listItems = new String[] {
            "My Profile",
            "Messsges",
            "People",
            "Angels",
            "Settings"
        };
    
    @SuppressWarnings("rawtypes")
	Class[] ActivityClass = new Class[] {
    		MyProfileActivity.class,
    		RecentMessagesActivity.class,
    		PeopleActivity.class,
    		AngelsActivity.class,
    		SettingsActivity.class};

    
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
    	
    	if (container == null) {
            // We have different layouts, and in one of them this
            // fragment's containing frame doesn't exist.  The fragment
            // may still be created from its saved state, but there is
            // no reason to try to create its view hierarchy because it
            // won't be displayed.  Note this is not needed -- we could
            // just run the code below, where we would create and return
            // the view hierarchy; it would just never be used.
            return null;
        }
    	
    	ArrayAdapter<String> adapter = new ArrayAdapter<String>(inflater.getContext(), android.R.layout.simple_list_item_1, listItems); 
        /** Setting the list adapter for the ListFragment */
        setListAdapter(adapter);
    	// Inflate the layout for this fragment
        return inflater.inflate(R.layout.menu_fragment, container, false);
    }
    
    
    @Override
    public void onListItemClick(ListView l, View v, int position, long id) {
        super.onListItemClick(l, v, position, id);
        {
        	Intent intent = new Intent();
            intent.setClass(getActivity(), ActivityClass[position]);
            intent.addFlags(Intent.FLAG_ACTIVITY_NO_ANIMATION);
            //this.overridePendingTransition(R.anim.animation_enter, R.anim.animation_enter);        
            startActivity(intent);
        }
    }
}

