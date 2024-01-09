import React, {FC, useEffect} from 'react';
import { observer } from 'mobx-react-lite';
import { Button } from 'semantic-ui-react';
import { useStore } from '../../app/stores/Store';
import {Profile} from "../../app/modules/interfaces/Profile.ts";

interface Props {
    profile: Profile;
}

const FollowButton: FC<Props> = ({ profile }) => {
    const { profileStore, userStore } = useStore();
    const { addFollowing, unfollowUser, loading } = profileStore;

    if (userStore.user?.id === profile.id) return null;

    function handleFollow(e: React.MouseEvent<HTMLButtonElement>) {
        e.preventDefault();
        if (profile.following) {
            unfollowUser(profile.id, false).then(() => {
            });
        } if(!profile.following){
            addFollowing(profile.id, true).then(() => {
            });
        }
    }

    return (
        <Button
            onClick={handleFollow}
            loading={loading}
            fluid
            color={profile.following ? 'red' : 'green'}
            content={profile.following ? 'Unfollow' : 'Follow'}
        />
    );
};

export default observer(FollowButton);
