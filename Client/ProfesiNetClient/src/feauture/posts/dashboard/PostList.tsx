import {FC} from "react";
import {Post} from "../../../app/modules/interfaces/Post.ts";
import {Button, Icon, Item, ItemImage, Label, Segment, Dropdown } from "semantic-ui-react";
interface Props {
    posts: Post[];
    selectPost: (id: string) => void;
}

const PostList: FC<Props> = ({posts, selectPost}: Props) => {
    return (

        <Item.Group divided >
            {posts.map((post) => (
                <Segment key={post.id} className="post-segment"> {/* Move key to here */}
                    
                    {/*<Dropdown icon='ellipsis horizontal' className="options-button right-aligned" floated={true} fluid={true}   >*/}
                    {/*    <Dropdown.Menu>*/}
                    {/*        <Dropdown.Item*/}
                    {/*            text='Delete'*/}
                    {/*            icon='delete'*/}
                    {/*            onClick={() =>{}} // Call deletePost function when clicked*/}
                    {/*        />*/}
                    {/*        <Dropdown.Item*/}
                    {/*            text='Update'*/}
                    {/*            icon='edit'*/}
                    {/*            onClick={() => {selectPost(post.id)}} // Call updatePost function when clicked*/}
                    {/*        />*/}
                    {/*    </Dropdown.Menu>*/}
                    {/*</Dropdown>*/}
                    <Button icon className="options-button" onClick={() => selectPost(post.id)}>
                        <Icon name='ellipsis horizontal' />
                    </Button>
                    
                    <Item.Content>
                      
                        <Item.Header >{post.creatorName}{" "}{post.creatorSurname}
                          
                        </Item.Header>
                        <Item.Description as='a' className="post-content">{post.description}</Item.Description>
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