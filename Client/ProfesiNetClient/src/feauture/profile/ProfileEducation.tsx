import {FC} from 'react';
import {Header, Item, Label, Segment} from 'semantic-ui-react';

// Mock data for experiences
const mockExperiences = [
    {
        id: '1',
        School: 'University of Science and Technology',
        degree: 'Masters in Computer Science',
        period: 'Sep 2021 - Jun 2023',
        location: 'Kielce, Poland',
    }
    // ... more experiences
];

const ProfileEducation: FC = () => {
    return (
        <Segment>
            <Header dividing size='large' content='Education'/>
            <Item.Group divided>
                {mockExperiences.map(exp => (
                    <Item key={exp.id}>

                        <Item.Content>
                            <Item.Header as='a'>{exp.School}</Item.Header>
                            <Item.Meta>
                                <span className='cinema'>{exp.degree}</span>
                            </Item.Meta>
                            <Item.Meta>
                                <span>{exp.period}</span>
                            </Item.Meta>
                            <Item.Meta>
                                <span>{exp.location}</span>
                            </Item.Meta>
                        
                        </Item.Content>
                    </Item>
                ))}
            </Item.Group>
        </Segment>
    );
};

export default ProfileEducation;
