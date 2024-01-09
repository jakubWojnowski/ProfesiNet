import {User, UserCertificate, UserEducation, UserExperience, UserSkill} from "./User.ts";

export interface IProfile{
    id: string;
    name: string;
    surname: string;
    title: string;
    address: string;
    bio: string;
    profilePicture: string | null;
    profilePictureId: string | null;
    followersCount: number;
    followingsCount: number;
    following: boolean;
    followedBy: boolean;
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
        this.profilePictureId = user.profilePictureId;
        this.followersCount = user.followersCount;
        this.followingsCount = user.followingCount;
        this.following = false;
        this.followedBy = false;
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
    profilePicture: string | null;
    profilePictureId: string | null;
    followersCount: number;
    followingsCount: number;
    following: boolean;
    followedBy: boolean;
    educations: UserEducation[];
    experiences: UserExperience[];
    skills: UserSkill[];
    certificates: UserCertificate[];
    
}

