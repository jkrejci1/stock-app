//This class allows the program to search for and in your individual tables
//Giant object that allows us to specify which tables we want

//TO MAKE CHANGES TO THE DB USE "dotnet ef migrations add ." then run "dotnet ef databse update" this will change it
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using api.Migrations;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    //Class inherits from our DBContext which will bring in stuff to search our tables
    public class ApplicationDBContext : IdentityDbContext<AppUser> //IdentityDb takes in our AppUser class to know when we are switching to that specific model
    {
        //Constructor ctor<tab> for the quick snippet
        public ApplicationDBContext(DbContextOptions dbContextOptions) //We'll have a type DbContextOptions called dbContextOptions
        : base(dbContextOptions) //Base is the same as DBContext so we're just passing the context options to the DBContext class we're inheriting from same as dbContext<dbContextOptions> where <>'s are actually parenthesis btw
        {
            
        }

        //Add the tables that we'll want as dbContext will allow us to access tables
        //We do this by wrapping this in a DbSet --> Fancy word for grabbing something out of the DB and you're gonna do something with it
        //Names (for where the type is) must match the class names for the models
        //DbSet manipulate the table of your choosing and creates the database for us, it grabs these tables and then creates the database with them
        public DbSet<Stock> Stocks { get; set; } //When using this you're manipulating the stock table and this creates the DB for us
        
        //Creates a table called Comments in our data base where the type of it will be the name of the class for our Comment Model (The data type needs to be named after the class (comment in this case) but we can name the table anything, best practice to do it like this though)
        public DbSet<Comment> Comments { get; set; } //This and above are the Stock and Comments table linking the DB to the code
    
        //For the portfolios table
        public DbSet<Portfolio> Portfolios { get; set; }

        //Create roles for user (user and admin roles/priviliges)
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //For on model creating for the join table of the users and stocks
            builder.Entity<Portfolio>(x=> x.HasKey(p => new {p.AppUserId, p.StockId}));
            //Connected the foriegn keys above to the table (for the users)
            builder.Entity<Portfolio>()
                .HasOne(u => u.AppUser)
                .WithMany(u => u.Portfolios)
                .HasForeignKey(p=> p.AppUserId);

            //Connected the foriegn keys above to the table (for the stocks)
            builder.Entity<Portfolio>()
                .HasOne(u => u.Stock)
                .WithMany(u => u.Portfolios)
                .HasForeignKey(p=> p.StockId);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN", //NormalizedName just means the name when capitalized
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER", //NormalizedName just means the name when capitalized
                },
            };
            //Now let's add it
            builder.Entity<IdentityRole>().HasData(roles);


        }
    }
}