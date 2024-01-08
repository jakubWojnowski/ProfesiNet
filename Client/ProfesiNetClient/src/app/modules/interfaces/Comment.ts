export interface ChatComment{
    id: string;
    creatorId: string;
    creatorName: string;
    creatorSurname: string;
    creatorProfilePicture: string;
    postId: string;
    content: string;
    publishedAt: string;
}

export interface CreateComment{
    postId: string;
    content: string;
}