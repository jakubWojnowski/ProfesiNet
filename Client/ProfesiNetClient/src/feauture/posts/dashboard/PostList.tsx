import {FC} from "react";
import {Item} from "semantic-ui-react";
import {useStore} from "../../../app/stores/Store.ts";
import PostListItem from "./PostListItem.tsx";


const PostList: FC = () => {

    const {postStore} = useStore();
    const {PostsBy} = postStore;

    return (
        <Item.Group divided>
            {PostsBy.map((post) => (
                <PostListItem post={post} key={post.id} />
               
            ))}
        </Item.Group>

    );
}

export default PostList;