package com.shvil.project.map.overlay;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.nio.charset.Charset;
import java.util.ArrayList;
import java.util.Collections;
import java.util.HashMap;
import java.util.Hashtable;
import java.util.List;
import java.util.Map;

import org.mapsforge.android.maps.MapView;
import org.mapsforge.android.maps.overlay.ArrayWayOverlay;
import org.mapsforge.android.maps.overlay.Overlay;
import org.mapsforge.android.maps.overlay.OverlayItem;
import org.mapsforge.android.maps.overlay.OverlayWay;
import org.mapsforge.core.GeoPoint;

import android.annotation.SuppressLint;
import android.content.Context;
import android.content.res.Resources;
import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.Paint.Cap;
import android.graphics.drawable.Drawable;

import com.shvil.project.MyApplication;
import com.shvil.project.R;
import com.shvil.project.db.Angel;
import com.shvil.project.db.GeneralItem;
import com.shvil.project.db.GeneralItem.GeneralItemType;
import com.shvil.project.db.GeneralItemDao;
import com.shvil.project.db.GeneralItemDao.Properties;
import com.shvil.project.map.MyMapActivity;



public class OverlayManager {
	
	Context mContext;
	MapView mMapView;
	MyApplication mMyApplication; 
	public static Map<OverlayType, Overlay> mOverlayByType;
	public Map<OverlayType, Drawable> mDrawableByType;	
	static final Map<OverlayType, GeneralItemType> mGeneralItemTypeByOverlayType = createGeneralItemTypeByOverlayTypeMap();
	public static final Map<GeneralItemType, OverlayType> OverlayTypeByGeneralItemType = createOverlayTypeByGeneralItemTypeMap();
	
	private static Map<OverlayType, GeneralItemType> createGeneralItemTypeByOverlayTypeMap() {
        Map<OverlayType, GeneralItemType> result = new HashMap<OverlayType, GeneralItemType>();
        result.put(OverlayType.ERROR, GeneralItemType.GENERAL_ITEM_TYPE_ERROR);
        result.put(OverlayType.INFORMATION, GeneralItemType.GENERAL_ITEM_TYPE_INFORMATION);
        result.put(OverlayType.PEOPLE, GeneralItemType.GENERAL_ITEM_TYPE_PEOPLE);
        result.put(OverlayType.WATER, GeneralItemType.GENERAL_ITEM_TYPE_WATER);
        result.put(OverlayType.WATER_HIDE, GeneralItemType.GENERAL_ITEM_TYPE_WATER_HIDE);
        result.put(OverlayType.SLEEP, GeneralItemType.GENERAL_ITEM_TYPE_SLEEP);
        return Collections.unmodifiableMap(result);
    }
	
	private static Map<GeneralItemType, OverlayType> createOverlayTypeByGeneralItemTypeMap() {
        Map<GeneralItemType, OverlayType> result = new HashMap<GeneralItemType, OverlayType>();
        result.put(GeneralItemType.GENERAL_ITEM_TYPE_ERROR, OverlayType.ERROR);
        result.put(GeneralItemType.GENERAL_ITEM_TYPE_INFORMATION, OverlayType.INFORMATION);
        result.put(GeneralItemType.GENERAL_ITEM_TYPE_PEOPLE, OverlayType.PEOPLE);
        result.put(GeneralItemType.GENERAL_ITEM_TYPE_WATER, OverlayType.WATER);
        result.put(GeneralItemType.GENERAL_ITEM_TYPE_WATER_HIDE, OverlayType.WATER_HIDE);
        result.put(GeneralItemType.GENERAL_ITEM_TYPE_SLEEP, OverlayType.SLEEP);
        return Collections.unmodifiableMap(result);
    }
	
	private Map<OverlayType, Drawable> createDrawableByTypeMap() {
		Map<OverlayType, Drawable> map =  new Hashtable<OverlayType, Drawable>();
		Resources res = mMyApplication.getResources();
		map.put(OverlayType.ERROR, res.getDrawable(R.drawable.ic_map_warning_48));
		map.put(OverlayType.LOCATION, res.getDrawable(R.drawable.ic_my_location_32));
		map.put(OverlayType.ANGEL, res.getDrawable(R.drawable.ic_angel_32));
		map.put(OverlayType.INFORMATION, res.getDrawable(R.drawable.ic_map_marker_48));
		map.put(OverlayType.PEOPLE, res.getDrawable(R.drawable.ic_map_people_sun_32));
		map.put(OverlayType.SLEEP, res.getDrawable(R.drawable.ic_bed));
		map.put(OverlayType.WATER, res.getDrawable(R.drawable.ic_water));
		map.put(OverlayType.WATER_HIDE, res.getDrawable(R.drawable.ic_water_carrier));
		return map;
	}
	
