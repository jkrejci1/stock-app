import { useEffect, useState } from "react";
import { useParams } from "react-router";
import CompanyDashboard from "../../Components/CompanyDashboard/CompanyDashboard";
import Sidebar from "../../Components/Sidebar/Sidebar";
import Tile from "../../Components/Tile/Tile";
import { getCompanyProfile } from "../../api";
import { CompanyProfile } from "../../company";

interface Props {}

const CompanyPage = (props: Props) => {
    //Fetch data for the company we want before the page loads
    //https:localhost:3000/(WHAT USE PARAMS IS LOOKING FOR BELOW) (WHATS AFTER THE / IS WHAT ticker WILL BE EQUAL TO)
    let { ticker } = useParams() //Being passed from URL
    const [company, setCompany] = useState<CompanyProfile>(); //Current state for the company we are looking for on the proper page
    
    useEffect(() => {
        const getProfileInit = async () => {
            console.log("THIS IS OUR TICKER: ", ticker)
            const result = await getCompanyProfile(ticker!) 
            setCompany(result?.data[0]) //Set's setCompany to result data optionally with ?
        }
        getProfileInit()
    }, [])
    return (
        <>
            {/* Returns company data if there's a company. If not, return company not found on the page */}
            {company ? (
                <div className="w-full relative flex ct-docs-disable-sidebar-content overflow-x-hidden">
                    <Sidebar />
                    {/* When we create children components inside our components, we need to add those as well with the parent component as well as sending data we made need */}
                    <CompanyDashboard><Tile title="Company Name" subTitle={company.companyName}></Tile></CompanyDashboard>
                </div>
            ) : (
                <div>Company not found</div>
            )}
        </>
    )
}

export default CompanyPage