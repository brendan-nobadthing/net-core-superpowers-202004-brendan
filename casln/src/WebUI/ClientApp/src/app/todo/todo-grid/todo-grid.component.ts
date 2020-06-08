import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { TodoGridService, TodoGridData } from '../todo-grid.service';
import { Observable } from 'rxjs';
import { DxDataGridComponent } from 'devextreme-angular';
import DataSource from 'devextreme/data/data_source';
import { LoadOptions } from 'devextreme/data/load_options';
import CustomStore from 'devextreme/data/custom_store';
import { TodoListsClient } from 'src/app/casln-api';

@Component({
  selector: 'app-todo-grid',
  templateUrl: './todo-grid.component.html',
  styleUrls: ['./todo-grid.component.css']
})
export class TodoGridComponent implements OnInit {


  dataSource: any;

  constructor(
    private todoClient: TodoListsClient
    ) {}

  ngOnInit(): void {
    this.dataSource = new CustomStore({
      key: 'itemId',
      load: (loadOptions: LoadOptions) => {
        console.log('calling Load... ', loadOptions);
        console.log('got service:', this.todoClient);
        return this.todoClient.getGrid(loadOptions.skip / loadOptions.take, loadOptions.take)
          .toPromise()
          .then((data: any) => {
            console.log('got data: ', data);
            return {
                data: data,
                totalCount: 100,
            };
          });
        }
    });
  }



}
