package com.shvil.project.angels;

import java.util.List;

import android.app.Activity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.TextView;

import com.shvil.project.R;
import com.shvil.project.db.Angel;

public class AngelsArrayAdapter extends ArrayAdapter<Angel> {

	private final Activity context;
	private final List<Angel> angels;
	
	static class ViewHolder {
	    public TextView city;
		public TextView name;
		public TextView phone1;
		//public TextView phone2;
		//public TextView information;
		//public TextView latitude;
		//public TextView longitude;
		//public ImageView passport;
		//public ImageView stamp;
		//public ImageView religious;
		//public ImageView distance; // from shvil

	  }

	public AngelsArrayAdapter(Activity context, List<Angel> values) {
		super(context, R.layout.angel_item_layout, values);
	    this.context = context;
	    this.angels = values;
	}

  @Override
  public View getView(int position, View convertView, ViewGroup parent) {
	  View rowView = convertView;
	  if (rowView == null) {
	      LayoutInflater inflater = context.getLayoutInflater();
	      rowView = inflater.inflate(R.layout.angel_item_layout, null);
	      ViewHolder viewHolder = new ViewHolder();
	      viewHolder.city = (TextView) rowView.findViewById(R.id.city);
	      viewHolder.name = (TextView) rowView.findViewById(R.id.name);
	      viewHolder.phone1 = (TextView) rowView.findViewById(R.id.phone1);
	      rowView.setTag(viewHolder);
	  }

	  ViewHolder holder = (ViewHolder) rowView.getTag();
	  Angel angel = angels.get(position);
	  holder.city.setText(angel.getCity());
	  holder.name.setText(angel.getName());
	  holder.phone1.setText(angel.getPhone1());
	  return rowView;
  }
}
