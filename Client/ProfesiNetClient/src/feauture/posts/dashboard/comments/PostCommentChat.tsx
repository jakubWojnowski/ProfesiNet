import  {FC, useEffect, } from 'react';
import {Comment, Form, Header, Segment, Loader} from 'semantic-ui-react';
import {useStore} from "../../../../app/stores/Store.ts";
import {Field, FieldProps, Formik} from "formik";
import {Link} from "react-router-dom";
import {observer} from "mobx-react-lite";

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
                {/*<Formik*/}
                {/*    onSubmit={(values, { resetForm }) =>*/}
                {/*        commentStore.addComment(values).then(() => resetForm())*/}
                {/*    }*/}
                {/*        */}
                {/*    initialValues={{ body: '' }}*/}
                {/*    */}
                {/*>*/}
                {/*    {({ isSubmitting, isValid, handleSubmit }) => (*/}
                {/*        <Form className='ui form'>*/}
                {/*            <Field name='body'>*/}
                {/*                {(props: FieldProps) => (*/}
                {/*                    <div style={{ position: 'relative' }}>*/}
                {/*                        <Loader active={isSubmitting} />*/}
                {/*                        <textarea*/}
                {/*                            placeholder='Enter your comment (Enter to submit, SHIFT + Enter for new line)'*/}
                {/*                            rows={2}*/}
                {/*                            {...props.field}*/}
                {/*                            onKeyPress={e => {*/}
                {/*                                if (e.key === 'Enter' && e.shiftKey) {*/}
                {/*                                    return;*/}
                {/*                                }*/}
                {/*                                if (e.key === 'Enter' && !e.shiftKey) {*/}
                {/*                                    e.preventDefault();*/}
                {/*                                    isValid && handleSubmit();*/}
                {/*                                }*/}
                {/*                            }}*/}
                {/*                        />*/}
                {/*                    </div>*/}
                {/*                )}*/}
                {/*            </Field>*/}
                {/*        </Form>*/}
                {/*    )}*/}
                {/*</Formik>*/}
                <Comment.Group>
                    {commentStore.comments.map(comment => (
                        <Comment key={comment.id}>
                            <Comment.Avatar src={ '/assets/user.png'} />
                            <Comment.Content>
                                <Comment.Author as={Link} to={`/profiles/${comment.creatorId}`}>
                                    {comment.creatorName}
                                </Comment.Author>
                                <Comment.Metadata>
                                    <div>{comment.publishedAt}</div>
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
