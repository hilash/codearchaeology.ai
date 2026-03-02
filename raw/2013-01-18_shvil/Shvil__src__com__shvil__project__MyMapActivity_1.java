
package com.shvil.project;

import java.io.File;
import java.util.ArrayList;
import java.util.List;
import java.util.Random;

import org.mapsforge.android.maps.MapActivity;
import org.mapsforge.android.maps.MapView;
import org.mapsforge.android.maps.overlay.Circle;
import org.mapsforge.android.maps.overlay.ListOverlay;
import org.mapsforge.android.maps.overlay.OverlayItem;
import org.mapsforge.core.model.BoundingBox;
import org.mapsforge.core.model.GeoPoint;

import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.drawable.Drawable;
import android.os.Bundle;
import android.os.Environment;

public class MyMapActivity extends MapActivity {
	private static final File MAP_FILE = new File(Environment.getExternalStorageDirectory().getPath(), "berlin.map");
	private static final int NUMBER_OF_CIRCLES = 10000;
	
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        MapView mapView = new MapView(this);
        mapView.setClickable(true);
        mapView.setBuiltInZoomControls(true);
        mapView.setMapFile(new File("/sdcard/israel.map"));
        setContentView(mapView);
        
        List<OverlayItem> overlayItems = createAngels();
		ListOverlay listOverlay = new ListOverlay();
		listOverlay.getOverlayItems().addAll(overlayItems);
		mapView.getOverlays().add(listOverlay);
    }
    
    
    private static List<OverlayItem> createAngels() {
    	int NUMBER_OF_CIRCLES = 20;
		List<OverlayItem> overlayItems = new ArrayList<OverlayItem>(NUMBER_OF_CIRCLES);

		Paint paintFill = new Paint(Paint.ANTI_ALIAS_FLAG);
		paintFill.setStyle(Paint.Style.FILL);
		paintFill.setColor(Color.BLUE);
		paintFill.setAlpha(64);

		Paint paintStroke = new Paint(Paint.ANTI_ALIAS_FLAG);
		paintStroke.setStyle(Paint.Style.STROKE);
		paintStroke.setColor(Color.BLUE);
		paintStroke.setAlpha(96);
		paintStroke.setStrokeWidth(3);

		Random random = new Random(0);
		BoundingBox boundingBox = new BoundingBox(32.06632, 13.08283, 34.77782, 13.76136);
		double latitudeSpan = boundingBox.getLatitudeSpan();
		double longitudeSpan = boundingBox.getLongitudeSpan();

		for (int i = 0; i < NUMBER_OF_CIRCLES; ++i) {
			double latitude = boundingBox.minLatitude + random.nextDouble() * latitudeSpan;
			double longitude = boundingBox.minLongitude + random.nextDouble() * longitudeSpan;
			GeoPoint geoPoint = new GeoPoint(latitude, longitude);
			overlayItems.add(new Circle(geoPoint, 50, paintFill, paintStroke));
		}

		return overlayItems;
	}
}