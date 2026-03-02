package com.shvil.project.map;

import android.app.Activity;
import android.content.Intent;
import android.graphics.Typeface;
import android.graphics.drawable.Drawable;
import android.os.Bundle;
import android.view.View;
import android.view.Window;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.shvil.project.MyApplication;
import com.shvil.project.R;
import com.shvil.project.db.Angel;
import com.shvil.project.db.AngelDao;
import com.shvil.project.db.DiaryItem;
import com.shvil.project.db.DiaryItemDao;
import com.shvil.project.db.GeneralItem;
import com.shvil.project.db.GeneralItem.GeneralItemType;
import com.shvil.project.db.GeneralItemDao;
import com.shvil.project.map.overlay.OverlayManager;
import com.shvil.project.map.overlay.OverlayType;

public class OverlayItemViewer extends Activity {
	
    // The activity incoming intent data
    public static final String INTENT_IN_DATA_OVERYLAY_VIEWER_OVERLAY_ID = "overlay_viewer_in_overlay_id";
	// the type of the wanted overlay viewer can be diary, angel or general
    public static final String INTENT_IN_DATA_OVERYLAY_VIEWER_OVERLAY_TYPE = "overlay_viewer_in_overlay_type";
    public static final int INTENT_IN_DATA_OVERYLAY_VIEWER_OVERLAY_TYPE_DIARY = 0;
    public static final int INTENT_IN_DATA_OVERYLAY_VIEWER_OVERLAY_TYPE_ANGEL = 1;
    public static final int INTENT_IN_DATA_OVERYLAY_VIEWER_OVERLAY_TYPE_GENERAL = 2;
    
    private MyApplication mApplication;
    private GeneralItemDao mGeneralItemDao;
    private DiaryItemDao mDiaryItemDao;
    private AngelDao mAngelDao;
    private long itemID;
    private int itemType;

    private GeneralItem mGeneralItem;
    private DiaryItem mDiaryItem;
    private Angel mAngelItem;


	@Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        requestWindowFeature(Window.FEATURE_LEFT_ICON);
        
        final Intent intent = getIntent();
        mApplication = (MyApplication) this.getApplication();
        
        itemType = intent.getIntExtra(INTENT_IN_DATA_OVERYLAY_VIEWER_OVERLAY_TYPE, 4);
        itemID = intent.getLongExtra(INTENT_IN_DATA_OVERYLAY_VIEWER_OVERLAY_ID, 0);

