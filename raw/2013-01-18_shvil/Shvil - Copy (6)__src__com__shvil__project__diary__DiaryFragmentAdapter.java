package com.shvil.project.diary;

import java.text.SimpleDateFormat;
import java.util.List;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.view.LayoutInflater;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageButton;
import android.widget.ImageView;
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
	  }

	public DiaryFragmentAdapter(Activity context, List<DiaryItem> values) {
		super(context, R.layout.diary_item_layout, values);
		this.context = context;
		this.diaryItems = values;
	    this.mApplication = (MyApplication)context.getApplicationContext();
	    this.dao = mApplication.daoSession.getDiaryItemDao();
	}
	
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
	    holder.deleteButton.setOnClickListener(this);
	    holder.editButton.setOnClickListener(this);
	    holder.shareButton.setOnClickListener(this);
	    holder.mapButton.setOnClickListener(this);
	  
	    
	    // specific for item type (picture, note, voice)
	    // TODO -> add consts
	    byte item_type = item.getType();
	    switch (item_type) {
        	case 0:
        		// Picture
        		//holder.picture.setImageURI((Uri) item.getTitleuri());
        		holder.title.setVisibility(View.GONE);
        		break;
        	case 1:
        		// Note
        		holder.picture.setVisibility(View.GONE);
        		holder.title.setText(item.getTitleuri());
        		break;
        	case 2:
        		//Voice
        		holder.title.setVisibility(View.GONE);
        		break;
        	default:
        		break;
        }
	    return rowView;
	}
	
	/* delete from gui and db */
	
	
	private void deletePost()
	{
		DiaryItem item = this.getItem(dialog_position);
		
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
		context.startActivityForResult(intent, DiaryFragment.EDIT_DIARY_ITEM);
	}
	
	public void updateNote(boolean is_new, long diaryItemID, int position)
	{
		// after NoteEditor Activity ends, (and the note is in DB),
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
	
	public void showPopupShareMenu(View v) {
	    PopupMenu popup = new PopupMenu(this.context, v);
	    popup.setOnMenuItemClickListener(sharePopupListener);
	    popup.inflate(R.menu.share_post);
	    popup.show();
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
		}
	}
}	