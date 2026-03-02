
package com.shvil.project.map;

import java.io.BufferedReader;
import java.io.File;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.nio.charset.Charset;
import java.util.ArrayList;
import java.util.Hashtable;
import java.util.List;
import java.util.Map;

import org.mapsforge.android.maps.MapActivity;
import org.mapsforge.android.maps.MapView;
import org.mapsforge.android.maps.overlay.ArrayWayOverlay;
import org.mapsforge.android.maps.overlay.Overlay;
import org.mapsforge.android.maps.overlay.OverlayItem;
import org.mapsforge.android.maps.overlay.OverlayWay;
import org.mapsforge.core.GeoPoint;

import android.annotation.SuppressLint;
import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.Paint.Cap;
import android.graphics.drawable.Drawable;
import android.os.Bundle;
import android.os.Environment;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.widget.PopupMenu;

import com.shvil.project.MyApplication;
import com.shvil.project.R;
import com.shvil.project.db.Water;

public class MyMapActivity extends MapActivity {
	
	private static final File MAP_FILE = new File(Environment.getExternalStorageDirectory().getPath(), "israel_and_palestine.map");
	
	MapView mapView;
	Map<Integer,Boolean> overlaysMenuItemsChecks = new Hashtable<Integer,Boolean>();
	Map<Integer, Overlay> MenuItemsIdOverlay = new Hashtable<Integer, Overlay>();
	MyApplication myApplication;
	
	// Overlays
	ArrayWayOverlay shvilOverlay;
	WaterOverlay waterOverlay;
	
	@Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
             
        setContentView(R.layout.my_map_activity);
        mapView = (MapView) findViewById(R.id.mapview);
        mapView.setClickable(true);
        mapView.setBuiltInZoomControls(true);
        mapView.setMapFile(MAP_FILE);
        
        myApplication = (MyApplication) this.getApplicationContext();
        
        // get Overlays   
        shvilOverlay = getShvilOverlay();
        waterOverlay = getWaterOverlay();
        
        
        // assign Popup item with overlay's
        MenuItemsIdOverlay.put(R.id.menu_overlay_water, waterOverlay);
        
