import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ExceptionComponent } from './exception/exception.component';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';
import { UserlistingComponent } from './userlisting/userlisting.component';
import { ProfileComponent } from './profile/profile.component';
import { UserfavouriteComponent } from './userfavourite/userfavourite.component';
import { AuthGuard } from '../auth.guard';
import { AdminGuard } from '../guard/admin.guard';
import { ForgotpasswordComponent } from './forgotpassword/forgotpassword.component';
import { UsersearchComponent } from './usersearch/usersearch.component';

const routes: Routes = [
  {path:'login',component:LoginComponent},
  {path:'signup',component:SignupComponent},
  {path:'admin',component :UserlistingComponent,canActivate :[AuthGuard,AdminGuard]},
  {path:'profile',component:ProfileComponent,canActivate :[AuthGuard]},
  {path:'unauthorized', component: ExceptionComponent},
  {path:'favourite',component:UserfavouriteComponent,canActivate :[AuthGuard]},
  {path:'login/forgotpassword',component:ForgotpasswordComponent},
  // {path:'search', component:UsersearchComponent}
  {path:'search/:searchkey', component:UsersearchComponent}  

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RoleRoutingModule { }
