using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;

//Makes it where we trim down responses when handeling data
//Here we will remove sending all the comments back and fourth every single time we call for the stock table in the api in order to save space and not have to send so much data at once that is unnecessary
namespace api.Dtos.Stock
{
    public class StockDto
    {
        //WE DON'T USE THIS FOR USERS TO ENTER DATA, SO WE DON'T NEED DATA VALIDATION HERE!!
        
        //Use Id's to identify things (the public key)
        public int Id { get; set; }
        //Can also identify with a symbol (also need to start it with an empty string to prevent null reference errors)
        public string Symbol { get; set; } = string.Empty; //If we don't initialize as an empty string we'll get null reference errors, don't want to store null when we have an empty string, we just would want an empty string
        //Also identify by company name
        public string CompanyName { get; set; } = string.Empty; //Makes Data Type be nvarchar(MAX)

        //In order to customize a data type in the DB you need to set it right before you make the getter and setter for the column name
        //So then, the data type for Purchase would be decimal(18,2) max 18 digits, and has 2 decimal places
        //Gonna deal with money, so we need to make sure we have monetary amount for money
        //Purchase 
        public decimal Purchase { get; set; }
        
        //The dividend one
        public decimal LastDiv { get; set; }
       
        //Industry data
        public string Industry { get; set; } = string.Empty;
        //Market Cap data (make long can it be in the trillions)
        public long MarketCap { get; set; } //Data Type == bigint

        //For the comments
        public List<CommentDto> Comments { get; set; }

    }
}