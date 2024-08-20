//This will be our join table to tie in users with stocks like for likes, comments, etc
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    //We're gonna need a list of these in the tables that are being linked --> Stock.cs, and AppUser.cs
    [Table("Portfolios")]
    public class Portfolio
    {
        //Foreign Keys to link the stock and user table
        public string AppUserId { get; set; }
        public int StockId { get; set; }

        //Our navigation property for us developers: Just for developing purposes
        public AppUser AppUser { get; set; }
        public Stock Stock { get; set; }
    }
}