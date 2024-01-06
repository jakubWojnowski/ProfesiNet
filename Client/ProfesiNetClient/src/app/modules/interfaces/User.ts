
export interface User {
    id: string;
    name: string;
    surname: string;
    token: string;
    address: string;
    bio: string;
    profilePicture: string;
    title: string;
    experiences: UserExperience[];
    educations: UserEducation[];
    skills: UserSkill[];
    certificates: UserCertificate[];
    followersCount: number;
    followingCount: number;
    networkConnectionsCount: number;
}

export interface UserFormValues {
    email: string;
    name?: string;
    surname?: string;
    password: string;
    confirmPassword?: string;
   
}
export interface UpdateUserInformationCommand {
    name?: string;
    surname?: string;
    title?: string;
    address?: string;
}
export interface UserExperience {
    id: string;
    company: string;
    position: string;
    description: string;
    startDate: Date;
    endDate: Date | null;
    userId: string;
}
export interface UserEducation {
    id: string;
    name: string;
    address: string;
    degree: string;
    fieldOfStudy: string;
    startDate: Date;
    endDate: Date | null;
    userId: string;
}
export interface UserSkill {
    id: string;
    name: string;
    userId: string;
}

export interface UserCertificate {
    id: string;
    name: string;
    description: string;
    date: Date;
    userId: string;
}


export interface UpdateUserFullNameCommand {
    name?: string;
    surname?: string;
}
export interface UpdateUserAddressCommand {
    address?: string;
}

export interface UpdateUserBioCommand {
    bio?: string;
}
export interface CreateUserEducationCommand {
    name: string;
    description: string;
    degree?: string;
    fieldOfStudy?: string;
    startDate: Date;
    endDate?: Date;
}
export interface UpdateUserEducationCommand {
    id: string;
    name: string;
    description: string;
    degree?: string;
    fieldOfStudy?: string;
    startDate: Date;
    endDate?: Date;
}
export interface DeleteUserEducationCommand {
    id: string;
}
export interface CreateUserExperienceCommand {
    company: string;
    position: string;
    description: string;
    startDate: Date;
    endDate?: Date;
}
export interface UpdateUserExperienceCommand {
    id: string;
    company: string;
    position: string;
    description: string;
    startDate: Date;
    endDate?: Date;
}
export interface DeleteUserExperienceCommand {
    id: string;
}

export interface CreateUserSkillCommand {
    names: string[];
}
export interface DeleteUserSkillCommand {
    id: string;
}
export interface UpdateUserSkillCommand {
    id: string;
    name: string;
}
export interface CreateUserCertificateCommand {
    name: string;
    description: string;
    date: Date;
}
export interface UpdateUserCertificateCommand {
    id: string;
    name: string;
    description: string;
    date: Date;
}
export interface DeleteUserCertificateCommand {
    id: string;
}


