import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../user.service';
import { ToastrService } from 'ngx-toastr';
import { UserLogin } from '../model/user-login';

@Component({
  selector: 'app-forgotpassword',
  templateUrl: './forgotpassword.component.html',
  styleUrls: ['./forgotpassword.component.css']
})
export class ForgotpasswordComponent {

  myform: FormGroup;
  isSubmitted: boolean = false;
  user: UserLogin ={
    email:'',
    password:'',
    role:''
  }

  constructor(private formBuilder: FormBuilder, private router: Router, private userService: UserService, private toastr: ToastrService) {
    this.myform = this.formBuilder.group({
      Email: new FormControl('', [Validators.required, Validators.pattern("^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$")]),
      Password: new FormControl('', [Validators.required]),
    })
  }


  onSubmit() {
    this.isSubmitted = true;
    if (this.myform.valid) {
      this.user.email = this.myform.value.Email,
      this.user.password = this.myform.value.Password,
      this.userService.changePassword(this.user).subscribe({
        next:(res : any)=>{
          if(res.message=='CHANGED'){
            this.toastr.success("Password Changed successfully")
          }else if(res.message=='NOTCHANGED'){
            this.toastr.error("Please Check your email")
          }else{
            this.toastr.error("Password cannot be changed")
          }
        }
      })
    }

  }
}
