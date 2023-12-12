import  {FC} from "react";
import {Grid} from "semantic-ui-react";
import {Post} from "../../../app/modules/interfaces/Post.ts";
import './PostDashboard.css';
import PostList from "./PostList.tsx";
import PostDetails from "../Details/PostDetails.tsx";
import PostEditForm from "../form/PostEditForm.tsx";

interface Props {
    posts: Post[];
    selectedPost: Post | undefined;
    selectPost: (id: string) => void;
    cancelSelectPost: () => void;
    editMode: boolean;
    openForm: (id: string) => void;
    closeForm: () => void;
}

const PostDashboard: FC<Props> = ({posts, selectPost, selectedPost, cancelSelectPost, openForm, closeForm, editMode}: Props) =>  {
    return (
        <Grid >
        <Grid.Column width={11}>
            <PostList posts={posts} 
            selectPost={selectPost}
            />
            </Grid.Column>
            {selectedPost && (
            <Grid.Column width={4} >
                <PostDetails post={selectedPost} cancelSelectPost={cancelSelectPost}
                openForm={openForm}
                />
                {editMode && (
                   <PostEditForm closeForm={closeForm} post={selectedPost} />
                   
                )}
                
            </Grid.Column>

            )}
        </Grid>
         
    );
}

export default PostDashboard;
