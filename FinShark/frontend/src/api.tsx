import axios from "axios";
//Imports from our company.d types
import { CompanyBalanceSheet, CompanyCashFlow, CompanyIncomeStatement, CompanyKeyMetrics, CompanyProfile, CompanySearch, CompanyTenK } from "./company";

//We need to create to wrap around to correctly make our axios call
//Gonna be an array
interface SearchResponse {
    data: CompanySearch[];
}

export const searchCompanies = async (query: string) => {
    try {
        const data = await axios.get<SearchResponse>(
            `https://financialmodelingprep.com/api/v3/search?query=${query}&limit=10&exchange=NASDAQ&apikey=${process.env.REACT_APP_API_KEY}`
        );

        return data;
    } catch (error) {
        if(axios.isAxiosError(error)) {
            console.log("error message: ", error.message)
            return error.message;
        } else {
            console.log("Unexpected error: ", error)
            return "An unexpected error has occured"
        }
    }
}

//Function that will get company data from the company profile data stuff for the company we want
export const getCompanyProfile = async (query: string) => {
    try {
        const data = await axios.get<CompanyProfile[]>(
            `https://financialmodelingprep.com/api/v3/profile/${query}?apikey=${process.env.REACT_APP_API_KEY}`
        )
        return data
    } catch (error: any) {
        console.log("error message from API: ", error.message)
    }
}

//Function that will get company data from the company profile for the key metrics
export const getKeyMetrics = async (query: string) => {
    try {
        const data = await axios.get<CompanyKeyMetrics[]>(
            `https://financialmodelingprep.com/api/v3/key-metrics-ttm/${query}?apikey=${process.env.REACT_APP_API_KEY}`
        )
        return data
    } catch (error: any) {
        console.log("error message from API: ", error.message)
    }
}

//Function that will get the income statement data that we need, limit 40 to prevent too much data
export const getIncomeStatement = async (query: string) => {
    try {
        const data = await axios.get<CompanyIncomeStatement[]>(
            `https://financialmodelingprep.com/api/v3/income-statement/${query}?Limit=40&apikey=${process.env.REACT_APP_API_KEY}`
        )
        return data
    } catch (error: any) {
        console.log("error message from API: ", error.message)
    }
}

//Function that will get the balance sheet data that we need, limit 40 to prevent too much data; query == our ticker we passed
export const getBalanceSheet = async (query: string) => {
    try {
        //Gets api data that is in an object when we do this call and put into data --> all of the types in our company.d.ts should be what all of the data is, but you can check it by console logging the data and it should match. They both might be in different orders though, but they don't need to be in the same exact order so it's fine.
        const data = await axios.get<CompanyBalanceSheet[]>(
            `https://financialmodelingprep.com/api/v3/balance-sheet-statement/${query}?Limit=40&apikey=${process.env.REACT_APP_API_KEY}`
        )
        return data
    } catch (error: any) {
        console.log("error message from API: ", error.message)
    }
}

//Function that will get the cash flow data that we need, limit 40 to prevent too much data; query == our ticker we passed
export const getCashFlowStatement = async (query: string) => {
    try {
        //Gets api data that is in an object when we do this call and put into data --> all of the types in our company.d.ts should be what all of the data is, but you can check it by console logging the data and it should match. They both might be in different orders though, but they don't need to be in the same exact order so it's fine.
        const data = await axios.get<CompanyCashFlow[]>(
            `https://financialmodelingprep.com/api/v3/cash-flow-statement/${query}?Limit=40&apikey=${process.env.REACT_APP_API_KEY}`
        )
        return data
    } catch (error: any) {
        console.log("error message from API: ", error.message)
    }
}

//Function that will get the 10k data that we need, limit 40 to prevent too much data; query == our ticker we passed
export const getTenK = async (query: string) => {
    try {
        //Gets api data that is in an object when we do this call and put into data --> all of the types in our company.d.ts should be what all of the data is, but you can check it by console logging the data and it should match. They both might be in different orders though, but they don't need to be in the same exact order so it's fine.
        const data = await axios.get<CompanyTenK[]>(
            `https://financialmodelingprep.com/api/v3/sec_filings/${query}?type=10-k&page=0&apikey=${process.env.REACT_APP_API_KEY}`
        )
        return data
    } catch (error: any) {
        console.log("error message from API: ", error.message)
    }
}