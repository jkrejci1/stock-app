import { useEffect, useState } from "react";
import { useOutletContext } from "react-router-dom";
import { formatLargeMonetaryNumber } from "../../Helpers/NumberFormatting";
import { getCashFlowStatement } from "../../api";
import { CompanyCashFlow } from "../../company";
import Spinner from "../Spinner/Spinner";
import Table from "../Table/Table";

type Props = {}

//Config data for proper rendering here
const config = [
  {
    label: "Date",
    render: (company: CompanyCashFlow) => company.date,
  },
  {
    label: "Operating Cashflow",
    render: (company: CompanyCashFlow) =>
      formatLargeMonetaryNumber(company.operatingCashFlow),
  },
  {
    label: "Investing Cashflow",
    render: (company: CompanyCashFlow) =>
      formatLargeMonetaryNumber(company.netCashUsedForInvestingActivites),
  },
  {
    label: "Financing Cashflow",
    render: (company: CompanyCashFlow) =>
      formatLargeMonetaryNumber(
        company.netCashUsedProvidedByFinancingActivities
      ),
  },
  {
    label: "Cash At End of Period",
    render: (company: CompanyCashFlow) =>
      formatLargeMonetaryNumber(company.cashAtEndOfPeriod),
  },
  {
    label: "CapEX",
    render: (company: CompanyCashFlow) =>
      formatLargeMonetaryNumber(company.capitalExpenditure),
  },
  {
    label: "Issuance Of Stock",
    render: (company: CompanyCashFlow) =>
      formatLargeMonetaryNumber(company.commonStockIssued),
  },
  {
    label: "Free Cash Flow",
    render: (company: CompanyCashFlow) =>
      formatLargeMonetaryNumber(company.freeCashFlow),
  },
  ];
//Cashflow statement component for the dashboard
const CashFlowStatement = (props: Props) => {
    const ticker = useOutletContext<string>()
    const [cashflowData, setCashflow] = useState<CompanyCashFlow[]>()
    useEffect(() => {
        const fetchCashflow = async () => {
            const result = await getCashFlowStatement(ticker!)
            setCashflow(result!.data) //Pass in the whole array
        }
        fetchCashflow()
    }, [])
  return (
    <>
        { cashflowData ? (
            <Table config={config} data={cashflowData} />
        ) : (
            <Spinner />
        )}
    </>
  )
}

export default CashFlowStatement