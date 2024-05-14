//Use tsfrafce snippet to get it started quickly

//We need to import even local images and files and classes etc using common pathing
import sharkImg from "./shark.jpg"
 
type Props = {}


//Practice card
const Card = (props: Props) => {
  return (
    <div className="card">
        <img
            src={sharkImg}
            alt="Image"
        />
        <div className="details">
            <h2>AAPL</h2>
            <p>$110</p>
        </div>
        <p className="Infon">
            Lorem ipsum dolor sit amet consectetur, adipisicing elit. Eos quis recusandae at error. Atque, reiciendis et quia incidunt asperiores blanditiis autem eaque, veniam tenetur, quaerat nemo amet soluta adipisci quo?
        </p> 
    </div> 
  )
}

export default Card