using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Extensions;
using api.Interfaces;
using api.Models;
using api.Mappers;
using api.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")] //Specifies route for our controller (comment --> name of our controller (NOT NAME OF A DIRECTORY)), and also If a matching route is found, Web API selects the appropriate controller and action.
                                //For example, if you have a controller named FolderController, the route "api/folder" would map to that controller.
                                //The action method within the controller would then handle the request.
    [ApiController] //Enables features that we want to use for our API
    public class CommentController : ControllerBase
    {
        //Make these private variables when working with our datbase to keep it hidden when working with it in the backend
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        private readonly UserManager<AppUser> _userManager; ///Bring in userManager to display a username with the comment of the matching user
        private readonly IFMPService _fmpService;

        public CommentController(ICommentRepository commentRepo,
        IStockRepository stockRepo, UserManager<AppUser> userManager,
        IFMPService fmpService)
        {
            //Make our private variable equal to the data in commentRepo from our comment repo interface
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
            _userManager = userManager;
            _fmpService = fmpService;
        }

        //Get method for getting comments
        [HttpGet]
        public async Task<IActionResult> GetAll() {

            //Checks if our validation is correct in our dtos before running our code
            //ModelState is inhereting from controller base object
            //We might not have it in gets, but it's good practice to keep it incase you want validation later
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var comments = await _commentRepo.GetAllAsync();

            //Like a JS map, returns a new data structure instead of manipulating the actual one
            var commentDto = comments.Select(s => s.ToCommentDto()); //For every object in the database we are getting we will turn it into the DTO version for our display

            return Ok(commentDto);
        }

        [HttpGet("{id:int}")] //The :int --> Checks if the data given really is an int for data checking purposes
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            //Checks if our validation is correct in our dtos before running our code
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            //Use our method from our comment repo file
            var comment = await _commentRepo.GetByIdAsync(id);

            //If the id for the comment wasn't there it doesn't exist
            if (comment == null) {
                return NotFound();
            }

            //Return comment when found
            return Ok(comment.ToCommentDto());
        }

        //Post for creating comments (remember we will also need the stockId for the whichever stock the comment is going to be on!)
        [HttpPost]
        [Route("{symbol:alpha}")]
        public async Task<IActionResult> Create([FromRoute] string symbol, CreateCommentDto commentDto) 
        {
            //Checks if our validation is correct in our dtos before running our code
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            //Check for the actual stock
            var stock = await _stockRepo.GetBySymbolAsync(symbol);

            if(stock == null)
            {
                stock = await _fmpService.FindStockBySymbolAsync(symbol);
                if(stock == null) //If it fails
                {
                    return BadRequest("This stock does not exist!");
                }
                else //If it works
                {
                    await _stockRepo.CreateAsync(stock);
                }
            }
                
            //If the stock doesn't exist for this comment, cancel out of it
            /**
            if(!await _stockRepo.StockExists(stockId)) {
                return BadRequest("Stock doesn't exist!"); //If we couldn't find a stock with the given stockId, then that stock doesn't exist
            }
            */

            //Go get our username from the claims to match it to the created comment
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            //If we found a stock create a stock while using the data from our dto and putting it in the full version of the comment model to be added to our database tied to a stock
            var commentModel = commentDto.ToCommentFromCreate(stock.Id);
            commentModel.AppUserId = appUser.Id; //Place our appUser Id that we got in the commentModel
            await _commentRepo.CreateAsync(commentModel);

            //Return the DTO version of the comment, we will find it by its id
            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
        }

        //Method for updating a comment
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateDto)
        {
            //Checks if our validation is correct in our dtos before running our code
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
                
            //See if you can find the comment to update
            var comment = await _commentRepo.UpdateAsync(id, updateDto.ToCommentFromUpdate());

            if (comment == null)
            {
                return NotFound("Comment not found");
            }

            //If it was possible, it would be updated, and we return the current comment as a dto as validation
            return Ok(comment.ToCommentDto());
        }

        //Delete method for deleting comments
        [HttpDelete]
        [Route("{id:int}")] //Need the id to delete it
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            //Checks if our validation is correct in our dtos before running our code
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
                
            //If the commentModel existed it will be deleted and return a val, if not it will return null
            var commentModel = await _commentRepo.DeleteAsync(id);

            //If it didn't exist, return a NotFound() error message
            if(commentModel == null)
            {
                return NotFound("Comment does not exist!");
            }

            //If it existed and was deleted, then return that model for confirmation
            return Ok(commentModel);
        }
    }
}