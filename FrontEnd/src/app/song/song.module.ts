import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SongRoutingModule } from './song-routing.module';
import { SongspageComponent } from './songspage/songspage.component';
import { SongdetailsComponent } from './songdetails/songdetails.component';

import { MatModule } from 'src/mat.module';
import { SongcontainerComponent } from './songcontainer/songcontainer.component';
@NgModule({
  declarations: [
    SongspageComponent,
    SongdetailsComponent,
    SongcontainerComponent
  ],
  imports: [
    CommonModule,
    SongRoutingModule,
    MatModule
  ],
  exports : [
    SongcontainerComponent
  ]
})
export class SongModule { }
