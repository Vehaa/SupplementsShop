import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
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
    httpOptions=new HttpHeaders().set('Authorization', 'Bearer '+ localStorage.getItem('token'));

  formData:Employee=new Employee();
  list:Employee[];

 

  postEmployee(){
    return this.http.post(this.url,this.formData,{headers:this.httpOptions});
  }

  putEmployee(id:number,params:Employee){
    return this.http.put(`${this.url}/${id}`,params,{headers:this.httpOptions});
  }

  deleteEmployee(id:number){
    return this.http.delete(`${this.url}/${id}`,{headers:this.httpOptions});
  }

  getEmployees(){
    this.http.get(this.url,{headers:this.httpOptions})
    .toPromise()
    .then(res=> this.list = res as Employee[]);
  }

  getEmployeeById(id:number){
    return this.http.get(`${this.url}/${id}`,{headers:this.httpOptions});
  }

  getClientsByName(name:string){
    const httpOptions=new HttpHeaders().set('Authorization', 'Bearer '+ localStorage.getItem('token'));

    var params = new HttpParams();

    params =params.set('firstName', name);
    return this.http.get(this.url,{headers:httpOptions,params:params})
    .toPromise()
    .then(res=>this.list=res as Employee[]);
  }
  

  getCities():Observable<any[]>{
    return this.http.get<any[]>(this.cityUrl);
  }

  getCityName(id:number):Observable<any>{
    return this.http.get<any>(`${this.cityUrl}/${id}`);
  }

}
