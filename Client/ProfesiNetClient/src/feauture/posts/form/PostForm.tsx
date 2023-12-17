import { FC, useState } from 'react';
import { Button, Modal, Form, TextArea, Icon, Grid, Segment } from 'semantic-ui-react';
import {CreatePost} from "../../../app/modules/interfaces/CreatePost.ts";

interface PostFormProps {
    onPostSubmit: (CreatePost:CreatePost) => void; // Add other props as needed
    submitting: boolean;
}

const PostForm: FC<PostFormProps> = ({ onPostSubmit, submitting }) => {
    const [open, setOpen] = useState(false);
    const [postContent, setPostContent] = useState('');

    const handleSubmit = () => {
        onPostSubmit({description: postContent,file: null });
        setPostContent('');
        setTimeout(() => {
            setOpen(false);
        }
    , 1000);
        
    };

    return (
        <Grid  centered={true}>

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
                            style={{ minHeight: 100 }} // Adjust the height of the TextArea
                        />
                    </Form>
                    <Segment secondary>
                        <Button.Group>
                            <Button icon labelPosition='left'>
                                <Icon name='file image outline' />
                                Image
                            </Button>
                            <Button icon>
                                <Icon name='smile outline' />
                            </Button>
                        </Button.Group>
                    </Segment>
                </Modal.Content>
                <Modal.Actions>
                    <Button color='green' onClick={handleSubmit} loading={submitting}>
                        Publish
                    </Button>
                </Modal.Actions>
            </Modal>
        </Grid>
    );
};

export default PostForm;
