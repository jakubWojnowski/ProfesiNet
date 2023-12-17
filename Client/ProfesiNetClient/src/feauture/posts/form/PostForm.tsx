import { FC, useState } from 'react';
import { Button, Modal, Form, TextArea, Icon, Grid, Segment } from 'semantic-ui-react';
import { CreatePost } from "../../../app/modules/interfaces/CreatePost";
import { observer } from "mobx-react-lite";
import { useStore } from "../../../app/stores/Store";

const PostForm: FC = () => {
    const [open, setOpen] = useState(false);
    const [postContent, setPostContent] = useState('');
    const [file, setFile] = useState<File | null>(null); // State to handle file
    const { postStore } = useStore();
    const { createPost, loading } = postStore;

 

    const handleSubmit = async () => {
        // Only proceed if there is content and a file selected
        const postData: CreatePost = {
            description: postContent,
            file: file, // This will be undefined if no file is selected, which is fine since it's optional
        };

        // Call createPost and pass the postData object
        try {
       
            await createPost(postData);
            setPostContent(''); // Reset the content state
            setFile(null); // Reset the file state
            setOpen(false); // Close the modal after successful submission
        } catch (error) {
            console.error('Error creating the post:', error);
            alert('Failed to create the post.');
        } finally {
    
        }
    };

    return (
        <Grid centered={true}>
            <Button size={"large"} onClick={() => setOpen(true)} icon labelPosition='left'>
                <Icon name='edit' />
                Start a post
            </Button>

            <Modal
                open={open}
                onClose={() => setOpen(false)}
                size='small'
            >
                <Modal.Header>Create a Post</Modal.Header>
                <Modal.Content>
                    <Form>
                        <TextArea
                            rows={3}
                            placeholder="What's on your mind?"
                            type='text'
                            value={postContent}
                            onChange={(e) => setPostContent(e.target.value)}
                            style={{ minHeight: 100 }}
                        />
                        <input
                            type="file"
                            onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
                                setFile(e.target.files ? e.target.files[0] : null)
                            }
                            hidden
                            id="fileInput"
                        />
                    </Form>
                    <Segment secondary>
                        <Button icon labelPosition='left' as="label" htmlFor="fileInput">
                            <Icon name='file image outline' />
                            Image
                        </Button>
                        <Button icon>
                            <Icon name='smile outline' />
                        </Button>
                    </Segment>
                </Modal.Content>
                <Modal.Actions>
                    <Button color='green' onClick={handleSubmit} loading={loading}>
                        Publish
                    </Button>
                </Modal.Actions>
            </Modal>
        </Grid>
    );
};

export default observer(PostForm);
