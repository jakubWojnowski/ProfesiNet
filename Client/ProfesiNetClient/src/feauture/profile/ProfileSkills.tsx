import {FC, useState} from "react";
import {Button, Grid, Header, Icon, Item, Segment} from "semantic-ui-react";
import {useTransition } from "react-spring";
import {observer} from "mobx-react-lite";
import {Profile} from "../../app/modules/interfaces/Profile.ts";

export interface SkillProps {
    profile:Profile;
}
const ProfileSkills: FC<SkillProps> = ({profile}:SkillProps) => {
    const [showAllSkills, setShowAllSkills] = useState(false);

    const initialSkillCount = 3;

    const transitions = useTransition(
        showAllSkills ? profile.skills : profile.skills.slice(0, initialSkillCount),
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
                        {profile.skills.length > 0 && (
                        <Button onClick={toggleSkillsDisplay}>
                            {showAllSkills ? 'Show Less' : `Show More (${profile.skills.length - 3})`}
                        </Button>
                        )}
                    </Grid.Column>
                </Grid.Row>
            </Grid>
        </Segment>
    );
};

export default observer(ProfileSkills);