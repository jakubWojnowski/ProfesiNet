import {FC} from "react";
import {Button, Grid, Header, Item, Segment} from "semantic-ui-react";

const ProfileBio: FC = () => {
    return (
        <Segment>
            <Grid>
                <Grid.Row>
                    <Grid.Column width={16}>
                        <Header size={"large"}>Bio</Header>
                    </Grid.Column>
                </Grid.Row>
                <Grid.Row>
                    <Grid.Column width={16}>
                        <Item.Group divided>
                            <Item>
                                <Item.Content>
                                    <Item.Description>
                                        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed neque elit, tristique
                                        placerat feugiat ac, facilisis vitae arcu. Proin eget egestas augue. Praesent ut sem
                                        nec arcu pellentesque aliquet. Duis dapibus diam vel metus tempus vulputate.
                                    </Item.Description>
                                    <Button icon='edit' content='Edit' floated='right' />
                                </Item.Content>
                            </Item>
                        </Item.Group>
                    </Grid.Column>
                </Grid.Row>
            </Grid>
        </Segment>
    );
};

export default ProfileBio;