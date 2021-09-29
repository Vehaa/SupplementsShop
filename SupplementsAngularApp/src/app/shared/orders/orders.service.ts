import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { application } from 'src/app/server/server.service';
import { ClientsService } from '../clients/clients.service';
import { Orders } from './order.model';
import { OrderDetails } from './orderDetails.model';

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
  clientOrders:Orders[];
  orderDetails:Orders;


  postOrder(order:Orders){
    return this.http.post(this.url,order,{headers:this.httpOptions});
  }

  putOrder(id:number,params:Orders){
    return this.http.put(`${this.url}/${id}`,params,{headers:this.httpOptions}).subscribe();
  }

  deleteOrder(id:number){
    return this.http.delete(`${this.url}/${id}`,{headers:this.httpOptions}).subscribe();
  }
  getAllOrders(){
    return this.http.get(this.url,{headers:this.httpOptions}).subscribe
    (res=> this.list = res as Orders[]);
  }

  getClientOrders(){
    var params = new HttpParams();

    params =params.set('customerId', this.userId);
    return this.http.get(this.url,{headers:this.httpOptions,params:params}).subscribe(res=>this.clientOrders=res as Orders[]);
  }

  getClientById(){
    return this.clientService.getClientById(this.userId);
  }
  
  getOrderDetailsByOrderId(id:number){
    var params = new HttpParams();

    params =params.set('orderId', id.toString());
    return this.http.get(this.url,{headers:this.httpOptions,params:params});

  }


}
