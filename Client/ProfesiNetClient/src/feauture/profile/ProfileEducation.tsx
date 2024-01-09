import {FC} from 'react';
import {Button, Grid, Header, Icon, Item, Segment} from 'semantic-ui-react';
import {observer} from "mobx-react-lite";
import {Profile} from "../../app/modules/interfaces/Profile.ts";
import {useStore} from "../../app/stores/Store.ts";
import AddEducationForm from "./forms/AddEducationForm.tsx";
import EditEducationForm from "./forms/EditEducationForm.tsx";
export interface EducationProps {
    profile:Profile;
}
    
const ProfileEducation: FC<EducationProps> = ({profile}:EducationProps) => {
    const {profileStore:{isCurrentUser}, modalStore} = useStore();
    
    return (
        <Segment>
            <Grid>
                <Grid.Row>
                    <Grid.Column width={16}>
                        <Grid>
                            <Grid.Column floated='left' width={8}>
                                <Header size={"large"}>Education</Header>
                            </Grid.Column>
                            <Grid.Column floated='right' width={8} textAlign='right'>
                                {isCurrentUser && (
                                <Button icon labelPosition='left' primary size='small' onClick={()=> modalStore.openModal(<AddEducationForm />)}>
                                    <Icon name='add' /> Add
                                </Button>
                                )}
                            </Grid.Column>
                        </Grid>
                    </Grid.Column>
                </Grid.Row>
                <Grid.Row>
                    <Grid.Column width={16}>
                        <Item.Group divided>
                            {profile.educations.map((exp) => (
                                <Item key={exp.id}>
                                    <Item.Content>
                                        <Item.Header as='a'>{exp.name}</Item.Header>
                                        <Item.Description >{exp.fieldOfStudy}</Item.Description>
                                        <Item.Content >{exp.degree}</Item.Content>
                                        <Item.Meta>{exp.address}</Item.Meta>
                                        <Item.Extra>
                                            {exp.startDate ? new Date(exp.startDate).toLocaleDateString('en-US') : 'No start date'}
                                            {' - '}
                                            {exp.endDate ? new Date(exp.endDate).toLocaleDateString('en-US') : 'No end date'}
                                            {isCurrentUser && (
                                            <Button icon='edit' content='Edit' floated='right' onClick={()=> modalStore.openModal(<EditEducationForm educationId={exp.id} />)} />    
                                            )}
                                        </Item.Extra>
                                    </Item.Content>
                                </Item>
                            ))}
                        </Item.Group>
                    </Grid.Column>
                </Grid.Row>
            </Grid>
        </Segment>
    );
};

export default observer(ProfileEducation);
