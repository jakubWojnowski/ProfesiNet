
import  {FC} from "react";
import {Button, Container, Menu} from "semantic-ui-react";
import {NavLink} from "react-router-dom";

    


const NavBar: FC = () =>  {
    return (
        <Menu inverted fixed='top' size="large">
            <Container  fluid={true}> {/* This will take up available space */}
                <Menu.Item as={NavLink} to='/' header>
                    <img src="/assets/logo.png" alt="logo" style={{ marginRight: '10px' }} />
                </Menu.Item>
                <Menu.Item name='Friends' />
                <Menu.Item name='Messages' />
                <Menu.Item name='Profile' />
                <Menu.Item as={NavLink} to='/posts' name='Posts' />
            </Container>
            <Container fluid={true}> {/* This will be pushed to the right */}
                <Menu.Item position='right'>
                    {/*<Button positive content='Login' />*/}
                    {/*<Button positive content='Register' style={{ marginLeft: '10px' }} />*/}
                </Menu.Item>
            </Container>
        </Menu>
    );
}

export default NavBar;