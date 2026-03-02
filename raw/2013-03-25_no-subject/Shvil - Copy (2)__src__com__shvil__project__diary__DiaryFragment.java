package com.shvil.project.diary;


import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Iterator;
import java.util.List;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.app.AlertDialog;
import android.content.Intent;
import android.database.Cursor;
import android.media.MediaScannerConnection;
import android.net.Uri;
import android.os.Bundle;
import android.os.Environment;
import android.provider.MediaStore;
import android.support.v4.app.ListFragment;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.View.OnClickListener;
import android.widget.TextView;
import android.widget.Toast;

import com.shvil.project.MyApplication;
import com.shvil.project.R;
import com.shvil.project.db.DiaryItem;
import com.shvil.project.db.DiaryItemDao;

public class DiaryFragment extends ListFragment {

	private DiaryFragmentAdapter adapter;
	private MyApplication mApplication;
	DiaryItemDao dao;
	public final static int INTENT_CAPTURE_PICTURE = 1;
	public final static int INTENT_EDIT_DIARY_ITEM = 2;
	public final static int INTENT_INSERT_DIARY_ITEM = 3;
	public final static int INTENT_CAPTURE_SOUND = 4;
	private boolean mExternalStorageWriteable = false;
	private String cameraFilePath = "";

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
            Bundle savedInstanceState) {
    	
    	if (container == null) {
            // We have different layouts, and in one of them this
            // fragment's containing frame doesn't exist.  The fragment
            // may still be created from its saved state, but there is
            // no reason to try to create its view hierarchy because it
            // won't be displayed.  Note this is not needed -- we could
            // just run the code below, where we would create and return
            // the view hierarchy; it would just never be used.
            return null;
        }
    	
    	mApplication = (MyApplication)getActivity().getApplicationContext();
    	dao = mApplication.daoSession.getDiaryItemDao();
    	
    	// check external storage
    	mExternalStorageWriteable = checkExternalStorageAvailability();
    	
    	// update list with items
	    List<DiaryItem> diaryItems = mApplication.daoSession.getDiaryItemDao().loadAll();
	    List<DiaryItem> validDiaryItems = validateDiaryItems(diaryItems);
	    adapter = new DiaryFragmentAdapter((Activity) inflater.getContext(), validDiaryItems);
	    setListAdapter(adapter);
	    
    	// Inflate the layout for this fragment
	    View rootview =  inflater.inflate(R.layout.diary_fragment, container, false);
	   
	    if (!mExternalStorageWriteable){
    		// add warning
	    	 TextView sd_alarm = (TextView)rootview.findViewById(R.id.diary_fragment_sdcard_problem);
	    	 sd_alarm.setVisibility(View.VISIBLE);	
    	}
	    
	    // handle clicks events
        rootview.findViewById(R.id.writeButton).setOnClickListener(clickListener);
        rootview.findViewById(R.id.cameraButton).setOnClickListener(clickListener);
        rootview.findViewById(R.id.recordButton).setOnClickListener(clickListener);
        rootview.findViewById(R.id.diary_fragment_sdcard_problem).setOnClickListener(clickListener);
	    
	    return rootview;
    }
    
    // remove any unexisting files from list
    private List<DiaryItem> validateDiaryItems(List<DiaryItem> diaryItems)
    {
    	File file;
		Iterator<DiaryItem> i = diaryItems.iterator();
		while (i.hasNext()) {
			DiaryItem diaryItem = i.next(); // must be called before you can call i.remove()
			
			switch (diaryItem.getType()){
			case DiaryItem.TYPE_PIC:
				file = new File(diaryItem.getTitleuri());
				if (!file.exists()){
					i.remove();
				}
				break;
			case DiaryItem.TYPE_SOUND:
				file = new File(diaryItem.getTitleuri());
				if (!file.exists()){
					i.remove();
				}
				break;
			}
		   
		}
		return diaryItems;
    }
    
    @Override
    public void onActivityCreated(Bundle savedInstanceState) {
        super.onActivityCreated(savedInstanceState);
        
        if (savedInstanceState != null) {
            // Restore last state for checked position.
        	cameraFilePath = savedInstanceState.getString("cameraFilePath");
        }
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putString("cameraFilePath", cameraFilePath);
    }
    
    ////// STORAGE ///////////////////////////////////////////////////////////////////////////
    
    private boolean checkExternalStorageAvailability()
    {
    	boolean ExternalStorageAvailable = false;
    	boolean ExternalStorageWriteable = false;
    	String state = Environment.getExternalStorageState();

    	if (Environment.MEDIA_MOUNTED.equals(state)) {
    	    // We can read and write the media
    	    ExternalStorageAvailable = ExternalStorageWriteable = true;
    	} else if (Environment.MEDIA_MOUNTED_READ_ONLY.equals(state)) {
    	    // We can only read the media
    	    ExternalStorageAvailable = true;
    	    ExternalStorageWriteable = false;
    	} else {
    	    // Something else is wrong. It may be one of many other states, but all we need
    	    //  to know is we can neither read nor write
    	    ExternalStorageAvailable = ExternalStorageWriteable = false;
    	}
		return (ExternalStorageAvailable && ExternalStorageWriteable);
    }
    
    private void showExternalStorageErrorDialog()
    {
    	AlertDialog.Builder builder = new AlertDialog.Builder(this.getActivity());
		builder.setIcon(android.R.drawable.ic_dialog_alert);
		builder.setTitle("External memory error");
		builder.setMessage("The application doesn't recognize an SD card,\n" +
							"So we can't save pictures and sound files :(");
		builder.setPositiveButton("OK", null);
		builder.show();
    }
    
    private File getMediaDirPath(String pubicDirectoryType){
    	
    	// get sound files directory
		File mediaStorageDir = Environment.getExternalStoragePublicDirectory(pubicDirectoryType);

	    // Create the storage directory if it does not exist
	    if (! mediaStorageDir.exists()){
	        if (! mediaStorageDir.mkdirs()){
	            Log.d("Shvil", "failed to create directory");
	            return null;
	        }
	    }
	    return mediaStorageDir;
    }
    
    @SuppressLint("SimpleDateFormat")
	private String getOutputTimeFile(File directoryPath, String extension)
    {
	    String timeStamp = new SimpleDateFormat("yyyyMMdd_HHmmss").format(new Date());
	    String FilePath = directoryPath.getAbsolutePath() + File.separator + "Shvil" + timeStamp + "." + extension;
	    return FilePath;
    }
    
    private void notifyMediaScanner(String newFilePath)
	{
		// Tell the media scanner about the new file so that it is
        // immediately available to the user.
        MediaScannerConnection.scanFile(this.getActivity(),
                new String[] { newFilePath }, null,
                new MediaScannerConnection.OnScanCompletedListener() {
            public void onScanCompleted(String path, Uri uri) {
                Log.i("ExternalStorage", "Scanned " + path + ":");
                Log.i("ExternalStorage", "-> uri=" + uri);
            }
        });
	}
        
    private void moveFile(String fromPath, String ToPath) throws IOException {
        
    	// copy
    	InputStream is = new FileInputStream(fromPath);
        OutputStream os = new FileOutputStream(ToPath);
        byte[] data = new byte[is.available()];
        is.read(data);
        os.write(data);
        is.close();
        os.close();
        
        // remove original
        (new File(fromPath)).delete();
     }
 
    ////// CAMERA //////////////////////////////////////////////////////////////////
    
    private void startCamera() {
    	// TODO
    	// add new file
		Toast.makeText(this.getActivity(), "Sorry, this feature is not available", Toast.LENGTH_LONG).show();
		return;
		
		/*
    	if (!mExternalStorageWriteable){
    		showExternalStorageErrorDialog();
    		return;	
    	}
    	
    	File MediaDirPath = getMediaDirPath(Environment.DIRECTORY_PICTURES);
    	cameraFilePath = getOutputTimeFile(MediaDirPath, "jpg");
	    File file = new File(cameraFilePath);

    	Intent intent = new Intent(MediaStore.ACTION_IMAGE_CAPTURE);
    	intent.putExtra(MediaStore.EXTRA_OUTPUT, Uri.fromFile(file));
    	startActivityForResult(intent, INTENT_CAPTURE_PICTURE);
    	*/
	}
    
    private void onActivityResultCamera(int requestCode, int resultCode, Intent data)
    {
    	if (resultCode != Activity.RESULT_OK){
    		cameraFilePath = "";
    		return;
    	}
    	
    	try {
    		addPictureFileToDbAndUpateGUI(cameraFilePath);
    		notifyMediaScanner(cameraFilePath);
    	
    		// add new file
    		Toast.makeText(this.getActivity(), "Image saved to:\n" +
    			cameraFilePath, Toast.LENGTH_LONG).show();
    	}
    	catch(Exception e) { 
			Toast.makeText(this.getActivity().getApplicationContext(),
        		    "Error saving picture",
        		    Toast.LENGTH_LONG).show();
			
	    }
    	cameraFilePath = "";
    }
    
	private void addPictureFileToDbAndUpateGUI(String filePath){
		
    	Date time = new Date();
        // TODO - get current location
    	DiaryItem item = new DiaryItem(null, DiaryItem.TYPE_PIC, filePath, "Picture", time, 32.0, 34.0);
    	long itemID = dao.insert(item);
    	boolean is_new = true;
    	
    	adapter.updateDiaryItem(is_new, itemID, 0);
		upateListView(is_new, 0);
	}
    
    ////// NOTES //////////////////////////////////////////////////////////////////
	
	private void startEditNewNote()
	{
		Intent intent = new Intent(mApplication, NoteEditor.class);
		intent.setAction(Intent.ACTION_INSERT);
		startActivityForResult(intent, INTENT_INSERT_DIARY_ITEM);
	}
	
	private void onActivityResultNote(int requestCode, int resultCode, Intent data)
    {
		// can return from edit note ot inserting a new one
		
		if (resultCode != Activity.RESULT_OK){
    		return;
    	} 
		
		boolean is_new = data.getBooleanExtra(NoteEditor.INTENT_DATA_IS_NEW, true);
		long diaryItemID = data.getLongExtra(NoteEditor.INTENT_DATA_NOTE_ID, 0);
		int position = data.getIntExtra(NoteEditor.INTENT_DATA_POSITION, 0);
		
		adapter.updateDiaryItem(is_new, diaryItemID, position);
		upateListView(is_new, position);
		
    }
	
	private void upateListView(boolean is_new, int position){
		// Sets the position to the last changed item
		if (this.getListView().getCount() > 0)
		{
			if (is_new == true)
			{
				this.getListView().setSelection(adapter.getCount() - 1);
			}
			else
			{
				this.getListView().setSelection(position);
			}
		}
	}
	
	////// SOUND //////////////////////////////////////////////////////////////////
	
	private void startSoundRecording()
	{
		if (!mExternalStorageWriteable){
    		showExternalStorageErrorDialog();
    		return;	
    	}
		
		Intent intent = new Intent(MediaStore.Audio.Media.RECORD_SOUND_ACTION);
		startActivityForResult(intent, INTENT_CAPTURE_SOUND);
	}
	
	private void onActivityResultSound(int requestCode, int resultCode, Intent data)
    {
		if (resultCode != Activity.RESULT_OK){
			return;
		}
		else if (data == null){
			return;
		}
		
		try {
			// copy file to extrnal storage
			Uri savedFile = data.getData();
			String savedFilePath = getAudioFilePathFromUri(savedFile);
		    File MediaDirPath = getMediaDirPath(Environment.DIRECTORY_PODCASTS);
		    String newFilePath = getOutputTimeFile(MediaDirPath, "mp3");
		    moveFile(savedFilePath, newFilePath);
		    notifyMediaScanner(newFilePath);
		    
		    // Add file to DB and list adapter
		    addSoundFileToDbandUpateGUI(newFilePath);
        }
		catch(Exception e) { 
			Toast.makeText(this.getActivity().getApplicationContext(),
        		    "Error saving sound file",
        		    Toast.LENGTH_LONG).show();
	    }
    }
	
	private void addSoundFileToDbandUpateGUI(String SoundFilePath){
				
    	Date time = new Date();
        // TODO - get current location
    	DiaryItem soundItem = new DiaryItem(null, DiaryItem.TYPE_SOUND, SoundFilePath, "Recording", time, 32.0, 34.0);
    	long itemID = dao.insert(soundItem);
    	boolean is_new = true;
    	
    	adapter.updateDiaryItem(is_new, itemID, 0);
		upateListView(is_new, 0);
	}
	
	private String getAudioFilePathFromUri(Uri uri) {
	      Cursor cursor = this.getActivity().getContentResolver()
	            .query(uri, null, null, null, null);
	      cursor.moveToFirst();
	      int index = cursor.getColumnIndex(MediaStore.Audio.AudioColumns.DATA);
	      String path =  cursor.getString(index);
	      cursor.close();
	      return path;
	   }
    
    ////// BUTTONS ACTIONS //////////////////////////////////////////////////////////////////

	@Override
	public void onActivityResult(int requestCode, int resultCode, Intent data)
	{
		switch(requestCode){
		case INTENT_CAPTURE_PICTURE:
			onActivityResultCamera(requestCode, resultCode, data);
			break;
		case INTENT_EDIT_DIARY_ITEM:
			onActivityResultNote(requestCode, resultCode, data);
			break;
		case INTENT_INSERT_DIARY_ITEM:
			onActivityResultNote(requestCode, resultCode, data);
			break;
		case INTENT_CAPTURE_SOUND:
			onActivityResultSound(requestCode, resultCode, data);
			break;
		default:
			return;
		}
	}

	//////ON CLICKS ////////////////////////////////////////////////////////////
	 OnClickListener clickListener = new OnClickListener() {
	     public void onClick(final View v) {
	    	 switch(v.getId()){
		 		case R.id.writeButton:
		 			startEditNewNote();
		 	        break;
		 		case R.id.cameraButton:
		 			startCamera();
		 			break;
		 		case R.id.recordButton:
		 			startSoundRecording();
		 			break;
		 		case R.id.diary_fragment_sdcard_problem:
		 			showExternalStorageErrorDialog();
		 			break;
		 		default:
		 			return;
	 		}
	     }
	 };
    
}