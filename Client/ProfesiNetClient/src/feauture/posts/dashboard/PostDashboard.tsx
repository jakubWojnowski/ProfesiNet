import  {FC} from "react";
import {Grid} from "semantic-ui-react";
import {Post} from "../../../app/modules/interfaces/Post.ts";
import './PostDashboard.css';
import PostList from "./PostList.tsx";
import PostEditForm from "../form/PostEditForm.tsx";
import {CreatePost} from "../../../app/modules/interfaces/CreatePost.ts";

interface Props {
    posts: Post[];
    selectedPost: Post | undefined;
    selectPost: (id: string) => void;
    cancelSelectPost: () => void;
    editMode: boolean;
    openForm: (id: string) => void;
    closeForm: () => void;
    handlePostUpdate: (post: CreatePost) => void;
}

const PostDashboard: FC<Props> = ({posts, selectPost, selectedPost, cancelSelectPost, openForm, closeForm, editMode, handlePostUpdate}: Props) =>  {
    return (
        <Grid centered={true}>
        <Grid.Column width={10}>
            <PostList posts={posts}
            selectPost={selectPost}
            />
            </Grid.Column>
            {selectedPost && (
                <PostEditForm closeForm={closeForm} post={selectedPost} cancelSelectPost={cancelSelectPost} handlePostUpdate={handlePostUpdate}/>
            )}
            
        </Grid>
         
    );
}

export default PostDashboard;
