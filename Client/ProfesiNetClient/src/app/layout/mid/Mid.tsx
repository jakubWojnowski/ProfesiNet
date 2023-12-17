import {FC, useEffect, useState} from "react";
import PostDashboard from "../../../feauture/posts/dashboard/PostDashboard.tsx";
import {Button, Container} from "semantic-ui-react";
import {Post} from "../../modules/interfaces/Post.ts";
import './Mid.css';
import PostForm from "../../../feauture/posts/form/PostForm.tsx";
import {CreatePost} from "../../modules/interfaces/CreatePost.ts";
import {UpdatePost} from "../../modules/interfaces/UpdatePost.ts";
import agent from "../../Api/Agent.ts";
import LoadingComponent from "../components/LoadingComponent.tsx";
import {useStore} from "../../stores/Store.ts";
import {observer} from "mobx-react-lite";


const Mid: FC = () => {
    const {postStore} = useStore();
    const [posts, setPosts] = useState<Post[]>([]);
    const [loading, setLoading] = useState<boolean>(true);
    const [submitting, setSubmitting] = useState<boolean>(false);
    const handlePostCreate = (CreatePost:CreatePost): void => {
        setSubmitting(true);
        agent.Posts.create(CreatePost).then(() => {
                setLoading(false);
                setSubmitting(false);
            agent.Posts.list().then(response => {
                setPosts(response);
               
            }
            );
            
        }
        );
    };


    const handlePostUpdate = (updatePost: UpdatePost): void => {
        setSubmitting(true);
       
        agent.Posts.update(updatePost).then(() => {
            setSubmitting(false);
            agent.Posts.list().then(response => {
                setPosts(response);
            }
            );
        }
        );
    }


    useEffect(() => {
        postStore.loadPosts().then(() => setLoading(false));
   
    }, [postStore]);





    const handlePostDelete = (id: string) => {
        agent.Posts.delete(id).then(() => {
            setPosts([...posts.filter(x => x.id !== id)]);
        }
        );
        

        
    };
    if (postStore.loadingInitial) return <LoadingComponent content='Loading app...' />;
    return (
        <Container fluid={true}>
            <PostForm 
                onPostSubmit={handlePostCreate}
                submitting={submitting}
            />
            <PostDashboard posts={postStore.posts}
                           handlePostDelete={handlePostDelete}
                           submitting={submitting}
                           handlePostUpdate={handlePostUpdate}
                           
            />
        </Container>
    );
}

export default observer(Mid) ;