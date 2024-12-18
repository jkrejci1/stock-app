import axios from "axios"
import { handleError } from "../Helpers/ErrorHandler"
import { PortfolioGet, PortfolioPost } from "../Models/Portfolio"

const api = "http://localhost:5144/api/portfolio"

//Adding
export const portfolioAddAPI = async (symbol: string) => {
    try {
        const data = await axios.post<PortfolioPost>(api + `?symbol=${symbol}`)
        return data;
    } catch (error) {
        handleError(error)
    }
}

//Deleting
export const portfolioDeleteAPI = async (symbol: string) => {
    try {
        const data = await axios.delete<PortfolioPost>(api + `?symbol=${symbol}`)
        return data;
    } catch (error) {
        handleError(error)
    }
}

//Getting
export const portfolioGetAPI = async () => {
    try {
        const data = await axios.get<PortfolioGet[]>(api);
        return data;
    } catch (error) {
        handleError(error)
    }
}