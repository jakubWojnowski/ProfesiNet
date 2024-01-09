import { Card, Image } from "semantic-ui-react";
import { observer } from "mobx-react-lite";
import { Link } from "react-router-dom";
import {Profile} from "../../app/modules/interfaces/Profile.ts";

interface Props {
    profile: Profile
}

export default observer(function ProfileCard({ profile }: Props) {
    function truncate(str: string | undefined) {
        if (str) {
            return str.length > 5 ? str.substring(0, 15) + '...' : str;
        }
    }

    return (
        <Card as={Link} to={`/profile/${profile.id}`}>
            <Image  circular src={profile.profilePicture || '/assets/user.png'} />
            <Card.Content>
                <Card.Header>{profile.name }</Card.Header>
                <Card.Header>{profile.surname }</Card.Header>
                <Card.Description>
                    {truncate(profile.title)}
                </Card.Description>
             
            </Card.Content>
            <Card.Content extra>
                {profile.address}
            </Card.Content>
         
        </Card>
    )
})