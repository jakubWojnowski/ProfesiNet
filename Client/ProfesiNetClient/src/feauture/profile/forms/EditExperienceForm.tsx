// EditExperienceForm.tsx

import  { FC, useEffect } from "react";
import { useStore } from "../../../app/stores/Store.ts";
import { Form, Formik } from "formik";
import { Button, Header } from "semantic-ui-react";
import MyTextInput from "../../../app/common/form/MyTextInput.tsx";
import MyTextArea from "../../../app/common/form/MyTextArea.tsx";
import MyDatePickerInput from "../../../app/common/form/MyDateInput.tsx";
import {observer} from "mobx-react-lite";
import * as Yup from "yup";
import {DeleteUserExperienceCommand} from "../../../app/modules/interfaces/User.ts";

interface EditExperienceFormProps {
    experienceId?: string; // This prop is the ID of the experience to be edited
}

const EditExperienceForm: FC<EditExperienceFormProps> = ({ experienceId }) => {
    const { profileStore, modalStore } = useStore();
    const { updateExperience, selectedExperience } = profileStore;
    const { closeModal } = modalStore;

    useEffect(() => {
        if (experienceId) {
            profileStore.selectExperience(experienceId);
        }
    }, [experienceId, profileStore]);
    const handleExperienceDelete = () => {
        console.log('Experience ID to delete:', selectedExperience?.id);
        if (selectedExperience?.id) {
            let command:DeleteUserExperienceCommand = {
                id: selectedExperience?.id,
                
            };
            profileStore.deleteExperience(command).then(r => console.log(r));
            closeModal();
        }
    };
    return (
        <Formik
            initialValues={{
                company: selectedExperience?.company || '',
                position: selectedExperience?.position || '',
                description: selectedExperience?.description || '',
                startDate: selectedExperience?.startDate || new Date(),
                endDate: selectedExperience?.endDate || null,
            }}
            validationSchema={Yup.object({
                company: Yup.string().required(),
                position: Yup.string().required(),
                description: Yup.string().required(),
                startDate: Yup.string().required()})
            }
            onSubmit={(values, { setSubmitting }) => {
                if (selectedExperience) {
                    updateExperience({
                        ...selectedExperience,
                        company: values.company,
                        position: values.position,
                        description: values.description,
                        startDate: values.startDate,
                        endDate: values.endDate ?? null,
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
                    <Header as='h2' content='Edit Experience' textAlign='center' color='blue' />
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

export default observer(EditExperienceForm);
