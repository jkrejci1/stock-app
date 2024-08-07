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