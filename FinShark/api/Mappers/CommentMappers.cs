using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Models;

namespace api.Mappers
{
    public static class CommentMappers
    {
        //Mapper for reading
        public static CommentDto ToCommentDto(this Comment commentModel) {
            return new CommentDto {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                StockId = commentModel.StockId
            };
        }

        //Mapper for creating comment --> where we put in the title, content, and stockId ourselves, and everything else is handled automatically
        //Id, and createdOn get's created automatically under the hood, so what's important is the title, content, and the StockId we need to match the comment to a stock with
        public static Comment ToCommentFromCreate(this CreateCommentDto commentDto, int stockId) {
            return new Comment {
                Title = commentDto.Title,
                Content = commentDto.Content,
                StockId = stockId
            };
        }

        //Mapper for doing the update of a comment. The only changeable data would be the same as the data that we would need from the user themself to created one, like the mapper method above
        public static Comment ToCommentFromUpdate(this UpdateCommentRequestDto commentDto, int stockId)
        {
            return new Comment
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
                StockId = stockId
            };
        }
    }
}