import {ChatComment} from "../modules/interfaces/Comment.ts";
import {HubConnection, HubConnectionBuilder, LogLevel} from "@microsoft/signalr";
import {makeAutoObservable, runInAction} from "mobx";
import {store} from "./Store.ts";

export default class CommentStore {
    comments:ChatComment[] = [];
    hubConnection: HubConnection | null = null;
    
    constructor() {
        makeAutoObservable(this);
    }
    
    
    
    setHubConnection = (postId:string) => {
        console.log('postId', postId);
        console.log('this.hubConnection', store.userStore.user?.token!);

      
            this.hubConnection = new HubConnectionBuilder()
                .withUrl('http://localhost:5000/chat?postId=' + postId, {
                    accessTokenFactory: () => localStorage.getItem('token')!
                })
                .withAutomaticReconnect()
                .configureLogging(LogLevel.Information)
                .build();
                console.log('this.hubConnection', this.hubConnection.baseUrl);
            this.hubConnection.start().catch(error => console.log('Error establishing the connection: ', error));
            
            this.hubConnection.on('LoadComments', (comments: ChatComment[]) => {
             runInAction(() => this.comments= comments);
                 
            })
            this.hubConnection.on('ReceiveComment', (comment: ChatComment) => {
                this.comments.unshift(comment);
                
            })
        }

    
    stopHubConnection = () => {
        this.hubConnection?.stop().catch(error => console.log('Error stopping connection: ', error));
    }
    
    clearComments = () => {
        this.comments = [];
        this.stopHubConnection();
    }
    

}