import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OrdersService } from '../shared/orders/orders.service';
import { OrderStatus } from '../shared/orders/orderStatus.model';

@Component({
  selector: 'app-myorders',
  templateUrl: './myorders.component.html',
  styleUrls: [

  ]
})
export class MyordersComponent implements OnInit {

  statusList: OrderStatus[];

  constructor(public orderService: OrdersService,
    private _router: Router) { }

  ngOnInit(): void {
    this.orderService.getClientOrders();
    this.orderService.getOrderStatuses().subscribe(res => this.statusList = res);

  }

  orderDetails(id: number) {
    this._router.navigate(['/MyOrderDetails', id]);
  }

  ordersByStatus(value: string) {
    this.orderService.getClientOrdersByStatus(value);
  }

}
