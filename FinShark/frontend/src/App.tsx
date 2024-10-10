import { Outlet } from 'react-router';
import { ToastContainer } from 'react-toastify';
import "react-toastify/dist/ReactToastify.css";
import './App.css';
import Navbar from './Components/Navbar/Navbar';
import { UserProvider } from './Context/useAuth';

 function App() {
  //Bring in the card into our app PRACITCE

  return (
    <>
      <UserProvider>
        <Navbar />
        {/** Outlet is what will display the page under the navbar */}
        <Outlet />
        <ToastContainer />
      </UserProvider>
    </>
  );
}

export default App;
