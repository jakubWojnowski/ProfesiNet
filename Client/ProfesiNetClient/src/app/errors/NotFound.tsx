import {FC} from "react";
import {Button, Header, Icon, Segment} from "semantic-ui-react";
import {Link} from "react-router-dom";


const NotFound: FC = () => {
    return (
     <Segment placeholder>
         <Header icon>
             <Icon name='search'/>
                Oops - we've looked everywhere but couldn't find this.
            </Header>
            <Segment.Inline>
              <Button as={Link} to="/posts"> Return to posts page</Button>
            </Segment.Inline>
         </Segment>
    );
};

export default NotFound;