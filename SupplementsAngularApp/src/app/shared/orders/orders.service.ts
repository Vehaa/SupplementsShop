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
  readonly oStatus=application.baseUrl + "/OrderStatus";




  newOrders:number=0;
  formData:Orders=new Orders();
  list:Orders[];
  clientOrders:Orders[];
  orderDetails:Orders;
  newOrdersList:Orders[];


  postOrder(order:Orders){
    const httpOptions=new HttpHeaders().set('Authorization', 'Bearer '+ localStorage.getItem('token'));

    return this.http.post(this.url,order,{headers:httpOptions});
  }

  putOrder(id:number,params:Orders){
    const httpOptions=new HttpHeaders().set('Authorization', 'Bearer '+ localStorage.getItem('token'));

    return this.http.put(`${this.url}/${id}`,params,{headers:httpOptions}).subscribe();
  }

  deleteOrder(id:number){
    const httpOptions=new HttpHeaders().set('Authorization', 'Bearer '+ localStorage.getItem('token'));

    return this.http.delete(`${this.url}/${id}`,{headers:httpOptions}).subscribe();
  }
  getAllOrders(){
    const httpOptions=new HttpHeaders().set('Authorization', 'Bearer '+ localStorage.getItem('token'));

    return this.http.get(this.url,{headers:httpOptions}).subscribe
    (res=> this.list = res as Orders[]);
  }

  getClientOrders(){
    const httpOptions=new HttpHeaders().set('Authorization', 'Bearer '+ localStorage.getItem('token'));

    var params = new HttpParams();
    const payLoad = JSON.parse(atob(localStorage.getItem('token').split('.')[1]));
    const userId=payLoad.unique_name;
    params =params.set('customerId', userId);
    return this.http.get(this.url,{headers:httpOptions,params:params}).subscribe(res=>this.clientOrders=res as Orders[]);
  }

  getClientOrdersByStatus(id:string){
    const httpOptions=new HttpHeaders().set('Authorization', 'Bearer '+ localStorage.getItem('token'));

    var params = new HttpParams();
    const payLoad = JSON.parse(atob(localStorage.getItem('token').split('.')[1]));
    const userId=payLoad.unique_name;
    params =params.set('customerId', userId);
    params =params.set('orderStatusName', id);
    return this.http.get(this.url,{headers:httpOptions,params:params}).subscribe(res=>this.clientOrders=res as Orders[]);
  }

  getOrdersById(id:number){
    const httpOptions=new HttpHeaders().set('Authorization', 'Bearer '+ localStorage.getItem('token'));

    var params = new HttpParams();

    params =params.set('orderId', id.toString());
    return this.http.get(this.url,{headers:httpOptions,params:params}).subscribe(res=>this.list=res as Orders[]);
  }
  setCount(){
    this.newOrders--;
  }
  setInc(){
    this.newOrders++;
  }

  getNewOrders(){
    const httpOptions=new HttpHeaders().set('Authorization', 'Bearer '+ localStorage.getItem('token'));
    var params = new HttpParams();

    params =params.set('orderStatusName', 'Na čekanju');
    return this.http.get(this.url,{headers:httpOptions,params:params}).subscribe(res=>{
      this.newOrdersList=res as Orders[];
      this.newOrders=this.newOrdersList.length;
    });
    }

  getOrdersByStatus(id:string){
    const httpOptions=new HttpHeaders().set('Authorization', 'Bearer '+ localStorage.getItem('token'));

    var params = new HttpParams();

    params =params.set('orderStatusName', id);
    return this.http.get(this.url,{headers:httpOptions,params:params}).subscribe(res=>this.list=res as Orders[]);
  }

  getOrderStatuses():Observable<any[]>{
    const httpOptions=new HttpHeaders().set('Authorization', 'Bearer '+ localStorage.getItem('token'));

    return this.http.get<any[]>(this.oStatus,{headers:httpOptions});
  }

  getClientById(){
    const payLoad = JSON.parse(atob(localStorage.getItem('token').split('.')[1]));
    const userId=payLoad.unique_name;
    return this.clientService.getClientById(userId);
  }
  
  getOrderDetailsByOrderId(id:number){
    const httpOptions=new HttpHeaders().set('Authorization', 'Bearer '+ localStorage.getItem('token'));

    var params = new HttpParams();

    params =params.set('orderId', id.toString());

    return this.http.get(this.url,{headers:httpOptions,params:params});

  }

  getClientOrderDetailsByOrderId(id:number){
    const httpOptions=new HttpHeaders().set('Authorization', 'Bearer '+ localStorage.getItem('token'));

    var params = new HttpParams();

    const payLoad = JSON.parse(atob(localStorage.getItem('token').split('.')[1]));
    const userId=payLoad.unique_name;
    params =params.set('orderId', id.toString());
    params =params.set('customerId',userId);

    return this.http.get(this.url,{headers:httpOptions,params:params});

  }


}
