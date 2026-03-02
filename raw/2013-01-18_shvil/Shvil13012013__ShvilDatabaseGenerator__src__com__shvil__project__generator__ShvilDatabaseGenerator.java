/*
 * Copyright (C) 2011 Markus Junginger, greenrobot (http://greenrobot.de)
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
package com.shvil.project.generator;

import de.greenrobot.daogenerator.DaoGenerator;
import de.greenrobot.daogenerator.Entity;
import de.greenrobot.daogenerator.Property;
import de.greenrobot.daogenerator.Schema;
import de.greenrobot.daogenerator.ToOne;

/**
 * Generates entities and DAOs for the example project DaoExample.
 * 
 * Run it as a Java application (not Android).
 * 
 * @author Markus
 */
public class ShvilDatabaseGenerator {

    public static void main(String[] args) throws Exception {
        Schema schema = new Schema(1, "com.shvil.project.db");
        schema.enableKeepSectionsByDefault();

        addAngel(schema);
        addMessageUser(schema);
        addDiaryItems(schema);
        addGeneralItems(schema);

        new DaoGenerator().generateAll(schema, "../Shvil/src-gen");
    }

    private static void addAngel(Schema schema) {
        Entity angel = schema.addEntity("Angel");
        angel.addIdProperty();
        angel.addStringProperty("city").notNull();
        angel.addStringProperty("name").notNull();
        angel.addStringProperty("phone1").notNull();
        angel.addStringProperty("phone2");
        angel.addStringProperty("information");
        angel.addDoubleProperty("latitude").notNull();
        angel.addDoubleProperty("longitude").notNull();
        angel.addBooleanProperty("passport");
        angel.addBooleanProperty("stamp");
        angel.addBooleanProperty("religious");
        angel.addBooleanProperty("is_new");
    }
    
    private static void addMessageUser(Schema schema) {
    	
    	Entity user = schema.addEntity("User");
        user.addIdProperty();
        user.addStringProperty("name").notNull();
        user.addStringProperty("imagePath"); 	  
    	
        Entity message = schema.addEntity("Message");
        message.addIdProperty();
        message.addStringProperty("message").notNull();
        message.addDateProperty("time").notNull();
        message.addLongProperty("conversationId");
        Property sender_id = message.addLongProperty("senderId").notNull().getProperty();
        ToOne message_sender_id = message.addToOne(user, sender_id);
        message_sender_id.setName("messageSenderId");     
        Property receiver_id = message.addLongProperty("receiverId").notNull().getProperty();
        ToOne message_receiver_id = message.addToOne(user, receiver_id);
        message_receiver_id.setName("messageReceiverId");
        message.addLongProperty("groupId");  
    }
    
    private static void addDiaryItems(Schema schema) {
    	
    	Entity diaryItem = schema.addEntity("DiaryItem");
    	diaryItem.addIdProperty();
    	diaryItem.addByteProperty("type");
    	diaryItem.addStringProperty("titleuri");
    	diaryItem.addStringProperty("text");
    	diaryItem.addDateProperty("date").notNull();
    	diaryItem.addDoubleProperty("longitude");
    	diaryItem.addDoubleProperty("latitude");
    }
    
    private static void addGeneralItems(Schema schema) {
    	
    	Entity generalItem = schema.addEntity("GeneralItem");
    	generalItem.addIdProperty();
    	generalItem.addByteProperty("type");
    	generalItem.addStringProperty("title");
    	generalItem.addStringProperty("text");
    	generalItem.addDoubleProperty("longitude");
    	generalItem.addDoubleProperty("latitude");
    	generalItem.addBooleanProperty("is_new");

    }
}
