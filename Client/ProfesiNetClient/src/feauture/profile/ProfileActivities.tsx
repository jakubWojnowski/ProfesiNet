import {FC, useState} from 'react';
import {Button, Card, Header, Icon, Segment} from 'semantic-ui-react';
import { useTransition, animated } from 'react-spring';

import './ProfileActivities.css';
import {useStore} from "../../app/stores/Store.ts";
import {Link} from "react-router-dom";
import {observer} from "mobx-react-lite";

// Mock data for activities/posts

const ProfileActivities: FC = () => {
    const [displayedActivities, setDisplayedActivities] = useState(3);
    const { postStore } = useStore();
    const { PostsByCreator } = postStore;

    const showMoreActivities = () => {
        setDisplayedActivities((prev) => (prev + 3 <= PostsByCreator.length ? prev + 3 : PostsByCreator.length));
    };

    const hideActivities = () => {
        setDisplayedActivities((prev) => (prev - 3 >= 0 ? prev - 3 : 0));
    };

    const visibleActivities = PostsByCreator.slice(0, displayedActivities); // Use the local PostsByCreator

    const transitions = useTransition(visibleActivities, {
        keys: (post) => post.id,
        from: { transform: 'translate3d(0,-40px,0)', opacity: 0 },
        enter: { transform: 'translate3d(0,0px,0)', opacity: 1 },
        leave: { transform: 'translate3d(0,-40px,0)', opacity: 0 },
    });

    return (
        <Segment>
            <Header dividing size="large" content="Activities" />
            {transitions((style, post) => (
                <animated.div style={style}>
                    <Card fluid key={post.id}>
                        <Card.Content>
                            <Card.Header as={Link} to={`/posts/${post.id}`}>{post.description}</Card.Header>
                            <Card.Meta>{post.publishedAt}</Card.Meta>
                        </Card.Content>
                        <Card.Content extra>
                            <Icon name="like"/>
                           <span>{post.likesCount} Likes </span>
                            <Icon name="comment" />
                            <span> {post.commentsCount} Comments </span>
                            <Icon name="share" />
                            <span> {post.sharesCount} Shares </span>
                        </Card.Content>
                    </Card>
                </animated.div>
            ))}
            {displayedActivities < PostsByCreator.length && (
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

export default observer(ProfileActivities);

