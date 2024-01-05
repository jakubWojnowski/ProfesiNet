import {User, UserCertificate, UserEducation, UserExperience, UserSkill} from "./User.ts";

export interface IProfile{
    id: string;
    name: string;
    surname: string;
    title: string;
    address: string;
    bio: string;
    profilePicture: string;
    followersCount: number;
    followingsCount: number;
    networkConnectionsCount: number;
    following: boolean;
    educations: UserEducation[];
    experiences: UserExperience[];
    skills: UserSkill[];
    certificates: UserCertificate[];
}
export class Profile implements IProfile{
    constructor(user:User) {
        this.id = user.id;
        this.name = user.name;
        this.surname = user.surname;
        this.bio = user.bio;
        this.title = user.title;
        this.address = user.address;
        this.profilePicture = user.profilePicture;
        this.followersCount = user.followersCount;
        this.followingsCount = user.followingCount;
        this.networkConnectionsCount = user.networkConnectionsCount;
        this.following = false;
        this.educations = user.educations;
        this.experiences = user.experiences;
        this.skills = user.skills;
        this.certificates = user.certificates;
    }
    id: string;
    name: string;
    surname: string;
    title: string;
    bio: string;
    address: string;
    profilePicture: string;
    followersCount: number;
    followingsCount: number;
    networkConnectionsCount: number;
    following: boolean;
    educations: UserEducation[];
    experiences: UserExperience[];
    skills: UserSkill[];
    certificates: UserCertificate[];
    
}

