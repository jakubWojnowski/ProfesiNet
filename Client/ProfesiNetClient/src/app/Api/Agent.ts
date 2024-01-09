import axios, {AxiosError, AxiosResponse} from 'axios';
import {Post} from "../modules/interfaces/Post.ts";
import {CreatePost} from "../modules/interfaces/CreatePost.ts";
import {UpdatePost} from "../modules/interfaces/UpdatePost.ts";
import {
    CreateUserCertificateCommand,
    CreateUserEducationCommand,
    CreateUserExperienceCommand,
    CreateUserSkillCommand, DeleteUserCertificateCommand,
    DeleteUserEducationCommand,
    DeleteUserExperienceCommand, DeleteUserSkillCommand,
    UpdateUserAddressCommand,
    UpdateUserBioCommand, UpdateUserCertificateCommand,
    UpdateUserEducationCommand,
    UpdateUserExperienceCommand,
    UpdateUserFullNameCommand,
    UpdateUserInformationCommand, UpdateUserSkillCommand,
    User,
    UserCertificate,
    UserEducation,
    UserExperience,
    UserFormValues,
    UserSkill
} from "../modules/interfaces/User.ts";
import {toast} from "react-toastify";
import {router} from "../router/Routes.tsx";
import {store} from "../stores/Store.ts";
import {Profile} from "../modules/interfaces/Profile.ts";

const sleep = (delay: number) => {
    return new Promise((resolve) => {
        setTimeout(resolve, delay)

    })
};
axios.defaults.baseURL = 'http://localhost:5000';
axios.interceptors.response.use(async response => {
    await sleep(500);
    return response;


}, (error: AxiosError) => {

    const {data, status,config} = error.response as AxiosResponse;
    switch (status) {
        case 400:
            if(config.method === 'get' && Object.prototype.hasOwnProperty.call(data.errors, 'id')) {
                router.navigate('/not-found').then(r => console.log(r));
            }
            if (data && data.response && data.response.errors) {
                const errorMessages = data.response.errors.map((e: { message: string }) => e.message).join(', ');
                toast.error(`Bad request: ${errorMessages}`);
            } else {
                toast.error('Bad request with no error details.');
            }
            break;
        case 401:
            toast.error('unauthorized');
            break;
        case 404:
            router.navigate('/not-found').then(r => console.log(r));
            break;
        case 500:
            store.commonStore.setServerError(data);
            router.navigate('/server-error').then(r => console.log(r));
            break;
    }
    return Promise.reject(error);

});


const responseBody = <T>(response: AxiosResponse<T>) => response.data;


axios.interceptors.request.use((config) => {
    const token = store.commonStore.token;
    if (token) {
        config.headers['Authorization'] = `Bearer ${token}`;
    }
    return config;
}, error => {
    return Promise.reject(error);
});

const requests = {
    get: <T>(url: string) => axios.get<T>(url).then(responseBody),
    post: <T>(url: string, body: {}) => axios.post<T>(url, body).then(responseBody),
    put: <T>(url: string, body: {}) => axios.put<T>(url, body).then(responseBody),
    del: <T>(url: string, body: {}) => axios.delete<T>(url, body).then(responseBody),
    patch: <T>(url: string, body: {}) => axios.patch<T>(url, body).then(responseBody)
};

