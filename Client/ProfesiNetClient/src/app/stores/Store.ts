import PostStore from "./PostStore.ts";
import {createContext, useContext} from "react";
import UserStore from "./UserStore.ts";
import CommonStore from "./CommonStore.ts";
import ModalStore from "./ModalStore.ts";
import ProfileStore from "./ProfileStore.ts";

interface Store {
    postStore: PostStore
    userStore: UserStore
    commonStore: CommonStore
    modalStore: ModalStore
    profileStore: ProfileStore

}

export const store: Store = {
    postStore: new PostStore(),
    userStore: new UserStore(),
    commonStore: new CommonStore(),
    modalStore: new ModalStore(),
    profileStore: new ProfileStore()
    
}

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}