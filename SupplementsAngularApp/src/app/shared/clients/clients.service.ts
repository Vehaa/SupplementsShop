import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
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
    readonly register = application.baseUrl + "/User/Register";
    readonly cityUrl=application.baseUrl + "/Cities";
    
  formData:Client=new Client();
  list:any[];

 

  postClient(){
    const httpOptions=new HttpHeaders().set('Authorization', 'Bearer '+ localStorage.getItem('token'));
    return this.http.post(this.url,this.formData,{headers:httpOptions});
  }

  registerClient(){
    return this.http.post(this.register,this.formData);

  }

  putClient(id:number,params:Client){
    const httpOptions=new HttpHeaders().set('Authorization', 'Bearer '+ localStorage.getItem('token'));

    return this.http.put(`${this.url}/${id}`,params,{headers:httpOptions});
  }

  deleteClient(id:number){
    const httpOptions=new HttpHeaders().set('Authorization', 'Bearer '+ localStorage.getItem('token'));

    return this.http.delete(`${this.url}/${id}`,{headers:httpOptions});
  }

  getClientById(id:number){
    const httpOptions=new HttpHeaders().set('Authorization', 'Bearer '+ localStorage.getItem('token'));

    return this.http.get(`${this.url}/${id}`,{headers:httpOptions});
  }

  getClient(){
    const httpOptions=new HttpHeaders().set('Authorization', 'Bearer '+ localStorage.getItem('token'));

    this.http.get(this.url,{headers:httpOptions})
    .toPromise()
    .then(res=> this.list = res as Client[]);
  }

  getClientsByName(name:string){
    const httpOptions=new HttpHeaders().set('Authorization', 'Bearer '+ localStorage.getItem('token'));

    var params = new HttpParams();

    params =params.set('firstName', name);
    return this.http.get(this.url,{headers:httpOptions,params:params})
    .toPromise()
    .then(res=>this.list=res as Client[]);
  }
  

  getCities():Observable<any[]>{
    return this.http.get<any[]>(this.cityUrl);
  }

  getCityName(id:number):Observable<any>{
    return this.http.get<any>(`${this.cityUrl}/${id}`);
  }

}
