import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { UsersService } from '../users/users.service';

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {
  constructor(private router:Router,private service:UsersService){}
  canActivate(next:ActivatedRouteSnapshot) {
    if(localStorage.getItem('token')!=null) {
      let roles=next.data['permittedRoles'] as Array<string>;
      if(roles){
        if(this.service.roleMatch(roles)){
          return true;
        }
        else{
          this.router.navigate(['/Forbidden']);
          return false;
        }
      }
      return true;
    }
    else{
      this.router.navigate(["/User/Login"]);
      return false;
    }
    
    
  }
  
}
