import { ChangeEvent, FC, useEffect, useState } from 'react';
import { Button, Icon, Image, Modal, Segment } from 'semantic-ui-react';
import { observer } from 'mobx-react-lite';
import { Formik, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import MyTextArea from '../../../app/common/form/MyTextArea';
import { useStore } from '../../../app/stores/Store';
import { Link, useNavigate } from 'react-router-dom';
import LoadingComponent from '../../../app/layout/components/LoadingComponent';
import { UpdatePost } from "../../../app/modules/interfaces/UpdatePost.ts";

interface FormValues {
    description: string;
    file: File | null;
}

const PostEditForm: FC = () => {
    const navigate = useNavigate();
    const { postStore } = useStore();
    const { selectedPost: post, updatePost, loading, closeForm } = postStore;

    const [file, setFile] = useState<File | null>(null);
    const [previewUrl, setPreviewUrl] = useState<string | null | undefined>(post?.imageUrl);

    useEffect(() => {
        setPreviewUrl(post?.imageUrl ?? null);
    }, [post?.imageUrl]);

    if (!post) return <LoadingComponent content='Loading post...' />;

    const validationSchema = Yup.object({
        description: Yup.string().required('Description is required'),
        file: Yup.mixed().nullable(),
    }).test('fileOrDescription', 'Either a description or file is required', (value) => {
        return !!(value.description || (value.file instanceof File && value.file.size > 0));
    });

    const handleSubmit = async (values: FormValues, { setSubmitting }: { setSubmitting: (isSubmitting: boolean) => void }) => {

        try {
            const postToUpdate: UpdatePost = {
                id: post.id,
                description: values.description,
                file: file // Use the state file instead of values.file
            };

            await updatePost(postToUpdate);
            setSubmitting(false);
            navigate('/posts');
        } catch (error) {
            console.error('Failed to update post:', error);
            setSubmitting(false);
        }
    };

    return (
        <Modal open={true} onClose={() => navigate('/posts')} size='small'>
            <Modal.Header>Update a Post</Modal.Header>
            <Modal.Content>
                <Formik
                    initialValues={{
                        description: post.description || '',
                        file: null,
                    }}
                    validationSchema={validationSchema}
                    onSubmit={(values, formikHelpers) => handleSubmit(values, formikHelpers)}
                    enableReinitialize
                >
                    {({ setFieldValue, handleSubmit, isSubmitting, isValid, dirty }) => (
                        <form onSubmit={handleSubmit} className='ui form'> {/* Standard HTML form tag */}
                            <MyTextArea
                                name='description'
                                placeholder="What's on your mind?"
                                rows={3}
                            />
                            <input
                                id='fileInput'
                                name='file'
                                type='file'
                                hidden
                                onChange={(event: ChangeEvent<HTMLInputElement>) => {
                                    const newFile = event.target.files ? event.target.files[0] : null;
                                    setFile(newFile);
                                    setFieldValue('file', newFile);
                                    if (newFile) {
                                        const reader = new FileReader();
                                        reader.onloadend = () => {
                                            setPreviewUrl(reader.result as string);
                                        };
                                        reader.readAsDataURL(newFile);
                                    } else {
                                        setPreviewUrl(null);
                                    }
                                }}
                            />
                            {previewUrl && (
                                <Segment>
                                    <Image src={previewUrl} size='big' centered />
                                    <Button icon onClick={() => {
                                        setFile(null);
                                        setFieldValue('file', null);
                                        setPreviewUrl(null);
                                    }}>
                                        <Icon name='cancel' />
                                    </Button>
                                </Segment>
                            )}

                            <Modal.Actions>
                                <label htmlFor='fileInput' className='ui icon button'>
                                    <Icon name='file image outline' />
                                    Image
                                </label>
                                <Button color='green' type='submit' loading={isSubmitting || loading} disabled={isSubmitting || !dirty || !isValid}>
                                    Update
                                </Button>
                                <Button as={Link} to='/posts' color='red' onClick={() => closeForm()}>
                                    Cancel
                                </Button>
                            </Modal.Actions>

                            <ErrorMessage name='description' render={msg => <Segment inverted color='red'>{msg}</Segment>} />
                            <ErrorMessage name='file' render={msg => <Segment inverted color='red'>{msg}</Segment>} />
                        </form>
                    )}
                </Formik>
            </Modal.Content>
        </Modal>
    );
};

export default observer(PostEditForm);
