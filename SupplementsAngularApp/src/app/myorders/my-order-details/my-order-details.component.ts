import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
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

  base="data:image/png;charset=utf-8;base64,";

  constructor(private route:ActivatedRoute,
    public orderService:OrdersService,
    private sanitizer: DomSanitizer) {

     }

  order: any;
  orderDetails:OrderDetails[];
  Total:number=0;
  
  ngOnInit(): void {
    this.route.paramMap.subscribe(params =>{
      const orderId=+params.get('id');
      if(orderId){
        this.getOrderDetails(orderId).subscribe(res=>{
          this.order=res as Orders;
          for(var i of this.order){
            for(var o of i.orderDetailsList)
            {
              this.Total+=o.totalPrice;
            }
          }
        });
      };
      
    });
  }

  getOrderDetails(id:number){
    return this.orderService.getOrderDetailsByOrderId(id);
  }

  sanitize(url: string) {
    //return url;
    return this.sanitizer.bypassSecurityTrustUrl(url);
    
  }
}
