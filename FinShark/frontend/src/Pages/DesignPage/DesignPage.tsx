//Design guide for our app 

import RatioList from "../../Components/RatioList/RatioList"
import Table from "../../Components/Table/Table"

type Props = {}

const DesignPage = (props: Props) => {
  return (
    <>
    <h1>FinShark Design Page</h1>
    <h2>This is FinShark's design page, where we'll house 
        various design aspects of the application.
    </h2>
    {/* Bring in the ratio list for displaying certain company data */}
    <RatioList />
    {/* Table that we'll use for all data */}
    <Table />
    </>
  )
}

export default DesignPage