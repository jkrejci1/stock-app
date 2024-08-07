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

        //Gets all comments and puts in list
        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        //Gets a comment by its id
        public async Task<Comment?> GetByIdAsync(int id)
        {
            //Return all the comments that match the id
            return await _context.Comments.FindAsync(id);
        }
    }
}