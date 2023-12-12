import {FC, useEffect, useState} from "react";
import PostDashboard from "../../../feauture/posts/dashboard/PostDashboard.tsx";
import {Container} from "semantic-ui-react";
import {Post} from "../../modules/interfaces/Post.ts";
import axios from "axios";
import './Mid.css';
import PostForm from "../../../feauture/posts/form/PostForm.tsx";
import {CreatePost} from "../../modules/interfaces/CreatePost.ts";


const Mid: FC = () =>  {
    const [posts, setPosts] = useState<Post[]>([]);
    const [, setCreatePost] = useState<CreatePost>();
    const [selectedPost, setSelectedPost] = useState<Post | undefined>(undefined);
    const [editMode, setEditMode] = useState<boolean>(false);
    const token = localStorage.getItem('token');
    const handlePostCreate = (content: string): void => {
        const token: string | null = localStorage.getItem('token');

        if (token) {
            axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
            const postData: CreatePost = {
                description: content,
            };
            axios.post<CreatePost>('https://localhost:5000/posts-module/Post', postData)
                .then(response => {
                    setCreatePost(response.data);
                })
                .catch(error => {
                    // Handle error here
                    console.error('There was an error creating the post:', error);
                });
        }
    };
    
    const handlePostUpdate = (post: Post): void => {
        const token: string | null = localStorage.getItem('token');
        if (token) {
            axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
        }
        axios.put<CreatePost>('https://localhost:5000/posts-module/Post', post)
            .then(response => {
                setCreatePost(response.data);
            })
            .catch(error => {
                // Handle error here
                console.error('There was an error updating the post:', error);
            });
    }
    useEffect(() => {
        
        if (token) {
            axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
            axios.get<Post[]>('https://localhost:5000/posts-module/Post/GetAll')
                .then(r => {
                    setPosts(r.data);
                });
        }
    }, []);
    
    
    const handlFormOpen = (id?: string): void => {
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
        

    return (
        <Container fluid={true}  >
            <PostForm onPostSubmit={handlePostCreate}  />
            <PostDashboard posts={posts}
            selectedPost={selectedPost}
            selectPost={handlePostSelect}
            cancelSelectPost={handleCancelSelect}
            editMode={editMode}
            openForm={handlFormOpen}
            closeForm={handleFormClose} 
            handlePostUpdate={handlePostUpdate}
            />
        </Container>
    );
}

export default Mid;