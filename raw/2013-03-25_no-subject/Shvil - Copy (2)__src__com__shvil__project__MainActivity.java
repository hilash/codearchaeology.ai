package com.shvil.project;

import android.annotation.TargetApi;
import android.app.ActionBar;
import android.app.FragmentTransaction;
import android.content.Context;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Build;
import android.os.Bundle;
import android.preference.PreferenceManager;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentActivity;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentPagerAdapter;
import android.support.v4.view.ViewPager;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;

import com.shvil.project.diary.DiaryFragment;
import com.shvil.project.map.MyMapFragment;
import com.shvil.project.menu.MenuFragment;
import com.shvil.project.sync.SyncDataBase;

public class MainActivity extends FragmentActivity implements ActionBar.TabListener {

    /**
     * The {@link android.support.v4.view.PagerAdapter} that will provide fragments for each of the
     * sections. We use a {@link android.support.v4.app.FragmentPagerAdapter} derivative, which will
     * keep every loaded fragment in memory. If this becomes too memory intensive, it may be best
     * to switch to a {@link android.support.v4.app.FragmentStatePagerAdapter}.
     */
    SectionsPagerAdapter mSectionsPagerAdapter;
    public Fragment[] main_fragments;
    
    public static final int FRAGMENT_INDEX_MENU = 0;
    public static final int FRAGMENT_INDEX_MAP = 1;
    public static final int FRAGMENT_INDEX_DIARY = 2;

    

    /**
     * The {@link ViewPager} that will host the section contents.
     */
    ViewPager mViewPager;

    @TargetApi(Build.VERSION_CODES.ICE_CREAM_SANDWICH)
	@Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        
        // initialize application with default settings
        PreferenceManager.setDefaultValues(this, R.xml.settings_preferences, false);
        
        setContentView(R.layout.activity_main);
        // Create the adapter that will return a fragment for each of the three primary sections
        // of the app.
        mSectionsPagerAdapter = new SectionsPagerAdapter(getSupportFragmentManager());

        // Set up the action bar.
        final ActionBar actionBar = getActionBar();
        actionBar.setNavigationMode(ActionBar.NAVIGATION_MODE_TABS);
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.ICE_CREAM_SANDWICH) {
        	actionBar.setHomeButtonEnabled(false);
		}   

        // Set up the ViewPager with the sections adapter.
        mViewPager = (ViewPager) findViewById(R.id.pager);
        mViewPager.setAdapter(mSectionsPagerAdapter);

        
        // When swiping between different sections, select the corresponding tab.
        // We can also use ActionBar.Tab#select() to do this if we have a reference to the
        // Tab.
        mViewPager.setOnPageChangeListener(new ViewPager.SimpleOnPageChangeListener() {
            @Override
            public void onPageSelected(int position) {
                actionBar.setSelectedNavigationItem(position);
            }
        });

        
        // For each of the sections in the app, add a tab to the action bar.
        for (int i = 0; i < mSectionsPagerAdapter.getCount(); i++) {
            // Create a tab with text corresponding to the page title defined by the adapter.
            // Also specify this Activity object, which implements the TabListener interface, as the
            // listener for when this tab is selected.
            actionBar.addTab(
                    actionBar.newTab()
                            .setText(mSectionsPagerAdapter.getPageTitle(i))
                            .setTabListener(this));
        }
    }  

    /**
     * A {@link FragmentPagerAdapter} that returns a fragment corresponding to one of the primary
     * sections of the app.
     */
    public class SectionsPagerAdapter extends FragmentPagerAdapter {

    	private Fragment[] fragments;
    	private String[] titles = {"Menu", "Map", "Notes"};
    	
    	
        public SectionsPagerAdapter(FragmentManager fm) {
            super(fm);
            
            fragments = new Fragment[] { new MenuFragment(), new MyMapFragment(), new DiaryFragment()};
            main_fragments = fragments;
        }

        @Override
        public Fragment getItem(int i) {
            return fragments[i];
        }

        @Override
        public int getCount() {
            return fragments.length;
        }

        public CharSequence getPageTitle(int position) {
            return titles[position];
        }
    }

    
    public void onTabUnselected(ActionBar.Tab tab, FragmentTransaction fragmentTransaction) {
    }

    public void onTabSelected(ActionBar.Tab tab, FragmentTransaction fragmentTransaction) {
        // When the given tab is selected, switch to the corresponding page in the ViewPager.
        mViewPager.setCurrentItem(tab.getPosition());
    }

    public void onTabReselected(ActionBar.Tab tab, FragmentTransaction fragmentTransaction) {
    }
    
    public void switchTab(int tab){
    	mViewPager.setCurrentItem(tab, true);
    }
    
    @Override
	protected void onActivityResult(int requestCode, int resultCode, Intent data)
	{
    	super.onActivityResult(requestCode, resultCode, data);
    	//MyApplication app = (MyApplication) this.getApplicationContext();
    	//app.mapActivity.onActivityResult(requestCode, resultCode, data);
    	MyMapFragment mapFragment = (MyMapFragment) this.main_fragments[MainActivity.FRAGMENT_INDEX_MAP];
    	mapFragment.onActivityResult(requestCode, resultCode, data);
	}
    
    public void onReportDialogClick(View v) {
    	MyMapFragment mapFragment = (MyMapFragment) this.main_fragments[MainActivity.FRAGMENT_INDEX_MAP];
    	mapFragment.onReportDialogClick(v);
    }
    
    private MenuItem refreshMenuItem;
    private boolean refreshing;
    
    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
    	super.onCreateOptionsMenu(menu);

        MenuInflater inflater = getMenuInflater();
        inflater.inflate(R.menu.main_activity_menu, menu);

        // refresh menu item
        refreshMenuItem = menu.findItem(R.id.main_activity_menu_refresh);
        return super.onCreateOptionsMenu(menu);
    }
    
    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {
        	// TODO change name to sync?
            case R.id.main_activity_menu_refresh:
            	if (!refreshing){
            		new SyncDatabaseTask().execute((MyApplication)this.getApplicationContext(), this);
            	}
            	break;
        }
        return true;
    }
    
    private void setRefreshing(boolean refreshing) {
        this.refreshing = refreshing;
        if(refreshMenuItem == null) return;

        if(refreshing)
            refreshMenuItem.setActionView(R.layout.actionbar_refresh_progress);
        else
            refreshMenuItem.setActionView(null);
    }
    
    // TODO
    /*
    private void syncWithServer()
    {
    	Intent intent = new Intent(getApplicationContext(), SyncDatabseIntentService.class);
    	startService(intent);
    }
    */
    
    private class SyncDatabaseTask extends AsyncTask<Context, Integer, Long> {
        protected Long doInBackground(Context... urls) {
        	/*
            int count = urls.length;
            long totalSize = 0;
            for (int i = 0; i < count; i++) {
                //totalSize += Downloader.downloadFile(urls[i]);
                publishProgress((int) ((i / (float) count) * 100));
                // Escape early if cancel() is called
                if (isCancelled()) break;
            }
            return totalSize;
            */
        	new SyncDataBase(urls[0], urls[1]).SyncDBWithServer();
        	long totalSize = 0;
        	return totalSize;
        }

        protected void onProgressUpdate(Integer... progress) {
            //setProgress(progress[0]);
        }
        
        protected void onPreExecute() {
            //showDialog("Downloaded " + result + " bytes");
        	setRefreshing(true);
        }

        protected void onPostExecute(Long result) {
            //showDialog("Downloaded " + result + " bytes");
        	setRefreshing(false);
        }
    }
    
}
