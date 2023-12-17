import {ChangeEvent, FC, useState} from "react";
import {Button,  Form, Icon, Modal, Segment, TextArea} from "semantic-ui-react";
import {Post} from "../../../app/modules/interfaces/Post.ts";
import {useStore} from "../../../app/stores/Store.ts";
import LoadingComponent from "../../../app/layout/components/LoadingComponent.tsx";
import {observer} from "mobx-react-lite";
interface Props{
   
    // handlePostUpdate: (UpdatePost: Post | {
    //     description: string;
    //     id: string;
    //     File: { prototype: File; new(fileBits: BlobPart[], fileName: string, options?: FilePropertyBag): File }
    // }) => void;
    submitting: boolean;
}
const PostEditForm: FC<Props> = ({submitting}:Props) => {
    const {postStore} = useStore();
    const {selectedPost, cancelSelectedPost, closeForm} = postStore;

    if(!selectedPost) return <LoadingComponent/>;
    
    const initialFormState = selectedPost ?? {
        id: '',
        description: '',
        File: File,
    };
    const [post, setPost] = useState(initialFormState);
    
    const handleSubmit = () => {
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
                
                <Button color='red' onClick={() => cancelSelectedPost()}>
                    Cancel
                </Button>
            </Modal.Actions>
        </Modal>
    );
}

export default observer( PostEditForm);