package com.shvil.project.map;

import org.mapsforge.android.maps.overlay.ArrayItemizedOverlay;
import org.mapsforge.android.maps.overlay.OverlayItem;

import android.app.AlertDialog;
import android.content.Context;
import android.graphics.drawable.Drawable;

class WaterOverlay extends ArrayItemizedOverlay {
	private final Context context;

	/**
	 * Constructs a new MyItemizedOverlay.
	 * 
	 * @param defaultMarker
	 *            the default marker (may be null).
	 * @param context
	 *            the reference to the application context.
	 */
	WaterOverlay(Drawable defaultMarker, boolean alignMarker, Context context) {
		super(defaultMarker, alignMarker);
		this.context = context;
	}

	/**
	 * Handles a tap event on the given item.
	 */
	@Override
	protected boolean onTap(int index) {
		OverlayItem item = createItem(index);
		if (item != null) {
			AlertDialog.Builder builder = new AlertDialog.Builder(this.context);
			builder.setIcon(android.R.drawable.ic_menu_info_details);
			builder.setTitle(item.getTitle());
			builder.setMessage(item.getSnippet());
			builder.setPositiveButton("OK", null);
			builder.show();
		}
		return true;
	}
}