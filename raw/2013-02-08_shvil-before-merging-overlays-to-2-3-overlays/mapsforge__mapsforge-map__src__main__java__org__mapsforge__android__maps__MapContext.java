/*
+ * Copyright 2010, 2011 mapsforge.org
+ *
+ * This program is free software: you can redistribute it and/or modify it under the
+ * terms of the GNU Lesser General Public License as published by the Free Software
+ * Foundation, either version 3 of the License, or (at your option) any later version.
+ *
+ * This program is distributed in the hope that it will be useful, but WITHOUT ANY
+ * WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A
+ * PARTICULAR PURPOSE. See the GNU Lesser General Public License for more details.
+ *
+ * You should have received a copy of the GNU Lesser General Public License along with
+ * this program. If not, see <http://www.gnu.org/licenses/>.
+ */
package org.mapsforge.android.maps;


/**
+ * Defines a special context which MapView can communicate with
+ */
public interface MapContext {

	/**
+	 * Returns a unique MapView ID on each call.
+	 * 
+	 * @return the new MapView ID.
+	 */
	int getMapViewId();

	/**
+	 * This method is called once by each MapView during its setup process.
+	 * 
+	 * @param mapView
+	 *            the calling MapView.
+	 */
	void registerMapView(MapView mapView);
}