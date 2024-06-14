import { useEffect, useState } from "react";
import { useParams } from "react-router";
import CompanyDashboard from "../../Components/CompanyDashboard/CompanyDashboard";
import Sidebar from "../../Components/Sidebar/Sidebar";
import Spinner from "../../Components/Spinner/Spinner";
import TenKFinder from "../../Components/TenKFinder/TenKFinder";
import Tile from "../../Components/Tile/Tile";
import { getCompanyProfile } from "../../api";
import { CompanyProfile } from "../../company";

interface Props {}

const CompanyPage = (props: Props) => {
    //Fetch data for the company we want before the page loads
    //https:localhost:3000/(WHAT USE PARAMS IS LOOKING FOR BELOW) (WHATS AFTER THE / IS WHAT ticker WILL BE EQUAL TO)
    let { ticker } = useParams() //Being passed from URL (Only after the first / though, not nested /'s (so in aapl/income, aapl will still be the ticker NOT aaplincome or aapl/income, JUST aapl still))
    console.log("Use params data: ", ticker)
    const [company, setCompany] = useState<CompanyProfile>(); //Current state for the company we are looking for on the proper page
    
    useEffect(() => {
        const getProfileInit = async () => {
            console.log("THIS IS OUR TICKER: ", ticker)
            const result = await getCompanyProfile(ticker!) //We have to put ! here to make sure that what's being passed is a string and makes it not be able to pass an undefined value. It asserts to TypeScript that this variable will always be nonnull so it overrides the error of react saying this could possibly pass a non null value if the ! symbol was not included here. It by passes the TypeScript usual check for null/undefined values as we know for sure this should never be such a value so it wouldn't break our code for sure (because if we are on a company page we have to have that ticker in the URL to get to said page in the first place, so it can never be null so there's no point for typescript to even check of such existence which was causing an error (from not using the !)).
            console.log("data[0]: ", result?.data[0])
            setCompany(result?.data[0]) //Set's setCompany to result data optionally with ?
                                        //We need to use a ? or we'll get an error cause there's a possibility that result.data[0] could be an undefined value which could break the code. Like if we tried searching for a company ticker that doesn't exist, like aapl exists but hhd34j23 wouldn't exist and would return undefined.
                                        //data[0] here would be an array of the data that would be in CompanyProfile for the current company we're searching for (the data in company.d.ts from our API)
        }
        getProfileInit()
    }, [])
    return (
        <>
            {/* Returns company data if there's a company. If not, return company not found on the page */}
            {company ? (
                <div className="w-full relative flex ct-docs-disable-sidebar-content overflow-x-hidden">
                    <Sidebar />
                    {/* When we create children components inside our components, we need to add those as well with the parent component as well as sending data we made need. CONTINUE */}
                    <CompanyDashboard ticker={ticker!}> {/* Pass down ticker, make sure its type is never undefined */}
                        <Tile title="Company Name" subTitle={company.companyName}></Tile>
                        <Tile title="Price" subTitle={company.price.toString()}></Tile>
                        <Tile title="Sector" subTitle={company.sector}></Tile>
                        <Tile title="DCF" subTitle={company.dcf.toString()}></Tile>
                        {/* Now bring in the 10k finder component */}
                        <TenKFinder ticker={company.symbol}/> {/* Remember the ticker we've been using would be the company symbol like AAPL for exmaple */}
                        {/* Also show the description */}
                        <p className="bg-white shadow rounded text-medium text-gray-900 p-3 mt-1 m-4">
                            {company.description}
                        </p>
                    </CompanyDashboard>
                </div>
            ) : (
                <Spinner />
            )}
        </>
    )
}

export default CompanyPage