import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TvshowspageComponent } from './tvshowspage/tvshowspage.component';
import { TvshowdetailsComponent } from './tvshowdetails/tvshowdetails.component';
import { AuthGuard } from '../auth.guard';

const routes: Routes = [
  {path:'tvshows',component:TvshowspageComponent},
  {path:'tvshows/tvshowdetails/:id',component:TvshowdetailsComponent,canActivate:[AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TvshowRoutingModule { }
