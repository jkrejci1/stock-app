//Use tsfrafce snippet to get it started quickly

//We need to import even local images and files and classes etc using common pathing
import { SyntheticEvent } from "react";
import { CompanySearch } from "../../company";
import AddPortfolio from "../Portfolio/AddPortfolio/AddPortfolio";
 
//Do checks on the data to make sure it's correct (The data tha'll be passed for the card)
interface Props {
  id: string;
  searchResult: CompanySearch;
  onPortfolioCreate: (e: SyntheticEvent) => void;
}


//Practice card (Pass the interface prop data down)
const Card: React.FC<Props> = ({ id, searchResult, onPortfolioCreate }: Props) : JSX.Element => {
  //Put variables carrying the data in {}'s in the HTML/React <>'s
  return (
    <div className="card">
        <img
            alt="company logo"
        />
        <div className="details">
            <h2>{searchResult.name} ({searchResult.symbol})</h2>
            <p>{searchResult.currency}</p>
        </div>
        <p className="info">
            {searchResult.exchangeShortName} - {searchResult.stockExchange}
        </p> 
        
        {/** Add button component for adding stocks to portfolio */}
        <AddPortfolio 
        onPortfolioCreate={onPortfolioCreate} 
        symbol={searchResult.symbol}
        />

    </div> 
  )
}

export default Card