//Use tsfrafce snippet to get it started quickly

//We need to import even local images and files and classes etc using common pathing
import sharkImg from "./shark.jpg";
 
//Do checks on the data to make sure it's correct (The data tha'll be passed for the card)
interface Props {
  companyName: string;
  ticker: string;
  price: number;
}


//Practice card (Pass the interface prop data down)
const Card: React.FC<Props> = ({ companyName, ticker, price }: Props) : JSX.Element => {
  //Put variables carrying the data in {}'s in the HTML/React <>'s
  return (
    <div className="card">
        <img
            src={sharkImg}
            alt="Image"
        />
        <div className="details">
            <h2>{companyName} ({ticker})</h2>
            <p>${price}</p>
        </div>
        <p className="Infon">
            Lorem ipsum dolor sit amet consectetur, adipisicing elit. Eos quis recusandae at error. Atque, reiciendis et quia incidunt asperiores blanditiis autem eaque, veniam tenetur, quaerat nemo amet soluta adipisci quo?
        </p> 
    </div> 
  )
}

export default Card