import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { application } from 'src/app/server/server.service';
import { ClientsService } from '../clients/clients.service';
import { Orders } from './order.model';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {

  constructor(private http:HttpClient,
    private clientService: ClientsService) { }

  readonly url = application.baseUrl + "/Orders";
  readonly usrUrl = application.baseUrl + "/Users";


  httpOptions=new HttpHeaders().set('Authorization', 'Bearer '+ localStorage.getItem('token'));
  payLoad = JSON.parse(atob(localStorage.getItem('token').split('.')[1]));
  userId=this.payLoad.unique_name;

  formData:Orders=new Orders();
  list:Orders[];

  postOrder(order:Orders){
    return this.http.post(this.url,order,{headers:this.httpOptions}).subscribe();
  }

  putOrder(id:number,params:Orders){
    return this.http.put(`${this.url}/${id}`,params,{headers:this.httpOptions}).subscribe();
  }

  deleteOrder(id:number){
    return this.http.delete(`${this.url}/${id}`,{headers:this.httpOptions}).subscribe();
  }

  getClientById(){
    return this.clientService.getClientById(this.userId);
  }

}
