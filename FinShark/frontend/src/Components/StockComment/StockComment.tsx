import { useEffect, useState } from "react";
import { toast } from "react-toastify";
import { CommentGet } from "../../Models/Comment";
import { commentGetAPI, commentPostAPI } from "../../Services/CommentService";
import Spinner from "../Spinner/Spinner";
import StockCommentList from "../StockCommentList/StockCommentList";
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
    const [comments, setComment] = useState<CommentGet[] | null>(null);
    const [loading, setLoading] = useState<boolean>(); //Loader for loading the comments from using above code

    //Also get comments when we render the app
    useEffect(() => {
        getComments();
    }, []);

    const handleComment = (e: CommentFormInputs) => {
        //We're gonna have to post to the database and make an API call we need to make a service
        commentPostAPI(e.title, e.content, stockSymbol)
        .then((res) => {
            //Check if the res is actually defined
            if(res) {
                //If it is and there's actually data, then the comment was created!
                toast.success("Comment created successfully!")
                getComments(); //After succesfully creating a comment, show it with the other comments
            }
        }).catch((e) => { //For any other unknown errors that still can't be figured out display them from this code
            toast.warning(e); //Like for SQL and connection errors, and other serious server issues
        })
    }

//Load the comments
const getComments = () => {
    setLoading(true);
    commentGetAPI(stockSymbol)
    .then((res) => {
        setLoading(false);
        setComment(res?.data!);
    })
}

  return ( 
    <div className="flex flex-col">
        {loading ? <Spinner /> : <StockCommentList comments={comments!} />}
        <StockCommentForm symbol={stockSymbol} handleComment={handleComment}/>
    </div>
  )
};

export default StockComment