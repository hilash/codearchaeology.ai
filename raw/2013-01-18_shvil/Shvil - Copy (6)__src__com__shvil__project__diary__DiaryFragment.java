package com.shvil.project.diary;


import java.io.File;
import java.util.List;

import android.app.Activity;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.os.Environment;
import android.provider.MediaStore;
import android.support.v4.app.ListFragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Toast;

import com.shvil.project.MyApplication;
import com.shvil.project.R;
import com.shvil.project.db.DiaryItem;

public class DiaryFragment extends ListFragment {

	private DiaryFragmentAdapter adapter;
	private MyApplication mApplication;
	public static int TAKE_PICTURE = 1;
	public static int EDIT_DIARY_ITEM = 2;
	public static int INSERT_DIARY_ITEM = 3;
	private Uri outputFileUri;
	final static int RQS_RECORDING = 1;
	Uri savedUri;

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
	    List<DiaryItem> diaryItems = mApplication.daoSession.getDiaryItemDao().loadAll();    
	    
	    adapter = new DiaryFragmentAdapter((Activity) inflater.getContext(), diaryItems);
	    setListAdapter(adapter);
	    
    	// Inflate the layout for this fragment
	    return inflater.inflate(R.layout.diary_fragment, container, false);
    }
    
    public void startCamera(View v) {
    	Intent cameraIntent = new Intent(android.provider.MediaStore.ACTION_IMAGE_CAPTURE);  
        startActivityForResult(cameraIntent, 1337);
    }
    
    private void saveFullImage() {
    	Intent intent = new Intent(MediaStore.ACTION_IMAGE_CAPTURE);
    	File file = new File(Environment.getExternalStorageDirectory(), "test.jpg");
    	outputFileUri = Uri.fromFile(file);
    	intent.putExtra(MediaStore.EXTRA_OUTPUT, outputFileUri);
    	startActivityForResult(intent, TAKE_PICTURE);
	}

	@Override
	public void onActivityResult(int requestCode, int resultCode, Intent data) {
		
		if ((requestCode == INSERT_DIARY_ITEM) || (requestCode == EDIT_DIARY_ITEM))
		{
			boolean is_new = data.getBooleanExtra(NoteEditor.INTENT_DATA_IS_NEW, true);
			long diaryItemID = data.getLongExtra(NoteEditor.INTENT_DATA_NOTE_ID, 0);
			int position = data.getIntExtra(NoteEditor.INTENT_DATA_POSITION, 0);
			
			adapter.updateNote(is_new, diaryItemID, position);
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
		else if ((requestCode == TAKE_PICTURE) && (resultCode == Activity.RESULT_OK))
		{
			// Check if the result includes a thumbnail Bitmap
			if (data == null)
			{    
				// TODO Do something with the full image stored
				// in outputFileUri. Perhaps copying it to the app folder
			}
		}
		 // TODO Auto-generated method stub
		if (requestCode == RQS_RECORDING)
		{
		  savedUri = data.getData();
		  Toast.makeText(this.getActivity().getApplicationContext(),
		    "Saved: " + savedUri.getPath(),
		    Toast.LENGTH_LONG).show();
		 }
	}
	


	
	private void startEditNewNote()
	{
		Intent intent = new Intent(mApplication, NoteEditor.class);
		//intent.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP | Intent.FLAG_ACTIVITY_NEW_TASK);
		intent.setAction(Intent.ACTION_INSERT);
		startActivityForResult(intent, INSERT_DIARY_ITEM);
	}
	
	private void startSoundRecording()
	{
		Intent intent = new Intent(MediaStore.Audio.Media.RECORD_SOUND_ACTION);
		startActivityForResult(intent, RQS_RECORDING);
	}
    
	public void myClickMethod(View v) {
		switch(v.getId()){
		case R.id.writeButton:
			startEditNewNote();
	        break;
		case R.id.cameraButton:
			saveFullImage();
			break;
		case R.id.recordButton:
			startSoundRecording();
			break;
		}
	}    
    
}