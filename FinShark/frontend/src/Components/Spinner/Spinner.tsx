//Component for the loading spinner for loading screens
import { ClipLoader } from 'react-spinners'; //Import react spinner --> npm i react-spinners
import "./Spinner.css"; //CSS for centering spinner

type Props = {
    isLoading?: boolean;
}

const Spinner = ({ isLoading = true }: Props) => {
  return (
    <>
        <div id="loading-spinner">
            <ClipLoader
                color="#36d7b7"
                loading={isLoading} //We will use isLoadings true or false value in order to properly display the spinner
                size={35}
                aria-Label="Loading Spinner" //Used incase somebody has a disability when using our site
                data-testid="loader"
            />
        </div>
    </>
  )
}

export default Spinner