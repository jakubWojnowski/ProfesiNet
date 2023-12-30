import {FC} from "react";
import {Formik, Form, ErrorMessage} from "formik";
import MyTextInput from "../../app/common/form/MyTextInput.tsx";
import {Button, Header, Label} from "semantic-ui-react";
import {useStore} from "../../app/stores/Store.ts";
import {observer} from "mobx-react-lite";
import * as Yup from 'yup';

const RegisterForm: FC = () => {
    const {userStore} = useStore();
    return (

        <Formik
            initialValues={{email: '', password: '', confirmPassword:'', name: '', surname: '', error: null}}

            onSubmit={(values,{setErrors}) => userStore.register(values)
                .catch(_ => setErrors({error: 'Email is taken'}))}
            validationSchema={Yup.object({
                name: Yup.string().required(),
                surname: Yup.string().required(),
                email: Yup.string().required().email(),
                password: Yup.string().required(),
                confirmPassword: Yup.string().required().oneOf([Yup.ref('password')], 'Passwords must match')
            })}
        >
            {({handleSubmit, isSubmitting,errors, isValid, dirty}) => (
                <Form className={'ui form'} onSubmit={handleSubmit} autoComplete='off'>
                    <Header as='h2' content='Register to ProfesiNet' color='teal' textAlign='center'/>
                    <MyTextInput  placeholder='Email' name='email'/>
                    <MyTextInput  placeholder='Password' name='password' type='password'/>
                    <MyTextInput  placeholder='Confirm Password' name='confirmPassword' type='password'/>
                    <MyTextInput  placeholder='Name' name='name'/>
                    <MyTextInput  placeholder='Surname' name='surname'/>
                    <ErrorMessage name='error' render={() => <Label style={{marginBottom: 10}} basic color='red' content={errors.error}/>}/>
                    <Button
                        disabled={!isValid || !dirty || isSubmitting}
                        loading={isSubmitting} 
                        positive content='Register' 
                        type='submit' 
                        fluid/>

                </Form>
            )}
        </Formik>
    );
};

export default observer(RegisterForm);