const Posts = {
    list: async () => requests.get<Post[]>('/posts-module/Post/getAll'),
    details: async (id: string): Promise<Post> => requests.get(`/posts-module/Post/GetById/${id}`),
    getAllByCreator: (creatorId: string) => requests.get<Post[]>(`/posts-module/Post/GetAllPerCreator/${creatorId}`),
    getAllOwn: () => requests.get('/posts-module/Post/getAllOwn'),
    create: async (createPost: CreatePost) => {
        const formData = new FormData();
        if (File && createPost.file) {
            formData.append('File', createPost.file);
        }
        formData.append('Description', createPost.description);
        return axios.post('/posts-module/Post', formData, {
            headers: {'Content-Type': 'multipart/form-data'},
        }).then(response => {
            if (response.headers && response.headers.location) {

                const locationUrl = response.headers.location;
                return locationUrl.substring(locationUrl.lastIndexOf('/') + 1);
            } else {
                throw new Error('Location header is missing');
            }
        }).catch(error => {
            console.error('Error:', error);
            throw error;
        });
    },
    update: async (UpdatePost: UpdatePost) => {
        const formData = new FormData();
        formData.append('Id', UpdatePost.id);
        if (UpdatePost.file) {
            formData.append('File', UpdatePost.file);
        }
        formData.append('Description', UpdatePost.description);
        return axios.put('/posts-module/Post', formData, {
            headers: {'Content-Type': 'multipart/form-data'}
        }).then(response => {
            // Check if the Location header is present
            if (response.headers && response.headers.location) {
                // Extract the ID from the Location header

                const locationUrl = response.headers.location;
                return locationUrl.substring(locationUrl.lastIndexOf('/') + 1); // This is the ID you're after
            } else {
                throw new Error('Location header is missing');
            }
        }).catch(error => {
            console.error('Error:', error);
            throw error; // Make sure to rethrow the error so you can handle it elsewhere if needed
        });
    },
    delete: (id: string) => axios.delete('/posts-module/Post', {data: {postId: id}}),
    like: (postId: string) => requests.post<number>('/posts-module/Post/PostLike', {postId:postId}),
    unlike: (id: string) => requests.del('/posts-module/Post/PostLike', {id:id}),
    getLikes: (id: string) => requests.get(`/posts-module/Post/PostLike/${id}`),
    getNumberOfLikes: (id: string) => requests.get(`/posts-module/Post/PostLikes/${id}`),
    share: (postId: string) => requests.post('/posts-module/Post/Share', {postId:postId}),
    unShare: (id: string) => requests.del('/posts-module/Post/Share', {id:id}),
    getShare: (id: string) => requests.get(`/posts-module/Post/Share/${id}`),
    getSharesPerPost: (id: string) => requests.get(`/posts-module/Post/SharesPerPost/${id}`),
    getSharesPerUser: (userId: string) => requests.get(`/posts-module/Post/SharesPerUser/${userId}`),
};
const Account = {
    login: (user: UserFormValues) => requests.post<User>('/users-module/UserAuthentication/login', user),
    register: (user: UserFormValues) => requests.post<User>('/users-module/UserAuthentication/register', user),
    current: () => requests.get<User>('/users-module/AccountProfile/GetOwnProfile'),
};
const Profiles = {

    //gets
    getProfile: () => requests.get<User>('/users-module/AccountProfile/GetOwnProfile'),
    getUserProfileById: (userId: string) => requests.get<Profile>(`/users-module/AccountProfile/GetUserProfileById/${userId}`),
    getUserById: (userId: string) => requests.get<User>(`/users-module/GetUserById/${userId}`),
    getAllUsers: () => requests.get<User[]>('/users-module/GetAllUsers'),
    getUserExperienceById: (id: string) => requests.get<UserExperience>(`/users-module/GetUserExperienceById/${id}`),
    getAllUserExperience: (userId: string) => requests.get<UserExperience[]>(`/users-module/GetAllUserExperience/${userId}`),
    getUserEducationById: (id: string) => requests.get<UserEducation>(`/users-module/GetUserEducationById/${id}`),
    getAllUserEducation: (userId: string) => requests.get<UserEducation[]>(`/users-module/GetAllUserEducation/${userId}`),
    getUserSkillById: (id: string) => requests.get<UserSkill>(`/users-module/GetSkillById/${id}`),
    getAllUserSkills: (userId: string) => requests.get<UserSkill[]>(`/users-module/GetAllUserSkills/${userId}`),
    getUserCertificateById: (id: string) => requests.get<UserCertificate>(`/users-module/GetUserCertificateById/${id}`),
    getAllUserCertificates: (userId: string) => requests.get<UserCertificate[]>(`/users-module/GetAllUserCertificates/${userId}`),

    // Account management
    updateUserFullName: (command: UpdateUserFullNameCommand) => requests.patch<{}>('/users-module/AccountProfile/UpdateUserFullName', command),
    updateUserAddress: (command: UpdateUserAddressCommand) => requests.patch<{}>('/users-module/AccountProfile/UpdateUserAddress', command),
    updateUserBio: (command: UpdateUserBioCommand) => requests.patch<{}>('/users-module/AccountProfile/UpdateUserBio', command),
    updateUserInformation: (command: UpdateUserInformationCommand) => requests.patch<{}>('/users-module/AccountProfile/UpdateUserInformation', command),


    // Education management
    addUserEducation: (command: CreateUserEducationCommand) => requests.post<string>('/users-module/AccountProfile/CreateUserEducation', command),
    deleteUserEducation: (command: DeleteUserEducationCommand) => requests.del<{}>('/users-module/AccountProfile/DeleteUserEducation', { data: command }),
    updateUserEducation: (command: UpdateUserEducationCommand) => requests.put<string>('/users-module/AccountProfile/UpdateUserEducation', command),

    // Experience management
    addUserExperience: (command: CreateUserExperienceCommand) => requests.post<string>('/users-module/AccountProfile/CreateUserExperience', command),
    deleteUserExperience: (command: DeleteUserExperienceCommand) => requests.del<{}>('/users-module/AccountProfile/DeleteUserExperience', { data: command }),
    updateUserExperience: (command: UpdateUserExperienceCommand) => requests.put<string>('/users-module/AccountProfile/UpdateUserExperience', command),

    // Skills management
    addUserSkills: (command: CreateUserSkillCommand) => requests.post<string[]>('/users-module/AccountProfile/CreateUserSkill', command),
    deleteUserSkill: (command: DeleteUserSkillCommand) => requests.del<{}>('/users-module/AccountProfile/DeleteUserSkill', { data: command }),
    updateUserSkill: (command: UpdateUserSkillCommand) => requests.put<{}>('/users-module/AccountProfile/UpdateUserSkill', command),

    // Certificate management
    addUserCertificate: (command: CreateUserCertificateCommand) => requests.post<{}>('/users-module/AccountProfile/CreateUserCertificate', command),
    updateUserCertificate: (command: UpdateUserCertificateCommand) => requests.post<{}>('/users-module/AccountProfile/UpdateUserCertificate', command),
    deleteUserCertificate: (command: DeleteUserCertificateCommand) => requests.del<{}>('/users-module/AccountProfile/DeleteUserCertificate', { data: command }),

    // Connection management
    updateUserFollowings: (targetId: string) => requests.patch('/users-module/AccountProfile/UpdateUserFollowings', {targetId:targetId}),
    removeUserFollowing: (targetId: string) => requests.patch('/users-module/AccountProfile/RemoveUserFollowing', {targetId:targetId}),
    updateUserConnectionInvitations: (targetId: string) => requests.patch<{}>('/users-module/AccountProfile/UpdateUserConnectionInvitations', targetId),
    getAllUserFollowings: (userId: string) => requests.get<Profile[]>(`/users-module/AccountProfile/GetAllUserFollowings/${userId}`),
    getAllUserFollowers: (userId: string) => requests.get<Profile[]>(`/users-module/AccountProfile/GetAllUserFollowers/${userId}`),
 

    // Profile picture management
    addUserProfilePicture: async (file: Blob): Promise<string> => {
        let formData: FormData = new FormData();
        formData.append('File', file);

        try {
            const response = await axios.post('/users-module/AccountProfile/AddUserProfilePicture', formData, {
                headers: {'Content-Type': 'multipart/form-data'},
            });
            // Assuming the URL is returned directly in the response
            const photoUrl = response.data;
            console.log('Uploaded image URL:', photoUrl);
            return photoUrl; // This will now return the URL to the caller
        } catch (error) {
            console.error('Error:', error);
            throw error;
        }
    },
    deleteUserProfilePicture: (photoId: string) => {
        return requests.del(`/users-module/AccountProfile/DeleteUserProfilePicture`, {
            data: { photoId: photoId }
        });
    }
};
const agent = {
    Posts,
    Account,
    Profiles
};

export default agent;