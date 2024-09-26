using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Helpers;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync(CommentQueryObject queryObject); //Gets all the comments from a given query
        Task<Comment?> GetByIdAsync(int id); //Function will be optional, and will get comments by id
        Task<Comment> CreateAsync(Comment commentModel); //Creates a comment
        Task<Comment?> UpdateAsync(int id, Comment commentModel); //Updates a current comment by finding it by its id
        Task<Comment?> DeleteAsync(int id); //Delete interface method
    }
}