        mapView.getOverlays().add(shvilOverlay);
        mapView.getOverlays().add(waterOverlay);
    }
	
	////// POPUP ////////////////////////////////////////////////////////////
	
    public void showPopupOverlay(View v) {
        PopupMenu popup = new PopupMenu(this, v);
        MenuInflater inflater = popup.getMenuInflater();
        inflater.inflate(R.menu.map_overlay_menu, popup.getMenu());
        
        // loop for menu items
        for (int i = 0; i < popup.getMenu().size(); ++i)
        {
            MenuItem item = popup.getMenu().getItem(i);
            int id = item.getItemId();
            if (!overlaysMenuItemsChecks.containsKey(id))
            {
            	overlaysMenuItemsChecks.put(id, item.isChecked());
            }
            boolean check = overlaysMenuItemsChecks.get(id);
            item.setChecked(check); 
        }
        
        popup.setOnMenuItemClickListener(new PopupMenu.OnMenuItemClickListener() {
            public boolean onMenuItemClick(MenuItem item) {
            	int id = item.getItemId();

                if (item.isChecked()){               	
                	Overlay overlay = MenuItemsIdOverlay.get(id);
                    mapView.getOverlays().remove(overlay);
                    MenuItemsIdOverlay.remove(id);
                }
                else {
                	// add it
                    Overlay overlay = getOverlayByID(id);
                    mapView.getOverlays().add(overlay);
                	MenuItemsIdOverlay.put(id, overlay);
                }
                
                // common
                overlaysMenuItemsChecks.put(id, !item.isChecked());
                item.setChecked(!item.isChecked());
                return true;
            }
        });
        popup.show();
    }
    
    public void showPopupReport(View v) {
        PopupMenu popup = new PopupMenu(this, v);
        MenuInflater inflater = popup.getMenuInflater();
        inflater.inflate(R.menu.map_report_menu, popup.getMenu());
        
        popup.setOnMenuItemClickListener(new PopupMenu.OnMenuItemClickListener() {
            public boolean onMenuItemClick(MenuItem item) {
            	switch(item.getItemId())
            	{
	            	case R.id.menu_report_note:
	            		break;
	            	case R.id.menu_report_problem:
	            		break;
            	}
                return true;
            }
        });
        
        popup.show();
        
        // try to save current location, and show toast
    }
    
    /////// OVERLAYS ///////////////////////////////////////////////////////////////
	
	private Overlay getOverlayByID(int id) {
		
		switch (id)
		{
			case R.id.menu_overlay_water:
				return getWaterOverlay();
			default:
				return null;
		}
	}
    
    @SuppressLint({ "UseValueOf" })
	private ArrayWayOverlay getShvilOverlay()
	{
    	Paint paintFill = new Paint(Paint.ANTI_ALIAS_FLAG);
		paintFill.setStyle(Paint.Style.FILL);
		paintFill.setColor(Color.BLUE);
		paintFill.setAlpha(64);

		Paint paintStroke = new Paint(Paint.ANTI_ALIAS_FLAG);
		paintStroke.setStyle(Paint.Style.STROKE);
		paintStroke.setColor(Color.BLUE);
		paintStroke.setAlpha(192);
		paintStroke.setStrokeWidth(2);
		paintStroke.setStrokeCap(Cap.ROUND);
		paintStroke.setStrokeJoin(Paint.Join.ROUND);
		
		GeoPoint[][] wayNodes = new GeoPoint[1][];
		List<GeoPoint> geoPoints = new ArrayList<GeoPoint>(20000);
		
		ArrayWayOverlay arrayWayOverlay = new ArrayWayOverlay(paintStroke, null);
		

		InputStream fis = null;
		try {
			fis = getResources().getAssets().open("ShvilBest.txt");
		} catch (IOException e1) {
			// TODO Auto-generated catch block
			e1.printStackTrace();
		}

		 BufferedReader br;
		 String line;
		 boolean read_points = false;
		 if (myApplication.daoSession.getWaterDao().loadAll().size() == 0){
			 read_points = true;
		 }
			 
		 br = new BufferedReader(new InputStreamReader(fis, Charset.forName("UTF-8")));
		 try {
			while ((line = br.readLine()) != null) {
			    if (line.startsWith("*"))
			    {
			    	if (geoPoints.size() > 0) {
			    		wayNodes[0] = new  GeoPoint[] {};
			    		wayNodes[0] = geoPoints.toArray(wayNodes[0]);
			    		OverlayWay overlayWay = new OverlayWay(wayNodes, null, paintStroke);
			   		 	arrayWayOverlay.addWay(overlayWay);
			   		 	
			   		 	if (read_points){
				   		 	double longitude = geoPoints.get(0).getLongitude();
				   		 	double latitude = geoPoints.get(0).getLatitude();
				   		 	Water water =  new Water(null, "title", "bla bla bla", longitude, latitude);
				   		 	myApplication.daoSession.getWaterDao().insert(water);
			   		 	}
			   		 	
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
        return arrayWayOverlay;
	}
    
    private WaterOverlay getWaterOverlay()
    {
    	// create a default marker for the overlay
        Drawable defaultMarker = getResources().getDrawable(R.drawable.ic_water);

        // create an ItemizedOverlay with the default marker
        WaterOverlay waterOverlay = new WaterOverlay(defaultMarker, true, this);

        // get all water points
        List<Water> waters = myApplication.daoSession.getWaterDao().loadAll();
        
        List<OverlayItem> overlayItems = new ArrayList<OverlayItem>(waters.size());
        
       for (Water water : waters){
    		double latitude = water.getLatitude();
    		double longitude = water.getLongitude();
    		GeoPoint geoPoint = new GeoPoint(latitude,longitude);
    		
    		// create an OverlayItem with title and description
    		overlayItems.add(new OverlayItem(geoPoint, water.getTitle(), water.getText()));
    	}
        waters.clear();

        // add the OverlayItems to the ArrayItemizedOverlay
        waterOverlay.addItems(overlayItems);

        return waterOverlay;        
    }    

}