package com.shvil.project.olddb;

public class User {
	  private long id;
	  private String name;
	  private String image;
	  
	  /* getters */
	  
	  public long getId() {
	    return id;
	  }
	  
	  public String getName() {
		  return name;
	  }
	  
	  public String getImage() {
		  return image;
	  }	  
	  
	  /* setters */

	  public void setId(long id) {
	    this.id = id;
	  }

	  public void setName(String name) {
	    this.name = name;
	  }
	  
	  public void setImage(String image) {
		    this.image = image;
		  }
	} 