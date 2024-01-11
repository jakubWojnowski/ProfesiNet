import {FC} from "react";
import {useStore} from "../../../app/stores/Store.ts";
import {Form, Formik} from "formik";
import {Button, Header} from "semantic-ui-react";
import MyDatePickerInput from "../../../app/common/form/MyDateInput.tsx";
import MyTextInput from "../../../app/common/form/MyTextInput.tsx";
import * as Yup from 'yup';

const AddEducationForm: FC = () => {
    const { profileStore, modalStore } = useStore();
    const { addEducation } = profileStore;
    const { closeModal } = modalStore;
    return (
        <Formik
            initialValues={{
                name: '',
                address: '',
                degree: '',
                fieldOfStudy: '',
                startDate: new Date,
                endDate: new Date,
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
                addEducation({
                  name: values.name,
                    address: values.address,
                    degree: values.degree,
                    fieldOfStudy: values.fieldOfStudy,
                    startDate: values.startDate,
                    endDate: values.endDate ?? null,
                    
                }).then(() => {
                    setSubmitting(false);
                    closeModal();
                });
            }}
            enableReinitialize
        >
            {({ handleSubmit, isSubmitting, dirty, isValid }) => (
                <>
                    <Header as='h2' content='Add Education"' textAlign='center' color='blue' />
                    <Form className='ui form' onSubmit={handleSubmit}>
                        <MyTextInput name='name' label='name' placeholder='name' type='text' />
                        <MyTextInput name='address' label='address' placeholder='address' type='text'/>
                        <MyTextInput name='degree' label='degree' placeholder='degree' type='text' />
                        <MyTextInput name='fieldOfStudy' label='fieldOfStudy' placeholder='fieldOfStudy' type='text'/>
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

export default AddEducationForm;