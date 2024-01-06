import {FC, FormEvent, useState} from "react";
import { Form, Header, Button } from 'semantic-ui-react';
import {useStore} from "../../../app/stores/Store.ts";
import {UpdateUserInformationCommand} from "../../../app/modules/interfaces/User.ts";
import {observer} from "mobx-react-lite";

const EditProfileHeaderForm: FC = () => {
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [address, setAddress] = useState('');
    const [title, setTitle] = useState('');
    const [loading, setLoading] = useState(false);
    const { profileStore, modalStore } = useStore();
    const { closeModal } = modalStore;
    const { profile, isCurrentUser, updateProfileInformation } = profileStore;
    const handleSubmit = (event: FormEvent<HTMLFormElement>) => {
        event.preventDefault(); // This prevents the default form submission behavior
        setLoading(true);
        const command: UpdateUserInformationCommand = {
            name: firstName,
            surname: lastName,
            address: address,
            title: title
        };
        updateProfileInformation(command).then(() => {
            closeModal();
        }).finally(() => setLoading(false));
        
    };
    return (
        <>
            <Header as='h2' content='Edytuj "O mnie"' textAlign='center' />
            <Form onSubmit={handleSubmit}>
                <Form.Input
                    fluid
                    label='Imię*'
                    placeholder='Imię'
                    value={firstName}
                    onChange={(e, { value }) => setFirstName(value)}
                />
                <Form.Input
                    fluid
                    label='Nazwisko*'
                    placeholder='Nazwisko'
                    value={lastName}
                    onChange={(e, { value }) => setLastName(value)}
                />
                <Form.Input
                    fluid
                    label='Dodatkowe imię/nazwisko'
                    placeholder='Dodatkowe imię/nazwisko'
                    value={address}
                    onChange={(e, { value }) => setAddress(value)}
                />
                <Form.Input
                    fluid
                    label='Nagłówek'
                    placeholder='.Net Developer'
                    value={title}
                    onChange={(e, { value }) => setTitle(value)}
                />
                <Button type='submit' color='green' loading={loading} onClick={close}>Save</Button>
               
            </Form>
        </>
    );
};

export default observer(EditProfileHeaderForm);