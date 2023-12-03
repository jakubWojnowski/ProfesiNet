import  {FC} from "react";
import {Grid} from "semantic-ui-react";
import {Post} from "../../../app/modules/interfaces/Post.ts";
import './PostDashboard.css';

import PostList from "./PostList.tsx";

interface Props {
    posts: Post[];
}

const PostDashboard: FC<Props> = ({posts}: Props) =>  {
    return (
        <Grid centered={true}>
        <Grid.Column width={11}>
            <PostList posts={posts}/>
            </Grid.Column>    
        </Grid>
         
    );
}

export default PostDashboard;
