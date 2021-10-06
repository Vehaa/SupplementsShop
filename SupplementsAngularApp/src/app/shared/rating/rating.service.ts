import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { application } from 'src/app/server/server.service';

@Injectable({
  providedIn: 'root'
})
export class RatingService {

  constructor(private http:HttpClient) { }
  readonly url = application.baseUrl + "/Evaluations";

  httpOptions=new HttpHeaders().set('Authorization', 'Bearer '+ localStorage.getItem('token'));


  postStar(form:FormGroup){
    
    return this.http.post(this.url,form,{headers:this.httpOptions});
  }

}
