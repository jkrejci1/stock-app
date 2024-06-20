using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Comment
    {
        public int Id { get; set; } //ID for unique selection like in our other stock model
        public string Title { get; set; } = string.Empty; //Title for our comments
        public string Content { get; set; } = string.Empty; //Content
        public DateTime CreatedOn { get; set; } = DateTime.Now; //Whenever this data is created and this will be set to the exact time it was created
        
        //Linking relationships within the comment using convention --> .NET Core will form relationships for us NEED BOTH BELOW
        public int? StockId { get; set; } //Key to form the relationship within the database
        public Stock? Stock { get; set; } //Navigation property --> Allows us to access the other side of the model (like Stock.CompanyName, etc)

    }
}