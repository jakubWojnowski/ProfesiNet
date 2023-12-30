import {FC} from "react";
import {Grid} from "semantic-ui-react";
import ProfileHeader from "./ProfileHeader.tsx";



const ProfilePage: FC = () => {
    return (
        <Grid centered={true}>
            <Grid.Column width={16} >
                <ProfileHeader/>
                
            </Grid.Column>
                
                
          

        </Grid>
     
    );
};

export default ProfilePage;