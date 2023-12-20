import { ChangeEvent, FC, useEffect, useState } from 'react';
import { Button, Form, Icon, Image, Modal, Segment, TextArea } from 'semantic-ui-react';
import { useStore } from '../../../app/stores/Store.ts';
import LoadingComponent from '../../../app/layout/components/LoadingComponent.tsx';
import { UpdatePost } from '../../../app/modules/interfaces/UpdatePost.ts';
import { observer } from 'mobx-react-lite';

const PostEditForm: FC = () => {
    const { postStore } = useStore();
    const {
        selectedPost,
        updatePost,
        cancelSelectedPost,
        closeForm,
        loading
    } = postStore;
    const [file, setFile] = useState<File | null>(null);
    const [previewUrl, setPreviewUrl] = useState<string | null>(null);

    // If there's no selectedPost, show the loading component.
    if (!selectedPost) return <LoadingComponent />;

    // Set up the initial state for the form based on the selectedPost.
    const initialFormState = selectedPost ?? {
        id: '',
        description: '',
        file: null, // Assuming this is how you store the file in your selectedPost
    };
    const [post, setPost] = useState(initialFormState);

    // This effect sets the preview URL when a post is selected for editing.
    useEffect(() => {
        if (selectedPost?.imageUrl) {
            // Check if selectedPost.file is a URL or a File object
            setPreviewUrl(selectedPost.imageUrl);
        }
    }, [selectedPost]);

    const handleSubmit = async () => {
        const postToUpdate: UpdatePost = {
            id: post.id,
            description: post.description,
            file: file
        };

        await updatePost(postToUpdate);
        setTimeout(() => {
            closeForm();
            cancelSelectedPost();
        }, 1000);
    };

    const handleFileChange = (event: ChangeEvent<HTMLInputElement>) => {
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
        const fileInput = document.getElementById('fileInput') as HTMLInputElement;
        fileInput.value = '';
    };

    const handleInputChange = (event: ChangeEvent<HTMLTextAreaElement>) => {
        const { name, value } = event.target;
        setPost({ ...post, [name]: value });
    };

    return (
        <Modal
            onClose={() => closeForm()}
            open={true}
            size='small'
        >
            <Modal.Header>Update a Post</Modal.Header>
            <Modal.Content>
                <Form onSubmit={handleSubmit} autoComplete='off'>
                    <TextArea
                        rows={3}
                        placeholder="What's on your mind?"
                        value={post.description}
                        name='description'
                        onChange={handleInputChange}
                        style={{ minHeight: 100 }}
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
                    <Button.Group>
                        <Button icon labelPosition='left' as="label" htmlFor="fileInput">
                            <Icon name='file image outline' />
                            Image
                        </Button>
                    </Button.Group>
                </Segment>
            </Modal.Content>
            <Modal.Actions>
                <Button color='green' onClick={handleSubmit} loading={loading}>
                    Publish
                </Button>
                <Button color='red' onClick={() => closeForm()}>
                    Cancel
                </Button>
            </Modal.Actions>
        </Modal>
    );
};

export default observer(PostEditForm);
