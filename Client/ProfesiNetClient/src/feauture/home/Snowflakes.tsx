// Snowflakes.js
import {FC} from 'react';
import './Snowflakes.css';


const Snowflakes: FC = () => {
    const numberOfSnowflakes = 200; // Change this number for more or fewer snowflakes

    // Generate snowflake elements with random starting positions
    const snowflakes = Array.from({ length: numberOfSnowflakes }, (_, index) => (
        <img
            key={index}
            src="/assets/Braun.jpg" // Update the path to your dollar image
            className="snowflake" // You might want to rename this class
            style={{
                left: `${Math.random() * 100}vw`, // Random horizontal position
                animationDelay: `${-Math.random() * 10}s`, // Negative delay for varied start times
                animationDuration: `${Math.random() * 5 + 5}s`, // Random duration between 5 and 10 seconds
            }}
            alt="Dollar"
        />
    ));

    return <div className="snowflake-container">{snowflakes}</div>;
};



export default Snowflakes;
// left: `${Math.random() * 100}vw`, // Random horizontal position
//     animationDelay: `${-Math.random() * 10}s`, // Negative delay for varied start times
//     animationDuration: `${Math.random() * 5 + 5}s`, // Random duration between 5 and 10 seconds