// EditExperienceForm.tsx

import  { FC, useEffect } from "react";
import { useStore } from "../../../app/stores/Store.ts";
import { Form, Formik } from "formik";
import { Button, Header } from "semantic-ui-react";
import MyTextInput from "../../../app/common/form/MyTextInput.tsx";
import MyDatePickerInput from "../../../app/common/form/MyDateInput.tsx";
import {observer} from "mobx-react-lite";
import * as Yup from "yup";
import {DeleteUserEducationCommand} from "../../../app/modules/interfaces/User.ts";

interface EditEducationForm {
    educationId?: string; // This prop is the ID of the experience to be edited
}

const EditEducationForm: FC<EditEducationForm> = ({ educationId }) => {
    const { profileStore, modalStore } = useStore();
    const { updateEducation, selectedEducation } = profileStore;
    const { closeModal } = modalStore;

    useEffect(() => {
        if (educationId) {
            profileStore.selectEducation(educationId);
        }
    }, [educationId, profileStore]);
    const handleExperienceDelete = () => {
        console.log('Experience ID to delete:', selectedEducation?.id);
        if (selectedEducation?.id) {
            let command:DeleteUserEducationCommand = {
                id: selectedEducation?.id,

            };
            profileStore.deleteEducation(command).then(r => console.log(r));
            closeModal();
        }
    };
    return (
        <Formik
            initialValues={{
               name: selectedEducation?.name || '',
                address: selectedEducation?.address || '',
                degree: selectedEducation?.degree || '',
                fieldOfStudy: selectedEducation?.fieldOfStudy || '',
                startDate: selectedEducation?.startDate || new Date(),
                endDate: selectedEducation?.endDate || null,
            }}
            validationSchema={Yup.object({
                name: Yup.string().required(),
                address: Yup.string().required(),
                degree: Yup.string().required(),
                fieldOfStudy: Yup.string().required(),
                startDate: Yup.string().required(),
                
            })
            }
            onSubmit={(values, { setSubmitting }) => {
                if (selectedEducation) {
                    updateEducation({
                        ...selectedEducation,
                        name: values.name,
                        address: values.address,
                        degree: values.degree,
                        fieldOfStudy: values.fieldOfStudy,
                        startDate: values.startDate,
                        endDate: values.endDate,
                    }).then(() => {
                        setSubmitting(false);
                        closeModal();
                    });
                }
            }}
            enableReinitialize
        >
            {({ handleSubmit, isSubmitting, dirty, isValid }) => (
                <>
                    <Header as='h2' content='Edit Education' textAlign='center' color='blue' />
                    <Form className='ui form' onSubmit={handleSubmit}>
                        <MyTextInput name='name' label='name' placeholder='name' type='text' />
                        <MyTextInput name='address' label='address' placeholder='address' type='text'/>
                        <MyTextInput name='degree' label='degree' placeholder='degree' type='text' />
                        <MyTextInput name='fieldOfStudy' label='fieldOfStudy' placeholder='fieldOfStudy' type='text'/>
                        <MyDatePickerInput name='startDate' placeholderText={"Start Date"}/>
                        <MyDatePickerInput name='endDate' placeholderText={'End Date'}/>
                        <Button.Group widths='2'>
                            <Button
                                type='submit'
                                color='blue'
                                loading={isSubmitting}
                                disabled={!dirty || !isValid || isSubmitting}
                            >
                                Save
                            </Button>
                            <Button
                                type="button"
                                onClick={handleExperienceDelete}
                                floated="right"
                                content="Delete Experience"
                            />
                        </Button.Group>
                    </Form>
                </>
            )}
        </Formik>
    );
};

export default observer(EditEducationForm);
