//Ratio List display for our table data

//Pass down the data from API
type Props = {
    config: any;
    data: any;
}




//Bring in test data FOR THE DUMMY TEST DATA
//const data = TestDataCompany[0]

//Give a type to the test data THIS IS DUMMY TEST DATA
/** 
type Company = typeof data

const config = [
    {
        lable: "Company Name",
        render: (company: Company) => company.companyName,
        subTitle: "This is the company name.",
    },
    {
        lable: "Company Name",
        render: (company: Company) => company.companyName,
        subTitle: "This is the company name.",
    },
]
*/




const RatioList = ({ config, data } : Props) => {

    //Render each cell row
    const renderedRows = config.map((row: any) => {
        return (
            <li className="py-3 sm:py-4">
                <div className="flex items-center space-x-4">
                    <div className="flex-1 min-w-0">
                        <p className="text-sm font-medium text-gray-900 truncate">
                            {row.label}
                        </p>
                        <p className="text-sm text-gray-500 truncate">
                            {row.subTitle && row.subTitle} {/* Shows a subtitle only if it contains one */}
                        </p>
                    </div>
                    {/* Now render the data that we want */}
                    <div className="inline-flex items-center text-base font-semibold text-gray-900">
                        {row.render(data)} {/* Calls render from the config function in BalanceSheet.tsx, we'll send the full data object with all the data in it each time and have the current render from the current object we're on select which one it wants to use to correspond with the correct label we just displayed in this current map/index in the mapping array */}
                    </div>
                </div>
            </li>
        )
    })
  return (

    //Now use the return statement to render everything from the function
    <div className="bg-white shadow rounded-lg ml-4 mt-4 mb-4 p-4 sm:p-6 w-full">
        {/* Tailwind to make our table */}
        <ul className="divide-y divided-gray-200">{renderedRows}</ul>
    </div>
  )
}

export default RatioList