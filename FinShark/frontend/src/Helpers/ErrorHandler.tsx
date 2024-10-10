//The error handler file for handeling server side errors
import axios from "axios";
import { toast } from "react-toastify";

export const handleError = (error: any) => {
    if(axios.isAxiosError(error)){
        var err = error.response;
        if(Array.isArray(err?.data.errors)) {
            for(let val of err?.data.errors) {
                //Display errors in form of toast notification
                //Use react tostify for popups on screen for server side errors
                toast.warning(val.description);
            }
        } else if (typeof err?.data.errors === 'object') {
            for (let e in err?.data.errors) {
                toast.warning(err.data.errors[e][0]); //Display the error if it's an object
            }
        } else if (err?.data) { //Display errors that aren't narrowed down like from above
            toast.warning(err.data);
        } else if (err?.status == 401) { //This is used to make sure the user is authorized, if not they need to log in
            toast.warning("Please login!");
            //Redirect the user to the login page
            window.history.pushState({}, "LoginPage", "/login");
        } else if (err) {
            toast.warning(err?.data); //For errors that still can't be narrowed down, they will be displayed from this
        }
    }
}