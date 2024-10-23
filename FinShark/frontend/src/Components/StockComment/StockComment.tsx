import { toast } from "react-toastify";
import { commentPostAPI } from "../../Services/CommentService";
import StockCommentForm from "./StockCommentForm/StockCommentForm";

type Props = {
    stockSymbol: string;
};

//Type the comment form inputs
type CommentFormInputs = {
    title: string;
    content: string;
};

const StockComment = ({ stockSymbol }: Props) => {
    const handleComment = (e: CommentFormInputs) => {
        //We're gonna have to post to the database and make an API call we need to make a service
        commentPostAPI(e.title, e.content, stockSymbol)
        .then((res) => {
            //Check if the res is actually defined
            if(res) {
                //If it is and there's actually data, then the comment was created!
                toast.success("Comment created successfully!")
            }
        }).catch((e) => { //For any other unknown errors that still can't be figured out display them from this code
            toast.warning(e); //Like for SQL and connection errors, and other serious server issues
        })
    }
  return ( <StockCommentForm symbol={stockSymbol} handleComment={handleComment}/>
  )
};

export default StockComment