export interface User {
    id: string;
    name: string;
    surname: string;
    token: string;
    address: string;
    bio: string;
    profilePicture: string;
}

export interface UserFormValues {
    email: string;
    name?: string;
    surname?: string;
    password: string;
    confirmPassword?: string;
   
}
