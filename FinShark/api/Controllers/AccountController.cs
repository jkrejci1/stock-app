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
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signinManager;

        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signinManager = signInManager;
        }

        //Post method for logging a current user in
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            //Find the user
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

            //Check if we have user
            if (user == null) return Unauthorized("Invald username!");

            //If we have a user, sign in the user
            var result = await _signinManager.CheckPasswordSignInAsync(user, loginDto.Password, false); //False is for lockout on failure, we turned it off for now

            //If we failed to login
            if(!result.Succeeded) return Unauthorized("Username not found and/or password incorrect!");

            //
            return Ok(
                new NewUserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user)
                }
            );
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
                        return Ok(
                            //Return data of the user and their token when registered
                            new NewUserDto
                            {
                                UserName = appUser.UserName,
                                Email = appUser.Email,
                                Token = _tokenService.CreateToken(appUser)
                            }
                        );
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