// TODO - when adding item and the overlay is not there,
// error. we need to add the overlay before, or check before update overlay
package com.shvil.project.map;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.util.Arrays;
import java.util.Collections;
import java.util.HashMap;
import java.util.Hashtable;
import java.util.Map;

import org.mapsforge.android.maps.MapActivity;
import org.mapsforge.android.maps.MapView;
import org.mapsforge.android.maps.overlay.ArrayItemizedOverlay;
import org.mapsforge.core.GeoPoint;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.res.AssetManager;
import android.location.Location;
import android.location.LocationManager;
import android.os.Bundle;
import android.provider.Settings;
import android.view.LayoutInflater;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.widget.PopupMenu;
import android.widget.Toast;

import com.shvil.project.MainActivity;
import com.shvil.project.MyApplication;
import com.shvil.project.R;
import com.shvil.project.db.GeneralItem.GeneralItemType;
import com.shvil.project.map.MyLocation.LocationResult;
import com.shvil.project.map.overlay.LocationOverlay;
import com.shvil.project.map.overlay.OverlayManager;
import com.shvil.project.map.overlay.OverlayType;

public class MyMapActivity extends MapActivity {
	
	//private static final File MAP_FILE2 = new File(Environment.getExternalStorageDirectory().getPath(), "israel_and_palestine.map");
	private static final String MAP_PATH = "israel_and_palestine.map";
	private File CACHED_MAP_FILE = null;

	
	MapView mapView;
	Map<Integer, ArrayItemizedOverlay> MenuItemsIdOverlay = new Hashtable<Integer, ArrayItemizedOverlay>();
	MyApplication myApplication;
	MainActivity mainActivity;
	GeoPoint LongPressGeoPoint;
	MyLocation myLocation;
	public OverlayManager mOverlayManager;
	
	// Intent request code
	public final static int INTENT_REQ_CODE_ANGEL_EDIT = 0;
	public final static int INTENT_REQ_CODE_ANGEL_INSERT = 1;
	public final static int INTENT_REQ_CODE_ANGEL_VIEW = 2;
	public final static int INTENT_REQ_CODE_GENERAL_EDIT = 3;
	public final static int INTENT_REQ_CODE_GENERAL_INSERT = 4;
	
	// overlays maps
	static final Map<Integer, OverlayType> mOverlayTypeByOverlayMenuItems = createOverlayTypeByOverlayMenuItemsMap();
	Map<Integer, Boolean> mChecksOfOverlayMenuItems = createChecksOfOverlayMenuItemsMap();
	
	@SuppressLint("UseSparseArrays")
	private static Map<Integer, OverlayType> createOverlayTypeByOverlayMenuItemsMap() {
        Map<Integer, OverlayType> result = new HashMap<Integer, OverlayType>();
        result.put(R.id.menu_overlay_angels, OverlayType.ANGEL);
        result.put(R.id.menu_overlay_info, OverlayType.INFORMATION);
        result.put(R.id.menu_overlay_diary, OverlayType.DIARY);
        result.put(R.id.menu_overlay_people, OverlayType.PEOPLE);
        result.put(R.id.menu_overlay_water, OverlayType.WATER);
        result.put(R.id.menu_overlay_water_hide, OverlayType.WATER_HIDE);
        result.put(R.id.menu_overlay_sleep, OverlayType.SLEEP);
        return Collections.unmodifiableMap(result);
    }
	
	private Map<Integer, Boolean> createChecksOfOverlayMenuItemsMap() {
		Map<Integer, Boolean> result =  new Hashtable<Integer, Boolean>();
		result.put(R.id.menu_overlay_angels, true);
        result.put(R.id.menu_overlay_info, true);
        result.put(R.id.menu_overlay_diary, true);
        result.put(R.id.menu_overlay_people, true);
        result.put(R.id.menu_overlay_water, true);
        result.put(R.id.menu_overlay_water_hide, true);
        result.put(R.id.menu_overlay_sleep, true);
		return result;
	}
	
