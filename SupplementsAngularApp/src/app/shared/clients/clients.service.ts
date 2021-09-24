import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { application } from '../../server/server.service';
import { CitiesService } from '../cities/cities.service';
import { City } from '../cities/city.model';
import { Client } from "./client.model";


@Injectable({
  providedIn: 'root'
})
export class ClientsService {

  constructor(private http:HttpClient) {
       
     }

    readonly url = application.baseUrl + "/Users";
    readonly cityUrl=application.baseUrl + "/Cities";
    httpOptions=new HttpHeaders().set('Authorization', 'Bearer '+ localStorage.getItem('token'));
    
  formData:Client=new Client();
  list:any[];

 

  postClient(){
    return this.http.post(this.url,this.formData,{headers:this.httpOptions});
  }

  putClient(id:number,params:Client){
    return this.http.put(`${this.url}/${id}`,params,{headers:this.httpOptions});
  }

  deleteClient(id:number){
    return this.http.delete(`${this.url}/${id}`,{headers:this.httpOptions});
  }

  getClientById(id:number){
    return this.http.get(`${this.url}/${id}`,{headers:this.httpOptions});
  }

  getClient(){
    this.http.get(this.url,{headers:this.httpOptions})
    .subscribe(res=> this.list = res as Client[]);
  }

  

  getCities():Observable<any[]>{
    return this.http.get<any[]>(this.cityUrl);
  }

  getCityName(id:number):Observable<any>{
    return this.http.get<any>(`${this.cityUrl}/${id}`);
  }

}
