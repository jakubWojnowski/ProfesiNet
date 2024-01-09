import {FC, useEffect,} from 'react';
import {Comment, Header, Segment, Button, Loader, Icon, Label} from 'semantic-ui-react';
import {useStore} from "../../../../app/stores/Store.ts";
import {Field, FieldProps, Form, Formik} from "formik";
import {Link} from "react-router-dom";
import {observer} from "mobx-react-lite";
import * as Yup from 'yup';

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
                style={{border: 'none'}}
            >
                <Header>Comments</Header>

            </Segment>
            <Segment attached clearing>
                <Formik
                    onSubmit={({body}, {resetForm}) => {
                        commentStore.addComment({body}, postId).then(() => resetForm());
                    }}
                    initialValues={{body: ''}}
                    validationSchema={
                        Yup.object({
                            body: Yup.string().required()
                        })
                    }
                    

                >
                    {({isSubmitting, isValid, handleSubmit}) => (
                        <Form className='ui form'>
                            <Field name='body'>
                                {(props: FieldProps) => (
                                    <div style={{position: 'relative'}}>
                                        <Loader active={isSubmitting}/>
                                        <textarea
                                            placeholder='Enter your comment (Enter to submit, SHIFT + Enter for new line)'
                                            rows={2}
                                            {...props.field}
                                            onKeyDown={e => {
                                                if (e.key === 'Enter' && e.shiftKey) {
                                                    return;
                                                }
                                                if (e.key === 'Enter' && !e.shiftKey) {
                                                    e.preventDefault();
                                                    isValid && handleSubmit();
                                                }
                                            }}
                                        />
                                    </div>
                                )}
                            </Field>
                        </Form>
                    )}
                </Formik>
                <Comment.Group>
                    {commentStore.comments.map(comment => (
                        <Comment key={comment.id}>
                            <Comment.Avatar src={comment.creatorProfilePicture || '/assets/user.png'}/>
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
                                <Comment.Text style={{whiteSpace: 'pre-wrap'}}>{comment.content}</Comment.Text>
                                <Comment.Actions>
                                    <Comment.Action>
                                        {/*<Button as='div' labelPosition='right' size={"mini"}>*/}
                                        {/*    <Button color='red' size={"mini"}>*/}
                                        {/*        <Icon name='heart' />*/}
                                        {/*        Like*/}
                                        {/*    </Button>*/}
                                        {/*    <Label size={"mini"} as='a' basic color='red' pointing='left' content={comment.likesCount? comment.likesCount : 0}/>*/}
                                        {/*</Button>*/}
                                    </Comment.Action>
                                </Comment.Actions>
                            </Comment.Content>
                        </Comment>
                    ))}


                </Comment.Group>
            </Segment>
        </>
    );
};

export default observer(PostCommentChat);
