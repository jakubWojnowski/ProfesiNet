import PostStore from "./PostStore.ts";
import {createContext, useContext} from "react";

interface Store {
    postStore: PostStore

}

export const store: Store = {
    postStore: new PostStore()
}

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}