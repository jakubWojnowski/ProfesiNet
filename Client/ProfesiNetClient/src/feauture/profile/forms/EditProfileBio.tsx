import {FC} from "react";
import {Button, Header} from "semantic-ui-react";
import {Form, Formik} from "formik";
import {useStore} from "../../../app/stores/Store.ts";
import MyTextArea from "../../../app/common/form/MyTextArea.tsx";

const EditProfileBio: FC = () => {
    const { profileStore, modalStore } = useStore();
    const { updateProfileBio, profile } = profileStore;
    const { closeModal } = modalStore;
    return (
        <Formik
            initialValues={{
                bio: profile?.bio || '',
            }}
            onSubmit={(values, { setSubmitting }) => {
                updateProfileBio({
                    bio: values.bio,
                }).then(() => {
                    setSubmitting(false);
                    closeModal();
                });
            }}
            enableReinitialize
        >
            {({ handleSubmit, isSubmitting, dirty, isValid }) => (
                <>
                    <Header as='h2' content='Edit Bio"' textAlign='center' color='blue' />
                    <Form className='ui form' onSubmit={handleSubmit}>
                        <MyTextArea name='bio' label='bio' placeholder='bio' type='text' rows={5} width={100} />
                     
                        <Button
                            type='submit'
                            color='blue'
                            loading={isSubmitting}
                            disabled={!dirty || !isValid || isSubmitting}
                        >
                            Save
                        </Button>
                    </Form>
                </>
            )}
        </Formik>
    );
};

export default EditProfileBio;