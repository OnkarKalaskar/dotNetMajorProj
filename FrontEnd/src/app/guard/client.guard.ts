import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { UserService } from '../role/user.service';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class ClientGuard implements CanActivate {
  constructor(private userService : UserService , private router :Router , private toastr : ToastrService){}
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      if(this.userService.getRoleByToken(this.userService.getToken())=='Client')
      {
        return true;
      }
      else{
        
        this.router.navigate(['unauthorized']);
        this.toastr.error("You are not allowed to access this page");
        return false;
      }
  }
  
}
