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
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        //Make read only to prevent it being mutable when running
        private readonly ApplicationDBContext _context;

        //Costructor
        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }

        //Get is a read --> getting it out of a DB and r
        [HttpGet]
        public IActionResult GetAll() {
            //.ToList() is needed because of deffered execution
            var stocks = _context.Stocks.ToList();

            return Ok(stocks);
        }

        //This will return one actual item unlike above
        [HttpGet("{id}")]

        //I action result is a return method --> wrapper so that when you return something from the api you dont have to go through to much coding to tell someone what status of their endpoint is or errors like http errors
        public IActionResult GetById([FromRoute] int id) {  //This takes the id from above and turns it into an int
            var stock = _context.Stocks.Find(id); //Searches in table by the id value

            //Null Check to see if stock is null (dont have something)
            if (stock == null) 
            {
                return NotFound(); //Return notfound if the stock doesn't exist
            }

            return Ok(stock); //If we have on return it
        }
    }
}