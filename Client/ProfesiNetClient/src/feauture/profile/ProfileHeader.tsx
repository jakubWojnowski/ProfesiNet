import {FC} from "react";
import {Button, Divider, Grid, Item, Reveal, Segment, Statistic} from "semantic-ui-react";


const ProfileHeader: FC = () => {
    return (
        <Segment>
            <Grid.Column width={12}>
                <Item.Group>
                    <Item>
                        <Item.Image avatar size='small' src='/assets/user.png'/>
                        <Item.Content verticalAlign='middle'>
                            <Item.Header as='h1'>Display Name</Item.Header>
                            <Item.Extra> Title</Item.Extra>
                            <Item.Description as='h2'> Lorem ipsum dolor sit amet, consectetur adipisicing elit. Amet, placeat!</Item.Description>
                        </Item.Content>
                    </Item>
                </Item.Group>
            </Grid.Column>
            <Grid.Column width={4}>
              <Statistic.Group widths={3}>
                    <Statistic label='Followers' value='5'/>
                    <Statistic label='Following' value='42'/>
                    <Statistic label='Friends' value='42'/>
                </Statistic.Group>
                <Divider/>
                <Reveal animated='move'>
                    <Reveal.Content visible style={{width: '100%'}}>
                        <Button
                            fluid
                            color='teal'
                            content='Following'
                        />
                    </Reveal.Content>
                    <Reveal.Content hidden style={{width: '100%'}}>
                        <Button
                            fluid
                            basic
                            color={true ? 'red' : 'green'}
                            content={true ? 'Unfollow' : 'Follow'}
                        />
                        
                    </Reveal.Content>
                    
                </Reveal>
                
            </Grid.Column>
        </Segment>
     
    );
};

export default ProfileHeader;