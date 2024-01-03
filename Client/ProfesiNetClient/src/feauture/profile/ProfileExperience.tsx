import {FC} from 'react';
import {Header, Item, Label, Segment} from 'semantic-ui-react';

// Mock data for experiences
const mockExperiences = [
    {
        id: '1',
        title: 'Internship',
        company: 'Silevis - Code People',
        period: 'Sep 2023 - Nov 2023',
        location: 'Kielce, Poland',
        description: 'I collaborated on developing a vote-casting module using React and Strapi',
        skills: ['React', 'TypeScript', 'Strapi']
    },
    {
        id: '2',
        title: 'Intern',
        company: 'Infover S.A.',
        period: 'Jun 2023 - Jul 2023',
        location: 'Kielce, Poland',
        description: 'I was developing a backend app called petrax with tools like ASP.NET Core and MSSQL',
        skills: ['NHibernate', 'Fluent Migrator', 'Microsoft SQL Server', 'ASP.NET', 'C#']
    },
    // ... more experiences
];

const ProfileExperience: FC = () => {
    return (
        <Segment>
            <Header dividing size='large' content='Experience'/>
        <Item.Group divided>
            {mockExperiences.map(exp => (
                <Item key={exp.id}>

                    <Item.Content>
                        <Item.Header as='a'>{exp.title}</Item.Header>
                        <Item.Meta>
                            <span className='cinema'>{exp.company}</span>
                        </Item.Meta>
                        <Item.Meta>
                            <span>{exp.period}</span>
                        </Item.Meta>
                        <Item.Meta>
                            <span>{exp.location}</span>
                        </Item.Meta>
                        <Item.Description>{exp.description}</Item.Description>
                        <Item.Extra>
                            {exp.skills.map((skill, index) => (
                                <Label key={index}>{skill}</Label>
                            ))}
                        </Item.Extra>
                    </Item.Content>
                </Item>
            ))}
        </Item.Group>
        </Segment>
    );
};

export default ProfileExperience;
