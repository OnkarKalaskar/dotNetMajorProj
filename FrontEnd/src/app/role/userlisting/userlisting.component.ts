import { Component, OnInit, ViewChild } from '@angular/core';
import { UserService } from '../user.service';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Registereduser } from '../model/registereduser';
import { ClientLogin } from '../model/clientLogin';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-userlisting',
  templateUrl: './userlisting.component.html',
  styleUrls: ['./userlisting.component.css']
})
export class UserlistingComponent implements OnInit{

  displayedColumns: string[] = ['name', 'email', 'mobileNo','alternateMobileNo', 'role','subscription','action'];
  displayedClientColumns: string[] = ['name', 'email', 'mobileNo', 'role', 'status', 'action'];
  dataSource!: MatTableDataSource<Registereduser>;
  clientDataSource!:MatTableDataSource<ClientLogin>;
  @ViewChild(MatSort) sort!:MatSort
 
  constructor(private userService : UserService ,  private toaster:ToastrService){}

  ngOnInit(): void {
    this.getUsers()
    this.getClients()
  }

  getUsers()
  {
    this.userService.getAllUsers().subscribe({
      next: (res) => {
        this.dataSource = new MatTableDataSource(res)
      },
      error: console.log
    })
  }


  getClients()
  {
    this.userService.getAllClients().subscribe({
      next:(res)=>{
        this.clientDataSource = new MatTableDataSource(res);
        this.clientDataSource.sort = this.sort
      },
      error:console.log
      
    })
  }

  approveClient(id:string)
  {
    this.userService.approveClient(id).subscribe({
      next:(res)=>{
        if(res.message=='APPROVED')
        {
          this.toaster.success("Client is approved")
          window.location.reload();
        }else if(res.message=='ALREADYAPPROVED')
        {
          this.toaster.success("Client is already approved")
        }
      }
    })
  }

  deleteUser(id:string)
  {
    this.userService.deleteUser(id).subscribe({
      next:(res)=>{
        if(res.message=='DELETED')
        {
          window.location.reload();
          this.toaster.success("Deleted successfully")
          
        }else if(res.message=='USER NOT FOUND')
        {
          this.toaster.error("User not found")
        }
      }
    })
  }

}
