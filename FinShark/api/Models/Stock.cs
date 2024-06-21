//This is a model for this schema in the database, it will be stored in the keys folder in the folder for this table/schema so dbo.Stock
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Stock
    {
        //Use Id's to identify things (the public key)
        public int Id { get; set; }
        //Can also identify with a symbol (also need to start it with an empty string to prevent null reference errors)
        public string Symbol { get; set; } = string.Empty; //If we don't initialize as an empty string we'll get null reference errors, don't want to store null when we have an empty string, we just would want an empty string
        //Also identify by company name
        public string CompanyName { get; set; } = string.Empty; //Makes Data Type be nvarchar(MAX)

        //In order to customize a data type in the DB you need to set it right before you make the getter and setter for the column name
        //So then, the data type for Purchase would be decimal(18,2) max 18 digits, and has 2 decimal places
        //Gonna deal with money, so we need to make sure we have monetary amount for money
        //Starts creation of column with the datatype decimal(18,2) which then will be called purchase from the next line
        [Column(TypeName = "decimal(18,2)")] //Forces SQL DB to have there be 18 digits and 2 decimal places at most
        //Purchase 
        public decimal Purchase { get; set; }
        
        //Like above, we need to put this before we set up the LastDiv part of our model so that it has the right data
        [Column(TypeName = "decimal(18,2)")] //Forces SQL DB to have there be 18 digits and 2 decimal places at most
        //The dividend one
        public decimal LastDiv { get; set; }
       
        //Industry data
        public string Industry { get; set; } = string.Empty;
        //Market Cap data (make long can it be in the trillions)
        public long MarketCap { get; set; } //Data Type == bigint

        //Work on our one to many relationship (Makes a list with the data type being Comment called Comments)
        //Has to match the class name in the Comment.cs file to create a comment list
        //Will tie certain comments to certain stocks then by doing this, displaying the right comments when appropriate for a specific stock
        //So then a stock can have many comments but a comment can't have many different stocks, that wouldn't make sense
        //As you can see from the comment model then, this will be tied in from the StockId (The Id for Stock model will be the primary key for the foreign key Comments here then) (Stock using StockId is the parent of the Comments foreign key)
        //We know that there's gonna be more than 1 comment per stock, so to access all of them we'll use the C# list data structure (like lists in Python)
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}