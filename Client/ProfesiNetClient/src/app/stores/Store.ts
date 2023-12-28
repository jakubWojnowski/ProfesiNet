import PostStore from "./PostStore.ts";
import {createContext, useContext} from "react";
import UserStore from "./UserStore.ts";
import CommonStore from "./CommonStore.ts";

interface Store {
    postStore: PostStore
    userStore: UserStore
    commonStore: CommonStore

}

export const store: Store = {
    postStore: new PostStore(),
    userStore: new UserStore(),
    commonStore: new CommonStore()
    
}

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}