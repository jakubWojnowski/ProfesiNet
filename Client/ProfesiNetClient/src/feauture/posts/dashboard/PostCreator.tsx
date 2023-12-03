// PostCreator.js
import React, { useState } from 'react';
import {Button, Modal, Form, TextArea, Icon, Container, Header, HeaderContent, Grid} from 'semantic-ui-react';
const PostCreator = ({ onPostSubmit }) => {
    const [open, setOpen] = useState(false);
    const [postContent, setPostContent] = useState('');

    const handleSubmit = () => {
        onPostSubmit(postContent); // Pass the post content up to the parent component
        setPostContent(''); // Clear the input field
        setOpen(false); // Close the modal
    };

    return (
        <Grid centered={true} fluid={true}> 
            
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
                            placeholder='Co chcesz przekazać?'
                            value={postContent}
                            onChange={(e) => setPostContent(e.target.value)}
                        />
                    </Form>
                </Modal.Content>
                <Modal.Actions>
                    <Button color='green' onClick={handleSubmit}>
                        Publish
                    </Button>
                </Modal.Actions>
            </Modal>
        </Grid>
    );
};

export default PostCreator;
