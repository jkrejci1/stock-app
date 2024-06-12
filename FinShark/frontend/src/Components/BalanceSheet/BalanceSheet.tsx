//Comp for the balance sheet for the company page

import { useEffect, useState } from "react";
import { useOutletContext } from "react-router-dom";
import { getBalanceSheet } from "../../api";
import { CompanyBalanceSheet } from "../../company";
import RatioList from "../RatioList/RatioList";

type Props = {}

//Config for our list of data objects we're going to show on the page
const config = [
    {
      label: <div className="font-bold">Total Assets</div>,
      render: (company: CompanyBalanceSheet) => company.totalAssets,
    },
    {
      label: "Current Assets",
      render: (company: CompanyBalanceSheet) => company.totalCurrentAssets,
    },
    {
      label: "Total Cash",
      render: (company: CompanyBalanceSheet) => company.cashAndCashEquivalents,
    },
    {
      label: "Property & equipment",
      render: (company: CompanyBalanceSheet) => company.propertyPlantEquipmentNet,
    },
    {
      label: "Intangible Assets",
      render: (company: CompanyBalanceSheet) => company.intangibleAssets,
    },
    {
      label: "Long Term Debt",
      render: (company: CompanyBalanceSheet) => company.longTermDebt,
    },
    {
      label: "Total Debt",
      render: (company: CompanyBalanceSheet) => company.otherCurrentLiabilities,
    },
    {
      label: <div className="font-bold">Total Liabilites</div>,
      render: (company: CompanyBalanceSheet) => company.totalLiabilities,
    },
    {
      label: "Current Liabilities",
      render: (company: CompanyBalanceSheet) => company.totalCurrentLiabilities,
    },
    {
      label: "Long-Term Debt",
      render: (company: CompanyBalanceSheet) => company.longTermDebt,
    },
    {
      label: "Long-Term Income Taxes",
      render: (company: CompanyBalanceSheet) => company.otherLiabilities,
    },
    {
      label: "Stakeholder's Equity",
      render: (company: CompanyBalanceSheet) => company.totalStockholdersEquity,
    },
    {
      label: "Retained Earnings",
      render: (company: CompanyBalanceSheet) => company.retainedEarnings,
    },
  ];

const BalanceSheet = (props: Props) => {
    //Bring in the data
    const ticker = useOutletContext<string>() //ticker == the route nested from /company, so /company/AAPL --> AAPL would be the ticker
    console.log("BalanceSheet Ticker: " + ticker)
    const [balanceSheet, setBalanceSheet] = useState<CompanyBalanceSheet>() //We're going to make this data into a list so we don't need to make it an array
    
    useEffect(() => {
        const getData = async () => {
            const value = await getBalanceSheet(ticker!) //We can get an undefined, allow it!
            setBalanceSheet(value?.data[0]) //Use the first one for our list. value is an array of objects from our api, and we want the data part of it. data is an array of objects which contains all of the data that we would want for the company.d.ts CompanyBalanceSheet.
                                                //Then, the company.d.ts categorizes the data from our api into types so that it all works properly how we want it for typescript, we grab the first one because it's the most recent date done --> data[0]
                                                //Then, the config function up top, creates a more visually appealing version of all that data with the label being the title of said data, and then renders creates said data.
            //console.log("Balance sheet obj data:", value?.data) //THESE ARE THE OBJECTS THAT ARE RETURNED FROM OUR API WHEN CALLING FOR THE BALANCE SHEET DATA FROM IT
        }
        getData() //Call getData to get our data according to the ticker
    }, []) //Prevents to much data to be grabbed like on other components
    return (
    <>
        {balanceSheet ? (
            <RatioList config={config} data={balanceSheet} />
        ) : (
            <>Loading...</>
        )}
    </>
  )
}

export default BalanceSheet