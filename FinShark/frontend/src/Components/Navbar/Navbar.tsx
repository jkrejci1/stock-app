import { Link } from "react-router-dom";
import "./Navbar.css";
import logo from "./logo1.png";

interface Props {}

const Navbar = (props: Props) => {
  return (
    <nav className="relative container mx-auto p-6">
      <div className="flex items-center justify-between">
        <div className="flex items-center space-x-20">
          <Link to="/"> {/* Let's make it to where if we click on the company logo in the navbar, it will bring us back to the home page, just like most websites */}
            <img src={logo} alt="" />
          </Link>
          {/* If you remove hidden it stops it from dissapearing when zooming in */}
          {/* This is connected to the tailwin.config.js file, it resenates with it, like lg goes with lg in the tailwind file. So if we want to adjust on a smaller screen, like so this stuff doesn't dissapear, we'd use sm from the tailwind file */}
          {/* So here, the search link is hidden until the page/screen is considered to be large (lg) once the page is the size of that many pixels or bigger, then the search link will pop up. Same happens for signup/login */}
          <div className="hidden font-bold lg:flex">

            {/* Create route to the search page using Link from react router */}
            <Link to="/search" className="text-black hover:text-darkBlue">
              Search
            </Link>
          </div>
        </div>
        <div className="hidden lg:flex items-center space-x-6 text-back">
          <div className="hover:text-darkBlue">Login</div>
          <a
            href=""
            className="px-8 py-3 font-bold rounded text-white bg-lightGreen hover:opacity-70"
          >
            Signup
          </a>
        </div>
      </div>
    </nav>
  );
};

export default Navbar;