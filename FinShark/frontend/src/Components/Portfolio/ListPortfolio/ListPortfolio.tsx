import { SyntheticEvent } from "react";
import CardPortfolio from "../CardPortfolio/CardPortfolio";

interface Props {
    portfolioValues: string[];
    onPortfolioDelete: (e: SyntheticEvent) => void;
}

const ListPortfolio = ({ portfolioValues, onPortfolioDelete}: Props) => {
  return ( 
    <>
        <h3>My Portfolio</h3>
        <ul>
            {/** If we have portfolio data, iterate through it --> is what this statement below is saying with the &&'s */}
            {portfolioValues && 
                portfolioValues.map((portfolioValue) => {
                    return <CardPortfolio portfolioValue={portfolioValue} onPortfolioDelete={onPortfolioDelete}/>; {/** Pass a single portfolio stock data to the card portfolio component */}
            })}
        </ul>
    </>
  )
}

export default ListPortfolio