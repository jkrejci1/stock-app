using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Dtos.Stock;
using api.Models;

//Use mapper with dto to trim out certain things you may not want when getting data
//We need the mapper and the dto to work together to do what we want to do to trim out the amount of data we want to grab from the database
namespace api.Mappers
{
    public static class StockMappers
    {
        //This will be used for a response DTO
        //This function will trim out just the comments for reading data
        //Takes the stock model data from the StockDto and creates a new object called StockDto that sets all of its own object values to the values we want from the dto then returns it to be used in the controller
        //ALSO VERY IMPORTANT because ToStockDto is now an extension of Type Stock from what we did here, we can 'dot' into it using this ToStockDto function by doing Stock (variableNameOfStock).ToStockDto like in the controller when we do it
        public static StockDto ToStockDto(this Stock stockModel) //Function of type StockDto called ToStockDto which extends the Stock Model itself and calls the variable for it stockModel(then we can use the Stock Model to use data that we would want to pass back specifically (trim down what we extended over from the Stock model))
        {
            return new StockDto 
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
                Comments = stockModel.Comments.Select(c => c.ToCommentDto()).ToList() //Get the comment tables and set them to the DTO version
            };
        }

        //This will be used a request DTO
        //This function will trim out the comments and id for posting new stock data in our database
        //When we send data to the database it has to be of type of the database as it wouldn't be a dto type
        //When sending data to the model with dto, type the dto as the same name as the model you're sending said data to
        //VERY IMPORTANT because ToStockFromCreateDTO is an extension of the type CreateStockRequestDto we can 'dot' into it using whatever would be your CreateStockRequestDto typed variable like in the controller when we do it
        public static Stock ToStockFromCreateDTO(this CreateStockRequestDto stockDto) //Extends the CreateStockRequestDto in Dtos-->CreateStockRe... and call it stockDto
        {
            //The type of the method is the Model itself when pushing/updating data, and it's the DTO when getting data it seems
            return new Stock //This object will be added to our Stock Model as a seperate stock
            {
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                Purchase = stockDto.Purchase,
                LastDiv = stockDto.LastDiv,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap
            };
        }
    }
}