import { SyntheticEvent } from "react";
import { Link } from "react-router-dom";
import DeletePortfolio from "../DeletePortfolio/DeletePortfolio";

//A single card for the portfolio
interface Props {
    portfolioValue: string;
    onPortfolioDelete: (e: SyntheticEvent) => void;
}

const CardPortfolio = ({ portfolioValue, onPortfolioDelete }: Props) => {
  return ( 
    <div className="flex flex-col w-full p-8 space-y-4 text-center rounded-lg shadow-lg md:w-1/3">
      {/* Let the portfolio valu also be clickable to show stock data and send the portfolio value to the company page through the route so we can access it there */}
      <Link to={`/company/${portfolioValue}/company-profile`} className="pt-6 text-xl font-bold">{portfolioValue}</Link>
      <DeletePortfolio
        portfolioValue={portfolioValue}
        onPortfolioDelete={onPortfolioDelete}
      />
    </div>
  )
}

export default CardPortfolio