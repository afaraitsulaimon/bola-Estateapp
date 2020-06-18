//what does inheritance mean?
//getting some class properties from the super class, so the class that inherited is 
//the sub class
// BaseEntity class is the super class
//why the Property and the Contact class are the sub classes
//because they inherited from BaseEntity class

namespace BolaEstateApp.Data.Entities
{ 

    //class  Contact inherited (:) from  BaseEntity
    // because in the property table we need the following
    //Id,CreatedAt,IdDeleted,ModifiedAt,DeletedAt
    // and we don't need to start recreating it again, so is better we inherit from BaseEntity
    //since we have inherited all this (Id,CreatedAt,IdDeleted,ModifiedAt,DeletedAt) from BaseEntity
    // All we just have to do is create the remaining colunms which are
    //State e.t.c

    public class Contact : BaseEntity
    {
    
        public string State { get; set; }  //Abuja

         public string LocalGovernmentArea { get; set; }  //Asokoro

    }
}

