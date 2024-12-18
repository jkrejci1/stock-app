import { Link } from "react-router-dom";
import { useAuth } from "../../Context/useAuth";
import "./Navbar.css";
import logo from "./logo.png";

interface Props {}

const Navbar = (props: Props) => {
  const { isLoggedIn, user, logout } = useAuth(); //For bringing in possible current user data to display for the navbar, and then display the logout button and functionality if a user is logged in already
  return (
    <nav className="relative container mx-auto p-6">
      <div className="flex items-center justify-between space-x-7">
        <div className="flex items-center space-x-10">
          <Link to="/"> {/* Let's make it to where if we click on the company logo in the navbar, it will bring us back to the home page, just like most websites */}
            <img src={logo} alt="" />
          </Link>
          {/* If you remove hidden it stops it from dissapearing when zooming in */}
          {/* This is connected to the tailwin.config.js file, it resenates with it, like lg goes with lg in the tailwind file. So if we want to adjust on a smaller screen, like so this stuff doesn't dissapear, we'd use sm from the tailwind file */}
          {/* So here, the search link is hidden until the page/screen is considered to be large (lg) once the page is the size of that many pixels or bigger, then the search link will pop up. Same happens for signup/login */}
          <div className="hidden font-bold sm:flex">

            {/* Create route to the search page using Link from react router */}
            <Link to="/search" className="text-black hover:text-darkBlue">
              Search
            </Link>
          </div>
        </div>
        {/* If a user is logged in, welcome them and display the logout button, if not display the login button and signup button */}
        {isLoggedIn() ? (
          <div className="hidden sm:flex items-center space-x-6 text-back">
            <div className="hover:text-darkBlue">Welcome, {user?.userName}</div>
            <a
              onClick={logout}
              className="px-8 py-3 font-bold rounded text-white bg-lightGreen hover:opacity-70"
            >
              Logout
            </a>
          </div>
        ) : (
          <div className="hidden sm:flex items-center space-x-6 text-back">
            <Link to="/login" className="hover:text-darkBlue">
              Login
            </Link>
            <Link
            to="/register"
              className="px-8 py-3 font-bold rounded text-white bg-lightGreen hover:opacity-70"
            >
              Signup
            </Link>
        </div>
        )}
      </div>
    </nav>
  );
};

export default Navbar;