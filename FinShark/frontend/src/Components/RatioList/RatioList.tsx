//Ratio List display for our table data
import { TestDataCompany } from '../Table/testData'

type Props = {}

//Bring in test data
const data = TestDataCompany[0]

//Give a type to the test data
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
const RatioList = (props: Props) => {

    //Render each cell row
    const renderedRows = config.map((row) => {
        return (
            <li className="py-3 sm:py-4">
                <div className="flex items-center space-x-4">
                    <div className="flex-1 min-w-0">
                        <p className="text-sm font-medium text-gray-900 truncate">
                            {row.lable}
                        </p>
                        <p className="text-sm text-gray-500 truncate">
                            {row.subTitle && row.subTitle} {/* Shows a subtitle only if it contains one */}
                        </p>
                    </div>
                    {/* Now render the data that we want */}
                    <div className="inline-flex items-center text-base font-semibold text-gray-900">
                        {row.render(data)}
                    </div>
                </div>
            </li>
        )
    })
  return (

    //Now use the return statement to render everything from the function
    <div className="bg-white shadow rounded-lg mb-4 p-4 sm:p-6 h-full">
        {/* Tailwind to make our table */}
        <ul className="divide-y divided-gray-200">{renderedRows}</ul>
    </div>
  )
}

export default RatioList