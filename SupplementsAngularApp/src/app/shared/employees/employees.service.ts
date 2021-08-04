import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { application } from '../../server/server.service';
import { CitiesService } from '../cities/cities.service';
import { City } from '../cities/city.model';
import { Employee } from "./employee.model";

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(private http:HttpClient) {
       
     }

    readonly url = application.baseUrl + "/Employees";
    readonly cityUrl=application.baseUrl + "/Cities";

  formData:Employee=new Employee();
  list:Employee[];

 

  postEmployee(){
    return this.http.post(this.url,this.formData);
  }

  putEmployee(id:number,params:Employee){
    return this.http.put(`${this.url}/${id}`,params);
  }

  deleteEmployee(id:number){
    return this.http.delete(`${this.url}/${id}`);
  }

  getEmployees(){
    this.http.get(this.url)
    .toPromise()
    .then(res=> this.list = res as Employee[]);
  }

  getEmployeeById(id:number){
    return this.http.get(`${this.url}/${id}`);
  }

  

  getCities():Observable<any[]>{
    return this.http.get<any[]>(this.cityUrl);
  }

  getCityName(id:number):Observable<any>{
    return this.http.get<any>(`${this.cityUrl}/${id}`);
  }

}
