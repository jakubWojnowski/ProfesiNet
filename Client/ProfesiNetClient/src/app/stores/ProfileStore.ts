import {Profile} from "../modules/interfaces/Profile.ts";
import {makeAutoObservable, runInAction} from "mobx";
import agent from "../Api/Agent.ts";
import {store} from "./Store.ts";
import {
    CreateUserExperienceCommand,
    UpdateUserBioCommand,
    UpdateUserInformationCommand, UserExperience
} from "../modules/interfaces/User.ts";

export default class ProfileStore {
    profile: Profile | null = null;
    loadingProfile = false;
    uploading = false;
    loading = false;
    experienceRegistry = new Map<string, Profile>();
    educationRegistry = new Map<string, Profile>();
    skillRegistry = new Map<string, Profile>();
    

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
    
    updateProfileBio = async (bio:UpdateUserBioCommand) => {
        try {
            await agent.Profiles.updateUserBio(bio);
            runInAction(() => {
                if(this.profile) {
                    this.profile = {...this.profile, ...bio}
                }
            })
        } catch (error) {
            console.log(error);
        }
    }
    uploadProfilePhoto = async (file: Blob) => {
        this.uploading = true;
        try {
            const photo = await agent.Profiles.addUserProfilePicture(file);
            runInAction(() => {
                if(this.profile) {
                    store.userStore.setImage(photo);
                    this.profile.profilePicture = photo;
                }
                this.uploading = false;
            })
        } catch (error) {
            console.log(error);
            runInAction(() => {
                this.uploading = false;
            }
            )
            
        }
    }
    
    deletePhoto = async (photoId: string) => {
        this.loading = true;
        try {
            await agent.Profiles.deleteUserProfilePicture(photoId);
            runInAction(() => {
                if(this.profile) {
                    this.profile.profilePicture = null;
                    this.profile.profilePictureId = null;
                    store.userStore.setImage(null);
                }
            })
        } catch (error) {
            console.log(error);
        }
    }
    
    addExperience = async (experience: any) => {
        try {
            await agent.Profiles.addUserExperience(experience);
            runInAction(() => {
                if(this.profile) {
                    this.profile.experiences.push(experience);
                }
            })
        } catch (error) {
            console.log(error);
        }
    }
    

}
    
    