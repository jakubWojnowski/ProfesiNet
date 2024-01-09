import {FC} from "react";
import {useStore} from "../../../app/stores/Store.ts";
import {Form, Formik} from "formik";
import {Button, Header} from "semantic-ui-react";
import MyTextArea from "../../../app/common/form/MyTextArea.tsx";
import MyDatePickerInput from "../../../app/common/form/MyDateInput.tsx";
import MyTextInput from "../../../app/common/form/MyTextInput.tsx";
import * as Yup from 'yup';

const AddExperienceForm: FC = () => {
    const { profileStore, modalStore } = useStore();
    const { addExperience } = profileStore;
    const { closeModal } = modalStore;
    return (
        <Formik
            initialValues={{
                company: '',
                position: '',
                description: '',
                startDate: new Date,
                endDate: new Date,
            }}
            validationSchema={Yup.object({
                company: Yup.string().required(),
                position: Yup.string().required(),
                description: Yup.string().required(),
                startDate: Yup.string().required()})
            }
            onSubmit={(values, { setSubmitting }) => {
                addExperience({
                    company: values.company,
                    position: values.position,
                    description: values.description,
                    startDate: values.startDate,
                    endDate: values.endDate,
                }).then(() => {
                    setSubmitting(false);
                    closeModal();
                });
            }}
            enableReinitialize
        >
            {({ handleSubmit, isSubmitting, dirty, isValid }) => (
                <>
                    <Header as='h2' content='Add your Experience' textAlign='center' color='blue' />
                    <Form className='ui form' onSubmit={handleSubmit}>
                        <MyTextInput name='company' label='company' placeholder='Company' type='text' />
                        <MyTextInput name='position' label='position' placeholder='Position' type='text'/>
                        <MyTextArea name='description' label='description' placeholder='Description' type='text' rows={5} width={100} />
                        <MyDatePickerInput name='startDate' placeholderText={"Start Date"}/>
                        <MyDatePickerInput name='endDate' placeholderText={'End Date'}/>

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

export default AddExperienceForm;