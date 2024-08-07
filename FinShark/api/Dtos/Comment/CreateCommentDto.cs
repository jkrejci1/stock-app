using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Comment
{
    //When creating a comment all you will need is the comment content and the title
    public class CreateCommentDto
    {
        public string Title { get; set; } = string.Empty; //Title for our comments
        public string Content { get; set; } = string.Empty; //Content
    }
}