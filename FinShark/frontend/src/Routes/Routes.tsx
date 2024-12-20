import { createBrowserRouter } from "react-router-dom";
import App from "../App";
import BalanceSheet from "../Components/BalanceSheet/BalanceSheet";
import CashFlowStatement from "../Components/CashFlowStatement/CashFlowStatement";
import CompanyProfile from "../Components/CompanyProfile/CompanyProfile";
import IncomeStatement from "../Components/IncomeStatement/IncomeStatement";
import CompanyPage from "../Pages/CompanyPage/CompanyPage";
import DesignGuide from "../Pages/DesignGuide/DesignGuide";
import HomePage from "../Pages/HomePage/HomePage";
import LoginPage from "../Pages/LoginPage/LoginPage";
import RegisterPage from "../Pages/RegisterPage/RegisterPage";
import SearchPage from "../Pages/SearchPage/SearchPage";
import ProtectedRoute from "./ProtectedRoute";

export const router = createBrowserRouter([
    //Path --> URL Route, element --> The index.tsx component we want our route to have data access to set up page
    //Children are the possible other routes that can be taken from the beginning route, you can use : to pass data with it like the ticker for company page
    {
        path: "/",
        element: <App />,
        children: [
            {path: "", element: <HomePage />},
            {path: "login", element: <LoginPage />},
            {path: "register", element: <RegisterPage />},
            {path: "search", element: <ProtectedRoute><SearchPage /></ProtectedRoute>}, //This will be a protected route, using our ProtectedRoute.tsx file to swith the page to the login page
            {path: "design-guide", element: <DesignGuide />},
            //The ticker is the company data from the company name abreviation like aapl or msft (it can be anything though from how it works, that's just how we're gonna use it in this case!)
            {
                //Create route for the Company Page, which also contains routes to company data like company profile and income statement
                //This creates nested routes used for one specific route, creating routes to other pages inside this one page only! COOL!!
                //RESUME VIDEO 19 AT 8 MINUTE MARK
                path: "company/:ticker", 
                element: <ProtectedRoute><CompanyPage /></ProtectedRoute>, //Protect all the routes for here using our ProtectedRoute.tsx file to redirect the user to the login page when attempting to go anywhere here
                children: [
                    { path: "company-profile", element: <CompanyProfile /> },
                    { path: "income-statement", element: <IncomeStatement /> },
                    { path: "balance-sheet", element: <BalanceSheet /> },
                    { path: "cashflow-statement", element: <CashFlowStatement /> }
                ],
            },
        
        ]
    }
])