        create_layout(itemType);
        
	}
	
	private void create_layout(int type)
	{
		switch (type){
		case INTENT_IN_DATA_OVERYLAY_VIEWER_OVERLAY_TYPE_DIARY:
			create_diary_item_layout();
			break;
		case INTENT_IN_DATA_OVERYLAY_VIEWER_OVERLAY_TYPE_ANGEL:
			create_angel_item_layout();
			break;
		case INTENT_IN_DATA_OVERYLAY_VIEWER_OVERLAY_TYPE_GENERAL:
			create_general_item_layout();
			break;
		default:
			break;
		}
	}
	
	private void create_general_item_layout()
	{
		// get drawable of item
        mGeneralItemDao = mApplication.daoSession.getGeneralItemDao();
		mGeneralItem = mGeneralItemDao.load(itemID);
        GeneralItemType generalItemType = GeneralItemType.values()[mGeneralItem.getType()];
        OverlayManager overlayManager = mApplication.mapFragment.mOverlayManager;
        OverlayType overlayType = OverlayManager.OverlayTypeByGeneralItemType.get(generalItemType);
	    Drawable drawable = overlayManager.mDrawableByType.get(overlayType);
	    
	    // set layout
	    setContentView(R.layout.overlay_item_viewer);
	    LinearLayout layout = (LinearLayout) findViewById(R.id.overlay_item_viewer_linear_layout);	    
	    TextView textView = new TextView(this);
	    String text = mGeneralItem.getText();
	    if (text != null) {
	    	textView.setText(text);
	    	textView.setTextAppearance(this, android.R.attr.textAppearanceMedium);
	    }
	    layout.addView(textView);
	    
	    getWindow().setFeatureDrawable(Window.FEATURE_LEFT_ICON, drawable);
        setTitle(mGeneralItem.getTitle());
	}
	
	private void create_diary_item_layout()
	{
        mDiaryItemDao = mApplication.daoSession.getDiaryItemDao();
		mDiaryItem = mDiaryItemDao.load(itemID);
		
	    // set layout
	    setContentView(R.layout.overlay_item_viewer);
	    LinearLayout layout = (LinearLayout) findViewById(R.id.overlay_item_viewer_linear_layout);	    
	    TextView textView = new TextView(this);
	    String text = mDiaryItem.getText();
	    if (text != null) {
	    	textView.setText(text);
	    }
	    layout.addView(textView);
	    
	    int drawable = 0;
	    switch (mDiaryItem.getType()){
	    case DiaryItem.TYPE_NOTE:
	    	drawable = R.drawable.ic_angel_32;
	    	break;
	    case DiaryItem.TYPE_PIC:
	    	drawable = R.drawable.ic_angel_32;
	    	break;
	    case DiaryItem.TYPE_SOUND:
	    	drawable = R.drawable.ic_angel_32;
	    	break;
	    }
	    getWindow().setFeatureDrawableResource(Window.FEATURE_LEFT_ICON, drawable);
	    
        setTitle("Trail Angel");
	}
	
	private void create_angel_item_layout()
	{
		mAngelDao = mApplication.daoSession.getAngelDao();
		mAngelItem = mAngelDao.load(itemID);
		
	    // set layout
	    setContentView(R.layout.overlay_item_viewer);
	    LinearLayout layout = (LinearLayout) findViewById(R.id.overlay_item_viewer_linear_layout);	    
	    TextView textView = new TextView(this);
	    String text = mAngelItem.getName();
	    if (text != null) {
	    	textView.setText(text);
	    }
	    layout.addView(textView);
	    
	    getWindow().setFeatureDrawableResource(Window.FEATURE_LEFT_ICON, R.drawable.ic_angel_32);
        setTitle("Trail Angel");
	}
	
	public void dislike(View v){
		if (mGeneralItem == null){
			return;
		}
		
		byte counter = mGeneralItem.getCounter();
		counter = -1;
		mGeneralItem.setCounter(counter);
		
	    TextView dislike_view = (TextView) findViewById(R.id.overlay_item_viewe_dislike);	    
	    TextView like_view = (TextView) findViewById(R.id.overlay_item_viewe_like);	    
	    dislike_view .setTypeface(null, Typeface.BOLD);
	    dislike_view.setTextColor(0xFF66CCFF);

	    like_view.setTypeface(null, Typeface.NORMAL);
	    like_view.setTextColor(0xFF003050);
	}
	
	public void like(View v){
		if (mGeneralItem == null){
			return;
		}
		
		byte counter = mGeneralItem.getCounter();
		counter = +1;
		mGeneralItem.setCounter(counter);
		
	    TextView dislike_view = (TextView) findViewById(R.id.overlay_item_viewe_dislike);	    
	    TextView like_view = (TextView) findViewById(R.id.overlay_item_viewe_like);	    
	    dislike_view .setTypeface(null, Typeface.NORMAL);
	    dislike_view.setTextColor(0xFF003050);

	    like_view .setTypeface(null, Typeface.BOLD);
	    like_view.setTextColor(0xFF66CCFF);

	}
	
	
	/*
	@SuppressWarnings("unused")
	private void deletePostDialog(View v){
		AlertDialog.Builder builder = new AlertDialog.Builder(this);

		builder.setMessage("Delete post?");
		
		builder.setPositiveButton("OK", new DialogInterface.OnClickListener() {
		           public void onClick(DialogInterface dialog, int id) {
		        	   switch (itemType){
			       		case INTENT_IN_DATA_OVERYLAY_VIEWER_OVERLAY_TYPE_DIARY:
			       			deleteOverlayItemDiary();
			       			break;
			       		case INTENT_IN_DATA_OVERYLAY_VIEWER_OVERLAY_TYPE_ANGEL:
			       			deleteOverlayItemAngel();
			       			break;
			       		case INTENT_IN_DATA_OVERYLAY_VIEWER_OVERLAY_TYPE_GENERAL:
			       			deleteOverlayItemGeneral();
			       			break;
			       		default:
			       			break;
			       		}
		           }
		       });
		builder.setNegativeButton("Cancel", new DialogInterface.OnClickListener() {
		           public void onClick(DialogInterface dialog, int id) {
		           }
		       });

		// 3. Get the AlertDialog from create()
		AlertDialog dialog = builder.create();
		
		dialog.show();
	}
	
	private void deleteOverlayItemAngel()
	{
		// remove from db
		mAngelDao.deleteByKey(mAngelItem.getId());
	    
	    // TODO - finish and return in intent that need to recreate ovelay 
	}
	
	private void deleteOverlayItemGeneral()
	{
		// remove from db
		mGeneralItemDao.deleteByKey(mGeneralItem.getId());
	    
	    // TODO - finish and return in intent that need to recreate ovelay 
	}
	
	private void deleteOverlayItemDiary()
	{
		// remove file
		if ((mDiaryItem.getType() == DiaryItem.TYPE_PIC) || (mDiaryItem.getType() == DiaryItem.TYPE_SOUND)){
			File file = new File(mDiaryItem.getTitleuri());
			file.delete();
		}
		
		// remove from db
		mDiaryItemDao.deleteByKey(mDiaryItem.getId());
	    // TODO - finish and return in intent that need to recreate ovelay 
	}
	*/
}
