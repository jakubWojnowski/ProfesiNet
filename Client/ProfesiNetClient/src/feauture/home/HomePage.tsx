import {Button, Container, Header, Image, Segment} from "semantic-ui-react";
import {Link} from "react-router-dom";
import FallingObjects from "./FallingObjects.tsx";


export default function HomePage() {


    return (
        <>
           
        <Segment inverted textAlign='center' vertical className='masthead'>
         <FallingObjects numberOfObjects={200} imageSrc={"/Coin.webp"} size={30}/>
          
            <Container text>
                <Header as='h1' inverted>
                    <Image size='massive' src='/assets/ProfesiNet.png' alt='logo' style={{marginBottom: 12}}/>
                    ProfesiNet
                </Header>
                <Header as='h2' inverted content='Welcome to ProfesiNet'/>
                <Button as={Link} to='/login' size='huge' inverted>
                    Login!
                </Button>
            </Container>
        
        </Segment>
        </>
    )

}