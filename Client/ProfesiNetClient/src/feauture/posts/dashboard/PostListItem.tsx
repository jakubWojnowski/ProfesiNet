import {FC} from 'react';
import {Button,  Dropdown, Icon, Image, Item, ItemImage, ItemMeta, Label, Segment} from "semantic-ui-react";
import {Link} from "react-router-dom";
import {useStore} from "../../../app/stores/Store.ts";
import {Post} from "../../../app/modules/interfaces/Post.ts";

interface Props {
    post: Post;
}

const PostListItem: FC<Props> = ({post}: Props) => {
    const {postStore} = useStore();
    const {openForm, deletePost} = postStore;

    return (
        <Segment.Group>
            <Segment key={post.id} className="post-segment"> {/* Move key to here */}
                <Item.Group>
                    <Item.Content>
                        <Item.Header className="item-header">

                            <ItemImage src={post.creatorProfilePicture} size="mini" circular
                                       className="post-creator-image" spaced="right"/>
                            <ItemMeta>{post.creatorName} {" "} {post.creatorSurname}</ItemMeta>
                            <ItemMeta className="dropdown">
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
                            </ItemMeta>
                            
                        </Item.Header>

                        <Item.Description as={Link} to={`/posts/${post.id}`}
                                          className="post-content">{post.description}</Item.Description>
                        {post.imageUrl && <Image src={post.imageUrl} alt='Post media' className="post-image"/>}
                        <ItemMeta className="post-date">{new Date(post.publishedAt).toLocaleString()}</ItemMeta>
                    </Item.Content>

                </Item.Group>

            </Segment>
            <Segment className="post-actions" >
   
           

                <Button  as='div' labelPosition='right' className="action-button">
                    <Button color='red'>
                        <Icon name='heart'/>
                        Like
                    </Button>
                    <Label as='a' basic color='red' pointing='left'>
                        {post.likesCount}
                    </Label>
                </Button >
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

              
            </Segment>

        </Segment.Group>
    );
}

export default PostListItem;