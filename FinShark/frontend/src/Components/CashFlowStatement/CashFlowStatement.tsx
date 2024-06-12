import { CompanyCashFlow } from "../../company";

type Props = {}

//Config data for proper rendering
const config = [
    {
      label: "Date",
      render: (company: CompanyCashFlow) => company.date,
    },
    {
      label: "Operating Cashflow",
      render: (company: CompanyCashFlow) => company.operatingCashFlow,
    },
    {
      label: "Investing Cashflow",
      render: (company: CompanyCashFlow) => company.netCashUsedForInvestingActivites,
    },
    {
      label: "Financing Cashflow",
      render: (company: CompanyCashFlow) => company.netCashUsedProvidedByFinancingActivities
        ,
    },
    {
      label: "Cash At End of Period",
      render: (company: CompanyCashFlow) => company.cashAtEndOfPeriod,
    },
    {
      label: "CapEX",
      render: (company: CompanyCashFlow) => company.capitalExpenditure,
    },
    {
      label: "Issuance Of Stock",
      render: (company: CompanyCashFlow) => company.commonStockIssued,
    },
    {
      label: "Free Cash Flow",
      render: (company: CompanyCashFlow) => company.freeCashFlow,
    },
  ];
//Cashflow statement component for the dashboard
const CashFlowStatement = (props: Props) => {
  return (
    <div>CashflowStatement</div>
  )
}

export default CashFlowStatement