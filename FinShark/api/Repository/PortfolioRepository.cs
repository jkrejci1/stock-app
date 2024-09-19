using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDBContext _context;
        public PortfolioRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        //Add the new portfolio to the portfolio database and save it
        public async Task<Portfolio> CreateAsync(Portfolio portfolio)
        {
            await _context.Portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();
            return portfolio; //Return the portfolio for confirmation
        }

        //Method for deleting a portfolio
        public async Task<Portfolio> DeletePortfolio(AppUser appUser, string symbol)
        {
            //Grab the portfolio by checking if the symbol mathces the user ID and the stock symbol
            //Grabs AppUserIds from each object in our database and compares it to the appUser's ID that we sent over here as a parameter
            var portfolioModel = await _context.Portfolios.FirstOrDefaultAsync(x => x.AppUserId == appUser.Id && x.Stock.Symbol.ToLower() == symbol.ToLower());

            //Null check
            if(portfolioModel == null)
            {
                return null;
            }

            //If it exists remove it and save the database
            _context.Portfolios.Remove(portfolioModel);
            await _context.SaveChangesAsync();
            return portfolioModel; //Return the portfolio model as a check to show that it worked.
        }

        //Method for getting the full portfolio for the specified user
        public async Task<List<Stock>> GetUserPortfolio(AppUser user)
        {
            //Have the stock(s) that should be a part of the current user's portfolio show up on their portfolio page
            return await _context.Portfolios.Where(u => u.AppUserId == user.Id)
            .Select( stock => new Stock{ //Select is like a map in JS where it will iterate over it, apply the changes to it, and return a new data structure
                Id = stock.StockId,
                Symbol = stock.Stock.Symbol,
                CompanyName = stock.Stock.CompanyName,
                Purchase = stock.Stock.Purchase,
                LastDiv = stock.Stock.LastDiv,
                Industry = stock.Stock.Industry,
                MarketCap = stock.Stock.MarketCap
            }).ToListAsync();
        }
    }
}