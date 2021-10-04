import { Component, OnInit } from '@angular/core';
import { AuthService } from '../shared/auth/auth.service';
import { Orders } from '../shared/orders/order.model';
import { OrdersService } from '../shared/orders/orders.service';

@Component({
  selector: 'app-nav-header',
  templateUrl: './nav-header.component.html',
  styleUrls: [

  ]
})
export class NavHeaderComponent implements OnInit {

  public newOrders:number=0;
  orders:Orders[];
  constructor(private authService:AuthService,
    private orderService:OrdersService) { }

  ngOnInit(): void {
    if(this.isAdmin() || this.isEmployee()){
      this.orderService.getNewOrders();
    }
    
  }

  isClient(){
    return this.authService.isClient();
  }
  isAdmin() {
    return this.authService.isAdmin();
  }

  isEmployee() {
    return this.authService.isEmployee();
  }
}
