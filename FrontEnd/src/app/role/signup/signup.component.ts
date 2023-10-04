import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../user.service';
import { Router } from '@angular/router';
import { User } from '../model/user';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent {

  myform: FormGroup;
  isSubmitted: boolean = false;
  constructor(private formBuilder: FormBuilder, private userService: UserService, private router: Router, private toaster: ToastrService) {
    this.myform = this.formBuilder.group({
      Name: new FormControl('', [Validators.required, Validators.pattern("^[a-zA-Z ]*$")]),
      Email: new FormControl('', [Validators.required, Validators.pattern("^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$")]),
      Password: new FormControl('', [Validators.required]),
      MobileNo: new FormControl('', [Validators.required, Validators.pattern("^[0-9]+$"), Validators.maxLength(10), Validators.minLength(10)]),
      AlternateMobileNo: new FormControl('', [Validators.required, Validators.pattern("^[0-9]+$"), Validators.maxLength(10), Validators.minLength(10)]),
      Role: new FormControl('', Validators.required),
      SubstricptionId: new FormControl('', Validators.required)
    
    })
  }

  user: User = {
    name: '',
    password: '',
    email: '',
    mobileNo: '',
    profilePic:'',
    alternateMobileNo: '',
    role: '',
    subscriptionId: 0,
  }

  onSubmit() {
    this.isSubmitted = true;
    if (this.myform.valid) {
      this.user.name = this.myform.value.Name;
      this.user.email = this.myform.value.Email;
      this.user.password = this.myform.value.Password;
      this.user.mobileNo = this.myform.value.MobileNo;
      this.user.alternateMobileNo = this.myform.value.AlternateMobileNo;
      this.user.role = this.myform.value.Role;
      this.user.subscriptionId = this.myform.value.SubstricptionId;
      this.userService.register(this.user).subscribe({
        next: (res) => {
          if (res.message == 'REGISTERED SUCCESSFULLY') {
            if (this.myform.value.Role == 'Client') {
              this.router.navigate(['login']);
              this.toaster.warning("Registered Successfully, Contact Admin for approval")
            } else {
              this.router.navigate(['login']);
              this.toaster.success("Registered Successfully")
            }
          } else if (res.message == 'ALREADY EXIST') {
            this.toaster.error("Email Already Exist")
          }
        }
      }
      )
    }
  }
}

// if (this.myform.value.Role == 'Client') {
//   this.router.navigate(['login']);
//   this.toaster.warning("Contact Admin for approval")
// } else if(res.message=='REGISTERED SUCCESSFULLY'){
//   this.router.navigate(['login']);
//   this.toaster.success("Registered Successfully")
// }else if(res.message=='ALREADY EXIST'){
//   this.toaster.error("Email already exist")
// }