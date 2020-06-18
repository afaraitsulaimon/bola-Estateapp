using System;



namespace BolaEstateApp.Data.Entities
{

    //we created a abstract class, we can create an instance of this class
    //that means you can't create a new object of it
    //e.g public string bola = new BaseEntity();
    //if you do the below example, it will give ua error, because we can't create
    //a new object of it.

    public abstract class BaseEntity
    {


        //we created this BaseEntity abstract class,
        //because for all our tables, we will like property table e.t.c
        //we will always have all this in the table of each, 
        //Id,CreatedAt,IdDeleted,ModifiedAt,DeletedAt
        //apart from other colunms
        //so to avoid the repetition in creating all of this in each table, so we created 
        //this public abstract class BaseEntity to hold it
        //but the new instance will not be ble to be created
        // but property.cscan inherit from it

        public string Id { get; set; }

        public bool IdDeleted { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }

        public DateTime DeletedAt { get; set; }

    }
}