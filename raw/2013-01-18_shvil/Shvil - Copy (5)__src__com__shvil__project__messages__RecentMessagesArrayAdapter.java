package com.shvil.project.messages;

import java.util.List;

import android.app.Activity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.TextView;

import com.shvil.project.R;


public class RecentMessagesArrayAdapter extends ArrayAdapter<ConversationPreview> {
  private final Activity context;
  private final List<ConversationPreview> conversations;

  static class ViewHolder {
	public TextView sender;
    public TextView datetime;
    public TextView message;

  }

  public RecentMessagesArrayAdapter(Activity context, List<ConversationPreview> values) {
    super(context, R.layout.recent_messages_activity_item, values);
    this.context = context;
    this.conversations = values;
  }

  @Override
  public View getView(int position, View convertView, ViewGroup parent) {
    View rowView = convertView;
    if (rowView == null) {
      LayoutInflater inflater = context.getLayoutInflater();
      rowView = inflater.inflate(R.layout.recent_messages_activity_item, null);
      ViewHolder viewHolder = new ViewHolder();
      viewHolder.sender = (TextView) rowView.findViewById(R.id.sender);
      viewHolder.datetime = (TextView) rowView.findViewById(R.id.datetime);
      viewHolder.message = (TextView) rowView.findViewById(R.id.message);
      rowView.setTag(viewHolder);
    }

    ViewHolder holder = (ViewHolder) rowView.getTag();
    ConversationPreview conversation = conversations.get(position);
    holder.message.setText(conversation.message.getMessage());
    holder.datetime.setText(conversation.message.getTime().toString());
    holder.sender.setText(conversation.user.getName());
   
    return rowView;
  }
} 