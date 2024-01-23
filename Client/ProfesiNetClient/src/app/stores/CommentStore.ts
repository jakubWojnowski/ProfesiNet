import {ChatComment} from "../modules/interfaces/Comment.ts";
import {HubConnection, HubConnectionBuilder, LogLevel} from "@microsoft/signalr";
import {makeAutoObservable, runInAction} from "mobx";

export default class CommentStore {
    comments:ChatComment[] = [];
    hubConnection: HubConnection | null = null;
    
    constructor() {
        makeAutoObservable(this);
    }
    
    
    
    setHubConnection = (postId:string) => {
        console.log('postId', postId);
        if (postId !=null) {
            this.hubConnection = new HubConnectionBuilder()
                .withUrl('http://localhost:5000/chat?postId=' + postId, {
                    accessTokenFactory: () => localStorage.getItem('token') as string
                })
                .withAutomaticReconnect()
                .configureLogging(LogLevel.Information)
                .build();
            console.log('this.hubConnection', this.hubConnection.baseUrl);
            this.hubConnection.start().catch(error => console.log('Error establishing the connection: ', error));

            this.hubConnection.on('LoadComments', (comments: ChatComment[]) => {
                runInAction(() => this.comments = comments);

            })
            this.hubConnection.on('n', (comment: ChatComment) => {
                runInAction(() =>   this.comments.unshift(comment)
                );
            })
        }
    }
    
    stopHubConnection = () => {
        this.hubConnection?.stop().catch(error => console.log('Error stopping connection: ', error));
    }
    
    clearComments = () => {
        this.comments = [];
        this.stopHubConnection();
    }

    addComment = async (values: { body: string }, postId: string) => {
        try {
        let comment =
            {
                postId: postId,
                content: values.body,
            }
            console.log('comment', comment);
    
            await this.hubConnection?.invoke('SendComment', comment);
        } catch (error) {
            console.log(error);
        }
    }
    

}