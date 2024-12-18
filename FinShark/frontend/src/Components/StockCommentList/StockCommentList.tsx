import { CommentGet } from "../../Models/Comment";
import StockCommentListItem from "../StockCommentListItem/StockCommentListItem";

type Props = {
    comments: CommentGet[];
}

const StockCommentList = ({comments}: Props) => {
  return (
    <>
    {/* Get the comments and if they don't exist, return an empty string "" */}
    {comments 
        ? comments.map((comment) => {
            return <StockCommentListItem comment={comment} />;
          })
        : ""}
    </>
  )
}

export default StockCommentList