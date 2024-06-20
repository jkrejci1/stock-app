using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    //Class inherits from our DBContext which will bring in stuff to search our tables
    public class ApplicationDBContext : DbContext
    {
        //Constructor ctor<tab> for the quick snippet
        public ApplicationDBContext(DbContextOptions dbContextOptions) //We'll have a type DbContextOptions called dbContextOptions
        : base(dbContextOptions) //Base is the same as DBContext so we're just passing the context options to the DBContext class we're inheriting from
        {
            
        }

        //Add the tables that we'll want as dbContext will allow us to access tables
        //We do this by wrapping this in a DbSet --> Fancy word for grabbing something out of the DB and you're gonna do something with it
        public DbSet<Stock> Stock { get; set; } //When using this you're manipulating the stock table and this creates the DB for us
        public DbSet<Comment> Comments { get; set; } //This and above are the Stock and Comments table linking the DB to the code
    }
}