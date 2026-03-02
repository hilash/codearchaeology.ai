package com.shvil.project.map.overlay;

import org.mapsforge.android.maps.MapView;
import org.mapsforge.android.maps.Projection;
import org.mapsforge.android.maps.overlay.ArrayItemizedOverlay;
import org.mapsforge.android.maps.overlay.OverlayItem;
import org.mapsforge.core.GeoPoint;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.graphics.Canvas;
import android.graphics.Point;
import android.graphics.drawable.Drawable;

import com.shvil.project.MainActivity;
import com.shvil.project.map.AngelEditor;
import com.shvil.project.map.MyMapFragment;
import com.shvil.project.map.OverlayItemViewer;

public class GeneralMapOverlay extends ArrayItemizedOverlay {
	
	private final Context context;
	GeoPoint touchedGeoPoint;
	MapView touchedMapView;
	
	final Runnable updateRunnable = new Runnable() {
		
        public void run() {
            //call the activity method that updates the UI
        	MainActivity activity = (MainActivity) context;
        	MyMapFragment mapFragment = (MyMapFragment) activity.main_fragments[MainActivity.FRAGMENT_INDEX_MAP];
        	mapFragment.onLongPress(touchedGeoPoint, touchedMapView);
        }
    };
	
	public GeneralMapOverlay(Drawable defaultMarker, boolean alignMarker, Context context) {
		super(defaultMarker, alignMarker);
		this.context = context;
		//.mDefaultMarker = defaultMarker;
	}
	
	/**
	 * Handles a tap event on the given item.
	 */
	@Override
	protected boolean onTap(int index) {
		OverlayItem item = createItem(index);
		OverlayType overlayType = OverlayType.values()[Integer.valueOf(item.getTitle())];
		long ID = Long.valueOf(item.getSnippet()).longValue();
		
		Intent intent = new Intent(context.getApplicationContext(), OverlayItemViewer.class);
		intent.putExtra(OverlayItemViewer.INTENT_IN_DATA_OVERYLAY_VIEWER_OVERLAY_ID, ID);
		
		int overlayViewerType = 4;
		switch(overlayType){
			case ANGEL:
				//overlayViewerType = OverlayItemViewer.INTENT_IN_DATA_OVERYLAY_VIEWER_OVERLAY_TYPE_ANGEL;
				Intent intent1 = new Intent(context.getApplicationContext(), AngelEditor.class);
				intent1.setAction(Intent.ACTION_VIEW);
				intent1.putExtra(AngelEditor.INTENT_IN_DATA_ANGEL_ID, ID);
				context.startActivity(intent1);
				return true;
		case DIARY:
				overlayViewerType = OverlayItemViewer.INTENT_IN_DATA_OVERYLAY_VIEWER_OVERLAY_TYPE_DIARY;
				break;
			case ERROR:
			case INFORMATION:
			case PEOPLE:
			case WATER:
			case WATER_HIDE:
			case SLEEP:
				overlayViewerType = OverlayItemViewer.INTENT_IN_DATA_OVERYLAY_VIEWER_OVERLAY_TYPE_GENERAL;
				break;
			default:
				// TODO
				break;
		}
		intent.putExtra(OverlayItemViewer.INTENT_IN_DATA_OVERYLAY_VIEWER_OVERLAY_TYPE, overlayViewerType);

		context.startActivity(intent);
		return true;
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
	protected void drawOverlayBitmap(Canvas canvas, Point drawPosition, Projection projection, byte drawZoomLevel) {
    	// TODO put zoom in preference
		if (drawZoomLevel > 8){
		super.drawOverlayBitmap(canvas, drawPosition, projection, drawZoomLevel); 
    	}
	}
}