	public OverlayManager(Context context, MapView mapView)
	{
		mContext = context;
		mMapView = mapView;
		mMyApplication = (MyApplication)context.getApplicationContext();
		mOverlayByType = new Hashtable<OverlayType, Overlay>();
		mDrawableByType = createDrawableByTypeMap() ;
	}
	
	public void createOverlay(OverlayType overlayType){
		
		boolean overlayCreated = false;
		Overlay overlay = null;
		
		// if the overlay exist, don't recreate it
		if (mOverlayByType.containsKey(overlayType)){
			return;
		}
		
		switch(overlayType){
		case SHVIL:
			overlay = createShvilOverlay();
			overlayCreated = true;
			break;
		case LISTENING:
			overlay = new ListeningOverlay((MyMapActivity) mContext);
			overlayCreated = true;
			break;
		case LOCATION:
			overlay = createLocationOverlay();
			overlayCreated = true;
			break;
		case ANGEL:
			overlay = createAngelOverlay();
			overlayCreated = true;
			break;
		case DIARY:
			// TODO
			break;
		case ERROR:
		case INFORMATION:
		case PEOPLE:
		case WATER:
		case WATER_HIDE:
		case SLEEP:
			GeneralItemType generalItemType = mGeneralItemTypeByOverlayType.get(overlayType);
			overlay = createGeneralOverlay(generalItemType);
			overlayCreated = true;
			break;
		}
		
		if (overlayCreated){
			mMapView.getOverlays().add(overlay);
			mOverlayByType.put(overlayType, overlay);
		}
	}
	
	public void removeOverlay(OverlayType overlayType){
		
		if (!mOverlayByType.containsKey(overlayType)){
			return;
		}
		
		mMapView.getOverlays().remove(mOverlayByType.get(overlayType));
		mOverlayByType.remove(overlayType);
		
	}
	
	private GeneralMapOverlay createGeneralOverlay(GeneralItemType type)
	{
		// create overlay
	    Drawable defaultMarker = mDrawableByType.get(OverlayTypeByGeneralItemType.get(type));
	    GeneralMapOverlay generalMapOverlay = new GeneralMapOverlay(defaultMarker, true, mContext);
	    
	    // insert only items of the specific type
	    GeneralItemDao dao = mMyApplication.daoSession.getGeneralItemDao();
	    List<GeneralItem> items = dao.queryBuilder().where(Properties.Type.eq(((byte)type.ordinal()))).list();
	    List<OverlayItem> overlayItems = new ArrayList<OverlayItem>(items.size());
	    for (GeneralItem item : items){
			double latitude = item.getLatitude();
			double longitude = item.getLongitude();
			GeoPoint geoPoint = new GeoPoint(latitude,longitude);
			overlayItems.add(new OverlayItem(geoPoint, item.getId().toString(), ""));
		}
	    items.clear();
	    generalMapOverlay.addItems(overlayItems);
	    return generalMapOverlay;        
	}
	
	@SuppressLint("UseValueOf")
	private ArrayWayOverlay createShvilOverlay()
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
		List<GeoPoint> geoPoints = new ArrayList<GeoPoint>(25000);
		
		ArrayWayOverlay arrayWayOverlay = new ArrayWayOverlay(paintStroke, null);
		

		InputStream fis = null;
		try {
			fis = mMyApplication.getResources().getAssets().open("ShvilBest.txt");
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
			    		wayNodes[0] = new  GeoPoint[] {};
			    		wayNodes[0] = geoPoints.toArray(wayNodes[0]);
			    		OverlayWay overlayWay = new OverlayWay(wayNodes, null, paintStroke);
			   		 	arrayWayOverlay.addWay(overlayWay);
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
	
    private AngelOverlay createAngelOverlay()
    {
    	// create a default marker for the overlay
        Drawable defaultMarker = mDrawableByType.get(OverlayType.ANGEL);

        // create an ItemizedOverlay with the default marker
        AngelOverlay angelOverlay = new AngelOverlay(defaultMarker, true, mContext);

        // get all water points
        List<Angel> angels = mMyApplication.daoSession.getAngelDao().loadAll();
        
        List<OverlayItem> overlayItems = new ArrayList<OverlayItem>(angels.size());
        
       for (Angel angel : angels){
    		double latitude = angel.getLatitude();
    		double longitude = angel.getLongitude();
    		GeoPoint geoPoint = new GeoPoint(latitude,longitude);
    		
    		// create an OverlayItem with title and description
    		overlayItems.add(new OverlayItem(geoPoint, angel.getId().toString(), ""));
    	}
        angels.clear();

        // add the OverlayItems to the ArrayItemizedOverlay
        angelOverlay.addItems(overlayItems);

        return angelOverlay;        
    }
    
    private LocationOverlay createLocationOverlay()
    {
    	// create a default marker for the overlay
        Drawable defaultMarker = mDrawableByType.get(OverlayType.LOCATION);

        // create an ItemizedOverlay with the default marker
        LocationOverlay overlay = new LocationOverlay(defaultMarker, true, mContext);

        return overlay;        
    }
}
