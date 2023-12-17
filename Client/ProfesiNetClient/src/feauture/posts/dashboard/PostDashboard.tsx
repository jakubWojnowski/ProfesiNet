import  {FC} from "react";
import {Grid} from "semantic-ui-react";
import {Post} from "../../../app/modules/interfaces/Post.ts";
import './PostDashboard.css';
import PostList from "./PostList.tsx";
import PostEditForm from "../form/PostEditForm.tsx";
import {UpdatePost} from "../../../app/modules/interfaces/UpdatePost.ts";
import {useStore} from "../../../app/stores/Store.ts";
import {observer} from "mobx-react-lite";

interface Props {
    posts: Post[];
 
    handlePostUpdate: (updatePost: UpdatePost) => void;
    handlePostDelete: (id: string) => void;
    submitting: boolean;
}

const PostDashboard: FC<Props> = ({posts, handlePostUpdate, handlePostDelete, submitting}: Props) =>  {
    const {postStore} = useStore();
    const {editMode} = postStore;
    return (
        <Grid centered={true}>
        <Grid.Column width={10}>
            <PostList posts={posts}
            handlePostDelete={handlePostDelete}
                      
            />
            </Grid.Column>
            {editMode && (
                <PostEditForm
                    handlePostUpdate={handlePostUpdate}
                    submitting={submitting}
                />
            )}
            
        </Grid>
         
    );
}

export default observer(PostDashboard);
