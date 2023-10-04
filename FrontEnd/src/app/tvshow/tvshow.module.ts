import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TvshowRoutingModule } from './tvshow-routing.module';
import { TvshowspageComponent } from './tvshowspage/tvshowspage.component';
import { HttpClientModule } from '@angular/common/http';
import { TvshowdetailsComponent } from './tvshowdetails/tvshowdetails.component';
import { MatModule } from 'src/mat.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TvshowcontainerComponent } from './tvshowcontainer/tvshowcontainer.component';


@NgModule({
  declarations: [
    TvshowspageComponent,
    TvshowdetailsComponent,
    TvshowcontainerComponent
  ],
  imports: [
    CommonModule,
    TvshowRoutingModule,
    HttpClientModule,
    MatModule,
    FormsModule,
    ReactiveFormsModule
  ],
  exports : [
    TvshowcontainerComponent
  ]
})
export class TvshowModule { }
