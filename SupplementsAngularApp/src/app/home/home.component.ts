import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../shared/auth/auth.service';
import { CartService } from '../shared/cart/cart.service';
import { HomeService } from '../shared/home/home.service';
import { RatingService } from '../shared/rating/rating.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: [

  ]
})
export class HomeComponent implements OnInit {

  public productList:any;
  public qty=new FormControl('1');
  constructor(public service:HomeService,
    private toastr:ToastrService,
    private _router:Router,
    private sanitizer: DomSanitizer,
    private cartService:CartService,
    private authService:AuthService) { }

  ngOnInit(): void {
    this.service.getAllProducts().subscribe(res=>{
      this.productList=res;


      
    });
    
    this.service.refreshList();
    this.service.getAllCategories();
    this.service.getAllBrands();
  }

  base="data:image/png;charset=utf-8;base64,";

 
  sanitize(url: string) {
    //return url;
    return this.sanitizer.bypassSecurityTrustUrl(url);
    
  }
  
  catProducts(id:number){
    this.service.getProductsByCategoryId(id);
  }

  productDetails(id:number){
    this._router.navigate(['/ProductDetails',id]);
  }
  allProducts(){
    this.service.refreshList();
  }

  subProducts(id:number){
    this.service.getProductsBySubCategoryId(id);
  }

  brandProducts(id:number){
    this.service.getProductsByBrandId(id);
  }

  productsByName(name:string){
    this.service.getProductsByName(name);
  }

  addtoCart(item:any){
    item.unitOnOrder=this.qty.value;
    this.cartService.addtoCart(item);
    this.toastr.success('Proizvod uspješno dodan u korpu!', 'Korpa');
  }

  canActivate(){
    return this.authService.isClient();
  }

  isLogged(){
    return this.authService.IsLoggedIn();
  }

  getSorted(value:string){
    this.service.getProductsByFilter(value);
  }

}
