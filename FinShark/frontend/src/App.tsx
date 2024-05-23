import { ChangeEvent, SyntheticEvent, useState } from 'react';
import './App.css';
import CardList from './Components/CardList/CardList'; //Going to use a card list now to set as all of our cards instead of just one
import Navbar from './Components/Navbar/Navbar';
import ListPortfolio from './Components/Portfolio/ListPortfolio/ListPortfolio';
import Search from './Components/Search/Search';
import { searchCompanies } from './api';
import { CompanySearch } from './company';

 function App() {
  //Bring in the card into our app PRACITCE

    //Use generic for useState to require type checking
    //The state is stored for current window, but by just doing this, it dissapears after the page is refreshed
    const [search, setSearch] = useState<string>(""); //NEED STATE HERE FOR PROPER DATA PASSING FOR EVENT HANDELING FUNCTIONS BELOW
    
    //Array for portfolio values
    const [portfolioValues, setPortfolioValues] = useState<string[]>([]); //Array of favorite stocks
    
    //State to store search results, stores in array
    const [searchResult, setSearchResult] = useState<CompanySearch[]>([])

    //Server error state POSSIBLY
    const [serverError, setServerError] = useState<string>("")

    //MAKE PROPS FOR DATA THAT NEEDS TO BE PASSED DOWN (LIKE WITH EVENTS) PUT EVENT HANDELING FUNCTIONS HERE FOR PROPER DATA PASSING
    //The click function for our search
    //any keyword allows anything to go into function, don't use that e: any
    //Need to get the event type (Go down to the e and copy the event type when highlighting over it)
    const handleSearchChange = (e: ChangeEvent<HTMLInputElement>) => {
        setSearch(e.target.value);
    };

    const onPortfolioCreate = (e: any) => {
        //Add prevent default to stop a refresh from happening
        e.preventDefault();
        const exists = portfolioValues.find((value) => value === e.target[0].value) //Checks to see if we already got something we were going to add in the spread data (which is why we would use index 0)
        if (exists) return; //If the card already exists, stop here and don't add it

        //console.log(e) TEST
        //Let's add to array of favorite stocks adds existing value and first value in the event from the form
        //Can't use syntheticEvent property for below stuff in updatePortfo, so we turn off the typescript by putting any as the property above instead of syntheticEvent
        const updatedPortfolio = [...portfolioValues, e.target[0].value]
        setPortfolioValues(updatedPortfolio); //Created the new array state
    }

    //Turn off the typescript for this one as adding it has it off to so we might have different types
    const onPortfolioDelete = (e: any) => {
      e.preventDefault();
      const removed = portfolioValues.filter((value) => {
        //We will set removed to a new array where each value that should be added will not be equal to the one we want removed
        return value !== e.target[0].value; //Check if it's not a part of the actual value we'll get through our e
      });
      setPortfolioValues(removed);
    }


    //Create function for the onClick, or oyu can use syntheticEvent as a check, which is used for auto type checking with events
    const onSearchSubmit = async (e: SyntheticEvent) => {
        //Prevent a refresh so it doesn't break
        e.preventDefault();
        //Get the company passing the current search state
        const result = await searchCompanies(search);

        //setSearchResult(result.data) CANT DO LIKE THIS NEED TYPE NARROWING
        //If you hover over result, you can see what the possible types of result could be (axios response returns a promise)
        if (typeof result === "string") { //Notice if we didn't get an array back, then we must've gotten an error, which would be of type string
          //Show the result if error
          //When we arent connecting to the internet we want them to see that, but NOT when we are that'd be ridiculous
          setServerError(result)
        } else if(Array.isArray(result.data)) { //If we got an array, then it worked, which also means we have the correct type as well for the setSearchResult state so we wouldn't get an error. If we didn't check this we'd get a type error
          setSearchResult(result.data)
        }

        console.log(searchResult)

    }


  //For Search, we pass down the needed functions that handle the data using props so we have any unique data we need passed into the Search.tsx file
  //{serverError} code is used so that we only show internet connection status if we aren't able to connect. So use && so we only show connected status when there's an error
  //Pass search results to cardlist to create list
  return (
    <div className="App">
      {/**<Search onClick={onClick} search={search} handleChange={handleChange} />*/}
      <Navbar />
      <Search onSearchSubmit={onSearchSubmit} search={search} handleSearchChange={handleSearchChange}/>
      <ListPortfolio portfolioValues={portfolioValues} onPortfolioDelete={onPortfolioDelete}/>
      <CardList searchResults={searchResult} onPortfolioCreate={onPortfolioCreate}/> 
      {serverError && <h1>{serverError}</h1>}
    </div>
  );
}

export default App;
