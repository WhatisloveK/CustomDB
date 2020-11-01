import { Component, OnInit } from '@angular/core';
import {grpc} from '@improbable-eng/grpc-web';
import {EntityService,DBService} from './generated/dbEngine_pb_service';
import {SelectRequest,SelectReply} from "./generated/dbEngine_pb"

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  
  title = 'dynamic-mat-table';
  tableCols = ['name', 'role', 'skillset'];
  tableData = [
    {
      name: 'Harsha Chinni',
      role: 'Fullstack Developer',
      skillset: 'Angular 9, Python 3, Flask, DSA'
    },
    {
      name: 'Bob',
      role: 'HR',
      skillset: 'Finding awesome candidates like Harsha :p'
    },
    {
      name: 'COVID-19',
      role: 'Making people panick',
      skillset: 'Infect people'
    },
  ];

  anotherTableCols = ['city', 'state', 'country', 'population'];
  anotherTableData = [
    {
      city: 'Hyderabad',
      state: 'Telangana',
      country: 'India',
      population: '10M'
    },
    {
      city: 'Salt Lake City',
      state: 'Utah',
      country: 'USA',
      population: '300k'
    },
    {
      city: 'Chennai',
      state: 'Tamil Nadu',
      country: 'India',
      population: '10M'
    },
    {
      city: 'New York City',
      state: 'New York',
      country: 'USA',
      population: '5M'
    },
    {
      city: 'Nellore',
      state: 'Andhra Pradesh',
      country: 'India',
      population: '3M'
    },
    {
      city: 'San Fransico',
      state: 'California',
      country: 'USA',
      population: '1M'
    },
  ];

  ngOnInit(): void {

    const selectRequest = new SelectRequest();
    selectRequest.setDbname("vikon");
    selectRequest.setShowsyscolumns(false);
    selectRequest.setTablename("vikontable");
    
    grpc.unary(EntityService.Select, {
      request: selectRequest,
      host: "https://localhost:5001", 
      onEnd: res => {
        const { status, message } = res;
        if (status === grpc.Code.OK && message) {
          var result = message.toObject() as SelectReply.AsObject;
          console.log(result);
        }
      }
    });
  }
}