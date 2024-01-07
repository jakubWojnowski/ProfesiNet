import {FC} from "react";
import {useStore} from "../../../app/stores/Store.ts";
import {Form, Formik} from "formik";
import {Button, Header} from "semantic-ui-react";
import * as Yup from 'yup';
import MySelectSearchInput from "../../../app/common/form/MySelectSearchInput.tsx";
import {SkillOptions} from "../../../app/common/options/SkillOptions.ts";

const AddSkillsForm: FC = () => {
    const { profileStore, modalStore } = useStore();
    const { addSkills } = profileStore;
    const { closeModal } = modalStore;
    return (
        <Formik
            initialValues={{
        names: [],
    }}
    validationSchema={Yup.object({
        names: Yup.array().required(),
    })
    }
    onSubmit={(values, { setSubmitting }) => {
        addSkills({
            names: values.names,
        }).then(() => {
            setSubmitting(false);
            closeModal();
        });
    }}
    enableReinitialize
    >
    {({ handleSubmit, isSubmitting, dirty, isValid }) => (
        <>
            <Header as='h2' content='Add your Skills' textAlign='center' color='blue' />
    <Form className='ui form' onSubmit={handleSubmit}>
        <MySelectSearchInput options={SkillOptions} name='names' placeholder='Skills' />
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

export default AddSkillsForm;