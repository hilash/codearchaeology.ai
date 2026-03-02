package com.shvil.server.db;

import javax.jdo.annotations.*;

@PersistenceCapable
@Inheritance(strategy=InheritanceStrategy.SUBCLASS_TABLE)
public class SyncEntity {
	
	@PrimaryKey
	@Persistent(valueStrategy = IdGeneratorStrategy.IDENTITY)
	protected Long id;
	
	@Persistent
	protected Byte counter;
	@Persistent
	protected Byte record_state;
	@Persistent
	protected Long server_time;
	
    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }
    
    public Byte getCounter() {
        return counter;
    }

    public void setCounter(Byte counter) {
        this.counter = counter;
    }

    public Byte getRecord_state() {
        return record_state;
    }

    public void setRecord_state(Byte record_state) {
        this.record_state = record_state;
    }

    public Long getServer_time() {
        return server_time;
    }

    public void setServer_time(Long server_time) {
        this.server_time = server_time;
    }
}
