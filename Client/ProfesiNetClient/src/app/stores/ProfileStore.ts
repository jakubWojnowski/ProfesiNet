import {Profile} from "../modules/interfaces/Profile.ts";
import {makeAutoObservable, runInAction} from "mobx";
import agent from "../Api/Agent.ts";
import {store} from "./Store.ts";
import {UpdateUserInformationCommand} from "../modules/interfaces/User.ts";

export default class ProfileStore {
    profile: Profile | null = null;
    loadingProfile = false;

    constructor() {
     makeAutoObservable(this);

    }
    
    get isCurrentUser() {
        if(store.userStore.user && this.profile) {
            return store.userStore.user.id === this.profile.id;
        }
        return false;
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
    updateProfileInformation = async (command:UpdateUserInformationCommand) => {
        try {
            await agent.Profiles.updateUserInformation(command);
            runInAction(() => {
                if(this.profile) {
                    this.profile = {...this.profile, ...command}
                }
            })
        } catch (error) {
            console.log(error);
        }
    }
    

}
    
    