import  {FC} from "react";
import {Grid} from "semantic-ui-react";
import './PostDashboard.css';
import PostList from "./PostList.tsx";
import PostEditForm from "../form/PostEditForm.tsx";
import {useStore} from "../../../app/stores/Store.ts";
import {observer} from "mobx-react-lite";



const PostDashboard: FC = () =>  {
    const {postStore} = useStore();
    const {editMode} = postStore;
    return (
        <Grid centered={true}>
        <Grid.Column width={10}>
            <PostList/>
            </Grid.Column>
            {editMode && (
                <PostEditForm
                />
            )}
            
        </Grid>
         
    );
}

export default observer(PostDashboard);
