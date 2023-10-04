import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserLogin } from '../model/user-login';
import { UserService } from '../user.service';
import { UserLogged } from '../model/userLogged.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  user!: any ;
  myform: FormGroup;
  isSubmitted: boolean = false;

  constructor(private formBuilder: FormBuilder, private router: Router, private userService: UserService, private toastr: ToastrService) {
    this.myform = this.formBuilder.group({
      Email: new FormControl('', [Validators.required, Validators.pattern("^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$")]),
      Password: new FormControl('', [Validators.required]),
      Role: new FormControl('', Validators.required)
    })
  }

  userlog: UserLogin = {
    email: '',
    password: '',
    role: ''
  }

  onSubmit() {
    this.isSubmitted = true;
    if (this.myform.valid) {
      this.userlog.email = this.myform.value.Email;
      this.userlog.password = this.myform.value.Password;
      this.userlog.role = this.myform.value.Role;
      this.userService.login(this.userlog).subscribe({

        next: (data) => {
          if (data.message == 'NOT APPROVED') {
            this.toastr.error("Sorry you are not approved yet by admin");
          } else if (data.message == 'INVALID CREDENTIALS') {
            this.toastr.error("Invalid Credentials");
          } else {
            //setting token 
            this.userService.setToken(data);
            //decoding object from token and setting as user in userLogged
            this.user = JSON.parse(atob(data.split('.')[1]));
            sessionStorage.setItem("userId", this.user.id);
            this.userService.updateMenu.next();
            this.router.navigate(['']);
            this.toastr.success("Login Successfull");
          }
        },

      }
      )
    }
  }



}
