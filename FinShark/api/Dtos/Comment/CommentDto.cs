using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Comment
{
    public class CommentDto
    {
        //It seems per the syntax for sql server that our first row will be the public key so Id will be the public key
        public int Id { get; set; } //ID for unique selection like in our other stock model THE PRIMARY KEY THEN!!
        public string Title { get; set; } = string.Empty; //Title for our comments
        public string Content { get; set; } = string.Empty; //Content
        public DateTime CreatedOn { get; set; } = DateTime.Now; //Whenever this data is created and this will be set to the exact time it was created
        
        //Linking relationships within the comment using CONVENTION --> .NET Core will form relationships for us NEED BOTH BELOW
        //Creates a StockId foreign key in the Comment table and uses Stock to access its properties
        public int? StockId { get; set; } //Key to form the relationship within the database (REMEMBER --> THIS IS SUPPOSSED TO BE AN ID THAT MATCHES WITH THE SEPERATE STOCK ID PARENT SO WE CAN ACCESS COMMENT CHILDREN FOR A STOCK) (SO THEN, THE FOREIGN KEY ITSELF NEEDS TO BE IN THE CHILD TABLE FOR THIS TO WORK REMEMBER!)
        
        //For the Dto you shouldn't have the navigation property, because that would inject a whole other entire object into the dto which would return each stock for each comment which would be a mess in our case
        //public Stock? Stock { get; set; } //Navigation property for the StockId key --> Allows us to access the other side of the model (Accessing data in the Stock model here for instance (which would be the data for whatever stock this comment will be tied to. If the comment was on Apple stock, then it will access the Apple stock data then for example so like Stock.CompanyName would == Apple then and so on. )) (like Stock.CompanyName, etc) while StockId is the foreign key for containing such data

    }
}