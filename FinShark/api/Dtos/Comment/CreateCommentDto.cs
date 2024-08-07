using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Comment
{
    //When creating a comment all you will need is the comment content and the title
    public class CreateCommentDto
    {
        //Create data validation (connected to the next table data 1 at a time so this will be for title)
        [Required]
        [MinLength(5, ErrorMessage = "Title must be 5 characters")]
        [MaxLength(280, ErrorMessage = "Title can only have up to 280 characters")]
        public string Title { get; set; } = string.Empty; //Title for our comments
        
        //Validation for content
        [Required]
        [MinLength(5, ErrorMessage = "Content must be 5 characters")]
        [MaxLength(280, ErrorMessage = "Content can only have up to 280 characters")]
        public string Content { get; set; } = string.Empty; //Content
    }
}