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
using api.Data; //Brings in the datbase for ApplicationDBContext
using api.Mappers; //Brings in toStockDto and other functions from mappers to be used here
using Microsoft.AspNetCore.Mvc;
using api.Dtos.Stock; //Need to bring this in to use our Dtos so we can fully use everything involving them
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using api.Interfaces; //Bring in interface folder data

//REMEMBER TO NEVER FORGET ;'s OR YOU MIGHT GET AN ERROR USING "dotnet watch run"
namespace api.Controllers
{
    [Route("api/stock")] //This is where we would be in the database as this is for the stock controller
    [ApiController] //What allows us to use the HTTP request stuff and response stuff for HTTP requests
    public class StockController : ControllerBase
    {
        //Make read only to prevent it being mutable when running
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepo; //Brings in our repository interface that will have methods to use

        //Costructor --> Brings in database by brining in DbContext and setting our database data to a variable called context
        //Brings in the files as we are already using them in the namespace, and then constructs our empty _context and _stockRepo into the classes themselves
        public StockController(ApplicationDBContext context, IStockRepository stockRepo)
        {
            _stockRepo = stockRepo; //Bring in the stock repository interface
            //So then _context will be our private variable that contains our db data so to access the Stocks table we do _context.Stocks, and to access the comments table we do _context.Comments, etc
            _context = context; //Set our private _context to the ApplicationDBContext value
        }

        //The call starts right here and then whatever is in our IActionResult after is what we do for it
        //Get is a read --> getting it out of a DB and r
        //This will be for getting every stock we need
        [HttpGet] //Putting this here marks the next function (IActionResult) as an HttpGet method which is why we put this here
        //Action result is for handeling returning api endpoint stuff and knowing that we are doing that easier
        public async Task<IActionResult> GetAll() {
            //.ToList() is needed because of deffered execution (the evaluation of this is delayed until its realized value is actually required to improve performance by preventing unnecessary execution)
            //So this action is only executed when we need stocks for something when using data, not when this code itself here executes but if we call stocks somewhere else
            var stocks = await _stockRepo.GetAllAsync(); //Access Stocks value in ApplicationDBContext using async to access data in the background of our program running (using the according method in our Repository)
            var stockDto = stocks.Select(s => s.ToStockDto()); //.NETs version of a map like in React --> returns a immutable array of the ToStockDto. So then we get a list of data according to our dto mapping of what exactly should and shouldn't be returned. (REQUIRED TO USE SELECT IF YOU WANT TO GET DATA IN A LIST/ARRAY ACCORDING TO A DTO)!! (SO s == each individual stock table data where s == 0 will be the first one and s == 1 will be the second one and so on like in a for loop and will put them all in a list called stocks--> then we will take each s (stock data) and transform the data using the dto we want to use for this situation to make sure we get the NEEDED data only)
            
            return Ok(stocks);
        }

        //This will return one actual item unlike above as it selects a specific item by its original ID value
        //This will get one specific stock to go through the specific details for that one stock itself on its own page
        [HttpGet("{id}")] //>NET will take this, turn it into an int below, and then we can use it below in our actual code
        //I action result is a return method --> wrapper so that when you return something from the api you dont have to go through to much coding to tell someone what status of their endpoint is or errors like http errors
        public async Task<IActionResult> GetById([FromRoute] int id) {  //This takes the id from above and turns it into an int
            var stock = await _stockRepo.GetByIdAsync(id); //Searches in table by the id value

            //Null Check to see if stock is null (dont have something for the requested stock)
            if (stock == null) 
            {
                return NotFound(); //Return notfound if the stock doesn't exist
            }

            return Ok(stock.ToStockDto()); //If we have on return it to the front end as a ToStockDto mapping format (because you are in Stocks --> we extended Stock objects to use the ToStockDto method in our stockMappers file which we imported above using the "using api.mappers")
        }

        //Here is our POST for getting stock data in the DB
        [HttpPost]
        //Need this from body as our data is going to be sent in JSON and use our dto as there is some data that we wouldn't want from the user (like our database assigns an ID for a stock, so we wouldn't want the user to send data for the ID as that wouldn't make sense and could cause errors)
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto) { //Unlike above we aren't gonna have all this JSON be passed in the URL, that wouldn't make sense, instead it will be passed in the body of the HTTP (like in HTTP requests (used this in JavaScript projects before when passing user data to MongoDB with their accounts and saved data to those accounts)) so select that data using '[FromBody]'
            //Take stockModel and have it == to our stockDto and use the method for it that transforms it back into a stock model using our mapper for putting data in the database
            var stockModel = stockDto.ToStockFromCreateDTO(); //Like in the GetById, we are using api.mappers, where we made an extension to createStockRequestDto to have the method ToStockFromCreateDTO extended to it which is why we can use it her
            await _stockRepo.CreateAsync(stockModel);

            //It's going to use the GetById method we created above and pass the ID from our newly created stockModel variable as an object to the URL "{id}" parameter and then the data will return as a DTO version of the stockModel data
            //WE ARE ABLE TO FIND THE ID WITHOUT ASSIGNING IT BECAUSE SQL SERVER AUTOMATICALLY ASSIGNS AN ID ITSELF FROM HOW WE CREATED OUR MODELS REMEMBER (like when you manually add data into sql server when editing top 200 rows, it block you from entering an ID and then when you put in the rest of the data and press enter, it will add that data and create an ID for it for the public key itself!!) Then it returns as the type of a ToStockDto
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto()); //This doesn't create and ID itself, we use stockModel's created Id to use the GetById method above to show that we have succesfully added our data
        }

        //
        //ID goes with the route, and we also got our body to work with
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto) {
            //We first need to search if we actually have that thing to update and track it
            //x would be our parameter where we'd access the Id of the stock we're looking at, and returns true if that Id == to the id of our Route"[id]" that was sent in the url for what we want to update in this method (so if it's true, then stockModel will be == to the stock in our database that is identified by that Id!!)
            var stockModel = await _stockRepo.UpdateAsync(id, updateDto);

            //If that stock doesn't exist, we don't want to do anything
            if(stockModel == null) {
                return NotFound();
            }

            return Ok(stockModel.ToStockDto());

        }

        //Delete api endpoint method for deleting a stock by its ID
        [HttpDelete]
        [Route("{id}")] //Need a route in order to find something remember
        public async Task<IActionResult> Delete([FromRoute] int id) {
            //Make sure that this stock exists firsts
                                                            //x where x.id is == id
            var stockModel = await _stockRepo.DeleteAsync(id);

            //If it doesn't exist, don't do anything
            if(stockModel == null) {
                return NotFound();
            }

            return NoContent(); //No content gives us a status 204 code which means that the delete was a success

        }
    }
}