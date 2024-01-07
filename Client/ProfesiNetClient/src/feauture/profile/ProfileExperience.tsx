import {FC} from 'react';
import {Button, Grid, Header, Icon, Item, Segment} from 'semantic-ui-react';
import {observer} from "mobx-react-lite";
import {Profile} from "../../app/modules/interfaces/Profile.ts";
import {toJS} from "mobx";
import {useStore} from "../../app/stores/Store.ts";
import AddExperienceForm from "./forms/AddExperienceForm.tsx";

export interface ExperienceProps {
    profile:Profile;
}
    
const ProfileExperience: FC<ExperienceProps> = ({profile}:ExperienceProps) => {
    const profileExperience = toJS(profile.experiences)
    const {profileStore:{isCurrentUser}, modalStore} = useStore();
    

    return (
        <Segment>
            <Grid>
                <Grid.Row>
                    <Grid.Column width={10}>
                        <Header as='h1' >
                            Experience
                        </Header>
                    </Grid.Column>
                    <Grid.Column width={6} textAlign='right'>
                        {isCurrentUser && (
                        <Button primary icon labelPosition='left' onClick={()=> modalStore.openModal(<AddExperienceForm />)}>
                            <Icon name='add'  /> Add
                            
                        </Button>
                        )}
                    </Grid.Column>
                </Grid.Row>
            </Grid>
            <Item.Group divided>
                {profileExperience.map(exp => (
                    <Item key={exp.id}>
                        <Item.Content>
                            <Item.Header as='a'>{exp.company}</Item.Header>
                            <Item.Description>
                                {exp.description}
                            </Item.Description>
                
                            <Item.Meta>
                                { exp.position}
                            </Item.Meta>
                            <Item.Extra>
                                {exp.startDate ? new Date(exp.startDate).toLocaleDateString('en-US') : 'No start date'}
                                {' - '}
                                {exp.endDate ? new Date(exp.endDate).toLocaleDateString('en-US') : 'No end date'}
                                {isCurrentUser && (
                                <Button icon='edit' content='Edit' floated='right' />
                                )}
                            </Item.Extra>
                        
                               
                           
                        </Item.Content>
                    </Item>
                ))}
            </Item.Group>
        </Segment>
    );
};

export default observer(ProfileExperience);
