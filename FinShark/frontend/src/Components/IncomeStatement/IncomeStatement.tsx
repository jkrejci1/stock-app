import { useEffect, useState } from "react";
import { useOutletContext } from "react-router-dom"; //REMEMBER TO GET FROM DOM
import { getIncomeStatement } from "../../api";
import { CompanyIncomeStatement } from "../../company";
import Table from "../Table/Table";

//Component for the income statement
type Props = {}

const configs = [
  {
    label: "Date",
    render: (company: CompanyIncomeStatement) => company.date,
  },
  {
    label: "Revenue",
    render: (company: CompanyIncomeStatement) => company.revenue,
  },
  {
    label: "Cost Of Revenue",
    render: (company: CompanyIncomeStatement) => company.costOfRevenue,
  },
  {
    label: "Depreciation",
    render: (company: CompanyIncomeStatement) =>
      company.depreciationAndAmortization,
  },
  {
    label: "Operating Income",
    render: (company: CompanyIncomeStatement) => company.operatingIncome,
  },
  {
    label: "Income Before Taxes",
    render: (company: CompanyIncomeStatement) => company.incomeBeforeTax,
  },
  {
    label: "Net Income",
    render: (company: CompanyIncomeStatement) => company.netIncome,
  },
  {
    label: "Net Income Ratio",
    render: (company: CompanyIncomeStatement) => company.netIncomeRatio,
  },
  {
    label: "Earnings Per Share",
    render: (company: CompanyIncomeStatement) => company.eps,
  },
  {
    label: "Earnings Per Diluted",
    render: (company: CompanyIncomeStatement) => company.epsdiluted,
  },
  {
    label: "Gross Profit Ratio",
    render: (company: CompanyIncomeStatement) => company.grossProfitRatio,
  },
  {
    label: "Opearting Income Ratio",
    render: (company: CompanyIncomeStatement) => company.operatingIncomeRatio,
  },
  {
    label: "Income Before Taxes Ratio",
    render: (company: CompanyIncomeStatement) => company.incomeBeforeTaxRatio,
  },
];

const IncomeStatement = (props: Props) => {
  //Get the data
  const ticker = useOutletContext<string>() //We should get a string type
  //Set up the state for the current income statement we want
  const [incomeStatement, setIncomeStatement] = useState<CompanyIncomeStatement[]>()
  
  //Remember we need useEffect() so that we can update the state in our component that we want to update could be DOM and others
  useEffect(() => {
    //Get the income statement data we need from the api
    const incomeStatementFetch = async () => {
      const result = await getIncomeStatement(ticker)
      setIncomeStatement(result!.data) //Could get an undefined possible put ! remember; notice how here it displays several dates data because we are showing the whole data array unlike some others where we use data[0] to just grab only the first object, while here we grabbed all of them
    }
    //Now call the income statement function to update the state as needed
    incomeStatementFetch()
  }, []) //Put brackets or it will call the api a ton
  
  return (
    <>
      {/* If we have data in our income statement variable show it, if not display loading during the time we're getting what we need */}
      {incomeStatement ? <><Table config={configs} data={incomeStatement}/></> : <>Loading...</>}
    </>
  )
}

export default IncomeStatement