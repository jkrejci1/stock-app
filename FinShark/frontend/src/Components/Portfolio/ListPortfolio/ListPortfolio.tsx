import CardPortfolio from "../CardPortfolio/CardPortfolio";

interface Props {
    portfolioValues: string[];
}

const ListPortfolio = ({portfolioValues}: Props) => {
  return ( 
    <>
        <h3>My Portfolio</h3>
        <ul>
            {/** If we have portfolio data, iterate through it --> is what this statement below is saying with the &&'s */}
            {portfolioValues && 
                portfolioValues.map((portfolioValue) => {
                    return <CardPortfolio portfolioValue={portfolioValue}/>; {/** Pass a single portfolio stock data to the card portfolio component */}
            })}
        </ul>
    </>
  )
}

export default ListPortfolio