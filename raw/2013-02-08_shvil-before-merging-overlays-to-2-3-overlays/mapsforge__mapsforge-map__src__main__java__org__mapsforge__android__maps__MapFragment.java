package org.mapsforge.android.maps;

import java.io.File;
import java.util.ArrayList;
import java.util.List;

import org.mapsforge.android.maps.mapgenerator.MapGenerator;
import org.mapsforge.core.GeoPoint;
import org.mapsforge.core.MapPosition;

import android.support.v4.app.Fragment;
import android.content.Context;
import android.content.ContextWrapper;
import android.content.SharedPreferences;
import android.content.SharedPreferences.Editor;

public class MapFragment extends Fragment {
	private static final String KEY_LATITUDE = "latitude";
	private static final String KEY_LONGITUDE = "longitude";
	private static final String KEY_MAP_FILE = "mapFile";
	private static final String KEY_ZOOM_LEVEL = "zoomLevel";
	private static final String PREFERENCES_FILE = "MapActivity";

	/**
	 * Counter to store the last ID given to a MapView.
	 */
	private int lastMapViewId;

	/**
	 * Internal list which contains references to all running MapView objects.
	 */
	private final List<MapView> mapViews = new ArrayList<MapView>(2);

	private void destroyMapViews() {
		while (!this.mapViews.isEmpty()) {
			MapView mapView = this.mapViews.remove(0);
			mapView.destroy();
		}
	}

	@Override
	public void onDestroy() {
		super.onDestroy();
		destroyMapViews();
	}

	@Override
	public void onPause() {
		super.onPause();
		for (MapView currentMapView : this.mapViews)
			currentMapView.onPause();

		Editor editor = getActivity().getSharedPreferences(PREFERENCES_FILE, Context.MODE_PRIVATE).edit();
		editor.clear();

		if (this.mapViews.size() > 0) {
			MapView mapView = this.mapViews.get(0);

			// save the map position and zoom level
			MapPosition mapPosition = mapView.getMapPosition().getMapPosition();
			if (mapPosition != null) {
				GeoPoint geoPoint = mapPosition.geoPoint;
				editor.putInt(KEY_LATITUDE, geoPoint.latitudeE6);
				editor.putInt(KEY_LONGITUDE, geoPoint.longitudeE6);
				editor.putInt(KEY_ZOOM_LEVEL, mapPosition.zoomLevel);
			}

			if (!mapView.getMapGenerator().requiresInternetConnection() && mapView.getMapFile() != null) {
				// save the map file
				editor.putString(KEY_MAP_FILE, mapView.getMapFile().getAbsolutePath());
			}
		}

		editor.commit();

		if (isRemoving()) {
			destroyMapViews();
		}
	}

	@Override
	public void onResume() {
		super.onResume();
		for (MapView currentMapView : this.mapViews) {
			currentMapView.onResume();
		}
	}

	public MapFragmentContext getMapContext() {
		return new MapFragmentContext(getActivity().getApplicationContext());
	}

	private class MapFragmentContext extends ContextWrapper implements MapContext {
		public MapFragmentContext(Context base) {
			super(base);
		}

		/**
		 * Returns a unique MapView ID on each call.
		 * 
		 * @return the new MapView ID.
		 */
		public int getMapViewId() {
			return ++MapFragment.this.lastMapViewId;
		}

		/**
		 * This method is called once by each MapView during its setup process.
		 * 
		 * @param mapView
		 *            the calling MapView.
		 */
		@Override
		public void registerMapView(MapView mapView) {
			MapFragment.this.mapViews.add(mapView);
			restoreMapView(mapView);
		}

		private boolean containsMapViewPosition(SharedPreferences sharedPreferences) {
			return sharedPreferences.contains(KEY_LATITUDE) && sharedPreferences.contains(KEY_LONGITUDE)
					&& sharedPreferences.contains(KEY_ZOOM_LEVEL);
		}

		private void restoreMapView(MapView mapView) {
			SharedPreferences sharedPreferences = getSharedPreferences(PREFERENCES_FILE, MODE_PRIVATE);
			if (containsMapViewPosition(sharedPreferences)) {
				MapGenerator mapGenerator = mapView.getMapGenerator();
				if (!mapGenerator.requiresInternetConnection() && sharedPreferences.contains(KEY_MAP_FILE)) {
					// get and set the map file
					mapView.setMapFile(new File(sharedPreferences.getString(KEY_MAP_FILE, null)));
				}

				// get and set the map position and zoom level
				int latitudeE6 = sharedPreferences.getInt(KEY_LATITUDE, 0);
				int longitudeE6 = sharedPreferences.getInt(KEY_LONGITUDE, 0);
				int zoomLevel = sharedPreferences.getInt(KEY_ZOOM_LEVEL, -1);

				GeoPoint geoPoint = new GeoPoint(latitudeE6, longitudeE6);
				MapPosition mapPosition = new MapPosition(geoPoint, (byte) zoomLevel);
				mapView.setCenterAndZoom(mapPosition);
			}
		}
	}
	
	final public void unregisterMapView (MapView mapView) {
		this.mapViews.remove (mapView);
	}
}
