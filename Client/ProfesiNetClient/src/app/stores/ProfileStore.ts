import {Profile} from "../modules/interfaces/Profile.ts";
import {makeAutoObservable, runInAction} from "mobx";
import agent from "../Api/Agent.ts";

export default class ProfileStore {
    profile: Profile | null = null;
    loadingProfile = false;

    constructor() {
     makeAutoObservable(this);

    }
    
    loadProfile = async (id: string) => {
        this.loadingProfile = true;
        try {
            const profile = await agent.Profiles.getUserProfileById(id);
            runInAction(() => {
                this.profile = profile;
                this.loadingProfile = false;
            })
        } catch (error) {
            console.log(error);
            runInAction(() => {
                this.loadingProfile = false;
            })
        }
    }
    

}
    
    