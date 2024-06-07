import { Outlet } from "react-router-dom"; //Outlet provides nested routes so we can take our incomeStatements and the actual pages/components to load on our page (allows us to pass our children --> our tiles for example) 
                                           //The children are any of the tags that would be inside of the CompanyDashboard tags <> (our <Tile /> tags for example)
//For childern we need to use react node
type Props = {
  children: React.ReactNode;
  ticker: string;
}

const CompanyDashboard = ({children, ticker}: Props) => {
  return (
   <div className="relative md:ml-64 bg-blueGray-100 w-full">
      
    <div className="relative pt-20 pb-32 bg-lightBlue-500">

      <div className="px-4 md:px-6 mx-auto w-full">

        <div>

          {/* Use outlet to have a route so we can get all the income statements and other pages to load */}
          {/* This is where we'll get the company dashboard pages children, (the other components we want added in this page) */}
          {/* The tiles aree our children */}
          <div className="flex flex-wrap">{children}</div> {/* The children being passed is like the tile data */}
          {/**<div className="flex flex-wrap">{<Outlet />}</div>*/} {/* Here would be a placeholder for the nested route data like IncomeStatement and CompoanyProfile and anything else we would want there conditionally. This data may change on the same page depending if we click on income statement or company profile, so we use 'Outlet' as like a variable that may contain either or, or other possible component calls if we want (DISPLAYS DATA FROM WHAT'S NESTED IN OUR CURRENT DASHBOARD ROUTE) */}
                                                              {/* So, <Outlet /> knows to get either income statement or company profile, by checking the current route we are at, see if we're in any nested routes for the component which needs to be also included in our routes.tsx file. If the route exists it will then display whatever is in the the corresponding tsx file for that route (like CompanyProfile.tsx for example) and it will place what's in that file right here where <Outlet /> is located. So then, instead of creating a whole new component for that specific route and have company profile or income statement itself be its own seperate component, it will take what would've been on such a component, and add it inside the component we're currently in instead (being the CompanyDashboard component). */}
          <div className="flex flex-wrap">{<Outlet context={ticker}/>}</div> {/* Use outlet context in order to pass data down the nested route page */}
        </div>

      </div>

    </div>

  </div>
  )
}

export default CompanyDashboard