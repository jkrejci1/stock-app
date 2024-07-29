using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;



//Interfaces don't exist; they're used to plug in to our repositories and implement the interface here using the Repository (so if our interace was huge)
//Interfaces contain the required methods to be used 
namespace api.Repository
{
    public class StockRepository : IStockRepository //Plug in the stock interface to implement it
    {
        //Get ready to make our database being brought in as private
        private readonly ApplicationDBContext _context;

        //Implement dependency injection by bringing in the database first as preparation
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        //Method that makes use of the GetAllAsync method which will return a list of stocks
        public Task<List<Stock>> GetAllAsync() {
            return _context.Stocks.ToListAsync();
        }
    }
}