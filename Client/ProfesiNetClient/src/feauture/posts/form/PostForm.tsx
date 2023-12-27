import React, { FC, useState } from 'react';
import { Button, Modal,TextArea, Icon, Grid, Segment, Image } from 'semantic-ui-react';
import { observer } from "mobx-react-lite";
import { useStore } from "../../../app/stores/Store";
import { Formik, Form as FormikForm, Field } from "formik";

interface FormValues {
    description: string;
    file: File | null;
}
const PostForm: FC = () => {
    const [open, setOpen] = useState(false);
    const [file, setFile] = useState<File | null>(null);
    const [previewUrl, setPreviewUrl] = useState<string | null>(null);
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
        const fileInput = document.getElementById('fileInput') as HTMLInputElement;
        fileInput.value = '';
    };

    const handleSubmit = async (
        values: FormValues,
        { resetForm }: { resetForm: () => void }
    ): Promise<void> => {
        const postData = {
            description: values.description,
            file: file,
        };

        try {
            await createPost(postData);
            resetForm();
            setFile(null);
            setPreviewUrl(null);
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
                    setPreviewUrl(null);
                }}
                size='small'
            >
                <Modal.Header>Create a Post</Modal.Header>
                <Modal.Content>
                    <Formik enableReinitialize
                        initialValues={{
                            description: '',
                            file: null, 
                        }}
                        onSubmit={handleSubmit}
                    >
                        {({ setFieldValue, isSubmitting }) => (
                            <FormikForm className='ui form'>
                                <Field name="description" as={TextArea} rows={3} placeholder="What's on your mind?" style={{ minHeight: 200 }} />
                                <input
                                    type="file"
                                    onChange={(event) => {
                                        setFieldValue('file', event.currentTarget.files ? event.currentTarget.files[0] : null).then(r => console.log(r));
                                        handleFileChange(event);
                                    }}
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
                                <Button icon labelPosition='left' as="label" htmlFor="fileInput">
                                    <Icon name='file image outline' />
                                    Image
                                </Button>
                                <Button type='submit' color='green' loading={isSubmitting || loading}>
                                    Publish
                                </Button>
                            </FormikForm>
                        )}
                    </Formik>
                </Modal.Content>
            </Modal>
        </Grid>
    );
};

export default observer(PostForm);
