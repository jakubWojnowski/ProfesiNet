import {FC, useEffect} from "react";
import {Button, Dropdown, Grid, GridColumn, Icon, Item, ItemImage, Label, Segment} from "semantic-ui-react";
import {useStore} from "../../../app/stores/Store.ts";
import {Link, useParams,} from "react-router-dom";
import {observer} from "mobx-react-lite";
import PostEditForm from "../form/PostEditForm.tsx";
import LoadingComponent from "../../../app/layout/components/LoadingComponent.tsx";



const PostDetails: FC = () => {
    const {postStore} = useStore();
    const {selectedPost: post, deletePost, openForm, loadPost, loadingInitial, editMode} = postStore;
    const {id} = useParams();

    useEffect(() => {
        const fetchData = async () => {
            if (id) {
                await loadPost(id);
            }
        };

        fetchData().then(r => r);
    }, [id, loadPost]);
    if (loadingInitial || !post) return <LoadingComponent content='Loading post...'/>;
    
    return (
        <Grid centered={true}  >
            <GridColumn width={15} >
                <Segment  key={post.id} className="post-segment"> {/* Move key to here */}
                    {editMode && (
                        <PostEditForm
                        />
                    )}
                    <Item.Content>
                      

                        <Item.Header className="item-header">
                            <ItemImage src={post.creatorProfilePicture} size="mini" circular className="post-creator-image" spaced="right" />
                            <div>{post.creatorName} {" "} {post.creatorSurname}</div>
                            <div className="dropdown">
                                <Dropdown icon='ellipsis horizontal' className="options-button" closeOnEscape>
                                    <Dropdown.Menu>
                                        <Dropdown.Item as={Link} to='/posts'
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
                        <Item.Description className="post-content">{post.description}</Item.Description>
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
            </GridColumn>
            </Grid>
        
    );
            
}

export default observer(PostDetails);