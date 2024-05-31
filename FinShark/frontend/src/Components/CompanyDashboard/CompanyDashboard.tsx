import { Outlet } from "react-router-dom";

//For childern we need to use react node
type Props = {
  children: React.ReactNode;
}

const CompanyDashboard = ({children}: Props) => {
  return (
   <div className="relative md:ml-64 bg-blueGray-100 w-full">
      
    <div className="relative pt-20 pb-32 bg-lightBlue-500">

      <div className="px-4 md:px-6 mx-auto w-full">

        <div>

          {/* Use outlet to have a rouote so we can get all the income statements and other pages to load */}
          {/* This is where we'll get the company dashboard pages children, (the other components we want added in this page) */}
          {/* The tiles aree our children */}
          <div className="flex flex-wrap">{children}</div>
          <div className="flex flex-wrap">{<Outlet />}</div>

        </div>

      </div>

    </div>

  </div>
  )
}

export default CompanyDashboard