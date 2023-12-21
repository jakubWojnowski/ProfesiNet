import {makeAutoObservable} from "mobx";

export default class CommonStore {
    token: string | null = null;
    appLoaded: boolean = false;
    
    constructor() {
        makeAutoObservable(this);
    }
    setToken = (token: string | null) => {
        if (token) {
            localStorage.setItem('token', token);
            this.token = token;
        }
    }
    setAppLoaded = () => {
        this.appLoaded = true;
    }
}
    