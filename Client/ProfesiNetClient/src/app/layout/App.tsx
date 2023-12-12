import { Fragment } from "react";
import './styles.css'; // Make sure this is included
import { Container } from "semantic-ui-react";
import NavBar from "./navBar/NavBar.tsx";
import Mid from "./mid/Mid.tsx";

function App() {
  


    return (
        <Fragment>
            <NavBar/>
            <Container  style={{ marginTop: '7em'}}>
               <Mid/>
            </Container>
        </Fragment>
    );
}

export default App;
