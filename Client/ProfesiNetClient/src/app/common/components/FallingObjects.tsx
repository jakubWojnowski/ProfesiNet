// Snowflakes.js
import {FC} from 'react';
import './FallingObjects.css';
interface FallingObjectsProps {
    imageSrc?: string; // Optional prop for image source
    numberOfObjects: number; // Required prop for number of objects
    size?: number; 
}

const FallingObjects: FC<FallingObjectsProps> = ({imageSrc, numberOfObjects, size}) => {


    const elements = Array.from({ length: numberOfObjects }, (_, index) => {
        const defaultSize = size || 20;
        const style = {
            left: `${Math.random() * 100}vw`, // Random horizontal position
            animationDelay: `${Math.random() * 10}s`, // Random delay for varied start times
            animationDuration: `${Math.random() * 5 + 5}s`, // Random duration between 5 and 10 seconds
            width: `${defaultSize}px`, // Set width using the size prop
            height: `${defaultSize}px`, // Set height using the size prop
        };

        return imageSrc ? (
            <img
                key={index}
                src={imageSrc}
                className="falling-object"
                style={style}
                alt="Falling object"
               
            />
        ) : (
            <div key={index} className="falling-object" style={style}></div>
        );
    });

    return <div className="falling-objects-container" >{elements}</div>;
};



export default FallingObjects;
