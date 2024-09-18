using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Extensions;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/portfolio")]
    [ApiController] //Attribute
    public class PortfolioController : ControllerBase
    {
        //Private variables
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepo;
        private readonly IPortfolioRepository _portfolioRepo;

        public PortfolioController(UserManager<AppUser> userManager,
        IStockRepository stockRepo, IPortfolioRepository portfolioRepo)
        {
            _userManager = userManager;
            _stockRepo = stockRepo;
            _portfolioRepo = portfolioRepo;
        }

        //Get the portfolio data for the user
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUsername(); //User object allows you to reach in and grab what's in the user from token data (the token data you'd see in inspecting the code on the webpage, if you console logged the token there you would see all sorts of that kind of data)
            var appUser = await _userManager.FindByNameAsync(username); //Get the user
            var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);//Find the user portfolio by going into the DB and pull the records associated with the user that's logged in from the data we got from the claims, then return the stocks associated with that user

            return Ok(userPortfolio); //Now return the user portfolio data!
        }

        //Create a portfolio with the stock and user IDs
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol)
        {
            //Get the user and stock
            var username = User.GetUsername(); //Claims from current JWT token
            var appUser = await _userManager.FindByNameAsync(username);
            var stock = await _stockRepo.GetBySymbolAsync(symbol);

            //Check if the stock is even there, and [Authorize] will automatically do that with the user
            if(stock == null) return BadRequest("Stock not found!");

            //Get the user portfoli
            var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);

            //See if the stock already exists in the portfolio to prevent duplicates
            if(userPortfolio.Any(e => e.Symbol.ToLower() == symbol.ToLower())) return BadRequest("Stock already exists!");

            //If we got here then we went through the checks okay and we can create the portfolio now!
            var portfolioModel = new Portfolio //Do the method of using the stock and user IDs in the join table to get proper data with proper connections
            {
                StockId = stock.Id,
                AppUserId = appUser.Id
            };

            //Create the portfolio and do a null check to see if it worked or not
            await _portfolioRepo.CreateAsync(portfolioModel);
            if(portfolioModel == null)
            {
                return StatusCode(500, "Could not create!");
            }
            else
            {
                return Created();
            }
        }
    }
}