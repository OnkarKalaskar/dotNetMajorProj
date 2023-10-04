import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MoviespageComponent } from './moviespage/moviespage.component';
import { MoviedetailsComponent } from './moviedetails/moviedetails.component';
import { AuthGuard } from '../auth.guard';

const routes: Routes = [
  {path:'movies',component:MoviespageComponent},
  {path:'movies/moviedetails/:id',component:MoviedetailsComponent,canActivate:[AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MovieRoutingModule { }
