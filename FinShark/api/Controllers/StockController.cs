//Controllers --> like the dols to the house, need to go through them using URLs/endpoints
/**List vs Detail
List: More of like a feed

Detail: Gives us further details

*/

//The controller for stock (Make on a model by model basis); done to seperate concerns between stock and comments model
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using api.Dtos.Stock; //Need to bring this in to use our Dtos

namespace api.Controllers
{
    [Route("api/stock")] //This is where we would be in the database as this is for the stock controller
    [ApiController]
    public class StockController : ControllerBase
    {
        //Make read only to prevent it being mutable when running
        private readonly ApplicationDBContext _context;

        //Costructor --> Brings in database by brining in DbContext
        public StockController(ApplicationDBContext context)
        {
            _context = context; //Set our private _context to the ApplicationDBContext value
        }

        //The call starts right here and then whatever is in our IActionResult after is what we do for it
        //Get is a read --> getting it out of a DB and r
        //This will be for getting every stock we need
        [HttpGet] //Putting this here marks the next function (IActionResult) as an HttpGet method which is why we put this here
        //Action result is for handeling returning api endpoint stuff and knowing that we are doing that easier
        public IActionResult GetAll() {
            //.ToList() is needed because of deffered execution (the evaluation of this is delayed until its realized value is actually required to improve performance by preventing unnecessary execution)
            //So this action is only executed when we need stocks for something when using data, not when this code itself here executes but if we call stocks somewhere else
            var stocks = _context.Stocks.ToList() //Access Stocks value in ApplicationDBContext
                .Select(s => s.ToStockDto()); //.NETs version of a map like in React --> returns a immutable array of the ToStockDto. So then we get a list of data according to our dto mapping of what exactly should and shouldn't be returned. (REQUIRED TO USE SELECT IF YOU WANT TO GET DATA IN A LIST/ARRAY ACCORDING TO A DTO)!!
            return Ok(stocks);
        }

        //This will return one actual item unlike above as it selects a specific item by its original ID value
        //This will get one specific stock to go through the specific details for that one stock itself on its own page
        [HttpGet("{id}")] //>NET will take this, turn it into an int below, and then we can use it below in our actual code
        //I action result is a return method --> wrapper so that when you return something from the api you dont have to go through to much coding to tell someone what status of their endpoint is or errors like http errors
        public IActionResult GetById([FromRoute] int id) {  //This takes the id from above and turns it into an int
            var stock = _context.Stocks.Find(id); //Searches in table by the id value

            //Null Check to see if stock is null (dont have something for the requested stock)
            if (stock == null) 
            {
                return NotFound(); //Return notfound if the stock doesn't exist
            }

            return Ok(stock.ToStockDto()); //If we have on return it
        }

        //Here is our POST for getting stock data in the DB
        [HttpPost]
        //Need this from body as our data is going to be sent in JSON and use our dto as there is some data that we wouldn't want from the user
        public IActionResult Create([FromBody] CreateStockRequestDto stockDto) {
            var stockModel = stockDto.ToStockFromCreateDTO();
            _context.Stocks.Add(stockModel);
            _context.SaveChanges();

            //It's going to pass the new id into the ID and then it's going to return in the form of a ToStock DTO
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }

        //
        //ID goes with the route, and we also got our body to work with
        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto) {
            //We first need to search if we actually have that thing to update and track it
            var stockModel = _context.Stocks.FirstOrDefault(x => x.Id == id); //We will saerch if this thing we want to update exists by using it's ID (which is also its public key remember)

            //If that stock doesn't exist, we don't want to do anything
            if(stockModel == null) {
                return NotFound();
            }

            //If we were able to actually retrieve an existing stock to update, let's update it then!
            stockModel.Symbol = updateDto.Symbol;
            stockModel.CompanyName = updateDto.CompanyName;
            stockModel.Purchase = updateDto.Purchase;
            stockModel.LastDiv = updateDto.LastDiv;
            stockModel.Industry = updateDto.Industry;
            stockModel.MarketCap = updateDto.MarketCap;

            //Save the updated changes to save those updates in the database
            _context.SaveChanges();
            return Ok(stockModel.ToStockDto());

        }
    }
}