import {FC} from 'react';
import {Button, Grid, Statistic, Segment, Image, Header, Divider} from 'semantic-ui-react';
import 'semantic-ui-css/semantic.min.css';
import {useStore} from "../../app/stores/Store.ts";
import EditProfileHeaderForm from "./forms/EditProfileHeaderForm.tsx";
import {Profile} from "../../app/modules/interfaces/Profile.ts";
import {observer} from "mobx-react-lite";
import FollowButton from "./FollowButton.tsx";

export interface ProfileHeaderProps {
    profile:Profile;
}
const ProfileHeader: FC<ProfileHeaderProps> = ({profile}:ProfileHeaderProps) => {
    const {modalStore} = useStore();
    const {profileStore:{isCurrentUser}} = useStore();



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
                   <Grid.Column width={4}>
                    <Statistic.Group >
                       
                        <Statistic label='FOLLOWERS' value={profile.followersCount} />
                        <Statistic label='FOLLOWING' value={profile.followingsCount} />
                       
                    </Statistic.Group>
                    <Divider/>
                       <FollowButton profile={profile}/>
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
