import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';


import { AppComponent } from './app.component';
import { MatTableComponent } from './mat-table/mat-table.component';
import { MatSortModule } from '@angular/material/sort';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatFormFieldModule } from '@angular/material/form-field';
import { HelperService } from './shared/services/helper.service';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import { OperationsService } from './shared/services/operations.service';

@NgModule({
  declarations: [
    AppComponent,
    MatTableComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    MatFormFieldModule,
    MatSnackBarModule
  ],
  providers: [
    HelperService,
    OperationsService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
