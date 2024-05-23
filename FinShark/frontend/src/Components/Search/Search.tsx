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
        <section className="relative bg-gray-100">
        <div className="max-w-4xl mx-auto p-6 space-y-6">
          <form
            className="form relative flex flex-col w-full p-10 space-y-4 bg-darkBlue rounded-lg md:flex-row md:space-y-0 md:space-x-3"
            onSubmit={onSearchSubmit}
          >
            <input
              className="flex-1 p-3 border-2 rounded-lg placeholder-black focus:outline-none"
              id="search-input"
              placeholder="Search companies"
              value={search}
              onChange={handleSearchChange}
            ></input>
          </form>
        </div>
      </section>
    ) 
}

export default Search
