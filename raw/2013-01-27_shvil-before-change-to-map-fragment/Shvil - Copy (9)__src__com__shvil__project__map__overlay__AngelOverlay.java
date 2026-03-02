package com.shvil.project.map.overlay;

import org.mapsforge.android.maps.Projection;
import org.mapsforge.android.maps.overlay.ArrayItemizedOverlay;
import org.mapsforge.android.maps.overlay.OverlayItem;

import android.content.Context;
import android.content.Intent;
import android.graphics.Canvas;
import android.graphics.Point;
import android.graphics.drawable.Drawable;

import com.shvil.project.map.OverlayItemViewer;

public class AngelOverlay extends ArrayItemizedOverlay {
	
	private final Context context;
	private static final String THREAD_NAME = "AngelOverlay";

	/**
	 * Constructs a new MyItemizedOverlay.
	 * 
	 * @param defaultMarker
	 *            the default marker (may be null).
	 * @param context
	 *            the reference to the application context.
	 */
	public AngelOverlay(Drawable defaultMarker, boolean alignMarker, Context context) {
		super(defaultMarker, alignMarker);
		this.context = context;
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
				OverlayItemViewer.INTENT_IN_DATA_OVERYLAY_VIEWER_OVERLAY_TYPE_ANGEL);

		context.startActivity(intent);
		/*
		OverlayItem item = createItem(index);
		long angelID = Long.valueOf(item.getTitle()).longValue();
		
		Intent intent = new Intent(context.getApplicationContext(), AngelEditor.class);
		intent.setAction(Intent.ACTION_VIEW);
		intent.putExtra(AngelEditor.INTENT_IN_DATA_ANGEL_ID, angelID);
		context.startActivity(intent);
		*/
		/*
		if (item != null) {
			AlertDialog.Builder builder = new AlertDialog.Builder(this.context);
			builder.setIcon(android.R.drawable.ic_menu_info_details);
			builder.setTitle(item.getTitle());
			builder.setMessage(item.getSnippet());
			builder.setPositiveButton("OK", null);
			builder.show();
		}
		*/
		return true;
	}
	
	@Override
	public String getThreadName() {
		return THREAD_NAME;
	}
	
	@Override
	protected void drawOverlayBitmap(Canvas canvas, Point drawPosition, Projection projection, byte drawZoomLevel) {
    	if (drawZoomLevel > 8){
		super.drawOverlayBitmap(canvas, drawPosition, projection, drawZoomLevel); 
    	}
	}
}
