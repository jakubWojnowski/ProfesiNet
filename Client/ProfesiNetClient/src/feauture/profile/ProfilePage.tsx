import {FC, useEffect} from "react";
import {Grid} from "semantic-ui-react";
import ProfileHeader from "./ProfileHeader.tsx";
import ProfileActivities from "./ProfileActivities.tsx";
import ProfileExperience from "./ProfileExperience.tsx";
import ProfileEducation from "./ProfileEducation.tsx";
import ProfileBio from "./ProfileBio.tsx";
import ProfileSkills from "./ProfileSkills.tsx";
import {observer} from "mobx-react-lite";
import {useParams} from "react-router-dom";
import {useStore} from "../../app/stores/Store.ts";
import LoadingComponent from "../../app/layout/components/LoadingComponent.tsx";
import ProfileFollowingsPanel from "./ProfileFollowingsPanel.tsx";



const ProfilePage: FC = () => {
    const {userId} = useParams<{userId: string}>();
    const {profileStore, postStore} = useStore();
    const {loadingProfile, loadProfile, profile} = profileStore;
    const {loadCreatorPosts} = postStore;


    useEffect(() => {
        if (userId) {
            loadProfile(userId).then(() => console.log(profile));
        }
    }, [loadProfile, userId]);
    
    useEffect(() => {
        if (userId)
        loadCreatorPosts(userId).then(r => console.log(r));
    }, [loadCreatorPosts, userId]);
    
    if (loadingProfile) return <LoadingComponent content={"loading profile"}/>;
    return (
        
        <Grid centered={true}>
            
            <Grid.Column width={16} >
                {profile && (
                <ProfileHeader profile={profile}/>
                )}
                
            </Grid.Column>
            <Grid.Column width={16}>
                {profile && (
                <ProfileBio profile={profile}/>
                )}
            </Grid.Column>
            <Grid.Column width={16}>
                <ProfileActivities/>
            </Grid.Column>
            <Grid.Column width={16}>
                {profile && (
                <ProfileExperience profile={profile}/>
                )}
            </Grid.Column>
            <Grid.Column width={16}>
                {profile && (
                <ProfileEducation profile={profile}/>
                )}
            </Grid.Column>
            <Grid.Column width={16}>
                {profile && (
                    <ProfileSkills profile={profile}/>
                )}
            </Grid.Column>
            <Grid.Column width={16}>
                {profile && (
                    <ProfileFollowingsPanel/>
                )}
            </Grid.Column>
            
            
        </Grid>
     
    );
};

export default observer(ProfilePage);