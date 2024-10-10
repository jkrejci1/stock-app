import { Outlet } from 'react-router';
import { ToastContainer } from 'react-toastify';
import "react-toastify/dist/ReactToastify.css";
import './App.css';
import Navbar from './Components/Navbar/Navbar';

 function App() {
  //Bring in the card into our app PRACITCE

  return <>
    <Navbar />
    {/** Outlet is what will display the page under the navbar */}
    <Outlet />
    <ToastContainer />
  </>;
}

export default App;
