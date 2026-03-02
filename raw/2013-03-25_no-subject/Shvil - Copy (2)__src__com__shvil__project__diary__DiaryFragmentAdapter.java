package com.shvil.project.diary;

import java.io.File;
import java.text.SimpleDateFormat;
import java.util.List;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.media.MediaPlayer;
import android.media.MediaPlayer.OnCompletionListener;
import android.net.Uri;
import android.view.LayoutInflater;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.PopupMenu;
import android.widget.TextView;

import com.shvil.project.MyApplication;
import com.shvil.project.R;
import com.shvil.project.db.DiaryItem;
import com.shvil.project.db.DiaryItemDao;

public class DiaryFragmentAdapter extends ArrayAdapter<DiaryItem> implements OnClickListener {
	
	private final Activity context;
	private final List<DiaryItem> diaryItems;
	private final MyApplication mApplication;
	private final DiaryItemDao dao;
	private int dialog_position;
	


	static class ViewHolder {
		public TextView date;
		public TextView title;
		public ImageView picture;
	    public TextView text;
	    public ImageButton deleteButton;
	    public ImageButton editButton;
	    public ImageButton shareButton;
	    public ImageButton mapButton;
	    public ImageButton PlayButton;
	    public ImageButton PauseButton;
	    public LinearLayout Player;
	  }

	public DiaryFragmentAdapter(Activity context, List<DiaryItem> values) {
		super(context, R.layout.diary_item_layout, values);
		this.context = context;
		this.diaryItems = values;
	    this.mApplication = (MyApplication)context.getApplicationContext();
	    this.dao = mApplication.daoSession.getDiaryItemDao();
	}
	
	@SuppressLint("SimpleDateFormat")
	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
		View rowView = convertView;
	    if (rowView == null) {
	      LayoutInflater inflater = context.getLayoutInflater();
	      rowView = inflater.inflate(R.layout.diary_item_layout, null);
	      ViewHolder viewHolder = new ViewHolder();
	      viewHolder.date = (TextView) rowView.findViewById(R.id.diary_date);
	      viewHolder.title = (TextView) rowView.findViewById(R.id.diary_title);
	      viewHolder.picture = (ImageView) rowView.findViewById(R.id.diary_picture);
	      viewHolder.text = (TextView) rowView.findViewById(R.id.diary_text);
	      viewHolder.deleteButton = (ImageButton)rowView.findViewById(R.id.diary_delete_button);
	      viewHolder.editButton = (ImageButton)rowView.findViewById(R.id.diary_edit_button);
	      viewHolder.shareButton = (ImageButton)rowView.findViewById(R.id.diary_share_button);
	      viewHolder.mapButton = (ImageButton)rowView.findViewById(R.id.diary_map_button);
	      viewHolder.PlayButton = (ImageButton)rowView.findViewById(R.id.diary_item_player_play);
	      viewHolder.PauseButton = (ImageButton)rowView.findViewById(R.id.diary_item_player_pause);
	      viewHolder.Player = (LinearLayout)rowView.findViewById(R.id.diary_item_player);
	      rowView.setTag(viewHolder);
	    }

	    ViewHolder holder = (ViewHolder) rowView.getTag();
	    DiaryItem item = this.getItem(position);
	    
	    // general
	    SimpleDateFormat sdf = new SimpleDateFormat("d.M.yy HH:mm");
	    String date = sdf.format(item.getDate());
	    holder.date.setText(date);
	    holder.text.setText(item.getText());
	    if ((item.getLatitude() == null) || (item.getLongitude() == null)){
	    	holder.mapButton.setVisibility(View.GONE);
	    }
	    else {
	    	holder.mapButton.setVisibility(View.VISIBLE);
	    }
	    
