import {makeAutoObservable} from "mobx";
import {ServerError} from "../modules/interfaces/ServerError.ts";

export default class CommonStore {
    token: string | null = null;
    appLoaded: boolean = false;
    error: ServerError | null = null;
    
    constructor() {
        makeAutoObservable(this);
    }
    
    setServerError = (error: ServerError) => {
        this.error = error;
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
    