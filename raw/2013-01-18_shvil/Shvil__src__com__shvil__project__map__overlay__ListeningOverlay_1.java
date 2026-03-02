package com.shvil.project.map.overlay;

import org.mapsforge.android.maps.MapView;
import org.mapsforge.android.maps.Projection;
import org.mapsforge.android.maps.overlay.Overlay;
import org.mapsforge.core.GeoPoint;

import android.app.Activity;
import android.content.Context;
import android.graphics.Canvas;
import android.graphics.Point;

import com.shvil.project.map.MyMapActivity;

public class ListeningOverlay extends Overlay {
	
	private static final String THREAD_NAME = "ListeningOverlay";
	private final Context context;
	GeoPoint touchedGeoPoint;
	MapView touchedMapView;
	
	final Runnable updateRunnable = new Runnable() {
		
        public void run() {
            //call the activity method that updates the UI
            ((MyMapActivity) context).onLongPress(touchedGeoPoint, touchedMapView);
        }
    };
	
	public ListeningOverlay(MyMapActivity context)
	{
		super();
		this.context = context;
	}

	@Override
	protected void drawOverlayBitmap(Canvas arg0, Point arg1, Projection arg2,
			byte arg3) {
		// TODO Auto-generated method stub

	}
	
	/**
	 * Handles a long press event. A long press event is only triggered if the map was not moved. A return value of true
	 * indicates that the long press event has been handled by this overlay and stops its propagation to other overlays.
	 * <p>
	 * The default implementation of this method does nothing and returns false.
	 * 
	 * @param geoPoint
	 *            the point which has been long pressed.
	 * @param mapView
	 *            the {@link MapView} that triggered the long press event.
	 * @return true if the long press event was handled, false otherwise.
	 */
	@Override
	public boolean onLongPress(GeoPoint geoPoint, MapView mapView) {
		touchedGeoPoint = geoPoint;
		touchedMapView = mapView;
		((Activity) this.context).runOnUiThread(updateRunnable);
		return true;
	}
	
	@Override
	public String getThreadName() {
		return THREAD_NAME;
	}
}
