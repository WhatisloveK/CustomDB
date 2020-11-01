import { Component, OnInit, ViewChild } from '@angular/core';
import {grpc} from '@improbable-eng/grpc-web';
import {EntityService,DBService} from './generated/dbEngine_pb_service';
import {SelectRequest,SelectReply, GetTableListReply, GetTableListRequest} from "./generated/dbEngine_pb"
import { MatTableComponent } from './mat-table/mat-table.component';
import { environment } from 'src/environments/environment';

import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{

  constructor(private _snackBar: MatSnackBar) { }
  
  title = 'dynamic-mat-table';

  tables:string[];
  tableName:string;
  dbName:string;

  @ViewChild(MatTableComponent) tableComponent:MatTableComponent;

  ngOnInit(): void {
    this.tables = [];
    this.tableName="";
    this.dbName="";
  }

  selectTable(table:string){
    this.tableName = table;
    this.tableComponent.tableName = table;
    this.tableComponent.ngOnInit();
  }

  connectToDb(value){
    this.dbName = value;
    this.tableComponent.dbName = value;
    var tablesRequest = new GetTableListRequest();
    tablesRequest.setDbname(this.dbName);
    grpc.unary(DBService.GetTableList, {
      request: tablesRequest,
      host: environment.host, 
      onEnd: res => {
        const { status, message } = res;
        if (status === grpc.Code.OK && message) {
          var result = message.toObject() as GetTableListReply.AsObject;
          this.tables = result.tablesList;
          this.openSnackBar(true, "You have successfully connected to database!");
        }
      }
    });
    this.tableComponent.clearGrid();
  }

  openSnackBar(success: boolean, message: string) {
    let action = "Ok";
    if(success){
      message = message;
      action = "Great"
    }
    this._snackBar.open(message, action, {
      duration: 10000,
    });
  }
}