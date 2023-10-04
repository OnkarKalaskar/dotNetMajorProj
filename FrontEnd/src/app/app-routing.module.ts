import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomepageComponent } from './homepage/homepage.component';

const routes: Routes = [
  {path:'',component:HomepageComponent},
  {path:'login',redirectTo:'login',pathMatch:'full'},
  {path:'signup',redirectTo:'signup',pathMatch:'full'},
  {path:'movies',redirectTo:'movies',pathMatch:'full'},
  {path:'songs',redirectTo:'songs',pathMatch:'full'},
  {path:'tvshows',redirectTo:'tvshows',pathMatch:'full'},
  {path:'admin',redirectTo:'admin',pathMatch:'full'},
  {path:'client-dashboard', redirectTo:'clientdashboard/dashboard',pathMatch:'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }