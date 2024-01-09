import { observer } from 'mobx-react-lite';
import { Tab } from 'semantic-ui-react';
import ProfileFollowings from './ProfileFollowings';
import {useStore} from "../../app/stores/Store.ts";



export default observer(function ProfileFollowingsPanel() {
    const {profileStore} = useStore();
    const panes = [

        { menuItem: 'Followers', render: () => <ProfileFollowings /> },
        { menuItem: 'Following', render: () => <ProfileFollowings /> },
    ];

    return (
        <Tab
            menu={{ fluid: true, vertical: true }}
            menuPosition='right'
            panes={panes}
            onTabChange={(_, data) => profileStore.setActiveTab(data.activeIndex as number)}
        />
    )
})