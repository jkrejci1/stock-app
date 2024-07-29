using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//Need to bring in proper data
using api.Dtos.Stock;
using api.Models;

//Interface for allowing us to plug in certain code to other places abstracting our code away
//The interface allows the controller to interact with the repository itself, like a mediator
//Purpose is so that we can plug in our IStockRepository to our repository itself and implement the database there
//The interface is the contract for the repository (the repository using it must take in all the abstract methods and use them as it is the contract YOU HAVE TO USE THIS CODE IF IMPLEMENTED)
namespace api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(); //Says that when using this interface we'll need to use the GetAllAsync method to return something (list of stocks here) the actual code that will do that is in the repository
    }
}