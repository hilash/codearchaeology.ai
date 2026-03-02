
package com.shvil.project.map;

import java.io.BufferedReader;
import java.io.File;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.nio.charset.Charset;
import java.util.ArrayList;
import java.util.List;

import org.mapsforge.android.maps.MapActivity;
import org.mapsforge.android.maps.MapView;
import org.mapsforge.android.maps.overlay.Circle;
import org.mapsforge.android.maps.overlay.ListOverlay;
import org.mapsforge.android.maps.overlay.Marker;
import org.mapsforge.android.maps.overlay.MyLocationOverlay;
import org.mapsforge.android.maps.overlay.OverlayItem;
import org.mapsforge.android.maps.overlay.PolygonalChain;
import org.mapsforge.android.maps.overlay.Polyline;
import org.mapsforge.core.model.GeoPoint;

import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.Paint.Cap;
import android.graphics.drawable.Drawable;
import android.os.Bundle;
import android.os.Environment;
import android.view.MenuInflater;
import android.view.View;
import android.widget.PopupMenu;

import com.shvil.project.R;

public class MyMapActivity extends MapActivity {
	private static final File MAP_FILE = new File(Environment.getExternalStorageDirectory().getPath(), "israel.map");
	private static final int NUMBER_OF_CIRCLES = 10000;
	MapView mapView;
	MyLocationOverlay myLocationOverlay;
	
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        
        setContentView(R.layout.my_map_activity);
        mapView = (MapView) findViewById(R.id.mapview);
        mapView.setClickable(true);
        mapView.setBuiltInZoomControls(true);
        mapView.setMapFile(MAP_FILE);
        
