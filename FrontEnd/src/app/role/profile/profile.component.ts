import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service';
import { User } from '../model/user';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { UserUpdate } from '../model/updateUser';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  myform : FormGroup
  isSubmitted: boolean = false;
  userId: any;
  user!: User
  profilePicUrl:string=''
  profilePic:string=''
  userToUpdate : UserUpdate={
    name:'',
    mobileNo:'',
    alternateMobileNo:'',
    profilePic:''
  }
  constructor(private userService: UserService , private toastr :ToastrService , private formBuilder: FormBuilder, ) {
    this.userId = sessionStorage.getItem("userId");
    this.myform = this.formBuilder.group({
      Name: new FormControl('', [Validators.required, Validators.pattern("^[a-zA-Z ]*$")]),
      MobileNo: new FormControl('', [Validators.required, Validators.pattern("^[0-9]+$"), Validators.maxLength(10), Validators.minLength(10)]),
      AlternateMobileNo: new FormControl('', [Validators.required, Validators.pattern("^[0-9]+$"), Validators.maxLength(10), Validators.minLength(10)]),
    })
  }

  ngOnInit(): void {
    this.getUserData();
  }

  getUserData() {
    if (this.userId != null) {
      this.userService.getUserDataById(this.userId).subscribe({
        next: (res : any) => {
          this.user = res
          this.profilePicUrl=res.profilePic,
          this.myform.setValue({
            Name:this.user.name,
            MobileNo:this.user.mobileNo,
            AlternateMobileNo:this.user.alternateMobileNo,
          })
        }
      })
    }
  }

  updateUser()
  {
    this.isSubmitted = true;
    if (this.myform.valid) {
      this.userToUpdate.name = this.myform.value.Name,
      this.userToUpdate.mobileNo = this.myform.value.MobileNo,
      this.userToUpdate.alternateMobileNo = this.myform.value.AlternateMobileNo
      
      this.userService.updateUser(this.userId,this.userToUpdate).subscribe({
        next:(res:any)=>{
          if(res.message=='UPDATED'){
            window.location.reload();
            this.toastr.success("Data updated successfully")
          }else if(res.message=='NOT UPDATED'){
            this.toastr.error("Cannot update data")
          }
        }
      })
    }
  }

  updateProfilePic()
  {
    this.userService.updateUserProfilePic(this.userId,this.profilePic.substring(12)).subscribe({
      next:(res:any)=>{
        if(res.message=='UPDATED PROFILEPIC'){
          window.location.reload();
          this.toastr.success("Profile pic updated successfully")
        }else if(res.message=='NOT UPDATED PROFILEPIC'){
          this.toastr.error("Profile pic cannot be changed")
        }
      }
    })
    
  }

}
