import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MovieRoutingModule } from './movie-routing.module';
import { MoviespageComponent } from './moviespage/moviespage.component';
import { MoviedetailsComponent } from './moviedetails/moviedetails.component';
import { MatButtonModule } from '@angular/material/button';
import { MatModule } from 'src/mat.module';
import { MoviecontainerComponent } from './moviecontainer/moviecontainer.component';


@NgModule({
  declarations: [
    MoviespageComponent,
    MoviedetailsComponent,
    MoviecontainerComponent
  ],
  imports: [
    MatModule,
    CommonModule,
    MovieRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    MatButtonModule
  ],
  exports : [
    MoviecontainerComponent
  ]
})
export class MovieModule { }
