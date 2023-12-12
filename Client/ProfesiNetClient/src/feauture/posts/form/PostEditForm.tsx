import React, {ChangeEvent, FC, useState} from "react";
import {Button, Form, Icon, Modal, Segment, TextArea} from "semantic-ui-react";
import {Post} from "../../../app/modules/interfaces/Post.ts";
interface Props{
    closeForm: () => void;
    post: Post | undefined;
    
}

const PostEditForm: FC<Props> = ({post:selectedPost,closeForm}:Props) => {
    const [postContent, setPostContent] = useState('');
    const initialFormState = selectedPost ?? {
        id: '',
        description: '',
        media: ''
    };
    const [post, setPost] = useState(initialFormState);
    
    const handleSubmit = () => {
        console.log(postContent);
        setPostContent('');
        
        closeForm();
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
                <Form onSubmit={handleSubmit} autocomplete='off'>
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
                <Button color='green' >
                    Publish
                </Button>
            </Modal.Actions>
        </Modal>
    );
}

export default PostEditForm;