using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Stock
    {
        //Use ID's to identify things
        public int Id { get; set; }
        //Can also identify with a symbol (also need to start it with an empty string to prevent null reference errors)
        public string Symbol { get; set; } = string.Empty;
        //Also identify by company name
        public string CompanyName { get; set; } = string.Empty;
        //Gonna deal with money, so we need to make sure we have monetary amount for money
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
        public long MarketCap { get; set; }

        //Work on our one to many relationship
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}