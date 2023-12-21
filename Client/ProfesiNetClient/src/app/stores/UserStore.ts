import {User, UserFormValues} from "../modules/interfaces/User.ts";
import {makeAutoObservable, runInAction} from "mobx";
import agent from "../Api/Agent.ts";
import {store} from "./Store.ts";
import {router} from "../router/Routes.tsx";

export default class UserStore {
    user: User | null = null;
    loading: boolean = false;
    loadingInitial: boolean = false;
    constructor() {
        makeAutoObservable(this);
    }
    get isLoggedIn() {
        return !!this.user;
    }
    login = async (values: UserFormValues) => {
            const token = await agent.Account.login(values);
            store.commonStore.setToken(token);
            runInAction(() => {
                router.navigate('/posts')
            });
    
    };
    logout = () => {
        store.commonStore.setToken(null);
        window.localStorage.removeItem('jwt');
        this.user = null;
        router.navigate('/')
    }
    
        
}
