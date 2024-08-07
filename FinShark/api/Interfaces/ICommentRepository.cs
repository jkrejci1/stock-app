using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync(); //Gets all the comments
        Task<Comment?> GetByIdAsync(int id); //Function will be optional, and will get comments by id
        Task<Comment> CreateAsync(Comment commentModel); //Creates a comment
    }
}