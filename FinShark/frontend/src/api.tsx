import axios from "axios";
import { CompanyIncomeStatement, CompanyKeyMetrics, CompanyProfile, CompanySearch } from "./company";

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