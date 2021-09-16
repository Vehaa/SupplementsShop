import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { HomeService } from 'src/app/shared/home/home.service';
import { Products } from 'src/app/shared/products/product.model';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: [

  ]
})
export class ProductDetailsComponent implements OnInit {

  constructor(public service:HomeService,
    private toastr:ToastrService,
    private _router:Router,
    private sanitizer: DomSanitizer,
    private route:ActivatedRoute) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params =>{
      const productId=+params.get('id');
      if(productId){
        this.getProduct(productId);
      }
    });
  }

  base="data:image/png;charset=utf-8;base64,";
  product:Products;
  photo:any;

  sanitize(url: string) {
    //return url;
    return this.sanitizer.bypassSecurityTrustUrl(url);

  }

  getProduct(id:number){
    this.service.getProductById(id).subscribe(
      res=>this.product=<Products>res); 

  }
}
