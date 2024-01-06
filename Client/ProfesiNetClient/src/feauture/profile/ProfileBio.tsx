import {FC} from "react";
import {Button, Grid, Header, Item, Segment} from "semantic-ui-react";
import {Profile} from "../../app/modules/interfaces/Profile.ts";
import {observer} from "mobx-react-lite";
import {useStore} from "../../app/stores/Store.ts";
import EditProfileBio from "./forms/EditProfileBio.tsx";
export interface ProfileBioProps {
    profile:Profile;
}
const ProfileBio: FC<ProfileBioProps> = ({profile}:ProfileBioProps) => {
    const {profileStore:{isCurrentUser}} = useStore();
    const {modalStore} = useStore();
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
                                        {profile.bio}
                                    </Item.Description>
                                    {isCurrentUser && (
                                        <Button icon='edit' content='Edit' floated='right'  onClick={()=> modalStore.openModal(<EditProfileBio />)}/>
                                    )}
                                   
                                </Item.Content>
                            </Item>
                        </Item.Group>
                    </Grid.Column>
                </Grid.Row>
            </Grid>
        </Segment>
    );
};

export default observer(ProfileBio);