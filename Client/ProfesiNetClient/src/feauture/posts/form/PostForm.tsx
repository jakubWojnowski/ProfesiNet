import React, { FC, useState } from 'react';
import { Button, Modal, Form, TextArea, Icon, Grid, Segment, Image } from 'semantic-ui-react';
import { observer } from "mobx-react-lite";
import { useStore } from "../../../app/stores/Store";


const PostForm: FC = () => {
    const [open, setOpen] = useState(false);
    const [postContent, setPostContent] = useState('');
    const [file, setFile] = useState<File | null>(null);
    const [previewUrl, setPreviewUrl] = useState<string | null>(null); // State for thumbnail preview URL
    const { postStore } = useStore();
    const { createPost, loading } = postStore;

    const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const file = event.target.files ? event.target.files[0] : null;
        setFile(file);

        if (file) {
            const reader = new FileReader();
            reader.onloadend = () => {
                setPreviewUrl(reader.result as string);
            };
            reader.readAsDataURL(file);
        } else {
            setPreviewUrl(null);
        }
    };

    const handleCancelImage = () => {
        setFile(null);
        setPreviewUrl(null);
        // Reset the file input
        const fileInput = document.getElementById('fileInput') as HTMLInputElement;
        fileInput.value = '';
    };

    const handleSubmit = async () => {
        const postData = {
            description: postContent,
            file: file,
        };

        try {
            await createPost(postData);
            setPostContent('');
            setFile(null);
            setPreviewUrl(null); // Reset the preview URL
            setOpen(false);
        } catch (error) {
            console.error('Error creating the post:', error);
            alert('Failed to create the post.');
        }
    };

    return (
        <Grid centered={true}>
            <Button size={"large"} style={{width: "80%"}} onClick={() => setOpen(true)} icon labelPosition='left' primary>
                <Icon name='edit outline' />
                Start a post
            </Button>

            <Modal
                open={open}
                onClose={() => {
                    setOpen(false);
                    setPreviewUrl(null); // Clear preview when closing the modal
                }}
                size='small'
            >
                <Modal.Header>Create a Post</Modal.Header>
                <Modal.Content>
                    <Form>
                        <TextArea
                            rows={3}
                            placeholder="What's on your mind?"
                            value={postContent}
                            onChange={(e) => setPostContent(e.target.value)}
                            style={{ minHeight: 200 }}
                        />
                        <input
                            type="file"
                            onChange={handleFileChange}
                            hidden
                            id="fileInput"
                        />
                        {previewUrl && (
                            <Segment>
                                <Image src={previewUrl} size='big' centered />
                                <Button icon onClick={handleCancelImage}>
                                    <Icon name='cancel' />
                                </Button>
                            </Segment>
                        )}
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
                    <Button  color='green' onClick={handleSubmit} loading={loading}>
                        Publish
                    </Button>
                </Modal.Actions>
            </Modal>
        </Grid>
    );
};

export default observer(PostForm);
