//Comp for the balance sheet for the company page

import { useEffect, useState } from "react";
import { useOutletContext } from "react-router-dom";
import { formatLargeMonetaryNumber } from "../../Helpers/NumberFormatting";
import { getBalanceSheet } from "../../api";
import { CompanyBalanceSheet } from "../../company";
import RatioList from "../RatioList/RatioList";
import Spinner from "../Spinner/Spinner";

type Props = {}

//Config for our list of data objects we're going to show on the page
//When render is called, these are all the possible 
const config = [
  {
    label: <div className="font-bold">Total Assets</div>,
    render: (company: CompanyBalanceSheet) =>
      formatLargeMonetaryNumber(company.totalAssets),
  },
  {
    label: "Current Assets",
    render: (company: CompanyBalanceSheet) =>
      formatLargeMonetaryNumber(company.totalCurrentAssets),
  },
  {
    label: "Total Cash",
    render: (company: CompanyBalanceSheet) =>
      formatLargeMonetaryNumber(company.cashAndCashEquivalents),
  },
  {
    label: "Property & equipment",
    render: (company: CompanyBalanceSheet) =>
      formatLargeMonetaryNumber(company.propertyPlantEquipmentNet),
  },
  {
    label: "Intangible Assets",
    render: (company: CompanyBalanceSheet) =>
      formatLargeMonetaryNumber(company.intangibleAssets),
  },
  {
    label: "Long Term Debt",
    render: (company: CompanyBalanceSheet) =>
      formatLargeMonetaryNumber(company.longTermDebt),
  },
  {
    label: "Total Debt",
    render: (company: CompanyBalanceSheet) =>
      formatLargeMonetaryNumber(company.otherCurrentLiabilities),
  },
  {
    label: <div className="font-bold">Total Liabilites</div>,
    render: (company: CompanyBalanceSheet) =>
      formatLargeMonetaryNumber(company.totalLiabilities),
  },
  {
    label: "Current Liabilities",
    render: (company: CompanyBalanceSheet) =>
      formatLargeMonetaryNumber(company.totalCurrentLiabilities),
  },
  {
    label: "Long-Term Debt",
    render: (company: CompanyBalanceSheet) =>
      formatLargeMonetaryNumber(company.longTermDebt),
  },
  {
    label: "Long-Term Income Taxes",
    render: (company: CompanyBalanceSheet) =>
      formatLargeMonetaryNumber(company.otherLiabilities),
  },
  {
    label: "Stakeholder's Equity",
    render: (company: CompanyBalanceSheet) =>
      formatLargeMonetaryNumber(company.totalStockholdersEquity),
  },
  {
    label: "Retained Earnings",
    render: (company: CompanyBalanceSheet) =>
      formatLargeMonetaryNumber(company.retainedEarnings),
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
            console.log("Balance sheet obj data:", value?.data) //THESE ARE THE OBJECTS THAT ARE RETURNED FROM OUR API WHEN CALLING FOR THE BALANCE SHEET DATA FROM IT
        }
        getData() //Call getData to get our data according to the ticker
    }, []) //Prevents to much data to be grabbed like on other components
    //console.log("BALANCE SHEET STATE: ", balanceSheet) //THE STATE WOULD BE THE WHOLE DATA OBJECT OF EVERY SINGLE THING
    return (
    <>
        {balanceSheet ? (
            <RatioList config={config} data={balanceSheet} />
        ) : (
            <Spinner />
        )}
    </>
  )
}

export default BalanceSheet