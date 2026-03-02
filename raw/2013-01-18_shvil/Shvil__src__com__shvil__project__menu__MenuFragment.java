package com.shvil.project.menu;

import java.util.ArrayList;
import java.util.List;

import android.content.Intent;
import android.content.res.Resources;
import android.os.Bundle;
import android.support.v4.app.ListFragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ListView;

import com.shvil.project.R;

class SettingsMenuItem {
	
	public int mMenuItemDescription;
	public Class<?>  mMenuItemActivity;
	
	SettingsMenuItem(int menuFragmentItemProfile, Class<?> menuItemActivity){
		mMenuItemDescription = menuFragmentItemProfile;
		mMenuItemActivity 	 = menuItemActivity;
	}
}

public class MenuFragment extends ListFragment {

    	SettingsMenuItem[] mMenuItems = new SettingsMenuItem[]{
			new SettingsMenuItem(R.string.menu_fragment_item_profile, MyProfileActivity.class),
			//new SettingsMenuItem(R.string.menu_fragment_item_messages, RecentMessagesActivity.class),
			//new SettingsMenuItem(R.string.menu_fragment_item_people, PeopleActivity.class),
			//new SettingsMenuItem(R.string.menu_fragment_item_angels, AngelsActivity.class),
			new SettingsMenuItem(R.string.menu_fragment_item_syncdb, SyncDbActivity.class),
			new SettingsMenuItem(R.string.menu_fragment_item_settings, SettingsActivity.class),
			new SettingsMenuItem(R.string.menu_fragment_item_help, HelpActivity.class),
			new SettingsMenuItem(R.string.menu_fragment_item_about, AboutActivity.class)
	};

    
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
    	
    	if (container == null) {
            return null;
        }
    	
    	// get the description of the items
    	Resources res = getResources();
    	List<String> listItems = new ArrayList<String>();
    	for (SettingsMenuItem item : mMenuItems){
    		String description = res.getString(item.mMenuItemDescription);
    		listItems.add(description);
    	}
    	
    	ArrayAdapter<String> adapter = new ArrayAdapter<String>(inflater.getContext(), android.R.layout.simple_list_item_1, listItems); 
        setListAdapter(adapter);
        return inflater.inflate(R.layout.menu_fragment, container, false);  
    }
    
    
    @Override
    public void onListItemClick(ListView l, View v, int position, long id) {
        super.onListItemClick(l, v, position, id);
        {
        	Intent intent = new Intent();
            intent.setClass(getActivity(), mMenuItems[position].mMenuItemActivity);
            //intent.addFlags(Intent.FLAG_ACTIVITY_NO_ANIMATION);
            //this.overridePendingTransition(R.anim.animation_enter, R.anim.animation_enter);        
            startActivity(intent);
            // TODO - do we need to finish()? i don't think so
        }
    }
}

