import {FC} from 'react';
import {Button, Grid, Header, Icon, Item, Segment} from 'semantic-ui-react';

// Mock data for experiences
const mockExperiences = [
    {
        id: '1',
        School: 'University of Science and Technology',
        degree: 'Masters in Computer Science',
        period: 'Sep 2021 - Jun 2023',
        location: 'Kielce, Poland',
    },
    {
        id: '2',
        School: 'University of Science and Technology',
        degree: 'Bachelor in Computer Science',
        period: 'Sep 2018 - Jun 2021',
        location: 'Kielce, Poland',
    },    {
        id: '3',
        School: 'University of Science and Technology',
        degree: 'Bachelor in Computer Science',
        period: 'Sep 2018 - Jun 2021',
        location: 'Kielce, Poland',
    }

];

const ProfileEducation: FC = () => {
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
                            {mockExperiences.map((exp) => (
                                <Item key={exp.id}>
                                    <Item.Content>
                                        <Item.Header as='a'>{exp.School}</Item.Header>
                                        <Item.Meta>{exp.degree}</Item.Meta>
                                        <Item.Meta>{exp.period}</Item.Meta>
                                        <Item.Meta>{exp.location}</Item.Meta>
                                        <Button icon='edit' content='Edit' floated='right' />
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

export default ProfileEducation;
