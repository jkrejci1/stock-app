using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;
        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        //Creates a comment
        public async Task<Comment> CreateAsync(Comment commentModel)
        {
            //Create the comment when given the full model, and save chabges in the database
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();

            //Return the comment we created as confirmation that the code worked
            return commentModel;
        }

        //Deletes comment by its id
        public async Task<Comment?> DeleteAsync(int id)
        {
            //See if the comment exists by its id (it will either be == to the stock that has that id, or it will be null)
            var commentModel = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);

            //Check to make sure if it exists
            if(commentModel == null) 
            {
                return null;
            }

            //If the comment existed, delete it and save the database
            _context.Comments.Remove(commentModel);
            await _context.SaveChangesAsync();

            //Return the commentModel to confirm that it was deleted
            return commentModel;
        }

        //Gets all comments and puts in list (need the "Include()" part for adding the username to the comment for the one to one relationship because of differed execution)
        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.Include(a => a.AppUser).ToListAsync();
        }

        //Gets a comment by its id
        public async Task<Comment?> GetByIdAsync(int id)
        {
            //Return all the comments that match the id
            //FirstOrDefault async is an ASP.NET extension for digging into the database remember!
            return await _context.Comments.Include(a => a.AppUser).FirstOrDefaultAsync(c => c.Id == id); //Get the comment that has the id matching the given id in the parameter (switched to using FirstOrDefault here so we can use our one to one model relationship with including the user that created the comment (before we used FindAsync(id)))
        }

        //Method for updating a comment, and we find the comment to update using its id
        public async Task<Comment?> UpdateAsync(int id, Comment commentModel)
        {
            //Find the comment by its id
            var existingComment = await _context.Comments.FindAsync(id);

            if (existingComment == null)
            {
                return null;
            }

            //If we found the comment by its id, switch its title and content to what the user will change it to
            existingComment.Title = commentModel.Title;
            existingComment.Content = commentModel.Content;

            //Save the updated changes
            await _context.SaveChangesAsync();

            //Return the comment in its new form as validation
            return existingComment;
        }
    }
}