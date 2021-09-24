import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { application } from 'src/app/server/server.service';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(private fb: FormBuilder, private http: HttpClient) { }

  readonly url = application.baseUrl + "/User";
  httpOptions={
    headers:new HttpHeaders({'Content-Type': 'application/json'})
  };

  login(formData) {
    return this.http.post(this.url + '/Login', formData, { responseType: 'text' });
  }


  roleMatch(allowedRoles): boolean {
    var isMatch = false;
    var payLoad = JSON.parse(atob(localStorage.getItem('token').split('.')[1]));
    var userRole = payLoad.role;
    for(let r of allowedRoles){
      if(r==userRole)
      isMatch=true;
    }
    return isMatch;

  }


}
