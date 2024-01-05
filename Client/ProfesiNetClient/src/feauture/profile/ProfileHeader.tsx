import {FC, useState} from 'react';
import {Button, Grid, Statistic, Segment, Image, Item} from 'semantic-ui-react';
import 'semantic-ui-css/semantic.min.css';
import {useStore} from "../../app/stores/Store.ts";
import EditProfileHeaderForm from "./forms/EditForm.tsx";
import {Profile} from "../../app/modules/interfaces/Profile.ts";
import {observer} from "mobx-react-lite"; // Ensure Semantic UI CSS is imported
export interface ProfileHeaderProps {
    profile:Profile;
}
const ProfileHeader: FC<ProfileHeaderProps> = ({profile}:ProfileHeaderProps) => {
    const [isFollowing, setIsFollowing] = useState(false);
    const {modalStore} = useStore();


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
                            src={profile.profilePicture} // Your user image path
                            avatar
                        />
                        <Item.Content verticalAlign='middle'>
                            <Item.Header as='h2'>{profile.name +" " + profile.surname}</Item.Header>
                            <Item.Extra as='h3'>{profile.title}</Item.Extra>
                            <Item.Extra>{profile.address}</Item.Extra>
                        </Item.Content>
                    </Grid.Column>
                    <Statistic.Group >
                        <Statistic label='FOLLOWERS' value={profile.followersCount} />
                        <Statistic label='FOLLOWING' value={profile.followingsCount} />
                        <Statistic label='FRIENDS' value={profile.networkConnectionsCount} />
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

export default observer(ProfileHeader);
