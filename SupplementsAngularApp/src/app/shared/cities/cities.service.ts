import { Injectable } from '@angular/core';
import { application } from "../../server/server.service";
import { City } from "../cities/city.model";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class CitiesService {

  constructor(private http:HttpClient) { }

  readonly url = application.baseUrl + "/Cities";
  httpOptions=new HttpHeaders().set('Authorization', 'Bearer '+ localStorage.getItem('token'));

  formData:City=new City();
  list:City[];

  postCity(){
    return this.http.post(this.url,this.formData,{headers:this.httpOptions});
  }

  putCity(){
    return this.http.put(`${this.url}/${this.formData.cityId}`,this.formData,{headers:this.httpOptions});
  }

  deleteCity(id:number){
    return this.http.delete(`${this.url}/${id}`,{headers:this.httpOptions});
  }

  refreshList(){
    this.http.get(this.url)
    .toPromise()
    .then(res=> this.list = res as City[]);
  }

  public GetAllCities=():Observable<any>=>{
    return this.http.get(this.url);
  }

}
