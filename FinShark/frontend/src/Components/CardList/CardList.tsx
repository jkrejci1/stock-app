import React, { SyntheticEvent } from "react";
import { v4 as uuidv4 } from "uuid";
import { CompanySearch } from '../../company';
import Card from '../Card/Card';


//Need to tell our file that we are recieving search results to get rid of error on app.tsx
interface Props {
  searchResults: CompanySearch[];
  onPortfolioCreate: (e: SyntheticEvent) => void
}

//Create CarList component with its type being declared as a component (React.FC (Functional Component)) which takes in our Props data from above
//We will then have the parameters from our Props interface be brought into this component data (called searchResults here) and they would then be of type Props as that's the name of our interface above
//Use JSX>Element to express that we'll have some return type
const CardList : React.FC<Props> = ({ searchResults, onPortfolioCreate }: Props) : JSX.Element => {
  //Iterate through our given cards, need a key to rerender lists
  return ( 
  <>
    {searchResults.length > 0 ? (
      //result would be each individual element/object in the array of CompanySearch[] --> Basically the same as iterating through an array i times and result would be your i data like from i = 0 i++ iterate through array -> i would be result in this instance
      searchResults.map((result) => {
        return (
          <Card 
          id={result.symbol} 
          key={uuidv4()} 
          searchResult={result} 
          onPortfolioCreate={onPortfolioCreate}
          />
        );
      })
    ) : (
      <h1>No results BRO</h1>
    )}
  </>
  )
}

export default CardList