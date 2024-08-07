using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Interfaces;
using api.Mappers;
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
        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo)
        {
            //Make our private variable equal to the data in commentRepo from our comment repo interface
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
        }

        //Get method for getting comments
        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var comments = await _commentRepo.GetAllAsync();

            //Like a JS map, returns a new data structure instead of manipulating the actual one
            var commentDto = comments.Select(s => s.ToCommentDto());

            return Ok(commentDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
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
        [HttpPost("{stockId}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, CreateCommentDto commentDto) 
        {
            //If the stock doesn't exist for this comment, cancel out of it
            if(!await _stockRepo.StockExists(stockId)) {
                return BadRequest("Stock doesn't exist!"); //If we couldn't find a stock with the given stockId, then that stock doesn't exist
            }

            //If we found a stock create a stock while using the data from our dto and putting it in the full version of the comment model to be added to our database tied to a stock
            var commentModel = commentDto.ToCommentFromCreate(stockId);
            await _commentRepo.CreateAsync(commentModel);

            //Return the DTO version of the comment, we will find it by its id
            return CreatedAtAction(nameof(GetById), new { id = commentModel }, commentModel.ToCommentDto());
        }
    }
}