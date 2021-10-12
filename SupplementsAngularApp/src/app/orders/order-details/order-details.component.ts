import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Orders } from 'src/app/shared/orders/order.model';
import { OrdersService } from 'src/app/shared/orders/orders.service';

@Component({
  selector: 'app-order-details',
  templateUrl: './order-details.component.html',
  styleUrls: [

  ]
})
export class OrderDetailsComponent implements OnInit {

  reason: boolean = false;
  displayReason:boolean=false;
  reasonText:string="";
  order: any;
  orderId: number;
  client: any;
  base = "data:image/png;charset=utf-8;base64,";
  Total: number = 0;
  orderStatusName: string;
  public Odobreno = false;
  public Odbijeno = false;
  public Isporuceno = false;
  text = new FormControl('');


  form2 = new FormGroup({
    orderStatusName: new FormControl(''),
    reason: new FormControl('')
  });


  constructor(private route: ActivatedRoute,
    public orderService: OrdersService,
    private sanitizer: DomSanitizer,
    private router: Router,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const orderId = +params.get('id');
      if (orderId) {
        this.getOrderDetails(orderId).subscribe(res => {
          this.order = res as Orders;
          this.orderId = orderId;
          for (let i of this.order) {
            this.orderStatusName = i.orderStatusName;
            this.reasonText=i.reason;
            if(this.reasonText!=null){
              this.displayReason=true;
            }
          }
          for (var i of this.order) {
            this.client = i.client;
            for (var o of i.orderDetailsList) {
              this.Total += o.totalPrice;
            }
          }

        });
      };

    });
  }

  getOrderDetails(id: number) {
    return this.orderService.getOrderDetailsByOrderId(id);
  }

  sanitize(url: string) {
    //return url;
    return this.sanitizer.bypassSecurityTrustUrl(url);

  }
  reloadCurrentRoute() {
    const currentUrl = this.router.url;
    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
    this.router.onSameUrlNavigation = 'reload';
    this.router.navigate([currentUrl]);

  };

  newOrdersCount(){
    
  }

  isOdobrena() {
    if (this.orderStatusName == "Odobrena") {
      return true;
    }
    return false;
  }

  isOdbijena() {
    if (this.orderStatusName == "Odbijena") {
      return true;
    }
    return false;
  }

  isIsporucena() {
    if (this.orderStatusName == "Isporučena") {
      return true;
    }
    return false;
  }
  setReason(bool:boolean){
    this.reason = bool;
    this.setOdbijena();
  }

  setOdobrena() {
    if (confirm("Da li ste sigurni da želite spremiti izmjene?")) {
      this.form2.patchValue({
        orderStatusName: "Odobrena"
      });
      this.order.orderStatusName = "Odobrena";
      this.orderService.putOrder(this.orderId, this.form2.value);
      this.getOrderDetails(this.orderId);
      this.orderService.setCount();
      this.orderService.getAllOrders();
      this.router.navigate(['/Orders']);
      this.toastr.success('Narudžba uspješno ODOBRENA!', 'NARUDŽBE');

    }
  }

  setOdbijena() {
    if (this.text.value != '') {
      if (confirm("Da li ste sigurni da želite spremiti izmjene?")) {
        this.form2.patchValue({
          orderStatusName: "Odbijena",
          reason: this.text.value
        });
        this.orderService.putOrder(this.orderId, this.form2.value);
        this.orderStatusName = "Odbijena";
        this.orderService.getOrderDetailsByOrderId(this.orderId);
        this.orderService.setCount();
        this.orderService.getAllOrders();
        this.router.navigate(['/Orders']);
        
        this.toastr.error('Narudžba uspješno ODBIJENA!', 'NARUDŽBE');

      }
    }
    else{
      this.toastr.error("Molimo vas da unesete razlog za odbijanje narudžbe!");
    }

  }


  setIsporucena() {
    if (confirm("Da li ste sigurni da želite spremiti izmjene?")) {
      this.form2.patchValue({
        orderStatusName: "Isporučena"
      });
      this.orderService.putOrder(this.orderId, this.form2.value);
      this.orderStatusName = "Isporučena";
      this.orderService.getOrderDetailsByOrderId(this.orderId);
      this.orderService.setCount();

      this.orderService.getAllOrders();
      this.router.navigate(['/Orders']);
      this.toastr.info('Narudžba uspješno ISPORUČENA!', 'NARUDŽBE');

    }
  }

  



}
