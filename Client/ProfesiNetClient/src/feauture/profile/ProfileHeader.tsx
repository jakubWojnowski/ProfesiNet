import {FC, useState} from 'react';
import {Button, Grid, Statistic, Segment, Image, Item, Header} from 'semantic-ui-react';
import 'semantic-ui-css/semantic.min.css';
import {useStore} from "../../app/stores/Store.ts";
import EditProfileHeaderForm from "./forms/EditProfileHeaderForm.tsx";
import {Profile} from "../../app/modules/interfaces/Profile.ts";
import {observer} from "mobx-react-lite";

export interface ProfileHeaderProps {
    profile:Profile;
}
const ProfileHeader: FC<ProfileHeaderProps> = ({profile}:ProfileHeaderProps) => {
    const [isFollowing, setIsFollowing] = useState(false);
    const {modalStore} = useStore();
    const {profileStore:{isCurrentUser}} = useStore();


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
                            src={profile.profilePicture || '/assets/user.png'} // Your user image path
                            avatar
                        />
                       <Header as='h1'>{profile.name +" " + profile.surname}
                        <Header.Subheader>
                            {profile.title}
                        </Header.Subheader>
                           <Header.Content>
                                 {profile.address}
                            </Header.Content>
                        </Header>
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
                        {isCurrentUser && (
                        <Button icon='edit' content='Edit' onClick={()=> modalStore.openModal(<EditProfileHeaderForm />)}/>
                        )  }
                    </Grid.Column>
                </Grid.Row>
            </Grid>
        </Segment>
    );
};

export default observer(ProfileHeader);
