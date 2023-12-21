import {FC} from "react";
import {Formik, Form, ErrorMessage} from "formik";
import MyTextInput from "../../app/common/form/MyTextInput.tsx";
import {Button, Label} from "semantic-ui-react";
import {useStore} from "../../app/stores/Store.ts";
import {observer} from "mobx-react-lite";

const LoginForm: FC = () => {
    const {userStore} = useStore();
    return (
        <Formik 
            initialValues={{email: '', password: '', error: null}}
                
         onSubmit={(values,{setErrors}) => userStore.login(values)
             .catch(error => setErrors({error: 'Invalid email or password'}))}
    >
            {({handleSubmit, isSubmitting,errors}) => (
                <Form className={'ui form'} onSubmit={handleSubmit} autoComplete='off'>
                    <MyTextInput  placeholder='Email' name='email'/>
                    <MyTextInput  placeholder='Password' name='password' type='password'/>
                    <ErrorMessage name='error' render={() => <Label style={{marginBottom: 10}} basic color='red' content={errors.error}/>}/>
                    <Button loading={isSubmitting} positive content='Login' type='submit' fluid/>
         
                </Form>
            )}
        </Formik>
    );
};

export default observer(LoginForm);