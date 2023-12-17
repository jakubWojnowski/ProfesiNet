import axios, {AxiosResponse} from 'axios';
import {Post} from "../modules/interfaces/Post.ts";
import {CreatePost} from "../modules/interfaces/CreatePost.ts";
import {UpdatePost} from "../modules/interfaces/UpdatePost.ts";

const sleep = (delay: number) => {
    return new Promise((resolve) => {
        setTimeout(resolve, delay)
    
    })
};
axios.defaults.baseURL = 'https://localhost:5000';
axios.interceptors.response.use(async response => {
    try {
        await sleep(1000);
        return response;
    } catch (error) {
        console.log(error);
        return await Promise.reject(error);
    }
});

// Function to extract the token from local storage
const getAuthToken = () => localStorage.getItem('token');

// Function to handle the response and return the data
const responseBody = <T> (response: AxiosResponse<T>) => response.data;

// Setting up the requests to include the token in the header
axios.interceptors.request.use((config) => {
    const token = getAuthToken();
    if (token) {
        config.headers['Authorization'] = `Bearer ${token}`;
    }
    return config;
}, error => {
    return Promise.reject(error);
});

const requests = {
    get: <T> (url: string) => axios.get<T>(url).then(responseBody),
    post:<T> (url: string, body: {}) => axios.post<T>(url, body).then(responseBody),
    put:<T> (url: string, body: {}) => axios.put<T>(url, body).then(responseBody),
    del:<T> (url: string, body: {}) => axios.delete<T>(url, body).then(responseBody),
    patch:<T> (url: string, body: {}) => axios.patch<T>(url, body).then(responseBody)
};

const Posts = {
    list: () => requests.get<Post[]>('/posts-module/Post/getAll'),
    details: (id: string) => requests.get(`/posts-module/Post/GetById/${id}`),
    getAllByCreator: (creatorId: string) => requests.get(`/posts-module/Post/GetAllPerCreator/${creatorId}`),
    getAllOwn: () => requests.get('/posts-module/Post/getAllOwn'),
    create: ( CreatePost:CreatePost) => {
        const formData = new FormData();
        if (File) {
            formData.append('File', CreatePost.file);
        }
        formData.append('Description', CreatePost.description);
        return axios.post('/posts-module/Post', formData, {
            headers: { 'Content-Type': 'multipart/form-data' },
        });
    },
    update: (UpdatePost:UpdatePost) => {
        const formData = new FormData();
        formData.append('Id', UpdatePost.id);
        if (UpdatePost.file) {
            formData.append('File', UpdatePost.file);
        }
        formData.append('Description', UpdatePost.description);
        return axios.put('/posts-module/Post', formData, {
            headers: { 'Content-Type': 'multipart/form-data' }
        });
    },
    delete: (id: string) => axios.delete('/posts-module/Post', { data: { postId:id } }),
    like: (postId: string, userId: string) => requests.post('/posts-module/Post/PostLike', { postId, id: userId }),
    unlike: (id: string) => requests.del('/posts-module/Post/PostLike', { id }),
    getLikes: (id: string) => requests.get(`/posts-module/Post/PostLike/${id}`),
    getNumberOfLikes: (id: string) => requests.get(`/posts-module/Post/PostLikes/${id}`),
    share: (postId: string, userId: string) => requests.post('/posts-module/Post/Share', { postId, id: userId }),
    unShare: (id: string) => requests.del('/posts-module/Post/Share', { id }),
    getShare: (id: string) => requests.get(`/posts-module/Post/Share/${id}`),
    getSharesPerPost: (id: string) => requests.get(`/posts-module/Post/SharesPerPost/${id}`),
    getSharesPerUser: (userId: string) => requests.get(`/posts-module/Post/SharesPerUser/${userId}`),
};

const agent = {
    Posts
};

export default agent;