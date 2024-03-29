import {FC, useState} from 'react';
import {Button,  Dropdown, Icon, Image, Item, ItemImage, ItemMeta, Label, Segment} from "semantic-ui-react";
import {NavLink} from "react-router-dom";
import {useStore} from "../../../app/stores/Store.ts";
import {Post} from "../../../app/modules/interfaces/Post.ts";
import PostCommentChat from "./comments/PostCommentChat.tsx";
import {observer} from "mobx-react-lite";


interface Props {
    post: Post;
}

const PostListItem: FC<Props> = ({post}: Props) => {
    const {postStore, userStore} = useStore();
    const {openForm, deletePost} = postStore;
    const [showComments, setShowComments] = useState(false);

    const handleToggleComments = () => {
        setShowComments(!showComments);
    };
    
        
    return (
        <Segment.Group>
            <Segment key={post.id} className="post-segment"> {/* Move key to here */}
                <Item.Group>
                    <Item.Content>
                        <Item.Header className="item-header">

                            <ItemImage as={NavLink} to={`/profile/${post.creatorId}`} src={post.creatorProfilePicture || '/assets/user.png'} size="mini" circular
                                       className="post-creator-image" spaced="right"/>
                            <ItemMeta>{post.creatorName} {" "} {post.creatorSurname}
                           
                            </ItemMeta>
                       
                            <ItemMeta className="dropdown">
                                {userStore.user?.id === post.creatorId && (
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
                                )}
                            </ItemMeta>
                         
                            
                        </Item.Header>

                        <Item.Description className="post-content">{post.description}</Item.Description>
                        {post.imageUrl && <Image src={post.imageUrl} alt='Post media' className="post-image"/>}
                        <ItemMeta className="post-date">{new Date(post.publishedAt).toLocaleString()}</ItemMeta>
                    </Item.Content>

                </Item.Group>

            </Segment>
            <Segment className="post-actions" >
   
           

                <Button  as='div' labelPosition='right' className="action-button">
                    <Button color='red' onClick={()=>postStore.likePost(post.id)}>
                        <Icon name='heart'/>
                        Like
                    </Button>
                    <Label as='a' basic color='red' pointing='left'>
                        {post.likesCount}
                    </Label>
                </Button >
                    <Button as='div' labelPosition='right' className="action-button">
                        <Button color='blue' onClick={handleToggleComments}>
                            <Icon name='comment'/>
                            Comment
                        </Button>
                </Button>
                <Button as='div' labelPosition='right' className="action-button">
                    <Button color='grey' onClick={()=>postStore.sharePost(post.id)}>
                        <Icon name='share alternate'/>
                        Share
                    </Button>
                    <Label as='a' basic color='grey' pointing='left'  >
                        {post.sharesCount}
                    </Label>
                </Button>

              
            </Segment>
            {showComments && <PostCommentChat postId={post.id} />}

        </Segment.Group>
    );
}

export default observer(PostListItem);