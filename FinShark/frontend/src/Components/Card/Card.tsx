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
    <div
      className="flex flex-col items-center justify-between w-full p-6 bg-slate-100 rounded-lg md:flex-row"
      key={id}
      id={id}
    >
      <h2 className="font-bold text-center text-black md:text-left">
        {searchResult.name} ({searchResult.symbol})
      </h2>
      <p className="text-black">{searchResult.currency}</p>
      <p className="font-bold text-black">
        {searchResult.exchangeShortName} - {searchResult.stockExchange}
      </p>
      <AddPortfolio
        onPortfolioCreate={onPortfolioCreate}
        symbol={searchResult.symbol}
      />
    </div>
  )
}

export default Card