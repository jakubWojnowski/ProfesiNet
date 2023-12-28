import {FC} from "react";
import {observer} from "mobx-react-lite";
import {Container, Header, Segment} from "semantic-ui-react";
import {useStore} from "../stores/Store.ts";

const ServerError: FC = () => {
    const {commonStore} = useStore();
    return (
        <Container>
            <Header as='h1' content='Server Error'/>
            <Header sub as='h5' color='red' content={commonStore.error?.message}/>
            {commonStore.error?.details &&(
                <Segment>
                    <Header as='h4' content='Stack trace'/>
                    <code style={{marginTop: '10px'}}>{commonStore.error.details}</code>
                </Segment>
            )}
                
        </Container>
    );
};

export default observer(ServerError);