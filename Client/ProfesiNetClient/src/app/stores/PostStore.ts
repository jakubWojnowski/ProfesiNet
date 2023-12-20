import {makeAutoObservable, runInAction} from "mobx";
import {Post} from "../modules/interfaces/Post.ts";
import agent from "../Api/Agent.ts";
import {CreatePost} from "../modules/interfaces/CreatePost.ts";
import {UpdatePost} from "../modules/interfaces/UpdatePost.ts";

export default class PostStore {
    postRegistry = new Map<string, Post>(); 
    selectedPost: Post | undefined = undefined;
    editMode: boolean = false; // This controls whether the edit form is shown or not.
    loading: boolean = false;
    loadingInitial: boolean = false;

    constructor() {
        makeAutoObservable(this);
    }
    
    get PostsBy() {
        return Array.from(this.postRegistry.values());
    }

    loadPosts = async (): Promise<void> => {
        this.setLoadingInitial(true);

        try {
            const fetchedPosts: Post[] = await agent.Posts.list();
            fetchedPosts.forEach(post => {
                this.postRegistry.set(post.id, post);
            });
            this.setLoadingInitial(false);
        } catch (error) {
            console.error(error);
            this.setLoadingInitial(false);
        }
    };


    createPost = async (createPostData: CreatePost) => {
        this.loading = true;
        try {
       const postId =  await agent.Posts.create(createPostData);
         const post = await agent.Posts.details(postId);
       console.log(postId);
          
            runInAction(() => {
                this.postRegistry.set(post.id, post);
                this.selectedPost = post;
                this.loading = false;
              
                // this.loadPosts();
                
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
            const postId = await agent.Posts.update(updatePost);
            const post = await agent.Posts.details(postId);
            runInAction(() => {
                this.loading = false;
                this.postRegistry.set(post.id, post);
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
                this.postRegistry.delete(id);
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
    
    loadPost = async (id: string) => {
        let post = this.getPost(id);
        if (post) this.selectedPost = post;
        else {
            this.loadingInitial = true;
            try {
                post = await agent.Posts.details(id);
                this.setPost(post);
                this.selectedPost = post;
                this.setLoadingInitial(false);
                return post;
            } catch (error) {
                console.error(error);
                this.setLoadingInitial(false);
            }
        }
    }
    
    private getPost = (id: string) => {
        return this.postRegistry.get(id);
    }
    
    private setPost = (post: Post) => {
        this.postRegistry.set(post.id, post);
    }
        


    setLoadingInitial = (state: boolean) => {
        this.loadingInitial = state;
    }

    selectPost = (id: string) => {
        this.selectedPost = this.postRegistry.get(id);
    };

    cancelSelectedPost = () => {
   
            this.selectedPost = undefined;

    };

    openForm = (id: string): void => {
     
            console.log(id);
            this.selectedPost = this.postRegistry.get(id);
            this.editMode = true;
    };
    closeForm = (): void => {
 
            this.editMode = false;
            this.selectedPost = undefined;
    };
        
    
    
   
}