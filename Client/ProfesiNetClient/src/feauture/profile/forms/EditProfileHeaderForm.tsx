import {FC, SyntheticEvent, useState} from 'react';
import { Formik, Form } from 'formik';
import { Header, Button } from 'semantic-ui-react';
import MyTextInput from '../../../app/common/form/MyTextInput';
import { observer } from 'mobx-react-lite';
import { useStore } from '../../../app/stores/Store';
import ManageProfileImage from "./ManageProfileImage.tsx";

const EditProfileHeaderForm: FC = () => {
    const { profileStore, modalStore } = useStore();
    const { updateProfileInformation, profile, loading } = profileStore;
    const { closeModal } = modalStore;
    const [target, setTarget] = useState('');
    const handleDeletePhoto = (photo: Photo, e: SyntheticEvent<HTMLButtonElement>) => {
        setTarget(e.currentTarget.name);
        profileStore.deletePhoto(photo).then(() => setTarget(''));
        
        
    }

    return (
        <Formik
            initialValues={{
                firstName: profile?.name || '',
                lastName: profile?.surname || '',
                address: profile?.address || '',
                title: profile?.title || '',
            }}
            onSubmit={(values, { setSubmitting }) => {
                updateProfileInformation({
                    name: values.firstName,
                    surname: values.lastName,
                    address: values.address,
                    title: values.title,
                }).then(() => {
                    setSubmitting(false);
                    closeModal();
                });
            }}
            enableReinitialize 
        >
            {({ handleSubmit, isSubmitting, dirty, isValid }) => (
                <>
                    <Header as='h2' content='Edit Information"' textAlign='center' color='blue' />
                    <Form className='ui form' onSubmit={handleSubmit}>
                        <MyTextInput name='firstName' label='Name' placeholder='Name' type='text' />
                        <MyTextInput name='lastName' label='Surname*' placeholder='Surname' type='text' />
                        <MyTextInput name='address' label='Address' placeholder='Address' type='text' />
                        <MyTextInput name='title' label='Title' placeholder='Position' type='text' />
                        <Button.Group widths='3'>
                        <Button onClick={()=> modalStore.openModal(<ManageProfileImage />)} floated={"left"} content={"Change Photo"}  />
                        <Button
                            type='submit'
                            color='blue'
                            loading={isSubmitting}
                            disabled={!dirty || !isValid || isSubmitting}
                        >
                            Save
                        </Button>
                            <Button basic color='red' content={"Delete Photo"}  loading={target === photo.id && loading} onClick={e => handleDeletePhoto(photo, e)} />
                        </Button.Group>
                    </Form>
                </>
            )}
        </Formik>
    );
};

export default observer(EditProfileHeaderForm);
