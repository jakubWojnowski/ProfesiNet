import {makeAutoObservable, runInAction} from "mobx";
import {Post} from "../modules/interfaces/Post.ts";
import agent from "../Api/Agent.ts";
import {CreatePost} from "../modules/interfaces/CreatePost.ts";
import {UpdatePost} from "../modules/interfaces/UpdatePost.ts";

export default class PostStore {
    posts: Post[] = [];
    selectedPost: Post | undefined = undefined;
    editMode: boolean = false; // This controls whether the edit form is shown or not.
    loading: boolean = false;
    loadingInitial: boolean = false;

    constructor() {
        makeAutoObservable(this);
    }

    loadPosts = async ()=> {
        this.setLoadingInitial(true);

        try {
            const fetchedPosts: Post[] = await agent.Posts.list();
            const postMap = new Map(fetchedPosts.map(post => [post.id, post]));
            this.setPosts(Array.from(postMap.values()));
            this.setLoadingInitial(false);
        } catch (error) {
            console.error(error);
            this.setLoadingInitial(false);
        }
    };

    createPost = async (createPostData: CreatePost) => {
        this.loading = true;
        try {
            await agent.Posts.create(createPostData);
            runInAction(() => {
                // Update your state as needed after the post is created.
                this.loading = false;
                // Reload the posts to include the new one.
                this.loadPosts();
            });
        } catch (error) {
            console.error(error);
            runInAction(() => {
                this.loading = false;
            });
        }
    }



    updatePost = async (updatePost: UpdatePost) => {
        this.loading = true;
        try {
            await agent.Posts.update(updatePost);
            runInAction(() => {
                this.loading = false;
                this.loadPosts();
                this.closeForm();
            });
        } catch (error) {
            console.error(error);
            runInAction(() => {
                this.loading = false;
            });
        }
    };
    
    deletePost = async (id: string) => {
        this.loading = true;
        try {
            await agent.Posts.delete(id);
            runInAction(() => {
                this.loading = false;
                this.loadPosts();
            });
        } catch (error) {
            console.error(error);
            runInAction(() => {
                this.loading = false;
            });
        }
    };
        

    setPosts = (posts: Post[]): void => {
        this.posts = posts;
    };
    setLoadingInitial = (state: boolean) => {
        this.loadingInitial = state;
    }

    selectPost = (id: string) => {
            this.selectedPost = this.posts.find(x => x.id === id);
    };

    cancelSelectedPost = () => {
   
            this.selectedPost = undefined;

    };

    openForm = (id: string): void => {
     
            console.log(id);
            this.selectedPost = this.posts.find((post) => post.id === id);
            this.editMode = true; // Set editMode to true to show the form
     
    };

    closeForm = (): void => {
 
            this.editMode = false;
            this.selectedPost = undefined;

    };
        
    
    
   
}