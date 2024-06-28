//This class allows the program to search for and in your individual tables
//Giant object that allows us to specify which tables we want

//TO MAKE CHANGES TO THE DB USE "dotnet ef migrations add ." then run "dotnet ef databse update" this will change it
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
        : base(dbContextOptions) //Base is the same as DBContext so we're just passing the context options to the DBContext class we're inheriting from same as dbContext<dbContextOptions> where <>'s are actually parenthesis btw
        {
            
        }

        //Add the tables that we'll want as dbContext will allow us to access tables
        //We do this by wrapping this in a DbSet --> Fancy word for grabbing something out of the DB and you're gonna do something with it
        //Names must match the class names for the models
        //DbSet manipulate the table of your choosing and creates the database for us, it grabs these tables and then creates the database with them
        public DbSet<Stock> Stocks { get; set; } //When using this you're manipulating the stock table and this creates the DB for us
        
        //Creates a table called Comments in our data base where the type of it will be the name of the class for our Comment Model (The data type needs to be named after the class (comment in this case) but we can name the table anything, best practice to do it like this though)
        public DbSet<Comment> Comments { get; set; } //This and above are the Stock and Comments table linking the DB to the code
    }
}