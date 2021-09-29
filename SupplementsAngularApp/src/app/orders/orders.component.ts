import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OrdersService } from '../shared/orders/orders.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: [
    
  ]
})
export class OrdersComponent implements OnInit {

  constructor(public orderService:OrdersService,
    private _router:Router) { }

  ngOnInit(): void {
    this.orderService.getAllOrders();
  }

  orderDetails(id:number){
    this._router.navigate(['/OrderDetails',id]);
  }

  

}
