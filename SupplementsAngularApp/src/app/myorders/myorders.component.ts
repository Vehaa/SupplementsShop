import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OrdersService } from '../shared/orders/orders.service';

@Component({
  selector: 'app-myorders',
  templateUrl: './myorders.component.html',
  styleUrls: [

  ]
})
export class MyordersComponent implements OnInit {

  constructor(public orderService:OrdersService,
    private _router:Router) { }

  ngOnInit(): void {
    this.orderService.getClientOrders();

  }

  orderDetails(id:number){
    this._router.navigate(['/MyOrderDetails',id]);
  }

}
