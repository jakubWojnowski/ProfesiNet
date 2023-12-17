import {FC, useEffect, useState} from "react";
import PostDashboard from "../../../feauture/posts/dashboard/PostDashboard.tsx";
import {Container} from "semantic-ui-react";
import {Post} from "../../modules/interfaces/Post.ts";
import './Mid.css';
import PostForm from "../../../feauture/posts/form/PostForm.tsx";
import {CreatePost} from "../../modules/interfaces/CreatePost.ts";
import {UpdatePost} from "../../modules/interfaces/UpdatePost.ts";
import agent from "../../Api/Agent.ts";
import LoadingComponent from "../components/LoadingComponent.tsx";


const Mid: FC = () => {
    const [posts, setPosts] = useState<Post[]>([]);
    const [, setCreatePost] = useState<CreatePost>();
    const [,setUpdatePost] = useState<UpdatePost>();
    const [selectedPost, setSelectedPost] = useState<Post | undefined>(undefined);
    const [editMode, setEditMode] = useState<boolean>(false);
    const [loading, setLoading] = useState<boolean>(true);
    const [submitting, setSubmitting] = useState<boolean>(false);
    const handlePostCreate = (CreatePost:CreatePost): void => {
        setSubmitting(true);
        agent.Posts.create(CreatePost).then(() => {
                setLoading(false);
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
            setUpdatePost(updatePost);
            setEditMode(false);
            setSubmitting(false);
        }
        );
    }


    useEffect(() => {
        agent.Posts.list().then(response => {
            setPosts(response);
            setLoading(false);
        }
        );
    }, []);


    const handleFormOpen = (id?: string): void => {
        id ? handlePostSelect(id) : handleCancelSelect();
        setEditMode(true);
    }

    const handleFormClose = (): void => {
        setEditMode(false);
    }

    const handlePostSelect = (id: string): void => {
        setSelectedPost(posts.find(x => x.id === id));
    }

    const handleCancelSelect = (): void => {
        setSelectedPost(undefined);
    }


    const handlePostDelete = (id: string) => {
        agent.Posts.delete(id).then(() => {
            setPosts([...posts.filter(x => x.id !== id)]);
        }
        );
        

        
    };
    if (loading) return <LoadingComponent content='Loading app...' />;
    return (
        <Container fluid={true}>
            <PostForm 
                onPostSubmit={handlePostCreate}
                submitting={submitting}
            />
            <PostDashboard posts={posts}
                           handlePostDelete={handlePostDelete}
                           selectedPost={selectedPost}
                           selectPost={handlePostSelect}
                           cancelSelectPost={handleCancelSelect}
                           editMode={editMode}
                           submitting={submitting}
                           openForm={handleFormOpen}
                           closeForm={handleFormClose}
                           handlePostUpdate={handlePostUpdate}
                           
            />
        </Container>
    );
}

export default Mid;