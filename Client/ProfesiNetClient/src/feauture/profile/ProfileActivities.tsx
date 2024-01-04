import {FC, useState} from 'react';
import {Button, Card, Header, Icon, Segment} from 'semantic-ui-react';
import { useTransition, animated } from 'react-spring';

import './ProfileActivities.css';
import {useStore} from "../../app/stores/Store.ts";

// Mock data for activities/posts
const MockActivities = [
    {
        id: '1',
        date: '2024-01-01',
        content: 'Jakub Wojnowski published a new post',
        description: 'Today we started a new project with my team...',
        likes: '5'
    },
    {
        id: '2',
        date: '2024-01-02',
        content: 'Jakub Wojnowski shared a photo',
        description: 'Exploring the beauty of nature...',
        likes: '15'
    },   {
        id: '3',
        date: '2024-01-02',
        content: 'Jakub Wojnowski shared a photo',
        description: 'Exploring the beauty of nature...',
        likes: '15'
    },   {
        id: '4',
        date: '2024-01-02',
        content: 'Jakub Wojnowski shared a photo',
        description: 'Exploring the beauty of nature...',
        likes: '15'
    },   {
        id: '5',
        date: '2024-01-02',
        content: 'Jakub Wojnowski shared a photo',
        description: 'Exploring the beauty of nature...',
        likes: '15'
    },   {
        id: '6',
        date: '2024-01-02',
        content: 'Jakub Wojnowski shared a photo',
        description: 'Exploring the beauty of nature...',
        likes: '15'
    },
    // ... more mock activities
];


const ProfileActivities: FC = () => {
    const [displayedActivities, setDisplayedActivities] = useState(3);
    const {postStore} = useStore();
    const {PostsByCreator} = postStore;

    const showMoreActivities = () => {
        setDisplayedActivities((prev) => (prev + 3 <= MockActivities.length ? prev + 3 : MockActivities.length));
    };

    const hideActivities = () => {
        setDisplayedActivities((prev) => (prev - 3 >= 0 ? prev - 3 : 0));
    };

    const visibleActivities = PostsByCreator.slice(0, displayedActivities);

    const transitions = useTransition(visibleActivities, {
        keys: (item) => item.id,
        from: { transform: 'translate3d(0,-40px,0)', opacity: 0 },
        enter: { transform: 'translate3d(0,0px,0)', opacity: 1 },
        leave: { transform: 'translate3d(0,-40px,0)', opacity: 0 },
    });

    return (
        <Segment>
            <Header dividing size="large" content="Activities" />

            {transitions((style, item) => (
                <animated.div style={style}>
                    <Card fluid key={item.id}>
                        <Card.Content>
                            <Card.Header>{item.content}</Card.Header>
                            <Card.Meta>{item.date}</Card.Meta>
                            <Card.Description>{item.description}</Card.Description>
                        </Card.Content>
                        <Card.Content extra>
                            <Icon name="like" />
                            {item.likes} Likes
                        </Card.Content>
                    </Card>
                </animated.div>
            ))}
            {displayedActivities < MockActivities.length && (
                <Button primary fluid onClick={showMoreActivities}>
                    Load More
                </Button>
            )}
            {displayedActivities > 3 && (
                <Button secondary fluid onClick={hideActivities}>
                    Show Less
                </Button>
            )}
    </Segment>
    );
};

export default ProfileActivities;

