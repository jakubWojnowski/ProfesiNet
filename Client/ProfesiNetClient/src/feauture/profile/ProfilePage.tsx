import {FC} from "react";
import {Grid} from "semantic-ui-react";
import ProfileHeader from "./ProfileHeader.tsx";
import ProfileActivities from "./ProfileActivities.tsx";
import ProfileExperience from "./ProfileExperience.tsx";
import ProfileEducation from "./ProfileEducation.tsx";



const ProfilePage: FC = () => {
    return (
        
        <Grid centered={true}>
            <Grid.Column width={16} >
                <ProfileHeader/>
            </Grid.Column>
            <Grid.Column width={16}>
                <ProfileActivities/>
            </Grid.Column>
            <Grid.Column width={16}>
                <ProfileExperience/>
            </Grid.Column>
            <Grid.Column width={16}>
                <ProfileEducation/>
            </Grid.Column>
            
        </Grid>
     
    );
};

export default ProfilePage;