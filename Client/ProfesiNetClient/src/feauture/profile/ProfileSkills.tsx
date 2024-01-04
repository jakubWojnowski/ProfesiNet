import {FC, useState} from "react";
import {Button, Grid, Header, Icon, Item, Segment} from "semantic-ui-react";
import {useTransition } from "react-spring";


const mockSkills = [
    {
         name: 'Java',
    },
    {
        name: 'C++',
    },
    {
        name: 'C#',
    },
{
        name: 'Python',
    },
    {
        name: 'JavaScript',
    },
    {
        name: 'TypeScript',
    },
    {
        name: 'React',
    },
    {
        name: 'Angular',
    },
    {
        name: 'Vue',
    },
    {
        name: 'SQL',
    },
    {
        name: 'MongoDB',
    },
    {
        name: 'PostgreSQL',
    },
    {
        name: 'Oracle',
    },
    {
        name: 'MySQL',
    },
    {
        name: 'Git',
    },
    {
        name: 'GitHub',
    },
    {
        name: 'GitLab',
    },
    {
        name: 'Jenkins',
    },
    {
        name: 'Docker',
    },
    {
        name: 'Kubernetes',
    },
    {
        name: 'Linux',
    },
    {
        name: 'Windows',
    },
    {
        name: 'MacOS',
    },
    {
        name: 'AWS',
    },
    {
        name: 'Azure',
    },
    {
        name: 'Google Cloud',
    },
    {
        name: 'Heroku',
    },
    {
        name: 'Firebase',
    },
    {
        name: 'Strapi',
    },
    {
        name: 'Node.js',
    },
    {
        name: 'ASP.NET',
    },
    {
        name: 'Spring',
    },
];
const ProfileSkills: FC = () => {
    const [showAllSkills, setShowAllSkills] = useState(false);

    // Define the number of skills to show initially and the increment step
    const initialSkillCount = 3;

    // Transition for animating the skill list
    const transitions = useTransition(
        showAllSkills ? mockSkills : mockSkills.slice(0, initialSkillCount),
        {
            keys: skill => skill.name,
            from: { opacity: 0, transform: 'translateY(-20px)' },
            enter: { opacity: 1, transform: 'translateY(0)' },
            leave: { opacity: 0, transform: 'translateY(-20px)' },
            config: { tension: 280, friction: 30 } // Adjust for smoother animation
        }
    );

    // Function to toggle the full list of skills
    const toggleSkillsDisplay = () => {
        setShowAllSkills(!showAllSkills);
    };
    return (
        <Segment>
            <Grid>
                <Grid.Row>
                    <Grid.Column width={16}>
                        <Grid>
                            <Grid.Column floated='left' width={8}>
                                <Header size="large">Skills</Header>
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
                            {transitions((style, skill) => (
                                    <Item style={style} key={skill.name}>
                                        <Item.Content>
                                            <Item.Header as='a'>{skill.name}</Item.Header>
                                        </Item.Content>
                                    </Item>
                                
                            ))}
                    </Item.Group>
                    </Grid.Column>
                </Grid.Row>
                <Grid.Row>
                    <Grid.Column textAlign='center'>
                        <Button onClick={toggleSkillsDisplay}>
                            {showAllSkills ? 'Show Less' : `Show More (${mockSkills.length - 3})`}
                        </Button>
                    </Grid.Column>
                </Grid.Row>
            </Grid>
        </Segment>
    );
};

export default ProfileSkills;