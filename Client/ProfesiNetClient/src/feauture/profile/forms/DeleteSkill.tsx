import {FC, useEffect} from "react";
import {useStore} from "../../../app/stores/Store.ts";
import {Button, Header} from "semantic-ui-react";
import {observer} from "mobx-react-lite";

interface DeleteSkillFormProps {
    skillId?: string;
}

const DeleteSkillFormProps: FC<DeleteSkillFormProps> = ({skillId}) => {
    const {profileStore, modalStore} = useStore();
    const {deleteSkill, selectedSkill} = profileStore;
    const {closeModal} = modalStore;

    useEffect(() => {
        if (skillId) {
            profileStore.selectSkill(skillId);
        }
    }, [skillId, profileStore]);
    const handleDeleteSkill = () => {
        if (selectedSkill?.id) {
            deleteSkill(selectedSkill?.id).then(r => console.log(r));
            closeModal();
        }
    };
    return (
        <>
            <Header as='h2' content='Deleting Skill' textAlign='center' color='blue'/>

            <Button.Group widths='2'>
                <Button
                    type='button'
                    color='yellow'
                    content='Cancel'
                    onClick={() => closeModal()}/>

                <Button
                    type="button"
                    onClick={handleDeleteSkill}
                    floated="right"
                    content="Delete skill"
                />
            </Button.Group>
        </>


    );
};

export default observer(DeleteSkillFormProps);
