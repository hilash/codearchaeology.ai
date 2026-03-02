/*
 * package com.shvil.project.messages;


import java.util.ArrayList;
import java.util.List;

import android.app.ActionBar;
import android.app.ListActivity;
import android.content.Intent;
import android.database.Cursor;
import android.os.Bundle;
import android.view.MenuItem;
import android.view.View;
import android.widget.ListView;

import com.shvil.project.MainActivity;
import com.shvil.project.MyApplication;
import com.shvil.project.R;
import com.shvil.project.db.Message;
import com.shvil.project.db.MessageDao;
import com.shvil.project.db.User;
import com.shvil.project.db.UserDao;

public class RecentMessagesActivity extends ListActivity {
	
	private MessageDao messageDao;
	private RecentMessagesArrayAdapter adapter;
	

	@Override
	  public void onCreate(Bundle savedInstanceState) {
	    super.onCreate(savedInstanceState);
	    setContentView(R.layout.recent_messages_activity);
	    ActionBar actionBar = getActionBar();
        actionBar.setDisplayHomeAsUpEnabled(true);
        actionBar.setHomeButtonEnabled(true);

        MyApplication mApplication = (MyApplication)getApplicationContext();
	    messageDao = mApplication.daoSession.getMessageDao();
	   
	    addMessagesUser();

	    List<ConversationPreview> values = getLastMessageFromEachConversation();

	    // Use the SimpleCursorAdapter to show the elements in a ListView
	    adapter = new RecentMessagesArrayAdapter(this, values);
	    setListAdapter(adapter);
	  }
	
	private void addMessagesUser()
	{
		Long id = null;
		String message = "Hello There!";
		java.util.Date time = new java.util.Date(0);
		Long conversation_id = new Long(12333);
		long sender_id = 0;
		long receiver_id = 1;
		Long group_id = null;
	   	
		Message m = new Message(id, message, time, conversation_id, sender_id, receiver_id, group_id);
		
		message = "What's up?!?";
		
		Message m2 = new Message(id, message, time, conversation_id, sender_id, receiver_id, group_id);


		messageDao.insert(m);
		messageDao.insert(m2);
		
		User u1 = new User(null, "Alice", null);
		User u2 = new User(null, "Bob", null);
		
		MyApplication mApplication = (MyApplication)getApplicationContext();
	  	UserDao userDao = mApplication.daoSession.getUserDao();
	  	
		userDao.insert(u1);
		userDao.insert(u2);
	}
	
	@Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {
            case android.R.id.home:
                // app icon in action bar clicked; go home
                Intent intent = new Intent(this, MainActivity.class);
                //intent.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
                startActivity(intent);
                
                //finish();
                return true;
            default:
                return super.onOptionsItemSelected(item);
        }
    }
	
	 @Override
	    public void onListItemClick(ListView l, View v, int position, long id) {
	        super.onListItemClick(l, v, position, id);
	        {
	        	Intent intent = new Intent(this, MessagesActivity.class);
	            intent.putExtra(MessageDao.Properties.ConversationId.name, adapter.getItem(position).message.getConversationId());
	            startActivity(intent);
	        }
	    }

	  public List<ConversationPreview> getLastMessageFromEachConversation() {
	        
		    List<ConversationPreview> conversations = new ArrayList<ConversationPreview>();
		    MyApplication mApplication = (MyApplication)getApplicationContext();
		  	UserDao userDao = mApplication.daoSession.getUserDao();
		    
		    String query = "SELECT m1.*, u1.*"
		    		+ " FROM "
	    			+ MessageDao.TABLENAME + " m1"
	    			+ " LEFT JOIN "
	    			+ UserDao.TABLENAME + " u1"
	    			
	    			+ " ON ("
		    		+ "m1." + MessageDao.Properties.SenderId.columnName
		    		+ " = "
		    		+ "u1." + UserDao.Properties.Id.columnName
		    		+ ")"
		    
	    			+ " LEFT JOIN "
	    			+ MessageDao.TABLENAME + " m2"
	    			+ " ON (m1." + MessageDao.Properties.ConversationId.columnName
	    			+ " = m2." + MessageDao.Properties.ConversationId.columnName
	    			+ " AND m1." + MessageDao.Properties.Time.columnName
	    			+ " < m2." + MessageDao.Properties.Time.columnName + ")"
	    			+ " WHERE m2." + MessageDao.Properties.Time.columnName
	    			+ " IS NULL";    		
		    
		    
		    Cursor cursor = messageDao.getDatabase().rawQuery(query, null);

		    cursor.moveToFirst();
		    while (!cursor.isAfterLast()) {
		    	ConversationPreview conv = new ConversationPreview();
		    	conv.message = messageDao.readEntity(cursor, 0);
		    	int len = messageDao.getAllColumns().length;
		    	conv.user = userDao.readEntity(cursor, len);
				  
		    	conversations.add(conv);
		    	cursor.moveToNext();
		    }
		    // Make sure to close the cursor
		    cursor.close();
		    return conversations;
		  }

}
*/
