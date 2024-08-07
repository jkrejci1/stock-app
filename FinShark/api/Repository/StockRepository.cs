using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;



//Interfaces don't exist; they're used to plug in to our repositories and implement the interface here using the Repository (so if our interace was huge)
//Interfaces contain the required methods to be used
//When we implement the interface we are required to use all the methods in it (like a contract), which is good for important code that needs to be run no matter what
namespace api.Repository
{
    public class StockRepository : IStockRepository //Plug in the stock interface to implement it
    {
        //Get ready to make our database being brought in as private (DATABASE CALLS SHOULDN'T BE MADE IN THE CONTROLLER, THAT'S WHY WE CREATE A REPOSITORY INSTEAD TO DO IT!!)
        private readonly ApplicationDBContext _context;

        //Implement dependency injection by bringing in the database first as preparation
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        //Method for the post
        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            //Take the newly created stock and add it to the database, then save it and reteurn the stock model
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        //Method for the delete
        public async Task<Stock?> DeleteAsync(int id)
        {
            //Find if the stock exists, x == stock, if it's id == the id we're matching it to, then that's the stock, if not it's null
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id ==id);
            if(stockModel == null) {
                return null;
            }

            //Remove the found stock, and save the changes and return the stock model
            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        //Method that makes use of the GetAllAsync method which will return a list of stocks
        //Now that we have abstracted our code doing this, whatever uses this method in the controller can all be modified at once (for whatever would cause such a change in this function) --> DEPENDENCY INJECTION
        public async Task<List<Stock>> GetAllAsync() {
            //Return all the stock data
            return await _context.Stocks.Include(c => c.Comments).ToListAsync(); //Does the usual, but to get our comments (foreign key relationship) we need to use include
        }

        //Method for the get by id
        public async Task<Stock?> GetByIdAsync(int id)
        {
            //Return all the stocks that mathc the id
            return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(i => i.Id == id); //Same as include above where i is true (done) if its given id == to our id we want to search with
        }

        public Task<bool> StockExists(int id)
        {
            return _context.Stocks.AnyAsync(s => s.Id ==id); //Returns true if the stock exists (matches our given id to the id of every stock in the database until it is found (it returns true not because it is == to true, but because it contains a value it would be true!!) (returns null if not found))
        }

        //Method for the update
        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {
            //We first need to search if we actually have that thing to update and track it
            //x would be our parameter where we'd access the Id of the stock we're looking at, and returns true if that Id == to the id of our Route"[id]" that was sent in the url for what we want to update in this method (so if it's true, then stockModel will be == to the stock in our database that is identified by that Id!!)
            var existingStock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            
            //If it doesn't exist, return null
            if (existingStock == null) {
                return null;
            }

            //If we were able to actually retrieve an existing stock to update, let's update it then!
            //Modify and map updates straight on the method here instead of outside it as entity framework starts tracking it right away here when we retireved it by making the stockModel variable above at the beginning(this is where the updates happen!!)
            existingStock.Symbol = stockDto.Symbol;
            existingStock.CompanyName = stockDto.CompanyName;
            existingStock.Purchase = stockDto.Purchase;
            existingStock.LastDiv = stockDto.LastDiv;
            existingStock.Industry = stockDto.Industry;
            existingStock.MarketCap = stockDto.MarketCap;

            //Save the updated changes to save those updates in the database
            await _context.SaveChangesAsync();
            return existingStock;
        }
    }
}