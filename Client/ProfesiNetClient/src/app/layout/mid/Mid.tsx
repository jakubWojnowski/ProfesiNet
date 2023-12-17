import {FC, useEffect} from "react";
import PostDashboard from "../../../feauture/posts/dashboard/PostDashboard.tsx";
import { Container} from "semantic-ui-react";

import './Mid.css';
import PostForm from "../../../feauture/posts/form/PostForm.tsx";


import LoadingComponent from "../components/LoadingComponent.tsx";
import {useStore} from "../../stores/Store.ts";
import {observer} from "mobx-react-lite";


const Mid: FC = () => {
    const {postStore} = useStore();




    


    useEffect(() => {
        postStore.loadPosts().then(() => postStore.setLoadingInitial(false));
   
    }, [postStore]);






    if (postStore.loadingInitial) return <LoadingComponent content='Loading app...' />;
    return (
        <Container fluid={true}>
            <PostForm 
            />
            <PostDashboard posts={postStore.posts}
               
            />
        </Container>
    );
}

export default observer(Mid) ;