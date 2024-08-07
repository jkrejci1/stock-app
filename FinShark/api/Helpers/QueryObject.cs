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
    }
}