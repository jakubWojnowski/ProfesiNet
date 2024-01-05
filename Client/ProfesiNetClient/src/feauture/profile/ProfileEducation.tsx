import {FC} from 'react';
import {Button, Grid, Header, Icon, Item, Segment} from 'semantic-ui-react';
import {observer} from "mobx-react-lite";
import {Profile} from "../../app/modules/interfaces/Profile.ts";
export interface EducationProps {
    profile:Profile;
}
    
const ProfileEducation: FC<EducationProps> = ({profile}:EducationProps) => {
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
                                <Button icon labelPosition='left' primary size='small'>
                                    <Icon name='add' /> Add
                                </Button>
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
                                        <Item.Meta>{exp.description}</Item.Meta>
                                        <Item.Meta>{exp.grade}</Item.Meta>
                                        <Item.Meta>{exp.fieldOfStudy}</Item.Meta>
                                        <Item.Extra>
                                            {exp.startDate ? new Date(exp.startDate).toLocaleDateString('en-US') : 'No start date'}
                                            {' - '}
                                            {exp.endDate ? new Date(exp.endDate).toLocaleDateString('en-US') : 'No end date'}
                                            <Button icon='edit' content='Edit' floated='right' />
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
