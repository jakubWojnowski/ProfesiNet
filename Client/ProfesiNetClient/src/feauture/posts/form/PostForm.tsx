import React, { FC, useState } from 'react';
import { Button, Modal, TextArea, Icon, Grid, Segment, Image } from 'semantic-ui-react';
import { observer } from "mobx-react-lite";
import { useStore } from "../../../app/stores/Store";
import {Formik, Form, Field, ErrorMessage} from "formik";
import * as Yup from 'yup';

interface FormValues {
    description: string;
    file: File | null;
}

const PostForm: FC = () => {
    const [open, setOpen] = useState(false);
    const [previewUrl, setPreviewUrl] = useState<string | null>(null);
    const { postStore } = useStore();
    const { createPost, loading } = postStore;

    const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>, setFieldValue: (field: string, value: any) => void) => {
        const file = event.target.files ? event.target.files[0] : null;
        setFieldValue('file', file);
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

    const handleCancelImage = (setFieldValue: (field: string, value: any) => void) => {
        setPreviewUrl(null);
        setFieldValue('file', null);
    };

    const handleSubmit = async (values: FormValues, { resetForm }: { resetForm: () => void }): Promise<void> => {
        try {
            await createPost(values);
            resetForm();
            setPreviewUrl(null);
            setOpen(false);
        } catch (error) {
            console.error('Error creating the post:', error);
            alert('Failed to create the post.');
        }
    };

    const validationSchema = Yup.object({
        description: Yup.string().nullable(),
        file: Yup.mixed().nullable(),
    }).test('fileOrDescription', 'Either a description or file is required', value => {
        return !!(value.description || (value.file instanceof File && value.file.size > 0));
    });
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
                    setPreviewUrl(null);
                }}
                size='small'
            >
                <Modal.Header>Create a Post</Modal.Header>
                <Modal.Content>
                    <Formik
                        initialValues={{
                            description: '',
                            file: null,
                        }}
                        onSubmit={handleSubmit}
                        validationSchema={validationSchema}
                    >
                        {({ setFieldValue, isSubmitting }) => (
                            <Form className='ui form'>
                                <Field name="description" as={TextArea} rows={3} placeholder="What's on your mind?" style={{ minHeight: 200 }} />
                                <input
                                    id="fileInput"
                                    name="file"
                                    type="file"
                                    onChange={(event) => handleFileChange(event, setFieldValue)}
                                    hidden
                                />
                                {previewUrl && (
                                    <Segment>
                                        <Image src={previewUrl} size='big' centered />
                                        <Button icon onClick={() => handleCancelImage(setFieldValue)}>
                                            <Icon name='cancel' />
                                        </Button>
                                    </Segment>
                                )}
                                <label htmlFor="fileInput" className="ui icon button">
                                    <Icon name='file image outline' />
                                    Image
                                </label>
                                <Button type='submit' color='green' loading={isSubmitting || loading}>
                                    Publish
                                </Button>
                                <ErrorMessage name={'description'} render={(error) => <Segment inverted color='red'>{error}</Segment>} />
                                <ErrorMessage name={'file'} render={(error) => <Segment inverted color='red'>{error}</Segment>} />
                            </Form>
                        )}
                    </Formik>
                </Modal.Content>
            </Modal>
        </Grid>
    );
};

export default observer(PostForm);
