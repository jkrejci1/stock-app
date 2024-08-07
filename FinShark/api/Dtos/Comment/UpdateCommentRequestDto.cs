using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; //Used for data checking
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Comment
{
    public class UpdateCommentRequestDto
    {
        //Data val for title
        [Required]
        [MinLength(5, ErrorMessage = "Title must be 5 characters")]
        [MaxLength(280, ErrorMessage = "Title cannot be over 280 characters")]
        public string Title { get; set; } = string.Empty;

        //Data val for content
        [Required]
        [MinLength(5, ErrorMessage = "Content must be 5 characters")]
        [MaxLength(280, ErrorMessage = "Content can only have up to 280 characters")]
        public string Content { get; set; } = string.Empty;
    }
}