	    // assign values to all view elements..
	    holder.deleteButton.setTag(position);
	    holder.editButton.setTag(position);
	    holder.shareButton.setTag(position);
	    holder.mapButton.setTag(position);
	    holder.PlayButton.setTag(position);
	    holder.PauseButton.setTag(position);
	    holder.deleteButton.setOnClickListener(this);
	    holder.editButton.setOnClickListener(this);
	    holder.shareButton.setOnClickListener(this);
	    holder.mapButton.setOnClickListener(this);
	    holder.PlayButton.setOnClickListener(this);
	    holder.PauseButton.setOnClickListener(this);
	  
	    
	    // specific for item type (picture, note, voice)
	    // TODO -> add consts
	    byte item_type = item.getType();
	    switch (item_type) {
        	case DiaryItem.TYPE_PIC:
        		// Picture
        		holder.picture.setImageURI(Uri.fromFile(new File(item.getTitleuri())));
        		holder.picture.setVisibility(View.VISIBLE);
        		holder.title.setVisibility(View.GONE);
        		holder.Player.setVisibility(View.GONE);
        		break;
        	case DiaryItem.TYPE_NOTE:
        		// Note
        		holder.picture.setVisibility(View.GONE);
        		holder.title.setText(item.getTitleuri());
        		holder.Player.setVisibility(View.GONE);
        		break;
        	case DiaryItem.TYPE_SOUND:
        		//Voice
        		holder.picture.setVisibility(View.GONE);
        		holder.title.setVisibility(View.GONE);
        		holder.Player.setVisibility(View.VISIBLE);
        		break;
        	default:
        		break;
        }
	    return rowView;
	}
	
	Bitmap getPreview(String filePath) {
	    File image = new File(filePath);

	    BitmapFactory.Options bounds = new BitmapFactory.Options();
	    bounds.inJustDecodeBounds = true;
	    BitmapFactory.decodeFile(image.getPath(), bounds);
	    if ((bounds.outWidth == -1) || (bounds.outHeight == -1))
	        return null;

	    int originalSize = (bounds.outHeight > bounds.outWidth) ? bounds.outHeight
	            : bounds.outWidth;

	    BitmapFactory.Options opts = new BitmapFactory.Options();
	    int THUMBNAIL_SIZE = 5;
		opts.inSampleSize = originalSize / THUMBNAIL_SIZE ;
	    return BitmapFactory.decodeFile(image.getPath(), opts);     
	}
	
	/* delete from gui and db */
	
	
	private void deletePost()
	{
		DiaryItem item = this.getItem(dialog_position);
		
		// remove file
		if ((item.getType() == DiaryItem.TYPE_PIC) || (item.getType() == DiaryItem.TYPE_SOUND)){
			File file = new File(item.getTitleuri());
			file.delete();
		}
		
		// remove from db
	    dao.deleteByKey(item.getId());
	    
	    // delete from list, update gui
	    this.remove(item);
	    //this.diaryItems.remove(position);
	    this.notifyDataSetChanged();    
	}
	
	private void deletePostDialog(int position){
		dialog_position = position;
		// 1. Instantiate an AlertDialog.Builder with its constructor
		AlertDialog.Builder builder = new AlertDialog.Builder(this.context);

		// 2. Chain together various setter methods to set the dialog characteristics
		//R.string.dialog_message R.string.dialog_title
		builder.setMessage("Delete post?");
		
		
		// Add the buttons
		//R.string.ok
		builder.setPositiveButton("OK", new DialogInterface.OnClickListener() {
		           public void onClick(DialogInterface dialog, int id) {
		               // User clicked OK button
		        	   deletePost();
		           }
		       });
		//R.string.cancel
		builder.setNegativeButton("Cancel", new DialogInterface.OnClickListener() {
		           public void onClick(DialogInterface dialog, int id) {
		               // User cancelled the dialog
		           }
		       });
		// Set other dialog properties

		// 3. Get the AlertDialog from create()
		AlertDialog dialog = builder.create();
		
		dialog.show();
	}
	
	private void startEditNote(int position)
	{
		Intent intent = new Intent(context, NoteEditor.class);
		intent.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP | Intent.FLAG_ACTIVITY_NEW_TASK);
		intent.setAction(Intent.ACTION_EDIT);
		long id = this.getItem(position).getId();
		intent.putExtra("DiaryItemID", id);
		intent.putExtra("DiaryItemPosition", position);
		context.startActivityForResult(intent, DiaryFragment.INTENT_EDIT_DIARY_ITEM);
	}
	
	public void updateDiaryItem(boolean is_new, long diaryItemID, int position)
	{
		// after the DiaryItem is in DB),
		// call this method to update the adapter and the gui
		
		DiaryItem item = dao.load(diaryItemID);
		
		if (is_new == true){
			this.add(item);
		}
		else {
			this.diaryItems.set(position, item);
			this.remove(item);
			if (position != 0){
				this.insert(item, position);
			} 
			else {
				this.add(item);
			}
		}
		this.notifyDataSetChanged();
	}
	
	private PopupMenu.OnMenuItemClickListener sharePopupListener = new PopupMenu.OnMenuItemClickListener() {
        public boolean onMenuItemClick(MenuItem item) {
        	switch (item.getItemId()) {
        	case R.id.share_menu_facebook:
        		return true;  		
	        default:
	            return false;
        	}
        }
	};
	
	@SuppressLint("NewApi")
	public void showPopupShareMenu(View v) {
	    PopupMenu popup = new PopupMenu(this.context, v);
	    popup.setOnMenuItemClickListener(sharePopupListener);
	    popup.inflate(R.menu.share_post);
	    popup.show();
	}
	
	public void PlaySound(int position){
		try {
		DiaryItem diaryItem = this.getItem(dialog_position);
		File file = new File(diaryItem.getTitleuri());
		MediaPlayer mediaPlayer = MediaPlayer.create(this.getContext(), Uri.fromFile(file));
		mediaPlayer.setOnCompletionListener(new OnCompletionListener() {
	        public void onCompletion(MediaPlayer playSuccess) {
	            playSuccess.release();
	        }
	    });
		mediaPlayer.start();
		}
		catch (Exception e) {
			// TODO
		}
	}
	
	public void StopSound(int position){
		this.getItem(dialog_position);
	}
	
	public void onClick(View v) {
	    int position=(Integer)v.getTag(); // now you know which button in list was clicked.
	    
	    switch(v.getId()){
		case R.id.diary_delete_button:
			deletePostDialog(position);
	        break;
		case R.id.diary_edit_button:
			startEditNote(position);
			break;
		case R.id.diary_map_button:
			break;
		case R.id.diary_share_button:
			showPopupShareMenu(v);
			break;
		case R.id.diary_item_player_play:
			PlaySound(position);
			break;
		case R.id.diary_item_player_pause:
			StopSound(position);
			break;	
		}
	}
}	