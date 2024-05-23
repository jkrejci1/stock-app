import React, { ChangeEvent, SyntheticEvent } from 'react';

//Need to know the type of data thats gonna be sent down (bring in event handeling functions from app.tsx)
//Checks that the data being passed down to our <Search /> object is correct
interface Props {
    //Return void as react is functional, doesn't want outside state being touched
    //onClick: (e: SyntheticEvent) => void;
    onSearchSubmit: (e: SyntheticEvent) => void;
    search: string | undefined; //Anticipate an undefined error, don't put void as it's the state and not an event value
    handleSearchChange: (e: ChangeEvent<HTMLInputElement>) => void;
    //handleChange: (e: ChangeEvent<HTMLInputElement>) => void;
} 

//Need to set up props for data to be passed down (pass down the event handeling functions that will be needed as parameters)
const Search : React.FC<Props> = ({
    //onClick,
    onSearchSubmit,
    search,
    handleSearchChange
    //handleChange,
}: Props) : JSX.Element => {

    return (
        //Use event handeling to handle the search (e contains data on the event)
        /**<div>
            <input value={search} onChange={(e) => handleChange(e)}></input>
            <button onClick={(e) => onClick(e)} />
        </div>
        */

        //You can also use a form instead to prevent extra work like example above and use enter to search instead of clicking on button
        <>
            <form onSubmit={onSearchSubmit}>
                <input value={search} onChange={handleSearchChange} />
            </form>
        </>
    ) 
}

export default Search
