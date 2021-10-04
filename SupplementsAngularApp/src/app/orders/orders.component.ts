import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OrdersService } from '../shared/orders/orders.service';
import { OrderStatus } from '../shared/orders/orderStatus.model';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: [
    
  ]
})
export class OrdersComponent implements OnInit {

  statusList:OrderStatus[];

  constructor(public orderService:OrdersService,
    private _router:Router) { }

  ngOnInit(): void {
    this.orderService.getAllOrders();
    this.orderService.getOrderStatuses().subscribe(res=>this.statusList=res);
  }


  orderDetails(id:number){
    this._router.navigate(['/OrderDetails',id]);
  }
  orderById(id:number){
    this.orderService.getOrdersById(id);
  }

  ordersByStatus(value:string){
    this.orderService.getOrdersByStatus(value);
  }
  

}
