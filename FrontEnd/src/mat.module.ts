import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatToolbarModule } from '@angular/material/toolbar';
//important module 
import { MatIconModule } from '@angular/material/icon';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatPaginatorModule} from '@angular/material/paginator';
import {MatSortModule} from '@angular/material/sort'
import {MatTableModule} from '@angular/material/table';
import {MatSidenavModule} from '@angular/material/sidenav';
import {MatExpansionModule} from '@angular/material/expansion';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { MatDialogModule} from '@angular/material/dialog';
import { NgxPaginationModule } from 'ngx-pagination';

//toaster related
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import {MatButtonToggleModule} from '@angular/material/button-toggle';
import { MatSelectModule } from '@angular/material/select';

//custom modules

const materials = [

  MatInputModule,
  MatButtonModule,
  MatCardModule,
  MatToolbarModule,
  MatIconModule,
  MatFormFieldModule,
  MatPaginatorModule,
  MatSortModule,
  MatTableModule,
  MatSidenavModule,
  MatExpansionModule,
  MatDialogModule,
  FormsModule,
  MatSidenavModule,
  MatButtonToggleModule,
  BrowserAnimationsModule,  
  ReactiveFormsModule,
  MatSelectModule,
  NgxPaginationModule
]
@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    materials

  ],
  exports: [
    MatInputModule,
    materials
  ]
})
export class MatModule { }
