export interface ChatComment{
    id: string;
    creatorId: string;
    creatorName: string;
    creatorSurname: string;
    creatorProfilePicture: string;
    postId: string;
    content: string;
    likesCount: number;
    publishedAt: string;
}

export interface CreateComment{
    postId: string;
    content: string;
}