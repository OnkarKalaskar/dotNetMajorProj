import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RoleRoutingModule } from './role-routing.module';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';
import { MatModule } from 'src/mat.module';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { UserlistingComponent } from './userlisting/userlisting.component';
import { ProfileComponent } from './profile/profile.component';
import { ExceptionComponent } from './exception/exception.component';
import { UserfavouriteComponent } from './userfavourite/userfavourite.component';
import { ForgotpasswordComponent } from './forgotpassword/forgotpassword.component';
import { UsersearchComponent } from './usersearch/usersearch.component'


@NgModule({
  declarations: [
    LoginComponent,
    SignupComponent,
    UserlistingComponent,
    ProfileComponent,
    UserlistingComponent,
    ExceptionComponent,
    UserfavouriteComponent,
    ForgotpasswordComponent,
    UsersearchComponent
  ],
  imports: [
    CommonModule,
    RoleRoutingModule,
    MatModule,
    ReactiveFormsModule,
    HttpClientModule
  
  ]
})
export class RoleModule { }
