export interface Post {
    id: string
    creatorId: string
    creatorName: string
    creatorSurname: string
    creatorProfilePicture: string
    imageUrl: string
    description: string
    publishedAt: string
    likesCount: number
    commentsCount: number
    sharesCount: number
    isLiked: boolean
    isShared: boolean
    
    shares: Shares[]
    likes: Likes[]
}

export interface Shares {
    id: string
    creatorId: string
    postId: string
}


export interface Likes {
    id: string
    creatorId: string
    postId: string
}
