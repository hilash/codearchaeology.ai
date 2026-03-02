package com.shvil.project.map.overlay;

import org.mapsforge.android.maps.overlay.ArrayItemizedOverlay;
import org.mapsforge.android.maps.overlay.OverlayItem;
import org.mapsforge.core.GeoPoint;

import android.content.Context;
import android.graphics.drawable.Drawable;

public class LocationOverlay extends ArrayItemizedOverlay {
	
	private static final String THREAD_NAME = "LocationOverlayOverlay";
	private final Context context;
	GeoPoint MyLocation;
	
	public LocationOverlay(Drawable defaultMarker, boolean alignMarker, Context context) {
		super(defaultMarker, alignMarker);
		this.context = context;
		// TODO - init this with last known location from preferences, in the first
		// call to the overlay
	}
	
	public boolean UpdateNewLocation(GeoPoint newLocation){
		if (newLocation == null){
			return false;
		}
		
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
