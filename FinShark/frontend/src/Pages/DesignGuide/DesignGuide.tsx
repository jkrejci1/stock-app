//Design guide for our app 

import RatioList from "../../Components/RatioList/RatioList"
import Table from "../../Components/Table/Table"
import { testIncomeStatementData } from "../../Components/Table/testData"

type Props = {}

//Dummy data for test
const tableConfig = [
  {
    label: "Market Cap",
    render: (company: any) => company.marketCapTTM,
    subTitle: "Total value of all a company's shares of stock",
  },
]

const DesignGuide = (props: Props) => {
  return (
    <>
    <h1>FinShark Design Page</h1>
    <h2>This is FinShark's design page, where we'll house 
        various design aspects of the application.
    </h2>
    {/* Bring in the ratio list for displaying certain company data */}
    {/* Also pass down the data to it */}
    <RatioList data={testIncomeStatementData} config={tableConfig} />
    {/* Table that we'll use for all data */}
    <Table data={testIncomeStatementData} config={tableConfig} />
    </>
  )
}

export default DesignGuide