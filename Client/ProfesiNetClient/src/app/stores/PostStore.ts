import {action, makeObservable, observable} from "mobx";
import {Post} from "../modules/interfaces/Post.ts";
import agent from "../Api/Agent.ts";

export default class PostStore {
     posts: Post[] = [];
     selectedPost: Post | undefined = undefined;
     editMode = false;
        loading = false;
        loadingInitial = false;
    
    constructor() {
        makeObservable(this, {
          
            
           
        })
    }
    
    loadPosts = async () => {
        this.loadingInitial = true;
        try {
            const posts = await agent.Posts.list();
            posts.forEach(post => {
                post.id = post.id.split('-')[0];
                this.posts.push(post);
            })
            this.setLoadingInitial(false);
        } catch (error) {
            console.log(error);
            this.setLoadingInitial(false);
        }
    }
    setLoadingInitial = (state: boolean) => {
        this.loadingInitial = state;
    }
    
    selectPost = (id: string) => {
        this.selectedPost = this.posts.find(x => x.id === id);
    }
    
    cancelSelectedPost = () => {
        this.selectedPost = undefined;
    }
    
    openForm = (id?: string) => {
        id ? this.selectPost(id) : this.cancelSelectedPost();
        this.editMode = true;
    }
    
    closeForm = () => {
        this.editMode = false;
    }
        
    
    
   
}