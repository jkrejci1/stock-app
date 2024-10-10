import axios from "axios";
import React, { createContext, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import { UserProfile } from "../Models/User";
import { loginAPI, registerAPI } from "../Services/AuthService";

//Context needs to be typed
type UserContextType = {
    user: UserProfile | null;
    token: string | null;
    registerUser: (email: string, username: string, password: string) => void;
    loginUser: (username: string, password: string) => void;
    logout: () => void;
    isLoggedIn: () => boolean;
};

//Type it here for passing into React component
type Props = { children: React.ReactNode };

//Create the context here
const UserContext = createContext<UserContextType>({} as UserContextType);

export const UserProvider = ({ children } : Props) => {
    const navigate = useNavigate();
    const [token, setToken] = useState<string | null>(null);
    const [user, setUser] = useState<UserProfile | null>(null);
    const [isReady, setIsReady] = useState(false);

    //Check if the token exists
    useEffect(() => {
        //Store the token in local storage
        const user = localStorage.getItem("user");
        const token = localStorage.getItem("token");

        //If the values are in the local storage, we will set them within our state
        if(user && token) {
            setUser(JSON.parse(user));
            setToken(token);
            axios.defaults.headers.common["Authorization"] = "Bearer " + token;
        }
        setIsReady(true);
    }, []);

    //Begin creating our functions
    //This will be for registering users
    const registerUser = async (email: string, username: string, password: string) => {
        //Reach into the API endpoints that were made
        await registerAPI(email, username, password).then((res) => {
            if(res) {
                localStorage.setItem("token", res?.data.token);
                const userObj = {
                    userName: res?.data.userName,
                    email: res?.data.email
                }
                //Pass in the object to our local storage
                localStorage.setItem("user", JSON.stringify(userObj));
                setToken(res?.data.token!); //Set the user's token
                setUser(userObj!); //Set the user object (like above, ! ==> means it can be null possibly)
                toast.success("Login Success!") //If all the above worked, the user logged in, give a success message
                navigate("/search") //Send the user to the search page in the application when logged in
            }
        }).catch((e) => toast.warning("Server error occured"));
    };

    //For logging in users
    const loginUser = async (username: string, password: string) => {
        //Reach into the API endpoints that were made
        await loginAPI(username, password).then((res) => {
            if(res) {
                localStorage.setItem("token", res?.data.token);
                const userObj = {
                    userName: res?.data.userName,
                    email: res?.data.email
                }
                //Pass in the object to our local storage
                localStorage.setItem("user", JSON.stringify(userObj));
                setToken(res?.data.token!); //Set the user's token
                setUser(userObj!); //Set the user object (like above, ! ==> means it can be null possibly)
                toast.success("Login Success!") //If all the above worked, the user logged in, give a success message
                navigate("/search") //Send the user to the search page in the application when logged in
            }
        }).catch((e) => toast.warning("Server error occured"));
    };

    //Check if the user is logged in
    const isLoggedIn = () => {
        return !!user;
    };

    //Logout a user
    const logout = () => {
        localStorage.removeItem("token");
        localStorage.removeItem("user");
        setUser(null);
        setToken("");
        navigate("/") //Send the user back to the home page when logged out
    };

    return (
        <UserContext.Provider
            value={{ loginUser, user, token, logout, isLoggedIn, registerUser }}
        >
            {isReady ? children : null}
        </UserContext.Provider>
    );
};

//Export this as a custom hook
export const useAuth = () => React.useContext(UserContext);