import {FC, useEffect} from "react";
import {Grid} from "semantic-ui-react";
import './PostDashboard.css';
import PostList from "./PostList.tsx";
import PostEditForm from "../form/PostEditForm.tsx";
import {useStore} from "../../../app/stores/Store.ts";
import {observer} from "mobx-react-lite";
import PostForm from "../form/PostForm.tsx";
import LoadingComponent from "../../../app/layout/components/LoadingComponent.tsx";



const PostDashboard: FC = () =>  {
    const {postStore} = useStore();
    const {loadPosts, postRegistry} = postStore;
    
    useEffect(() => {
        if(postRegistry.size <= 1) loadPosts().then(() => postStore.setLoadingInitial(false));

    }, [loadPosts]);


    if (postStore.loadingInitial) return <LoadingComponent content='Loading app...'/>;
    const {editMode} = postStore;
    return (
        <Grid centered={true}>
        <Grid.Column width={10} >
            <PostForm />
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
