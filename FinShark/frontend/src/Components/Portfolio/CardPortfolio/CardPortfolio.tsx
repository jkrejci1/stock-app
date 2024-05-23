//A single card for the portfolio
interface Props {
    portfolioValue: string;
}

const CardPortfolio = ({portfolioValue}: Props) => {
  return ( 
    <>
        <h4>{portfolioValue}</h4>
        <button>X</button> {/**Delete button */}
    </>
  )
}

export default CardPortfolio