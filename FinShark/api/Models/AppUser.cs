//The model for user profile data
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace api.Models
{
    //Inherits idenity user that is a given class that gives us functionality with auth and profile data
    public class AppUser : IdentityUser
    {
        //The portfolios together
        public List<Portfolio> Portfolios {get; set;} = new List<Portfolio>();
    }
}