	@SuppressLint("DefaultLocale")
	private void createMapFile() throws IOException
	{
        CACHED_MAP_FILE = new File(getCacheDir(), MAP_PATH);

	    AssetManager am = getAssets();
	    OutputStream os = new FileOutputStream(CACHED_MAP_FILE);
	    CACHED_MAP_FILE.createNewFile();
	    byte []b = new byte[1024];
	    int i, r;
	    String []Files = am.list("");
	    Arrays.sort(Files);
	    i = 0;
	    while (true)
	    {
	    	String fn = String.format("%s%d", MAP_PATH, i);
	        if(Arrays.binarySearch(Files, fn) < 0)
	               break;
	        InputStream is = am.open(fn);
	        while((r = is.read(b)) != -1)
	            os.write(b, 0, r);
	        is.close();
	        i++;
	    }
	    os.close();
	}
	
	@Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.my_map_activity);

        myApplication = (MyApplication) this.getApplicationContext();
        mapView = (MapView) findViewById(R.id.mapview);
        mapView.setClickable(true);
        mapView.setBuiltInZoomControls(true);    
		try {
			createMapFile();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
        mapView.setMapFile(CACHED_MAP_FILE);


        
        mainActivity = (MainActivity) this.getParent();
        myApplication.mapActivity = this;
    	myLocation = new MyLocation();
  
    	mOverlayManager = new OverlayManager(this, mapView);
    	mOverlayManager.createOverlay(OverlayType.SHVIL);
    	mOverlayManager.createOverlay(OverlayType.LISTENING);
    	mOverlayManager.createOverlay(OverlayType.ERROR);
    	// update the current location
    	mOverlayManager.createOverlay(OverlayType.LOCATION);
    	onClickUpdateMyLocationButton(null);

    	// create the checked menu overlays
    	createMenuOverlays(); 
    	
    	// TODO - when recreating location,
    	// get the last location (maybe before we added pic or something
    	// and it saved the location but in the overlay it's not updated yet)
    }
	
	@Override
	protected void onPause() {
	    super.onPause();
	    myLocation.cancelTimer();
	}
	
	@Override
	protected void onStart() {
	    super.onStart();

	    // This verification should be done during onStart() because the system calls
	    // this method when the user returns to the activity, which ensures the desired
	    // location provider is enabled each time the activity resumes from the stopped state.
	    LocationManager locationManager =
	            (LocationManager) getSystemService(Context.LOCATION_SERVICE);
	    final boolean gpsEnabled = locationManager.isProviderEnabled(LocationManager.GPS_PROVIDER);

	    if (!gpsEnabled) {
	        // Build an alert dialog here that requests that the user enable
	        // the location services, then when the user clicks the "OK" button,
	        // call enableLocationSettings()
			
			AlertDialog.Builder builder = new AlertDialog.Builder(this);
			builder.setTitle("enable location settings");
			builder.setMessage("To show you current location on the map, Turn on GPS");
			
			builder.setPositiveButton("Settings", new DialogInterface.OnClickListener() {
			           public void onClick(DialogInterface dialog, int id) {
			        	   enableLocationSettings();
			        	   }
			           });
			builder.setNegativeButton("Skip", new DialogInterface.OnClickListener() {
			           public void onClick(DialogInterface dialog, int id) {}
			           });
			// 3. Get the AlertDialog from create()
			AlertDialog dialog = builder.create();	
			dialog.show();
	    }
	}

	private void enableLocationSettings() {
	    Intent settingsIntent = new Intent(Settings.ACTION_LOCATION_SOURCE_SETTINGS);
	    startActivity(settingsIntent);
	}
	
	////// OVERLAYS POPUP MENU ////////////////////////////////////////////////////////////
	
	private void createMenuOverlays()
	{	
		for (Integer menuItemId : mChecksOfOverlayMenuItems.keySet())
		{
            // add overlay if checked
            if (mChecksOfOverlayMenuItems.get(menuItemId)){
                OverlayType overlayType = mOverlayTypeByOverlayMenuItems.get(menuItemId);
                mOverlayManager.createOverlay(overlayType);
            }
        }
	}
	
    public void showPopupOverlay(View v) {
        PopupMenu popup = new PopupMenu(this, v);
        MenuInflater inflater = popup.getMenuInflater();
        inflater.inflate(R.menu.map_overlay_menu, popup.getMenu());
        
        // loop for menu items
        for (int i = 0; i < popup.getMenu().size(); ++i)
        {
            MenuItem item = popup.getMenu().getItem(i);
            int id = item.getItemId();
            if (!mChecksOfOverlayMenuItems.containsKey(id))
            {
            	mChecksOfOverlayMenuItems.put(id, item.isChecked());
            }
            boolean check = mChecksOfOverlayMenuItems.get(id);
            item.setChecked(check); 
        }
        
        popup.setOnMenuItemClickListener(new PopupMenu.OnMenuItemClickListener() {
            public boolean onMenuItemClick(MenuItem item) {
            	int menuItemId = item.getItemId();
            	OverlayType overlayType = mOverlayTypeByOverlayMenuItems.get(menuItemId);

                if (item.isChecked()){ 
                    mOverlayManager.removeOverlay(overlayType);
                }
                else {
                	// create overlay
                    mOverlayManager.createOverlay(overlayType);
                }
                
                // common
                mChecksOfOverlayMenuItems.put(menuItemId, !item.isChecked());
                item.setChecked(!item.isChecked());
                return true;
            }
        });
        popup.show();
    }
    
	////// ON REPORT ////////////////////////////////////////////////////////////

    public boolean onLongPress(GeoPoint geoPoint, MapView mapView) {
    	// 	TODO check why here it's work and in 'onActivityresult' not
		//Toast.makeText(this, "onLongPress: " + geoPoint.toString(), Toast.LENGTH_LONG).show();
		//waterOverlay.addItem(new OverlayItem(geoPoint, "Title", "Text"));
    	//this.angelOverlay.addItem(new OverlayItem(geoPoint, "Title", "Text"));
    	
    	// TODO - have to had option to delete personal and water hiding items
    	LongPressGeoPoint = geoPoint;
    	showReportDialog();
		return true;
	}
    
    AlertDialog report_dialog;
	public void showReportDialog()
	{
		LayoutInflater inflater = this.getLayoutInflater();
    	
	    AlertDialog.Builder builder = new AlertDialog.Builder(this);
		builder.setIcon(android.R.drawable.ic_menu_info_details);
		builder.setTitle("Add");
		builder.setMessage("presonal notes and water hiding places are private");
		builder.setView(inflater.inflate(R.layout.report_dialog_layout, null));
		report_dialog = builder.show();
	}
	
	public void onReportDialogClick(View v) {
		int viewID = v.getId();
		Intent intent;
		
		if (viewID == R.id.report_dialog_button_angel){
			intent = new Intent(myApplication, AngelEditor.class);
			intent.setAction(Intent.ACTION_INSERT);
			intent.putExtra(AngelEditor.INTENT_IN_DATA_ANGEL_LAT, LongPressGeoPoint.getLatitude());
			intent.putExtra(AngelEditor.INTENT_IN_DATA_ANGEL_LON, LongPressGeoPoint.getLongitude());
			intent.putExtra(AngelEditor.INTENT_IN_DATA_ANGEL_POSITION, 0);
			mainActivity.startActivityForResult(intent, INTENT_REQ_CODE_ANGEL_INSERT);	
		}
		// TODO - add support for diary items
		else {
			// General Item Type - TODO - put it in a map
			GeneralItemType generalItemType = null;
			
			switch(v.getId()){
			case R.id.report_dialog_button_error:
				generalItemType = GeneralItemType.GENERAL_ITEM_TYPE_ERROR;
				break;
			case R.id.report_dialog_button_info:
				generalItemType = GeneralItemType.GENERAL_ITEM_TYPE_INFORMATION;
				break;
			case R.id.report_dialog_button_water:
				generalItemType = GeneralItemType.GENERAL_ITEM_TYPE_WATER;
				break;
			case R.id.report_dialog_button_water_hide:
				generalItemType = GeneralItemType.GENERAL_ITEM_TYPE_WATER_HIDE;
				break;
			case R.id.report_dialog_button_sleep:
				generalItemType = GeneralItemType.GENERAL_ITEM_TYPE_SLEEP;
					
			default:
				break;
			}
				
			intent = new Intent(myApplication, GeneralItemEditor.class);
			intent.setAction(Intent.ACTION_INSERT);
			intent.putExtra(GeneralItemEditor.INTENT_IN_DATA_GENERAL_TYPE, (byte)generalItemType.ordinal());
			intent.putExtra(GeneralItemEditor.INTENT_IN_DATA_GENERAL_LAT, LongPressGeoPoint.getLatitude());
			intent.putExtra(GeneralItemEditor.INTENT_IN_DATA_GENERAL_LON, LongPressGeoPoint.getLongitude());
			mainActivity.startActivityForResult(intent, INTENT_REQ_CODE_GENERAL_INSERT);
		}
		
		report_dialog.dismiss();	
	}
	

	
	private void onActivityResultAngel(int requestCode, int resultCode, Intent data)
    {
		// wait for activity
		// if success, add new overlay, add to db to send?  - add new column to each angel, water - new (truefalse)
		// so we know if to send it to DB or not. when the next update arrives, remove all the 'new items' 
		// (since will be getting them from server. in update we need to receate all overlays)
		
		// can return from edit note or inserting a new one
		if ((resultCode != Activity.RESULT_OK) || (data == null)){
			return;
		}
		
		Long angelID = data.getLongExtra(AngelEditor.INTENT_OUT_DATA_ANGEL_ID, Long.MAX_VALUE);
	    //double mAngelLatitude = data.getDoubleExtra(AngelEditor.INTENT_IN_DATA_ANGEL_LAT, Double.MAX_VALUE);
	    //double mAngelLongitude = data.getDoubleExtra(AngelEditor.INTENT_IN_DATA_ANGEL_LON, Double.MAX_VALUE);
	    
		if (angelID == Long.MAX_VALUE){
			return;
		}
		if (requestCode == INTENT_REQ_CODE_ANGEL_INSERT){
			//GeoPoint geoPoint = new GeoPoint(mAngelLatitude, mAngelLongitude);
			// TODO - check why addItem to overlay doesnt work.
			// in the mean time, recreate the overlay
			//angelOverlay.addItem(new OverlayItem(geoPoint, angelID.toString(), "text"));
	    	//this.angelOverlay.addItem(new OverlayItem(geoPoint, angelID.toString(), "Text"));
			//angelOverlay.requestRedraw();
            mOverlayManager.removeOverlay(OverlayType.ANGEL);
            mOverlayManager.createOverlay(OverlayType.ANGEL);
		}
		
		//mApplication = (MyApplication) this.getApplication();
        //mAngelDao = mApplication.daoSession.getAngelDao();
        //mAngel = mAngelDao.load(mAngelID);
    }
	
	private void onActivityResultGeneralItem(int requestCode, int resultCode, Intent data)
    {
		// can return from edit note or inserting a new one
		if ((resultCode != Activity.RESULT_OK) || (data == null)){
			return;
		}
		
		Long itemID = data.getLongExtra(GeneralItemEditor.INTENT_OUT_DATA_GENERAL_ID, Long.MAX_VALUE);
		byte generalOverlayType = data.getByteExtra(GeneralItemEditor.INTENT_OUT_DATA_GENERAL_TYPE, Byte.MAX_VALUE);
	    //double mItemLatitude = data.getDoubleExtra(GeneralItemEditor.INTENT_IN_DATA_GENERAL_LAT, Double.MAX_VALUE);
	    //double mItemLongitude = data.getDoubleExtra(GeneralItemEditor.INTENT_IN_DATA_GENERAL_LON, Double.MAX_VALUE);
	    
		if (itemID == Long.MAX_VALUE){
			return;
		}
		if (requestCode == INTENT_REQ_CODE_GENERAL_INSERT){
			// TODO - fix it to use the 'addOverlay' method and not to recreate the overlays
			//GeoPoint geoPoint = new GeoPoint(mItemLatitude, mItemLongitude);
			//getOverlayByGeneralItemType(GeneralItemType.values()[itemType]).addItem(new OverlayItem(geoPoint, itemID.toString(), ""));
			
			// get the overlayType from the generalOverlayType
			//generalOverlayType
			OverlayType overlayType = OverlayManager.OverlayTypeByGeneralItemType.get(GeneralItemType.values()[generalOverlayType]);
			if (overlayType != null){
				mOverlayManager.removeOverlay(overlayType);
				mOverlayManager.createOverlay(overlayType);
			}
		}
    }
	
	@Override
	public void onActivityResult(int requestCode, int resultCode, Intent data)
	{
		
		// if overlay not exists, create it and show it (check = true)
		// if exists, add and need to refresh (somehow..)
		switch(requestCode){
		case INTENT_REQ_CODE_ANGEL_EDIT:
			onActivityResultAngel(requestCode, resultCode, data);
			break;
		case INTENT_REQ_CODE_ANGEL_INSERT:
			onActivityResultAngel(requestCode, resultCode, data);
			break;
		case INTENT_REQ_CODE_ANGEL_VIEW:
			onActivityResultAngel(requestCode, resultCode, data);
			break;
		case INTENT_REQ_CODE_GENERAL_EDIT:
			onActivityResultGeneralItem(requestCode, resultCode, data);
			break;
		case INTENT_REQ_CODE_GENERAL_INSERT:
			onActivityResultGeneralItem(requestCode, resultCode, data);
			break;
		default:
			return;
		}
	}
    
	////// ON REPORT POPUP MENU - ////////////////////////////////////////////////////////////
	// TODO - change it to just make a toast and launch the popup report dialog

    public void showPopupReport(View v) {
    	
    	final MyMapActivity map = this;
    	
    	// get current location and update overlay
    	LocationResult locationResult = new LocationResult(){

			@Override
			public void gotLocation(Location location) {

				if (location != null){
					LongPressGeoPoint = new GeoPoint(location.getLatitude(), location.getLongitude());
					map.runOnUiThread(new Runnable() {
						  public void run() {
							  Toast.makeText(map, "current location saved", Toast.LENGTH_LONG).show();
							  showReportDialog();
						  }
						});
				}
				else {
					map.runOnUiThread(new Runnable() {
						  public void run() {
							  Toast.makeText(map, "can't save current location", Toast.LENGTH_LONG).show();
						  }
						});
				}
			}
    	};
    	if (false == myLocation.getLocation(this, locationResult, myLocation.GET_LAST_LOCATION_DELAY)){
			  Toast.makeText(this, "turn GPS on to get current location", Toast.LENGTH_LONG).show();
    	}
    }
    
    /*
    private void ChangeFragemnt(int index)
    {
    	MainActivity mainActivity = (MainActivity) this.getParent();
    	mainActivity.switchTab(index);
    }
    */
    
	////// ON UPDATE MY LOCATION BUTTON - ////////////////////////////////////////////////////////////

    public void onClickUpdateMyLocationButton(View v) {
    	// TODO - sync calls to my location via sync keyword in MyLocation class
    	
    	// get last location and update overlay
    	// (we do this since getting the current location may take time and the user
    	// don't want to wait)
    	LocationOverlay locationOverlay = (LocationOverlay) OverlayManager.mOverlayByType.get(OverlayType.LOCATION);
		//locationOverlay.setMapCenterToLastLocation();
		locationOverlay.UpdateNewLocation(new GeoPoint(32.07472,34.776484));

    	
    	// get current location and update overlay
    	LocationResult locationResult = new LocationResult(){

			@Override
			public void gotLocation(Location location) {
				LocationOverlay locationOverlay = (LocationOverlay) OverlayManager.mOverlayByType.get(OverlayType.LOCATION);
				if (location != null){
					locationOverlay.UpdateNewLocation(new GeoPoint(location.getLatitude(), location.getLongitude()));
				}
				else {
					locationOverlay.setMapCenterToLastLocation();
				}
				
			}
    	};
    	if (false == myLocation.getLocation(this, locationResult, myLocation.DEFAULT_DELAY)){
			  Toast.makeText(this, "turn GPS on to get current location", Toast.LENGTH_LONG).show();
    	}
    }
        
}