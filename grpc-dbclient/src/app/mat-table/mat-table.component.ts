import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import {grpc} from '@improbable-eng/grpc-web';
import {EntityService,DBService} from '../generated/dbEngine_pb_service';
import {SelectRequest,SelectReply, GetEntityRequest, GetEntityReply} from "../generated/dbEngine_pb"
import { environment } from 'src/environments/environment';
import { HelperService } from '../services/helper.service';

@Component({
  selector: 'app-mat-table',
  templateUrl: './mat-table.component.html',
  styleUrls: ['./mat-table.component.css']
})
export class MatTableComponent implements OnInit {

  tableDataSrc: any;
  // tslint:disable-next-line: no-input-rename
  @Input() tableName: string;
  @Input() dbName: string;
  tableCols: string[];
  tableData: {}[] = [];

  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;


  constructor(private helperService: HelperService) { }

  ngOnInit() {
    let entityRequest = new GetEntityRequest()
    entityRequest.setDbname(this.dbName);
    entityRequest.setTablename(this.tableName);
    grpc.unary(EntityService.GetEntity, {
      request: entityRequest,
      host: environment.host, 
      onEnd: res => {
        const { status, message } = res;
        if (status === grpc.Code.OK && message) {
          var result = message.toObject() as GetEntityReply.AsObject;
          this.tableCols = result.columnsList.map((item)=>item.name);
        }
      }
    });

    var selectRequest = new SelectRequest();
    selectRequest.setTablename(this.tableName);
    selectRequest.setDbname(this.dbName);
    selectRequest.setShowsyscolumns(true);
    grpc.unary(EntityService.Select, {
      request: selectRequest,
      host: environment.host, 
      onEnd: res => {
        const { status, message } = res;
        if (status === grpc.Code.OK && message) {
          var result = message.toObject() as SelectReply.AsObject;
          this.tableData = result.rowsList.map((item)=> this.helperService.fromArrayToObject(item.itemsList, this.tableCols));
        }
      }
    });
    
    this.tableDataSrc = new MatTableDataSource(this.tableData);
    this.tableDataSrc.sort = this.sort;
    this.tableDataSrc.paginator = this.paginator;
  }

  onSearchInput(ev) {
    const searchTarget = ev.target.value;
    this.tableDataSrc.filter = searchTarget.trim().toLowerCase();
  }

  clearGrid(){
    this.tableCols = [];
    this.tableData = [];
  }
}