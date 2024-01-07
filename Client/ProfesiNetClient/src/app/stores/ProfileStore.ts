import {Profile} from "../modules/interfaces/Profile.ts";
import {makeAutoObservable, runInAction} from "mobx";
import agent from "../Api/Agent.ts";
import {store} from "./Store.ts";
import {
    CreateUserEducationCommand,
    CreateUserExperienceCommand, CreateUserSkillCommand, DeleteUserEducationCommand, DeleteUserExperienceCommand,
    UpdateUserBioCommand, UpdateUserEducationCommand, UpdateUserExperienceCommand,
    UpdateUserInformationCommand, UserEducation, UserExperience
} from "../modules/interfaces/User.ts";

export default class ProfileStore {
    profile: Profile | null = null;
    loadingProfile = false;
    uploading = false;
    loading = false;
    experienceRegistry = new Map<string, UserExperience>();
    selectedExperience: UserExperience | undefined = undefined;
    selectedEducation: UserEducation | undefined = undefined;
    educationRegistry = new Map<string, Profile>();
    skillRegistry = new Map<string, Profile>();


    constructor() {
        makeAutoObservable(this);

    }

    get isCurrentUser() {
        if (store.userStore.user && this.profile) {
            return store.userStore.user.id === this.profile.id;
        }
        return false;
    }

    selectExperience = (experienceId: string) => {
        this.selectedExperience = this.profile?.experiences.find(e => e.id === experienceId);
    }
    selectEducation = (educationId: string) => {
        this.selectedEducation = this.profile?.educations.find(e => e.id === educationId);
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
    updateProfileInformation = async (command: UpdateUserInformationCommand) => {
        try {
            await agent.Profiles.updateUserInformation(command);
            runInAction(() => {
                if (this.profile) {
                    this.profile = {...this.profile, ...command}
                }
            })
        } catch (error) {
            console.log(error);
        }
    }

    updateProfileBio = async (bio: UpdateUserBioCommand) => {
        try {
            await agent.Profiles.updateUserBio(bio);
            runInAction(() => {
                if (this.profile) {
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
                if (this.profile) {
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
                if (this.profile) {
                    this.profile.profilePicture = null;
                    this.profile.profilePictureId = null;
                    store.userStore.setImage(null);
                }
            })
        } catch (error) {
            console.log(error);
        }
    }

    addExperience = async (experience: CreateUserExperienceCommand) => {
        try {
            const id = await agent.Profiles.addUserExperience(experience);
            runInAction(() => {
                if (this.profile) {
                    this.profile.experiences.push({...experience, id, userId: this.profile.id});
                }
            })
        } catch (error) {
            console.log(error);
        }
    }

    updateExperience = async (experience: UpdateUserExperienceCommand) => {
        this.loading = true;
        try {
            const id = await agent.Profiles.updateUserExperience(experience);
            runInAction(() => {
                if (this.profile) {
                    let updatedExperience = {...experience, id, userId: this.profile.id};
                    this.profile.experiences = [...this.profile.experiences.filter(x => x.id !== id), updatedExperience];
                    this.loading = false;
                }
            })
        } catch (error) {
            console.log(error);
            runInAction(() => {
                this.loading = false;
            })
        }
    }

    deleteExperience = async (command: DeleteUserExperienceCommand) => {
        this.loading = true;
        try {
            await agent.Profiles.deleteUserExperience(command);
            runInAction(() => {
                if (this.profile) {
                    this.profile.experiences = [...this.profile.experiences.filter(x => x.id !== command.id)];
                    this.loading = false;
                }
            })
        } catch (error) {
            console.log(error);
            runInAction(() => {
                this.loading = false;
            })
        }
    }

    addEducation = async (education: CreateUserEducationCommand) => {
        this.loading = true;
        try {
            const id = await agent.Profiles.addUserEducation(education);
            runInAction(() => {
                if (this.profile) {
                    let updatedEducation = {...education, id, userId: this.profile.id};
                    this.profile.educations = [...this.profile.educations.filter(x => x.id !== id), updatedEducation];
                    this.loading = false;
                }
            })
        } catch (error) {
            console.log(error);
            runInAction(() => {
                this.loading = false;
            })
        }
    }
    
    updateEducation = async (education: UpdateUserEducationCommand) => {
        this.loading = true;
        try {
            const id = await agent.Profiles.updateUserEducation(education);
            runInAction(() => {
                if (this.profile) {
                    let updatedEducation = {...education, id, userId: this.profile.id};
                    this.profile.educations = [...this.profile.educations.filter(x => x.id !== id), updatedEducation];
                    this.loading = false;
                }
            })
        } catch (error) {
            console.log(error);
            runInAction(() => {
                this.loading = false;
            })
        
        }
    }


    deleteEducation = async (command: DeleteUserEducationCommand) => {
        try {
            await agent.Profiles.deleteUserEducation(command);
            runInAction(() => {
                if (this.profile) {
                    this.profile.educations = [...this.profile.educations.filter(x => x.id !== command.id)];
                }
            })
        } catch (error) {
            console.log(error);
        }
    }
    addSkill = async (command: CreateUserSkillCommand) => {
        try {
            await agent.Profiles.addUserSkill(command);
            runInAction(() => {
                if (this.profile) {
                    this.profile.skills.push(command);
                }
            })
        } catch (error) {
            console.log(error);
        }
    }

    updateSkill = async (skill: string) => {
        try {
            await agent.Profiles.updateUserSkill(skill);
            runInAction(() => {
                if (this.profile) {
                    this.profile.skills.push(skill);
                }
            })
        } catch (error) {
            console.log(error);
        }
    }
    
    deleteSkill = async (skill: string) => {
        try {
            await agent.Profiles.deleteUserSkill(skill);
            runInAction(() => {
                if (this.profile) {
                    this.profile.skills = [...this.profile.skills.filter(x => x !== skill)];
                }
            })
        } catch (error) {
            console.log(error);
        }
    }
    
}
    
    