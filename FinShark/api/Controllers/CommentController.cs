using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly ICommentRepository _commentRepo;
        public CommentController(ICommentRepository commentRepo)
        {
            //Make our private variable equal to the data in commentRepo from our comment repo interface
            _commentRepo = commentRepo;
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
    }
}