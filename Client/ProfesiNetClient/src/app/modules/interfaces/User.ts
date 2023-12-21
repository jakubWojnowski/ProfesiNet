export interface User {
    name: string;
    surname: string;
    token: string;
    address: string;
    bio: string;
    ProfilePicture: string;
}

export interface UserFormValues {
    email: string;
    password: string;
    confirmPassword?: string;
    name?: string;
    surname?: string;
}
