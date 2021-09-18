import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CartService } from 'src/app/shared/cart/cart.service';
import { Client } from 'src/app/shared/clients/client.model';
import { Orders } from 'src/app/shared/orders/order.model';
import { OrdersService } from 'src/app/shared/orders/orders.service';
import { Products } from 'src/app/shared/products/product.model';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: [

  ]
})
export class CartComponent implements OnInit {

  public products: any=[];
  public grandTotal :number=0;
  public Total :number=0;
  public shipping:number=0;
  order:Orders=new Orders();
  userId:number;
  ime:string;
  prezime:string;
  grad:string;
  adresa:string;
  telefon:string;
  Klijent:Client;
  racun:boolean=false;

  base="data:image/png;charset=utf-8;base64,";

  form=new FormGroup({
    ime:new FormControl(''),
    prezime:new FormControl(''),
    adresa:new FormControl(''),
    grad:new FormControl(''),
    telefon:new FormControl('')
  });

  constructor(private cartService:CartService,
    private orderService:OrdersService,
    private toastr:ToastrService,
    private sanitizer: DomSanitizer,
    private _router:Router  ) { }

  ngOnInit(): void {
    this.cartService.getProducts()
    .subscribe(res=>{
      this.products=res;
      this.grandTotal=0;
      for(let p of this.products){
        this.grandTotal+=(p.totalPrice * p.unitOnOrder);
      }
      if(this.grandTotal<100){
        this.shipping=7;
      }
      this.Total=this.grandTotal+this.shipping;
    })
  }

  removeItem(item:any){
    this.grandTotal-=item.totalPrice;
    this.Total.toFixed(2);
    this.cartService.removeCartItem(item);
    this.toastr.error('Proizvod uspješno uklonjen!', 'Korpa');
  }

  emptyCart(){
    this.cartService.removeAllCart();
    this.toastr.error('Proizvodi uspješno uklonjeni!', 'Korpa');

  }


 
  sanitize(url: string) {
    //return url;
    return this.sanitizer.bypassSecurityTrustUrl(url);
    
  }

  clickShop(){
    this._router.navigate([""]);
  }
  
  naruciClick(){
    this.orderService.getClientById().subscribe(
      (client:Client)=>this.loadClientBil(client)
    );
    this.racun=true;
  }
  loadClientBil(client:Client){
    this.userId=client.userId;
    this.ime=client.firstName;
    this.prezime=client.lastName;
    this.adresa=client.address;
    this.telefon=client.phone;
    this.grad=client.cityName;
  }

  submitOrder(){
    this.order.orderProductList=this.products;
    this.order.customerId=this.userId;
    this.orderService.postOrder(this.order);
    this.toastr.success("Narudžba uspješno kreirana!","Narudžbe");
  }


}
