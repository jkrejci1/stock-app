//For API calls that don't need to be in the context for authentication
import axios from "axios";
import { handleError } from "../Helpers/ErrorHandler";
import { UserProfileToken } from "../Models/User";

//REMEMBER THE LOCALHOST NUMBER MUST MATCH THE PORT YOU ARE USING FOR AXIOS SWAGGER ON YOUR BACK END!!!! (To check what that number is --> Run dotnet watch run in the api folder, and in the url you will see localhost:(THE NUMBER YOU NEED)/swagger/index.html)
//ABOVE THIS COMMENT IS HOW I SOLVED THE Failed to load resource: net::ERR_CONNECTION_REFUSED error!!
//You also are going to need to run the backend as well with dotnet watch run in finshark/api folder at the same time to work
//MIGHT NEED TO PLAY AROUND WITH THIS WHEN ATTEMPTING TO DEPLOY!!
const api = "http://localhost:5144/api/";

//API call for logging in
export const loginAPI = async (username: string, password: string) => {
  try {
    const data = await axios.post<UserProfileToken>(api + "account/login", {
      username: username,
      password: password,
    });
    return data;
  } catch (error) {
    handleError(error);
  }
};

//API call for registering
export const registerAPI = async (
  email: string,
  username: string,
  password: string
) => {
  try {
    const data = await axios.post<UserProfileToken>(api + "account/register", {
      email: email,
      username: username,
      password: password,
    });
    return data;
  } catch (error) {
    handleError(error);
  }
};