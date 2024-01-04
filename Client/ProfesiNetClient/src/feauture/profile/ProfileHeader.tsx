import {FC, useState} from 'react';
import {Button, Grid, Statistic, Segment, Image, Item} from 'semantic-ui-react';
import 'semantic-ui-css/semantic.min.css';
import {useStore} from "../../app/stores/Store.ts";
import EditProfileHeaderForm from "./forms/EditForm.tsx"; // Ensure Semantic UI CSS is imported

const ProfileHeader: FC = () => {
    const [isFollowing, setIsFollowing] = useState(false);
    const {userStore,modalStore} = useStore();


    const handleFollowClick = () => {
        setIsFollowing(!isFollowing);
    };

    return (
        <Segment>
            <Grid>
                <Grid.Row>
                    <Grid.Column width={11}>
                        <Image
                            floated='left'
                            size='small'
                            src='/assets/user.png' // Your user image path
                            avatar
                        />
                        <Item.Content verticalAlign='middle'>
                            <Item.Header as='h1'>Display Name</Item.Header>
                            <Item.Extra as='h2'>Title</Item.Extra>
                            <Item.Extra>Krakow</Item.Extra>
                        </Item.Content>
                    </Grid.Column>
                    <Statistic.Group  >
                        <Statistic label='FOLLOWERS' value='52' />
                        <Statistic label='FOLLOWING' value='42' />
                        <Statistic label='FRIENDS' value='42' />
                    </Statistic.Group>
                    <Grid.Column width={6} textAlign='center' >
                        
                     
                        <Item style={{ marginTop: '1em' }} >
                            <Button size={'large'}
                                color={isFollowing ? 'red' : 'teal'}
                                content={isFollowing ? 'Unfollow' : 'Follow'}
                                onClick={handleFollowClick}
                            />
                            <Button basic color='blue' content='Message' size={'large'}  />
                            <Button basic color='green' content='Send Invitation' size={'large'} />
                        </Item>
                    </Grid.Column>
                    <Grid.Column width={16} textAlign='right'>
                        <Button icon='edit' content='Edit' onClick={()=> modalStore.openModal(<EditProfileHeaderForm/>)}/>
                    </Grid.Column>
                </Grid.Row>
            </Grid>
        </Segment>
    );
};

export default ProfileHeader;
