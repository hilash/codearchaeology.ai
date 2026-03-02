package com.shvil.project.olddb;

public class Message {
	  private long id;
	  private String message;
	  private long time;
	  private long conversation_id;
	  private long sender_id;
	  private long receiver_id;
	  private long group_id;
	  
	  /* getters */
	  
	  public long getId() {
	    return id;
	  }
	  
	  public String getMessage() {
		  return message;
	  }
	  
	  public long getTime() {
		    return time;
		  }
	  
	  public long getConverationId() {
		  return conversation_id;
	  }
	  
	  public long getSenderId() {
		  return sender_id;
	  }
	  
	  public long getReceiverId() {
		  return receiver_id;
	  }
	  
	  public long getGroupId() {
		  return group_id;
	  }
	  
	  /* setters */

	  public void setId(long id) {
	    this.id = id;
	  }

	  public void setMessage(String message) {
	    this.message = message;
	  }
	  
	  public void setTime(long time) {
		  this.time = time;
	  }
	  
	  public void setConverationId(long conversation_id) {
		  this.conversation_id = conversation_id;
	  }
	  
	  public void setSenderId(long sender_id) {
		  this.sender_id = sender_id;
	  }
	  
	  public void setReceiverId(long receiver_id) {
		  this.receiver_id = receiver_id;
	  }
	  
	  public void setGroupId(long group_id) {
		  this.group_id = group_id;
	  }
	} 