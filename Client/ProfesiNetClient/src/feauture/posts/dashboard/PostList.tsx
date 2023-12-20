import {FC} from "react";
import {Button, Icon, Item, Label, Segment, Dropdown, ItemImage, ItemHeader} from "semantic-ui-react";
import {useStore} from "../../../app/stores/Store.ts";


const PostList: FC = () => {

    const {postStore} = useStore();
    const {PostsBy} = postStore;
    const {openForm, deletePost} = postStore;

    return (
        <Item.Group divided>
            {PostsBy.map((post) => (
                <Segment key={post.id} className="post-segment"> {/* Move key to here */}
                    <Item.Content>
                    
                        <Item.Header className="item-header">
                            <ItemImage src={post.creatorProfilePicture} size="mini" circular className="post-creator-image" spaced="right" />
                            <div>{post.creatorName} {" "} {post.creatorSurname}</div>
                            <div className="dropdown">
                                <Dropdown icon='ellipsis horizontal' className="options-button" closeOnEscape>
                                    <Dropdown.Menu>
                                        <Dropdown.Item
                                            text='Delete'
                                            icon='delete'
                                            onClick={() => {
                                                deletePost(post.id).then()
                                            }} // Call deletePost function when clicked
                                        />
                                        <Dropdown.Item
                                            text='Update'
                                            icon='edit'
                                            onClick={() => {
                                                openForm(post.id)
                                            }} // Call updatePost function when clicked
                                        />
                                    </Dropdown.Menu>
                                </Dropdown>
                            </div>
                        </Item.Header>
                        <Item.Description as='a' className="post-content">{post.description}</Item.Description>
                        {post.imageUrl && <img src={post.imageUrl} alt='Post media' className="post-image"/>}
                        <div className="post-date">{new Date(post.publishedAt).toLocaleString()}</div>
                        <Item.Extra className="post-actions">

                            <Button as='div' labelPosition='right' className="action-button">
                                <Button color='red'>
                                    <Icon name='heart'/>
                                    Like
                                </Button>
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