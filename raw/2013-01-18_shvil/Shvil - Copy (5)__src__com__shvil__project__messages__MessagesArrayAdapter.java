package com.shvil.project.messages;

import java.util.List;

import android.app.Activity;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.LinearLayout;
import android.widget.TableLayout;
import android.widget.TextView;

import com.shvil.project.R;
import com.shvil.project.db.Message;

public class MessagesArrayAdapter extends ArrayAdapter<Message> {
  private final Activity context;
  private final List<Message> messages;

  static class ViewHolder {
    public TextView text;
    public LinearLayout wrapper;
    public TableLayout table_message;
    public TextView datetime;

  }

  public MessagesArrayAdapter(Activity context, List<Message> values) {
    super(context, R.layout.message_item_layout, values);
    this.context = context;
    this.messages = values;
  }

  @Override
  public View getView(int position, View convertView, ViewGroup parent) {
    View rowView = convertView;
    if (rowView == null) {
      LayoutInflater inflater = context.getLayoutInflater();
      rowView = inflater.inflate(R.layout.message_item_layout, null);
      ViewHolder viewHolder = new ViewHolder();
      viewHolder.text = (TextView) rowView.findViewById(R.id.comment);
      viewHolder.wrapper = (LinearLayout)rowView.findViewById(R.id.wrapper);
      viewHolder.table_message = (TableLayout)rowView.findViewById(R.id.table_message);
      viewHolder.datetime = (TextView) rowView.findViewById(R.id.datetime);
      rowView.setTag(viewHolder);
    }

    ViewHolder holder = (ViewHolder) rowView.getTag();
    Message message = messages.get(position);
    holder.text.setText(message.getMessage());
    //Time time = new Time();
    //time.set(Long.valueOf(message.getTime()));
    //holder.datetime.setText(time.format(" %d/%m %H:%M"));
    holder.datetime.setText(message.getTime().toGMTString());
    holder.datetime.setGravity(message.getId() % 2  == 0 ? Gravity.LEFT : Gravity.RIGHT);
    holder.table_message.setBackgroundResource(message.getId() % 2 == 0 ? R.drawable.bubble_yellow : R.drawable.bubble_green);
    holder.wrapper.setGravity(message.getId() % 2  == 0 ? Gravity.LEFT : Gravity.RIGHT);
        
    return rowView;
  }
} 