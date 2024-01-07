import FC from "react";
import {useStore} from "../../../app/stores/Store.ts";
import {Form, Formik} from "formik";
import {Button, Header} from "semantic-ui-react";
import MyTextArea from "../../../app/common/form/MyTextArea.tsx";
import MyDatePickerInput from "../../../app/common/form/MyDateInput.tsx";

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
                startDate: '',
                endDate: '',
            }}
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
                    <Header as='h2' content='Edit Bio"' textAlign='center' color='blue' />
                    <Form className='ui form' onSubmit={handleSubmit}>
                        <MyTextArea name='company' label='company' placeholder='company' type='text' rows={5} width={100} />
                        <MyTextArea name='position' label='position' placeholder='position' type='text' rows={5} width={100} />
                        <MyTextArea name='description' label='description' placeholder='description' type='text' rows={5} width={100} />
                        <MyDatePickerInput name='startDate'/>
                        <MyDatePickerInput name='endDate'/>

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