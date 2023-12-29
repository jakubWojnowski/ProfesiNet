import PostStore from "./PostStore.ts";
import {createContext, useContext} from "react";
import UserStore from "./UserStore.ts";
import CommonStore from "./CommonStore.ts";
import ModalStore from "./ModalStore.ts";

interface Store {
    postStore: PostStore
    userStore: UserStore
    commonStore: CommonStore
    modalStore: ModalStore

}

export const store: Store = {
    postStore: new PostStore(),
    userStore: new UserStore(),
    commonStore: new CommonStore(),
    modalStore: new ModalStore()
    
}

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}