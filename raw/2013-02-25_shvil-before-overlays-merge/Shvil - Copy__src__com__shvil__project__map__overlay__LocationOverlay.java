package com.shvil.project.map.overlay;

import org.mapsforge.android.maps.overlay.ArrayItemizedOverlay;
import org.mapsforge.android.maps.overlay.OverlayItem;
import org.mapsforge.core.GeoPoint;

import android.app.Activity;
import android.content.Context;
import android.content.SharedPreferences;
import android.content.SharedPreferences.Editor;
import android.graphics.drawable.Drawable;

public class LocationOverlay extends ArrayItemizedOverlay {
	
	@SuppressWarnings("unused")
	private static final String THREAD_NAME = "LocationOverlayOverlay";
	private static final String MY_LOCATION_PREF_LAT = "my_location_lat";
	private static final String MY_LOCATION_PREF_LON = "my_location_lon";
	private static final float DEFAULT_MY_LOCATION_PREF_LAT = 32.07472F;
	private static final float DEFAULT_MY_LOCATION_PREF_LON = 34.776484F;
	private final Context context;
	GeoPoint MyLocation;
	
	public LocationOverlay(Drawable defaultMarker, boolean alignMarker, Context context) {
		super(defaultMarker, alignMarker);
		this.context = context;
		
		// init this with last known location from preferences	
		SharedPreferences sharedPreferences = ((Activity)context).getPreferences(Context.MODE_PRIVATE);
		MyLocation = new GeoPoint(sharedPreferences.getFloat(MY_LOCATION_PREF_LAT, DEFAULT_MY_LOCATION_PREF_LAT),
				 				   sharedPreferences.getFloat(MY_LOCATION_PREF_LAT, DEFAULT_MY_LOCATION_PREF_LON));
	}
	
	public boolean UpdateNewLocation(GeoPoint newLocation){
		if (newLocation == null){
			return false;
		}
		
		SharedPreferences sharedPreferences = ((Activity)context).getPreferences(Context.MODE_PRIVATE);
		Editor preferencesEditor = sharedPreferences.edit();
		preferencesEditor.putFloat(MY_LOCATION_PREF_LAT, (float) newLocation.getLatitude());
		preferencesEditor.putFloat(MY_LOCATION_PREF_LON, (float) newLocation.getLongitude());
		preferencesEditor.commit();
		
		MyLocation = newLocation;
		this.clear();
		this.addItem(new OverlayItem(MyLocation, "", ""));
		this.internalMapView.setCenter(MyLocation);
		this.requestRedraw();
		return true;
	}
	
	public boolean setMapCenterToLastLocation(){
		if (MyLocation == null){
			return false;
		}
		this.internalMapView.setCenter(MyLocation);
		return true;
	}
}
