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
            const user = await agent.Account.login(values);
            store.commonStore.setToken(user.token);
            runInAction(() => {
                router.navigate('/posts')
                this.user = user;
                store.modalStore.closeModal();
            });
    
    };
    register = async (values: UserFormValues): Promise<void> => {
        try {
            await agent.Account.register(values); 
            const user = await agent.Account.login(values);
            runInAction(() => {
                this.user = user;
                store.commonStore.setToken(user.token);
                router.navigate('/posts');
            });
            store.modalStore.closeModal();
        } catch (error) {
            throw error;
        }
    };
    logout = () => {
        store.commonStore.setToken(null);
        this.user = null;
        router.navigate('/').then(r => console.log(r));
    }
    
    getUser = async () => {
        this.loadingInitial = true;
        try {
            const user = await agent.Account.current();
            runInAction(() => {
                this.user = user;
            })
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => {
                this.loadingInitial = false;
            })
        }
    }
    
        
}
