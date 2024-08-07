using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers
{
    //Object for our queries when handeling data from our database
    public class QueryObject
    {
        //These query objects will be optionally given by the user
        public string? Symbol { get; set; } = null;
        public string? CompanyName { get; set; } = null;

        //For sorting found data in the database (using queries)
        public string? SortBy { get; set; } = null;
        public bool IsDecsending { get; set; } = false;

        //Pagination for making sure there's not too much data going onto one page when calling all the data from the data base
        public int PageNumber { get; set; } = 1; //Starts it at one page by default
        public int PageSize { get; set; } = 20; //Sets the amount of data on a single page
    }
}