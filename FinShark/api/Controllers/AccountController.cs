//Controller for authentication
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Account;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace api.Controllers
{
    [Route("api/account")] //Assigns the route
    [ApiController] //Add the api controller
    public class AccountController : ControllerBase
    {
        //UserManager == contains the required methods to manage users in the underlying data store
        private readonly UserManager<AppUser> _userManager;

        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        //Post method for registering a user to our database
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            //Wrap in try catch for server errors
            try {
                //Validation error check first
                if(!ModelState.IsValid)
                return BadRequest(ModelState);

                //Create a user object with the new users username and email from our dto
                var appUser = new AppUser
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email,
                };

                //Now create the user using user namanger, and send the password through here so it is hashed and salted with the CreateAsync method
                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

                //createUser will return an object that will allow you to do checks
                if(createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User"); //Anybody who is assigned through the user endpoint will be assigned the user role which will only give them user priviliges (For admin priviliges you should add that manually or from another created point that isn't public!!)
                    
                    //If we successfully created the user and their role (privliges), then return an ok message, if not, return error message
                    if(roleResult.Succeeded)
                    {
                        return Ok("User Created");
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }

                }
                else
                {
                    return StatusCode(500, createdUser.Errors); //If the user failed to be created, return error message
                }
            } catch (Exception e)
            {
                return StatusCode(500, e); //If any other error happens, return that error message
            }
        }
    }
}