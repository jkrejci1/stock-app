import { useEffect, useState } from "react";
import { getTenK } from "../../api";
import { CompanyTenK } from "../../company";
import Spinner from "../Spinner/Spinner";
import TenKFinderItem from "./TenKFinderItem/TenKFinderItem";

//10k Component
type Props = {
    ticker: string;
}

const TenKFinder = ({ ticker }: Props) => {
    //State for the 10k data
    const [companyData, setCompanyData] = useState<CompanyTenK[]>()

    //Function to grab 10k data from API
    useEffect(() => {
        const getTenKData = async () => {
            const value = await getTenK(ticker)
            setCompanyData(value?.data) //Possible undefiend
            console.log("Button company data: " + companyData)
        }
        getTenKData()
    }, [ticker]) //Dependencies
  return (
    //Bring in the tenK data 
    <div className="inline-flex rounded-md shadow-sm m-4">
        {companyData ? (
            companyData?.slice(0, 5).map((tenK) => { //Only bring in the most recent using slice 
                return <TenKFinderItem tenK={tenK} />
            })
        ) :  (
            //Show the loading spinner when the 10k data is bing loaded in
            <Spinner />
        )}
    </div>
  )
}

export default TenKFinder