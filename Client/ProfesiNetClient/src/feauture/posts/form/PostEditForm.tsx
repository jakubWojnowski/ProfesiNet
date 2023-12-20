import {ChangeEvent, FC, useState} from "react";
import {Button,  Form, Icon, Modal, Segment, TextArea} from "semantic-ui-react";
import {useStore} from "../../../app/stores/Store.ts";
import LoadingComponent from "../../../app/layout/components/LoadingComponent.tsx";
import {UpdatePost} from "../../../app/modules/interfaces/UpdatePost.ts";
import {observer} from "mobx-react-lite";

const PostEditForm: FC = () => {
    const {postStore} = useStore();
    const {selectedPost, updatePost, cancelSelectedPost, closeForm, loading} = postStore;
    const [file, setFile] = useState<File | null>(null); // State to handle file

    if(!selectedPost) return <LoadingComponent/>;
    
    const initialFormState = selectedPost ?? {
        id: '',
        description: '',
        File: File,
    };
    const [post, setPost] = useState(initialFormState);
    
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
        }
        , 1000);
    };

    const handleInputChange = (event: ChangeEvent<HTMLTextAreaElement>) => {
        const { name, value } = event.target;
        setPost({ ...post, [name]: value });
        console.log(name, value);
    };
    return (
        <Modal
            onClose={() => closeForm()}
            open={true}
            size='small'
        >
            <Modal.Header>Create a Post</Modal.Header>
            <Modal.Content>
                <Form onSubmit={handleSubmit} autoComplete='off'>
                    <TextArea 
                        rows={3}
                        placeholder="What's on your mind?"
                        value={post.description}
                        name='description'
                        onChange={ handleInputChange}
                        style={{ minHeight: 100 }} // Adjust the height of the TextArea
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
}

export default observer(PostEditForm);