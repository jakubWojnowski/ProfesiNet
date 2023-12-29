import {Fragment, useEffect} from "react";
import './styles.css'; // Make sure this is included
import {Container} from "semantic-ui-react";
import NavBar from "./navBar/NavBar.tsx";
import {observer} from "mobx-react-lite";
import {Outlet, useLocation} from "react-router-dom";
import HomePage from "../../feauture/home/HomePage.tsx";
import {ToastContainer} from "react-toastify";
import {useStore} from "../stores/Store.ts";
import LoadingComponent from "./components/LoadingComponent.tsx";
import ModalContainer from "../common/modals/ModalContainer.tsx";

function App() {
    const location = useLocation();
    const {commonStore, userStore} = useStore();
    
    useEffect(() => {
        if (commonStore.token) {
            userStore.getUser().finally(() => commonStore.setAppLoaded());
        } else {
            commonStore.setAppLoaded();
        }
    }
    , [commonStore, userStore]);
    
    if (!commonStore.appLoaded) return <LoadingComponent content='Loading app...'/>


    return (
        <>
            <ModalContainer/>
            <ToastContainer position='bottom-right' hideProgressBar theme='colored'/>
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

     
        </>
    );

}

export default observer(App);
