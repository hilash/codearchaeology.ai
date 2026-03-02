package com.shvil.project.map.overlay;

import org.mapsforge.android.maps.Projection;
import org.mapsforge.android.maps.overlay.ArrayItemizedOverlay;
import org.mapsforge.android.maps.overlay.OverlayItem;

import com.shvil.project.map.OverlayItemViewer;

import android.content.Context;
import android.content.Intent;
import android.graphics.Canvas;
import android.graphics.Point;
import android.graphics.drawable.Drawable;

public class GeneralMapOverlay extends ArrayItemizedOverlay {
	
	private final Context context;
	//private final Drawable mDefaultMarker;
	
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
		long ID = Long.valueOf(item.getTitle()).longValue();
		
		Intent intent = new Intent(context.getApplicationContext(), OverlayItemViewer.class);
		intent.putExtra(OverlayItemViewer.INTENT_IN_DATA_OVERYLAY_VIEWER_OVERLAY_ID, ID);
		intent.putExtra(OverlayItemViewer.INTENT_IN_DATA_OVERYLAY_VIEWER_OVERLAY_TYPE, 
				OverlayItemViewer.INTENT_IN_DATA_OVERYLAY_VIEWER_OVERLAY_TYPE_GENERAL);

		context.startActivity(intent);
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
