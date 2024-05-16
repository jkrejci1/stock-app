import { ChangeEvent, SyntheticEvent, useState } from 'react';
import './App.css';
import CardList from './Components/CardList/CardList'; //Going to use a card list now to set as all of our cards instead of just one
import Search from './Components/Search/Search';
import { searchCompanies } from './api';
import { CompanySearch } from './company';

 function App() {
  //Bring in the card into our app PRACITCE

    //Use generic for useState to require type checking
    //The state is stored for current window, but by just doing this, it dissapears after the page is refreshed
    const [search, setSearch] = useState<string>(""); //NEED STATE HERE FOR PROPER DATA PASSING FOR EVENT HANDELING FUNCTIONS BELOW
    
    //State to store search results, stores in array
    const [searchResult, setSearchResult] = useState<CompanySearch[]>([])

    //Server error state POSSIBLY
    const [serverError, setServerError] = useState<string>("")

    //MAKE PROPS FOR DATA THAT NEEDS TO BE PASSED DOWN (LIKE WITH EVENTS) PUT EVENT HANDELING FUNCTIONS HERE FOR PROPER DATA PASSING
    //The click function for our search
    //any keyword allows anything to go into function, don't use that e: any
    //Need to get the event type (Go down to the e and copy the event type when highlighting over it)
    const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
        setSearch(e.target.value);
        console.log(e);
    };


    //Create function for the onClick, or oyu can use syntheticEvent as a check, which is used for auto type checking with events
    const onClick = async (e: SyntheticEvent) => {
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
  return (
    <div className="App">
      <Search onClick={onClick} search={search} handleChange={handleChange} />
      {serverError && <h1>{serverError}</h1>}
      <CardList />
    </div>
  );
}

export default App;
