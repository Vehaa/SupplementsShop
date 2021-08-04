import { Injectable } from '@angular/core';
import { application } from "../../server/server.service";
import { City } from "../cities/city.model";
import {HttpClient} from "@angular/common/http";
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class CitiesService {

  constructor(private http:HttpClient) { }

  readonly url = application.baseUrl + "/Cities";

  formData:City=new City();
  list:City[];

  postCity(){
    return this.http.post(this.url,this.formData);
  }

  putCity(){
    return this.http.put(`${this.url}/${this.formData.cityId}`,this.formData);
  }

  deleteCity(id:number){
    return this.http.delete(`${this.url}/${id}`);
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
