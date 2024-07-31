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
    //This class only has abstract methods for what it's gonna be used for --> (type) (methodName)();
    //These methods only have a body in the implemented class (in our case, the actual repository (StockRepository.cs here))
    //INTERFACES BY THEMSELVES CAN'T BE USED TO CREATE OBJECTS UNLIKE REGULAR CLASSES (interface is like an abstract class then)
        //On implementation of an interface, you must override all of its methods
        //Interfaces can contain properties and methods, but not fields/variables
        //Interface members are by default abstract and public
        //An interface cannot contain a constructor (as it cannot be used to create objects)
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(); //Says that when using this interface we'll need to use the GetAllAsync method to return something (list of stocks here) the actual code that will do that is in the repository
        Task<Stock?> GetByIdAsync(int id); //? == used for first or defaults as they have the possibility to be null. Notice it also should take in a parameter called which will be assigned to the variable --> id
        Task<Stock> CreateAsync(Stock stockModel);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto);
        Task<Stock?> DeleteAsync(int id);
    }
}