import {FC} from "react";
import {Post} from "../../../app/modules/interfaces/Post.ts";
import {Button, Icon, Item, ItemImage, Label, Segment} from "semantic-ui-react";
interface Props {
    posts: Post[];
}

const PostList: FC<Props> = ({posts}: Props) => {
    return (

        <Item.Group divided>
            {posts.map((post) => (
                <Segment key={post.id} className="post-segment"> {/* Move key to here */}
                    <Item.Content>
                        <Item.Header >{post.creatorName}{" "}{post.creatorSurname}</Item.Header>
                        <Item.Description as='a' className="post-content">{post.description}</Item.Description>
                        {/*<ItemImage src="/drwal.jpg" className="post-image"  />*/}
                        {post.media && <img src="/drwal.jpg" alt='Post media' className="post-image"/>}
                        <div className="post-date">{new Date(post.publishedAt).toLocaleString()}</div>
                        <Item.Extra className="post-actions">

                            <Button as='div' labelPosition='right' className="action-button">
                                <Button color='red'>
                                    <Icon name='heart'/>
                                    Like
                                </Button >
                                <Label as='a' basic color='red' pointing='left'>
                                    {post.likesCount}
                                </Label>
                            </Button>
                            <Button as='div' labelPosition='right' className="action-button">
                                <Button color='blue'>
                                    <Icon name='comment'/>
                                    Comment
                                </Button>
                                <Label as='a' basic color='blue' pointing='left'>
                                    {post.commentsCount}
                                </Label>
                            </Button>
                            <Button as='div' labelPosition='right' className="action-button">
                                <Button color='grey'>
                                    <Icon name='share alternate'/>
                                    Share
                                </Button>
                                <Label as='a' basic color='grey' pointing='left'>
                                    {post.sharesCount}
                                </Label>
                            </Button>
                        </Item.Extra>
                    </Item.Content>
                </Segment>
            ))}
        </Item.Group>

    );
}

export default PostList;