import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Observable } from 'rxjs';
import { application } from 'src/app/server/server.service';
import { User } from './user.model';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(private http: HttpClient) { }

  readonly url = application.baseUrl + "/User";
  readonly pwUrl = application.baseUrl + "/User/UpdatePassword";
  readonly eUser = application.baseUrl + "/User/UpdateProfile";
  readonly usersUrl = application.baseUrl + "/Users";
  readonly cityUrl=application.baseUrl + "/Cities";

  user:User;
  httpOptions={
    headers:new HttpHeaders({'Content-Type': 'application/json'})
  };

  httpOptions2=new HttpHeaders().set('Authorization', 'Bearer '+ localStorage.getItem('token'));

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

  putPassword(params:Observable<any>){
    const payLoad = JSON.parse(atob(localStorage.getItem('token').split('.')[1]));
    const userId=payLoad.unique_name;
    return this.http.put(`${this.pwUrl}/${userId}`,params,{headers:this.httpOptions2});
  }



  putUser(params:Observable<any>){
    const payLoad = JSON.parse(atob(localStorage.getItem('token').split('.')[1]));
    const userId=payLoad.unique_name;
    return this.http.put(`${this.eUser}/${userId}`,params,{headers:this.httpOptions2});
  }

  getProfile(){
    const payLoad = JSON.parse(atob(localStorage.getItem('token').split('.')[1]));
    const userId=payLoad.unique_name;
    return this.http.get(`${this.usersUrl}/${userId}`,{headers:this.httpOptions2});
  }

  getCities():Observable<any[]>{
    return this.http.get<any[]>(this.cityUrl);
  }
}
