import PostStore from "./PostStore.ts";
import {createContext, useContext} from "react";
import UserStore from "./UserStore.ts";
import CommonStore from "./CommonStore.ts";
import ModalStore from "./ModalStore.ts";
import ProfileStore from "./ProfileStore.ts";
import CommentStore from "./CommentStore.tsx";

interface Store {
    postStore: PostStore
    userStore: UserStore
    commonStore: CommonStore
    modalStore: ModalStore
    profileStore: ProfileStore 
    commentStore: CommentStore

}

export const store: Store = {
    postStore: new PostStore(),
    userStore: new UserStore(),
    commonStore: new CommonStore(),
    modalStore: new ModalStore(),
    profileStore: new ProfileStore(),
    commentStore: new CommentStore()
    
}

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}