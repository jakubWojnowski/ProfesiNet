import {FC, useEffect, useState} from "react";
import PostDashboard from "../../../feauture/posts/dashboard/PostDashboard.tsx";
import {Container} from "semantic-ui-react";
import {Post} from "../../modules/interfaces/Post.ts";
import axios from "axios";
import './Mid.css';
import PostCreator from "../../../feauture/posts/dashboard/PostCreator.tsx";
import {CreatePost} from "../../modules/interfaces/CreatePost.ts";


const Mid: FC = () =>  {
    const [posts, setPosts] = useState<Post[]>([]);
    const [createPost, setCreatePost] = useState<CreatePost>();
    const token = localStorage.getItem('token');
    const handlePostSubmit = (content: string): void => {
        const token: string | null = localStorage.getItem('token');

        if (token) {
            axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;

            // Construct the post data according to the CreatePost interface.
            const postData: CreatePost = {
                description: content,
                media: 'ssssssss',
            };

            axios.post<CreatePost>('https://localhost:5000/posts-module/Post', postData)
                .then(response => {
                    // Assuming you want to do something with the response here, like adding the new post to your state.
                    // If your state expects an array, you might need to do something like this:
                    // setPosts([...posts, response.data]);

                    // If you're updating a single post state:
                    setCreatePost(response.data);
                })
                .catch(error => {
                    // Handle error here
                    console.error('There was an error creating the post:', error);
                });
        }
    };

    useEffect(() => {
        
        if (token) {
            axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
            axios.get<Post[]>('https://localhost:5000/posts-module/Post/GetAll')
                .then(r => {
                    setPosts(r.data);
                });
        }
    }, []);

    return (
        <Container className="centered-content" fluid={true}>
            <PostCreator onPostSubmit={handlePostSubmit}  />
            <PostDashboard posts={posts} />
        </Container>
    );
}

export default Mid;