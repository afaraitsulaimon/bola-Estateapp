//This file will contain every details of each property
//Name of property, addess, location e.t.c  
//creting all this files, BaseEntity.cs . Property.cs
//this is representing the list of the tables wehave in ourdatabase and their properties

namespace BolaEstateApp.Data.Entities
{ 

    //class  Property inherited (:) from  BaseEntity
    // because in the property table we need the following
    //Id,CreatedAt,IdDeleted,ModifiedAt,DeletedAt
    // and we don't need to start recreating it again, so is better we inherit from BaseEntity
    //since we have inherited all this (Id,CreatedAt,IdDeleted,ModifiedAt,DeletedAt) from BaseEntity
    // All we just have to do is create the remaining colunms which are
    //location, PropertyAddress,Title e.t.c

    public class Property : BaseEntity
    {
    
        public string Title { get; set; }  //3bedroom flat in Asokoro.

    }
}