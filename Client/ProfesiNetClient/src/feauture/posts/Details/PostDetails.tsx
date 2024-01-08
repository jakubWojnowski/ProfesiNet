import {FC, useEffect, useState} from "react";
import {
    Button,
    Dropdown,
    Grid,
    GridColumn,
    Icon,
    Image,
    Item,
    ItemImage,
    ItemMeta,
    Label,
    Segment
} from "semantic-ui-react";
import {useStore} from "../../../app/stores/Store.ts";
import {Link, useParams,} from "react-router-dom";
import {observer} from "mobx-react-lite";
import PostEditForm from "../form/PostEditForm.tsx";
import LoadingComponent from "../../../app/layout/components/LoadingComponent.tsx";
import PostCommentChat from "../dashboard/comments/PostCommentChat.tsx";


const PostDetails: FC = () => {
    const {postStore, userStore} = useStore();
    const {selectedPost: post, deletePost, openForm, loadPost, loadingInitial, editMode} = postStore;
    const [showComments, setShowComments] = useState(false);
    const {id} = useParams();
    const handleToggleComments = () => {
        setShowComments(!showComments);
    };

    useEffect(() => {
        const fetchData = async () => {
            if (id) {
                try {
                    await loadPost(id);
                } catch (error) {
                    console.error('Error loading post:', error);
                    // Handle error state here if needed
                }
            }
        };

        fetchData().then(r => console.log(r));
    }, [id, loadPost]);

    if (loadingInitial || !post) return <LoadingComponent content='Loading post...'/>;

    return (
        <Grid centered={true}>
            <GridColumn width={15}>
                <Segment.Group>

                    <Segment key={post.id} className="post-segment"> {/* Move key to here */}
                        {editMode && (
                            <PostEditForm
                            />
                        )}
                        <Item.Group>
                            <Item.Content>
                                <Item.Header className="item-header">

                                    <ItemImage src={post.creatorProfilePicture} size="mini" circular
                                               className="post-creator-image" spaced="right"/>
                                    <ItemMeta>{post.creatorName} {" "} {post.creatorSurname}</ItemMeta>
                                    <ItemMeta className="dropdown">
                                        {userStore.user?.id === post.creatorId && (

                                            <Dropdown icon='ellipsis horizontal' className="options-button"
                                                      closeOnEscape>
                                                <Dropdown.Menu>
                                                    <Dropdown.Item
                                                        text='Delete'
                                                        icon='delete'
                                                        as={Link} to={`/posts`}
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

                                <Item.Description as={Link} to={`/posts/${post.id}`}
                                                  className="post-content">{post.description}</Item.Description>
                                {post.imageUrl && <Image src={post.imageUrl} alt='Post media' className="post-image"/>}
                                <ItemMeta className="post-date">{new Date(post.publishedAt).toLocaleString()}</ItemMeta>
                            </Item.Content>

                        </Item.Group>

                    </Segment>
                    <Segment className="post-actions">
                        
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
                            <Button color='blue' onClick={handleToggleComments}>
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
                    {showComments && <PostCommentChat postId={post.id}/>}
                </Segment.Group>

            </GridColumn>

        </Grid>
    );

}

export default observer(PostDetails);