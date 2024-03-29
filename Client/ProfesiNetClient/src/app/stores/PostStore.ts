import {makeAutoObservable, runInAction} from "mobx";
import {Post} from "../modules/interfaces/Post.ts";
import agent from "../Api/Agent.ts";
import {CreatePost} from "../modules/interfaces/CreatePost.ts";
import {UpdatePost} from "../modules/interfaces/UpdatePost.ts";

export default class PostStore {
    postRegistry = new Map<string, Post>(); 
    postPerCreatorRegistry = new Map<string, Post>();
    selectedPost: Post | undefined = undefined;
    editMode: boolean = false; 
    loading: boolean = false;
    loadingInitial: boolean = false;

    constructor() {
        makeAutoObservable(this);
    }
    
    get PostsBy() {
        return Array.from(this.postRegistry.values());
    }
    get PostsByCreator() {
        return Array.from(this.postPerCreatorRegistry.values());
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

    loadPost = async (id: string): Promise<Post> => {
        console.log(`loadPost started with id: ${id}`);

        let post: Post | undefined = this.getPost(id);
        console.log(`getPost result for id ${id}:`, post);

        if (post) {
            this.selectedPost = post;
            console.log(`Post already in cache:`, post);
            return post;
        } else {
            this.loadingInitial = true;
            console.log(`Post not in cache. Fetching post for id ${id}`);
            try {
                post = await agent.Posts.details(id);
                console.log(`Received post data for id ${id}:`, post);

                this.setPost(post);
                runInAction(() => {
                    this.selectedPost = post;
                });

                console.log(`Post set successfully for id ${id}`);
            } catch (error) {
                console.error(`Error loading post for id ${id}:`, error);
            } finally {
                this.setLoadingInitial(false);
                console.log(`Finished loading post for id ${id}. loadingInitial set to false`);
            }

            return post as Post;
        }
        
    }
    loadCreatorPosts = async (id: string): Promise<void> => {
        this.setLoadingInitial(true); // Assuming setLoadingInitial is an action
        this.postPerCreatorRegistry.clear(); // Assuming postPerCreatorRegistry is observable and clear is an action

        try {
            const fetchedPosts: Post[] = await agent.Posts.getAllByCreator(id);
           runInAction(() => {
                fetchedPosts.forEach((post: Post) => {
                    this.postPerCreatorRegistry.set(post.id, post);
                });
                this.setLoadingInitial(false);
            });
        } catch (error) {
            console.error(error);
            runInAction(() => {
                this.setLoadingInitial(false);
            });
        }
    };

    likePost = async (id: string): Promise<void> => {
        this.loading = true;
        try {
            // Assuming agent.Posts.like returns the new likes count
            const newLikesCount = await agent.Posts.like(id);
            runInAction(() => {
                const post = this.postRegistry.get(id);
                if (post) {
                    post.likesCount = newLikesCount;
                    post.isLiked = true;
                    this.postRegistry.set(id, post); 
                    this.selectedPost = post;
                }
                this.loading = false;
            });
        } catch (error) {
            console.error(error);
            runInAction(() => {
                this.loading = false;
            });
        }
    }


    sharePost = async (id: string) => {
        this.loading = true;
        try {
            await agent.Posts.share(id);
            runInAction(() => {
                const post = this.postRegistry.get(id);
                if (post) {
                    post.sharesCount++;
                    post.isShared = true;
                    this.postRegistry.set(id, post);
                    this.selectedPost = post;
                    this.loading = false;
                }
            });
        } catch (error) {
            console.error(error);
            runInAction(() => {
                this.loading = false;
            });
        }
    }
    
    unSharePost = async (id: string) => {
        this.loading = true;
        try {
            await agent.Posts.unShare(id);
            runInAction(() => {
                const post = this.postRegistry.get(id);
                if (post) {
                    post.sharesCount--;
                    post.isShared = false;
                    this.postRegistry.set(id, post);
                    this.selectedPost = post;
                    this.loading = false;
                }
            });
        } catch (error) {
            console.error(error);
            runInAction(() => {
                this.loading = false;
            });
        }
    }
    
    unLikePost = async (id: string) => {
        this.loading = true;
        try {
             await agent.Posts.unlike(id);
            runInAction(() => {
                const post = this.postRegistry.get(id);
                if (post) {
                    post.isLiked = false;
                    post.likesCount --;
                    this.postRegistry.set(id, post);
                    this.selectedPost = post;
                }
                this.loading = false;
            });
        } catch (error) {
            console.error(error);
            runInAction(() => {
                this.loading = false;
            });
        }
    }
    isLiked = (id: string) => {
        const post = this.postRegistry.get(id);
        if (post) {
            return post.isLiked;
        }
        return false;
    }
    
    isShared = (id: string) => {
        const post = this.postRegistry.get(id);
        if (post) {
            return post.isShared;
        }
        return false;
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
    
    clearSelectedPost = () => {
        this.selectedPost = undefined;
    }
    
    
        
    
    
   
}