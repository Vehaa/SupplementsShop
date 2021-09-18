import { Component, OnInit } from '@angular/core';
import { OrdersService } from '../shared/orders/orders.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: [
    
  ]
})
export class OrdersComponent implements OnInit {

  constructor(private orderService:OrdersService) { }

  ngOnInit(): void {
    
  }

}
