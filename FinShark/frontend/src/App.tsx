import { Outlet } from 'react-router';
import './App.css';
import Navbar from './Components/Navbar/Navbar';

 function App() {
  //Bring in the card into our app PRACITCE

  return <>
    <Navbar />
    {/** Outlet is what will display the page under the navbar */}
    <Outlet />
  </>;
}

export default App;
