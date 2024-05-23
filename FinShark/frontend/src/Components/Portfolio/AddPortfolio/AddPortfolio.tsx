import { SyntheticEvent } from "react"

interface Props {
    onPortfolioCreate: (e: SyntheticEvent) => void
    symbol: string
}

const AddPortfolio = ({onPortfolioCreate, symbol}: Props) => {
  return (
    <form onSubmit={onPortfolioCreate}>
        <input readOnly={true} hidden={true} value={symbol} />
        <button type="submit">Add</button>
    </form>
  )
}


//Start from bottom and work to the top
export default AddPortfolio