import { Component, OnInit } from '@angular/core';
import { UserService } from './role/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  isLogin: boolean = false;
  title = 'Popcorn box app';
  token: any = '';
  menuType : string ='anyone';
  user : any;
  searchKey:string='';

  displayMenu: boolean = false;
  displayLog : boolean = false;
  displayAdmin : boolean = false;
  displayClient : boolean = false;

  currentRole : any;
  constructor(private us: UserService, private router : Router) { 
  }   

  ngOnInit(): void {
    this.us.updateMenu.subscribe(res =>{
      this.menuDisplay();
    });
    this.menuDisplay();
  }

  // setSearchKey(){
  //   this.us.setSearchKey(this.searchKey);
  // }

  getSearchResult(){

    this.searchKey= this.searchKey.trimStart();
    this.searchKey= this.searchKey.trimEnd();

    if (this.searchKey != '') {
      this.router.navigate(['search/' + this.searchKey]);
    }

  }

  menuDisplay()
  {
    if(sessionStorage.getItem('user') != '')
    {
      if(this.us.getToken() != '')
      {
        this.currentRole  = this.us.getRoleByToken(this.us.getToken());
        this.displayLog = (this.currentRole == 'User' || this.currentRole =='Admin' || this.currentRole == 'Client');
        this.displayAdmin = (this.currentRole =='Admin');
        this.displayClient = (this.currentRole == 'Client');
      }
      
    }
  }

  logout()
  {
    this.us.removeToken();
    this.router.navigate(['']);
    this.displayLog = false;
    this.displayAdmin = false;
    this.displayClient = false;
  }
  
}
