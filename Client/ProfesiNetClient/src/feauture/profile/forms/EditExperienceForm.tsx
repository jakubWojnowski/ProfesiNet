// EditExperienceForm.tsx

import  { FC, useEffect } from "react";
import { useStore } from "../../../app/stores/Store.ts";
import { Form, Formik } from "formik";
import { Button, Header } from "semantic-ui-react";
import MyTextInput from "../../../app/common/form/MyTextInput.tsx";
import MyTextArea from "../../../app/common/form/MyTextArea.tsx";
import MyDatePickerInput from "../../../app/common/form/MyDateInput.tsx";
import {observer} from "mobx-react-lite";

interface EditExperienceFormProps {
    experienceId?: string; // This prop is the ID of the experience to be edited
}

const EditExperienceForm: FC<EditExperienceFormProps> = ({ experienceId }) => {
    const { profileStore, modalStore } = useStore();
    const { updateExperience, selectedExperience, loading } = profileStore;
    const { closeModal } = modalStore;

    useEffect(() => {
        if (experienceId) {
            profileStore.selectExperience(experienceId);
        }
    }, [experienceId, profileStore]);

    return (
        <Formik
            initialValues={{
                company: selectedExperience?.company || '',
                position: selectedExperience?.position || '',
                description: selectedExperience?.description || '',
                startDate: selectedExperience?.startDate || new Date(),
                endDate: selectedExperience?.endDate || new Date(),
            }}
            onSubmit={(values, { setSubmitting }) => {
                if (selectedExperience) {
                    updateExperience({
                        ...selectedExperience,
                        company: values.company,
                        position: values.position,
                        description: values.description,
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
                    <Header as='h2' content='Edit Information' textAlign='center' color='blue' />
                    <Form className='ui form' onSubmit={handleSubmit}>
                        <MyTextInput name='company' label='company' placeholder='Company' type='text' />
                        <MyTextInput name='position' label='position' placeholder='Position' type='text'/>
                        <MyTextArea name='description' label='description' placeholder='Description' rows={5} />
                        <MyDatePickerInput name='startDate' placeholderText="Start Date"/>
                        <MyDatePickerInput name='endDate' placeholderText="End Date"/>
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
                                onClick={() => { /* Add your delete experience logic here */ }}
                                floated="right"
                                content="Delete Experience"
                                loading={loading}
                            />
                        </Button.Group>
                    </Form>
                </>
            )}
        </Formik>
    );
};

export default observer(EditExperienceForm);
