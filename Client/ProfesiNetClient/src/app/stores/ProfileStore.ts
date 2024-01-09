import {Profile} from "../modules/interfaces/Profile.ts";
import {makeAutoObservable, reaction, runInAction} from "mobx";
import agent from "../Api/Agent.ts";
import {store} from "./Store.ts";
import {
    CreateUserEducationCommand,
    CreateUserExperienceCommand,
    CreateUserSkillCommand,
    DeleteUserEducationCommand,
    DeleteUserExperienceCommand,
    DeleteUserSkillCommand,
    UpdateUserBioCommand,
    UpdateUserEducationCommand,
    UpdateUserExperienceCommand,
    UpdateUserInformationCommand,
    UserEducation,
    UserExperience, UserSkill
} from "../modules/interfaces/User.ts";

export default class ProfileStore {
    profile: Profile | null = null;
    loadingProfile = false;
    uploading = false;
    loading = false;
    loadingFollowings = false;
    followings: Profile[] = [];
    followers: Profile[] = [];
    selectedExperience: UserExperience | undefined = undefined;
    selectedEducation: UserEducation | undefined = undefined;
    selectedSkill: UserSkill | undefined = undefined;
    activeTab: number = 0;


    constructor() {
        makeAutoObservable(this);
        reaction(
            () => this.activeTab,
        activeTab =>{
            if(activeTab === 0){
                this.loadFollowers(this.profile!.id).then(r => console.log(r));
            }
            else if(activeTab === 1){
                this.loadFollowings(this.profile!.id).then(r => console.log(r));
            }else {
                this.followings = [];
                this.followers = [];
            }
        }
        )

    };

    get isCurrentUser() {
        if (store.userStore.user && this.profile) {
            return store.userStore.user.id === this.profile.id;
        }
        return false;
    };

    selectExperience = (experienceId: string) => {
        this.selectedExperience = this.profile?.experiences.find(e => e.id === experienceId);
    };
    selectEducation = (educationId: string) => {
        this.selectedEducation = this.profile?.educations.find(e => e.id === educationId);
    };
    selectSkill = (skillId: string) => {
        this.selectedSkill = this.profile?.skills.find(s => s.id === skillId);
    };

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
    };
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
    };

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
    };
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
    };

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
    };

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
    };

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
    };

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
    };

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
    };

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
    };


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
    };
    addSkills = async (command: CreateUserSkillCommand) => {
        this.loading = true;
        try {
            const ids = await agent.Profiles.addUserSkills(command);
            runInAction(() => {
                if (this.profile) {
                    // Map each skill name to an ID returned from the API call
                    const skillsWithIds = command.names.map((name, index) => ({
                        id: ids[index],
                        name: name,
                        userId: this.profile!.id
                    }));


                    this.profile.skills.push(...skillsWithIds);
                }
                this.loading = false;
            });
        } catch (error) {
            console.log(error);
            runInAction(() => {
                this.loading = false;
            });
        }
    };
    deleteSkill = async (id: string): Promise<void> => {
        this.loading = true;
        console.log(id);
        try {
            let command: DeleteUserSkillCommand = {id: id};
            await agent.Profiles.deleteUserSkill(command);
            runInAction(() => {
                if (this.profile) {
                    this.profile.skills = this.profile.skills.filter(skill => skill.id !== id);
                }
                this.loading = false;
            });
        } catch (error) {
            console.log(error);
            runInAction(() => {
                this.loading = false;
            });
        }
    };
    addFollowing = async (ProfileId: string, following: boolean) => {
        this.loading = true;
        try {
            await agent.Profiles.updateUserFollowings(ProfileId);
            runInAction(() => {
                if (this.profile && this.profile.id !== store.userStore.user?.id && this.profile.id === ProfileId) {
                    this.profile.followersCount++;
                    this.profile.following = following;
                }
                this.loading = false;
            })
        } catch (error) {
            console.log(error);
            runInAction(() => {
                this.loading = false;
            })
        }
    }
    unfollowUser = async (ProfileId: string, following: boolean) => {
        this.loading = true;
        try {
            await agent.Profiles.removeUserFollowing(ProfileId);
            runInAction(() => {
                if (this.profile && this.profile.id !== store.userStore.user?.id && this.profile.id === ProfileId) {
                    this.profile.followersCount--;
                    this.profile.following = following;
                }
                this.loading = false;
            })
        } catch (error) {
            console.log(error);
            runInAction(() => {
                this.loading = false;
            })
        }


    }

    loadFollowings = async (userId: string) => {
        this.loadingFollowings = true;
        try {
            const profiles = await agent.Profiles.getAllUserFollowings(userId);
            runInAction(() => {
                this.followings = profiles;
                this.loadingFollowings = false;
            })
        } catch (error) {
            console.log(error);
            runInAction(() => {
                this.loadingFollowings = false;
            })
        }
    }

    loadFollowers = async (userId: string) => {
        this.loadingFollowings = true;
        try {
            const profiles = await agent.Profiles.getAllUserFollowers(userId);
            runInAction(() => {
                this.followings = profiles;
                this.loadingFollowings = false;
            })
        } catch (error) {
            console.log(error);
            runInAction(() => {
                this.loadingFollowings = false;
            })
        }
    }

    clearProfile = () => {
        this.profile = null;
    }
    setActiveTab = (activeTab: any) => {
        this.activeTab = activeTab;
    }

}
    
    