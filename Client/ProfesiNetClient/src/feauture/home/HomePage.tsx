import {Container} from "semantic-ui-react";
import {Link} from "react-router-dom";

export default function HomePage() {



    return( 
    <Container style = {{marginTop: '7em'}}>
        <h1>zarabiam milliony o to dane z zeszlego roku 999999999999999</h1>
        <h3> Go to <Link to='/posts'> Posts </Link> </h3>
    </Container>
        )
}