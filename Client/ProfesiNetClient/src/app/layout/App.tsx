import {Fragment} from "react";
import './styles.css'; // Make sure this is included
import {Container} from "semantic-ui-react";
import NavBar from "./navBar/NavBar.tsx";
import {observer} from "mobx-react-lite";
import {Outlet, useLocation} from "react-router-dom";
import HomePage from "../../feauture/home/HomePage.tsx";

function App() {
    const location = useLocation();


    return (
        <>
            {location.pathname === '/' ? <HomePage/> : (
                <>
                    <Fragment>
                        <NavBar/>
                        <Container style={{marginTop: '7em'}}>
                            <Outlet/>
                        </Container>
                    </Fragment>
                </>
            )}

            )
        </>
    );

}

export default observer(App);
