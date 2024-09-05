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
            var username = User.GetUsername(); //User object allows you to reach in and grab what's in the user from token data
            var appUser = await _userManager.FindByNameAsync(username); //Get the user
            var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);//Find the user portfolio by going into the DB and pull the records associated with the user that's logged in from the data we got from the claims, then return the stocks associated with that user

            return Ok(userPortfolio); //Now return the user portfolio data!
        }
    }
}