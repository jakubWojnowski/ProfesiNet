import { Fragment, useEffect, useState } from "react";
import './styles.css'; // Make sure this is included
import { Button, Container, Icon, Label, List, Segment } from "semantic-ui-react";
import NavBar from "./navBar/NavBar.tsx";
import Mid from "./mid/Mid.tsx";
import PostCreator from "../../feauture/posts/dashboard/CreatePost.tsx";

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
