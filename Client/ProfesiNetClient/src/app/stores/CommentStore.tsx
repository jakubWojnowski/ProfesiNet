import {ChatComment} from "../modules/interfaces/Comment.ts";
import {HubConnection, HubConnectionBuilder, LogLevel} from "@microsoft/signalr";
import {makeAutoObservable, runInAction} from "mobx";
import {store} from "./Store.ts";
import Agent from "../Api/Agent.ts";

export default class CommentStore {
    comments:ChatComment[] = [];
    hubConnection: HubConnection | null = null;
    
    constructor() {
        makeAutoObservable(this);
    }
    
    
    
    setHubConnection = (postId:string) => {
        if (store.postStore.selectedPost) {
            console.log('postId', postId);
            this.hubConnection = new HubConnectionBuilder()
                .withUrl('https://localhost:5000/chat?postId=' + postId, {
                    accessTokenFactory: () => store.userStore.user?.token as string
                })
                .withAutomaticReconnect()
                .configureLogging(LogLevel.Information)
                .build();

            this.hubConnection.start().catch(error => console.log('Error establishing the connection: ', error));
            
            this.hubConnection.on('LoadComments', (comments: ChatComment[]) => {
             runInAction(() => this.comments= comments);
                 
            })
            this.hubConnection.on('ReceiveComment', (comment: ChatComment) => {
                runInAction(() => this.comments.push(comment));
                
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
    

}