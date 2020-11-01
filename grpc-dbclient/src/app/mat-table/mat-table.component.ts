import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import {grpc} from '@improbable-eng/grpc-web';
import {EntityService,DBService} from '../generated/dbEngine_pb_service';
import {SelectRequest,SelectReply, GetEntityRequest, GetEntityReply} from "../generated/dbEngine_pb"
import { environment } from 'src/environments/environment';
import { HelperService } from '../shared/services/helper.service';

@Component({
  selector: 'app-mat-table',
  templateUrl: './mat-table.component.html',
  styleUrls: ['./mat-table.component.css']
})
export class MatTableComponent implements OnInit {

  tableDataSrc: any;

  tableCols: string[];
  tableData: {}[] = [];

  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;


  constructor(private helperService: HelperService) { }

  ngOnInit() {
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