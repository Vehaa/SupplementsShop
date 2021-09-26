import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { subscribeOn } from 'rxjs/operators';
import { Orders } from 'src/app/shared/orders/order.model';
import { OrderDetails } from 'src/app/shared/orders/orderDetails.model';
import { OrdersService } from 'src/app/shared/orders/orders.service';

@Component({
  selector: 'app-my-order-details',
  templateUrl: './my-order-details.component.html',
  styleUrls: [

  ]
})
export class MyOrderDetailsComponent implements OnInit {

  constructor(private route:ActivatedRoute,
    public orderService:OrdersService) {

     }

  order: Orders;
  orderDetails:OrderDetails[];
  Total:number;
  
  ngOnInit(): void {
    this.route.paramMap.subscribe(params =>{
      const orderId=+params.get('id');
      if(orderId){
        this.getOrderDetails(orderId).subscribe(res=>{
          this.order=res as Orders;
          console.log(this.order);
        });
      };
    });
  }

  getOrderDetails(id:number){
    return this.orderService.getOrderDetailsByOrderId(id);
  }
}
