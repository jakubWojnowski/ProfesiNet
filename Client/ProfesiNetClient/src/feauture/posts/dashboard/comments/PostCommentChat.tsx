import  {FC, useEffect, } from 'react';
import {Comment, Header, Segment, Button} from 'semantic-ui-react';
import {useStore} from "../../../../app/stores/Store.ts";
import { Form, Formik} from "formik";
import {Link} from "react-router-dom";
import {observer} from "mobx-react-lite";
import MyTextArea from "../../../../app/common/form/MyTextArea.tsx";

interface Props {
    postId: string;
}
const PostCommentChat: FC<Props> = ({postId}: Props) => {
const {commentStore} = useStore();

useEffect(() => {
    if (postId) {
        commentStore.setHubConnection(postId);
    }
    return () => {
        commentStore.clearComments();
    }
}, [commentStore, postId]);
    
    return (
        <>
            <Segment
                textAlign='center'
                attached='top'
                inverted
                color='teal'
                style={{ border: 'none' }}
            >
                <Header>{postId}</Header>
                
            </Segment>
            <Segment attached clearing>
                <Formik
                    onSubmit={({ body }, { resetForm }) => {
                        commentStore.addComment({ body }, postId).then(() => resetForm());
                    }}
                    initialValues={{ body: '' }}
                    
                >
                    {({ isSubmitting, isValid }) => (
                        <Form className='ui form'>
                          <MyTextArea placeholder='Add comment' name='body' rows={2} />
                            <Button
                                loading={isSubmitting}
                                disabled={isSubmitting }
                                positive
                                type='submit'
                                content='Submit'
                                primary
                            />
                        </Form>
                    )}
                </Formik>
                <Comment.Group>
                    {commentStore.comments.map(comment => (
                        <Comment key={comment.id}>
                            <Comment.Avatar src={ comment.creatorProfilePicture ||'/assets/user.png'} />
                            <Comment.Content>
                                <Comment.Author as={Link} to={`/profile/${comment.creatorId}`}>
                                    {comment.creatorName} {comment.creatorSurname}
                                </Comment.Author>
                                <Comment.Metadata>
                                    <div>{new Date(comment.publishedAt.split('T')[0]).toLocaleDateString('en-US', {
                                        year: 'numeric',
                                        month: 'long',
                                        day: 'numeric'
                                    })}</div>
                                    <div>{comment.publishedAt.split('T')[1].slice(0, 5)}</div>
                                </Comment.Metadata>
                                <Comment.Text style={{ whiteSpace: 'pre-wrap' }}>{comment.content}</Comment.Text>
                            </Comment.Content>
                        </Comment>
                    ))}


                </Comment.Group>
            </Segment>
        </>
    );
};

export default observer(PostCommentChat);
