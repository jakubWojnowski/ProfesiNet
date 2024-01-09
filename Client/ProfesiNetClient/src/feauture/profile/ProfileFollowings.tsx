import {observer} from "mobx-react-lite";
import {useStore} from "../../app/stores/Store.ts";
import {Tab, Grid, Header, Card} from "semantic-ui-react";
import ProfileCard from "./ProfileCard.tsx";
import {useEffect} from "react";

export default observer(function ProfileFollowings(){
const {profileStore} = useStore();
const {loadingFollowings, profile, followings, loadFollowings, loadFollowers} = profileStore;

    useEffect(() => {
        loadFollowers(profile!.id);
    }, [loadFollowings, loadFollowers, profile!.id]);

    return (
        <Tab.Pane loading={loadingFollowings}>
            <Grid>
                <Grid.Column width='16'>
                    <Header
                        floated='left'
                        icon='user'
                        content={
                             `People following ${profile!.name} ${profile!.surname}`}
                    />
                </Grid.Column>
                <Grid.Column width='16'>
                    <Card.Group itemsPerRow='5'>
                        {followings.map(profile => (
                            <ProfileCard key={profile.id} profile={profile} />
                        ))}     
                    </Card.Group>
                </Grid.Column>
            </Grid>
        </Tab.Pane>
    )
})


