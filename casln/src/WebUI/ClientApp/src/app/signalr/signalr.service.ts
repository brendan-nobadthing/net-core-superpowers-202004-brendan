import { Injectable } from '@angular/core';
import * as signalR from "@aspnet/signalr";
import { UpdateTodoItemCommand } from '../casln-api';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {

  constructor() { }

  public itemChanged$: BehaviorSubject<UpdateTodoItemCommand> = new BehaviorSubject<UpdateTodoItemCommand>(null);

  private hubConnection: signalR.HubConnection;

  startConnection() {
    this.hubConnection = new signalR.HubConnectionBuilder()
                            .withUrl('https://localhost:5001/TodoHub')
                            .build();
    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err));

      this.hubConnection.on('UpdateItem', (data) => {
        this.itemChanged$.next(data);
      });
  }

}
