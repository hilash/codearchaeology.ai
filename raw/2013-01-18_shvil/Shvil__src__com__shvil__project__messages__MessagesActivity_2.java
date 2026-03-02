package com.shvil.project.messages;

import java.util.List;

import android.app.ActionBar;
import android.app.ListActivity;
import android.content.Intent;
import android.os.Bundle;
import android.view.MenuItem;
import android.widget.ListView;

import com.shvil.project.MyApplication;
import com.shvil.project.R;
import com.shvil.project.db.Message;
import com.shvil.project.db.MessageDao;

public class MessagesActivity extends ListActivity {
	
	private MessageDao messageDao;
	private MessagesArrayAdapter adapter;

	@Override
	  public void onCreate(Bundle savedInstanceState) {
	    super.onCreate(savedInstanceState);
	    setContentView(R.layout.my_messages_activity);
	    ActionBar actionBar = getActionBar();
        actionBar.setDisplayHomeAsUpEnabled(true);
        actionBar.setHomeButtonEnabled(true);
        
        Intent intent = getIntent();
        long converation_id = intent.getLongExtra(MessageDao.Properties.ConversationId.name, 0);
        converation_id = 12333;
	    
	    MyApplication mApplication = (MyApplication)getApplicationContext();
	    messageDao = mApplication.daoSession.getMessageDao();
	    //addMessages();
	    
	    List<Message> messages = messageDao.queryBuilder()
	    		.where(MessageDao.Properties.ConversationId.eq(converation_id))
	    		.orderAsc(MessageDao.Properties.Time).list();
	    
	    adapter = new MessagesArrayAdapter(this, messages);
	    setListAdapter(adapter);
	    
	    this.getListView().setTranscriptMode(ListView.TRANSCRIPT_MODE_ALWAYS_SCROLL);
	    this.getListView().setStackFromBottom(true);
	  }
	
	@Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {
            case android.R.id.home:
                // app icon in action bar clicked; go home
                Intent intent = new Intent(this, RecentMessagesActivity.class);
                intent.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
                startActivity(intent);
                finish();
                return true;
            default:
                return super.onOptionsItemSelected(item);
        }
    }

}
