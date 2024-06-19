//NEED TO MAKE RESPONSIVE
import { testIncomeStatementData } from "./testData";

//This will contain the test data for us to play around with
const data = testIncomeStatementData

type Props = {
    config: any;
    data: any;
}

//Let's type it FOR TEST DATA
/**
type Company = (typeof data)[0];

console.log("Company type: ", data[0])


//Need to create a config object for our table to help keep it DRY
//TEST DATA
const configs = [
    {
        label: "Year",
        render: (company: Company) => company.acceptedDate //Returns accepted date
    },
    {
        label: "Cost of Revenue",
        render: (company: Company) => company.costOfRevenue //Returns cost of revenue
    }
]
*/


//We can create functions to create certain parts of the table then use the Table function to bridge them all together how we want them to
const Table = ({ config, data }: Props) => {
    //Let's create those table functions
    const renderedRows = data.map((company: any) => {
        return (
        //Stocks usually have a cik so we'll use that for its key or we can use uuid like the other time we used that
        <tr key={company.cik}>
            {config.map((val:any) => {
                return (
                    <td className="p-3">
                        {val.render(company)}
                    </td>
                )
            })} 
        </tr>
       )
    })
    //Now let's do the header
    const renderedHeaders = config.map((config: any) => {
        return (
            <th className="p-4 text-left text-xs font-medium text-fray-500 uppercase tracking-wider"
                key={config.label}
            >
                {config.label}
            </th>
        )
    })
  return (
    <div className="bg-white shadow rounded-lg p-4 sm:p-6 xl:p-8">
        <table className="min-w-full divide-y divide-gray-200 m-5">
            <thead className="bg-gray-100">{renderedHeaders}</thead>
            <tbody>{renderedRows}</tbody>
        </table>
    </div>
  )
}

export default Table