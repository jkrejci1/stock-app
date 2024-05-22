import React, { ChangeEvent, SyntheticEvent } from 'react';

//Need to know the type of data thats gonna be sent down (bring in event handeling functions from app.tsx)
//Checks that the data being passed down to our <Search /> object is correct
interface Props {
    //Return void as react is functional, doesn't want outside state being touched
    onClick: (e: SyntheticEvent) => void;
    search: string | undefined; //Anticipate an undefined error, don't put void as it's the state and not an event value
    handleChange: (e: ChangeEvent<HTMLInputElement>) => void;
} 

//Need to set up props for data to be passed down (pass down the event handeling functions that will be needed as parameters)
const Search : React.FC<Props> = ({
    onClick,
    search,
    handleChange,
}: Props) : JSX.Element => {

    return (
        //Use event handeling to handle the search (e contains data on the event)
        <div>
            <input value={search} onChange={(e) => handleChange(e)}></input>
            <button onClick={(e) => onClick(e)} />
        </div>
    ) 
}

export default Search
