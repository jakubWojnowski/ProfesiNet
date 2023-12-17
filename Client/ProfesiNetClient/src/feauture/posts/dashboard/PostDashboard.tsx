import  {FC} from "react";
import {Grid} from "semantic-ui-react";
import {Post} from "../../../app/modules/interfaces/Post.ts";
import './PostDashboard.css';
import PostList from "./PostList.tsx";
import PostEditForm from "../form/PostEditForm.tsx";
import {useStore} from "../../../app/stores/Store.ts";
import {observer} from "mobx-react-lite";

interface Props {
    posts: Post[];
    
}

const PostDashboard: FC<Props> = ({posts}: Props) =>  {
    const {postStore} = useStore();
    const {editMode} = postStore;
    return (
        <Grid centered={true}>
        <Grid.Column width={10}>
            <PostList posts={posts}
                      
            />
            </Grid.Column>
            {editMode && (
                <PostEditForm
                />
            )}
            
        </Grid>
         
    );
}

export default observer(PostDashboard);
