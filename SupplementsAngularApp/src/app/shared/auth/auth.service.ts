import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor() { }

  IsLoggedIn(){
    return !!localStorage.getItem('token');
  }

  isClient(){

    var payLoad = JSON.parse(atob(localStorage.getItem('token').split('.')[1]));
    var userRole = payLoad.role;
    if(userRole=="Klijent")
      return true;
    return false;
  }

  isAdmin(){

    var payLoad = JSON.parse(atob(localStorage.getItem('token').split('.')[1]));
    var userRole = payLoad.role;
    if(userRole=="Administrator")
      return true;
    return false;
  }

  isEmployee(){

    var payLoad = JSON.parse(atob(localStorage.getItem('token').split('.')[1]));
    var userRole = payLoad.role;
    if(userRole=="Uposlenik")
      return true;
    return false;
  }
}