        //drawLinesOnTelAviv();
        getShvilGeoPoints();
    }
    
    private void drawOnTelAviv()
    {
    	GeoPoint telaviv = new GeoPoint(32.0945, 34.77511);
    	Drawable defaultMarker = getResources().getDrawable(R.drawable.androidmarker);
        defaultMarker.setBounds(0, 0, defaultMarker.getIntrinsicWidth(), defaultMarker.getIntrinsicHeight()); 
        Marker marker = new Marker(telaviv, defaultMarker);

        List<OverlayItem> overlayItems = new ArrayList<OverlayItem>(1);
        overlayItems.add(marker);
        ListOverlay listOverlay = new ListOverlay();
        listOverlay.getOverlayItems().addAll(overlayItems);
        mapView.getOverlays().add(listOverlay);
        
        
    }
    
    private void drawCircleOnTelAviv()
    {
    	Paint paintFill = new Paint(Paint.ANTI_ALIAS_FLAG);
		paintFill.setStyle(Paint.Style.FILL);
		paintFill.setColor(Color.BLUE);
		paintFill.setAlpha(64);

		Paint paintStroke = new Paint(Paint.ANTI_ALIAS_FLAG);
		paintStroke.setStyle(Paint.Style.STROKE);
		paintStroke.setColor(Color.BLUE);
		paintStroke.setAlpha(96);
		paintStroke.setStrokeWidth(3);
		
    	GeoPoint telaviv = new GeoPoint(32.0945, 34.77511);
    	GeoPoint telaviv2 = new GeoPoint(32.0945, 34.9);
    	
        List<OverlayItem> overlayItems = new ArrayList<OverlayItem>(2);
        overlayItems.add(new Circle(telaviv, 1000, paintFill, paintStroke));
        overlayItems.add(new Circle(telaviv2, 1000, paintFill, paintStroke));
        ListOverlay listOverlay = new ListOverlay();
        listOverlay.getOverlayItems().addAll(overlayItems);
        mapView.getOverlays().add(listOverlay);
    }
  
   
    
    /*
    private static Polyline createPolyline() {
    	
    	GeoPoint telaviv = new GeoPoint(32.0945, 34.77511);
    	GeoPoint telaviv2 = new GeoPoint(32.0945, 34.9);
    	GeoPoint telaviv3 = new GeoPoint(32.5, 34.9);
    	
		//List<GeoPoint> geoPoints = Arrays.asList(telaviv, telaviv2, telaviv3);
    	List<GeoPoint> geoPoints = new ArrayList<GeoPoint>();
    	geoPoints.add(telaviv);
    	geoPoints.add(telaviv2);
    	geoPoints.add(telaviv3);
		//PolygonalChain polygonalChain = new PolygonalChain(geoPoints);
    	PolygonalChain polygonalChain = new PolygonalChain(svhilGPS.getShvilGeoPoints3());

		Paint paintStroke = new Paint(Paint.ANTI_ALIAS_FLAG);
		paintStroke.setStyle(Paint.Style.STROKE);
		paintStroke.setColor(Color.BLUE);
		paintStroke.setAlpha(192);
		paintStroke.setStrokeWidth(2);
		paintStroke.setStrokeCap(Cap.ROUND);
		paintStroke.setStrokeJoin(Paint.Join.ROUND);

		return new Polyline(polygonalChain, paintStroke);
	}
    
    private void drawLinesOnTelAviv()
    {
		Polyline polygon = createPolyline();
    	
        List<OverlayItem> overlayItems = new ArrayList<OverlayItem>(1);
        overlayItems.add(polygon);
        ListOverlay listOverlay = new ListOverlay();
        listOverlay.getOverlayItems().addAll(overlayItems);
        mapView.getOverlays().add(listOverlay);
    }*/
    
    
    public void showPopupOverlay(View v) {
        PopupMenu popup = new PopupMenu(this, v);
        MenuInflater inflater = popup.getMenuInflater();
        inflater.inflate(R.layout.map_overlay_menu, popup.getMenu());
        popup.show();
    }
    
    public void showPopupReport(View v) {
        PopupMenu popup = new PopupMenu(this, v);
        MenuInflater inflater = popup.getMenuInflater();
        inflater.inflate(R.layout.map_report_menu, popup.getMenu());
        popup.show();
    }
    
    public void CenterMyLocation(View v) {
    	myLocationOverlay.enableMyLocation(true);
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

		/*
		Random random = new Random(0);
		BoundingBox boundingBox = new BoundingBox(34.77511, 32.0945, 34.9, 32.9);
		double latitudeSpan = boundingBox.getLatitudeSpan();
		double longitudeSpan = boundingBox.getLongitudeSpan();

		
		for (int i = 0; i < NUMBER_OF_CIRCLES; ++i) {
			double latitude = boundingBox.minLatitude + random.nextDouble() * latitudeSpan;
			double longitude = boundingBox.minLongitude + random.nextDouble() * longitudeSpan;
			GeoPoint geoPoint = new GeoPoint(latitude, longitude);
			overlayItems.add(new Circle(geoPoint, 50, paintFill, paintStroke));
		}
		*/
		
		double latitude = 34.77511;
		double longitude = 32.0945;
		
		GeoPoint geoPoint1 = new GeoPoint(latitude, longitude);
		GeoPoint geoPoint2 = new GeoPoint(longitude, latitude);
		overlayItems.add(new Circle(geoPoint1, 50, paintFill, paintStroke));
		overlayItems.add(new Circle(geoPoint2, 50, paintFill, paintStroke));

		return overlayItems;
	}
    
    private void getShvilGeoPoints()
	{
    	ListOverlay listOverlay = new ListOverlay();
    	List<OverlayItem> overlayItems = new ArrayList<OverlayItem>(20);
		List<GeoPoint> geoPoints = new ArrayList<GeoPoint>();
		PolygonalChain polygonalChain = null;
		Polyline polyline = null;

		Paint paintStroke = new Paint(Paint.ANTI_ALIAS_FLAG);
		paintStroke.setStyle(Paint.Style.STROKE);
		paintStroke.setColor(Color.BLUE);
		paintStroke.setAlpha(192);
		paintStroke.setStrokeWidth(2);
		paintStroke.setStrokeCap(Cap.ROUND);
		paintStroke.setStrokeJoin(Paint.Join.ROUND);

		InputStream fis = null;
		try {
			fis = getResources().getAssets().open("ShvilBest.txt");
		} catch (IOException e1) {
			// TODO Auto-generated catch block
			e1.printStackTrace();
		}

		 BufferedReader br;
		 String line;
		 br = new BufferedReader(new InputStreamReader(fis, Charset.forName("UTF-8")));
		 try {
			while ((line = br.readLine()) != null) {
			    if (line.startsWith("*"))
			    {
			    	if (geoPoints.size() > 0) {
			    		polygonalChain = new PolygonalChain(geoPoints);
			    		polyline = new Polyline(polygonalChain, paintStroke);
			    		overlayItems.add(polyline);
			    		geoPoints.clear();
			    	}
			    }
			    else 
			    {
			    	String[] tokens = line.split(" ");
			    	geoPoints.add(new GeoPoint(new Double(tokens[0]), new Double(tokens[1])));
			    }
				
				
			 }
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
        
        listOverlay.getOverlayItems().addAll(overlayItems);
        mapView.getOverlays().add(listOverlay);
	} 
}