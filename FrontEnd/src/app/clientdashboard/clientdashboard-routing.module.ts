import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ClientGuard } from '../guard/client.guard';
import { AuthGuard } from '../auth.guard';

const routes: Routes = [
  {path:'clientdashboard/dashboard', component: DashboardComponent,canActivate:[AuthGuard,ClientGuard]}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class ClientdashboardRoutingModule { }
