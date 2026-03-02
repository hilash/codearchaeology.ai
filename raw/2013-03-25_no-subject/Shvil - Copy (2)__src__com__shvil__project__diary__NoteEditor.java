/**
 * 
 */
package com.shvil.project.diary;

import android.annotation.SuppressLint;
import android.app.ActionBar;
import android.app.Activity;
import android.content.Intent;
import android.os.Build;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.widget.EditText;

import com.shvil.project.MyApplication;
import com.shvil.project.R;
import com.shvil.project.db.DiaryItem;
import com.shvil.project.db.DiaryItemDao;

/**
 * @author Purple Fire
 *
 */
public class NoteEditor extends Activity {

	// This Activity can be started by more than one action. Each action is represented
    // as a "state" constant
    private static final int STATE_EDIT = 0;
    private static final int STATE_INSERT = 1;
    
    // The Activity return Intent data
    public static final String INTENT_DATA_IS_NEW = "is_new";
    public static final String INTENT_DATA_NOTE_ID = "note_id";
    public static final String INTENT_DATA_POSITION = "position";
    
    private MyApplication mApplication;
    private int mState;
    private long mNoteID;
    private long mNotePosition;
    private DiaryItem mNote;
    private EditText mNoteTitle;
    private EditText mNoteText;
    private DiaryItemDao dao;
    
    
    @SuppressLint("NewApi")
	@Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        ActionBar actionBar = getActionBar();
        actionBar.setDisplayHomeAsUpEnabled(true);
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.ICE_CREAM_SANDWICH) {
        	actionBar.setHomeButtonEnabled(true);
		}

        final Intent intent = getIntent();
        final String action = intent.getAction();
        mApplication = (MyApplication) this.getApplication();
        dao = mApplication.daoSession.getDiaryItemDao();
        
        if (Intent.ACTION_EDIT.equals(action)) {
        	
            mState = STATE_EDIT;
            mNoteID = intent.getLongExtra("DiaryItemID", 0);
            mNotePosition = intent.getLongExtra("DiaryItemPosition", 0);
            
            
    	    mNote = dao.load(mNoteID); 

        } else if (Intent.ACTION_INSERT.equals(action)) {

            mState = STATE_INSERT;
            
            java.util.Date time = new java.util.Date();
            // TODO - get current location
            mNote = new DiaryItem(null, DiaryItem.TYPE_NOTE, "", "", time, 32.0, 34.0);

        } else {

            // Logs an error that the action was not understood, finishes the Activity, and
            // returns RESULT_CANCELED to an originating Activity.
            Log.e("NoteEditor", "Unknown action, exiting");
            finish();
            return;
        }

        setContentView(R.layout.note_editor);
        mNoteTitle = (EditText) findViewById(R.id.editNoteTitle);
        mNoteText = (EditText) findViewById(R.id.editNoteText);
        
        mNoteTitle.setText(mNote.getTitleuri());
        mNoteText.setText(mNote.getText());
    }
    
    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
    	super.onCreateOptionsMenu(menu);
        MenuInflater inflater = getMenuInflater();
        inflater.inflate(R.menu.note_editor_menu, menu);
        return true;
    }
    
    private void saveNote() {
    	mNote.setTitleuri(mNoteTitle.getText().toString());
    	mNote.setText(mNoteText.getText().toString());
    	mNoteID = dao.insertOrReplace(mNote);
  	
    	Intent resultIntent = new Intent();
  
    	resultIntent.putExtra(INTENT_DATA_IS_NEW, (mState == STATE_INSERT)?true:false);
    	resultIntent.putExtra(INTENT_DATA_NOTE_ID, mNoteID);
    	resultIntent.putExtra(INTENT_DATA_POSITION, mNotePosition);

    	setResult(Activity.RESULT_OK, resultIntent);
    	finish();
    }
    
    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle item selection
        switch (item.getItemId()) {
            case R.id.note_editor_menu_save:
            	saveNote();
                return true;
            case android.R.id.home:
                finish();
                return true;
            default:
                return super.onOptionsItemSelected(item);
        }
    }
}
