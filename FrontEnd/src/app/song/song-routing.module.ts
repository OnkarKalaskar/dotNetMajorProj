import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SongdetailsComponent } from './songdetails/songdetails.component';
import { SongspageComponent } from './songspage/songspage.component';

const routes: Routes = [
  {path:'songs',component:SongspageComponent},
  {path:'songs/songdetails/:id',component : SongdetailsComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SongRoutingModule { }
