import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { application } from 'src/app/server/server.service';
import { Brand } from '../brands/brand.model';
import { ProductCategory } from '../productCategories/product-categories.model';
import { Products } from '../products/product.model';

@Injectable({
  providedIn: 'root'
})
export class HomeService {

  constructor(private http:HttpClient) { }

  
  readonly url = application.baseUrl + "/Products";
  readonly cat = application.baseUrl + "/ProductCategory";
  readonly sub = application.baseUrl + "/ProductSubCategory";
  readonly brands = application.baseUrl + "/Brands";
  httpOptions=new HttpHeaders().set('Authorization', 'Bearer '+ localStorage.getItem('token'));
   

  list:Products[];
  catList:ProductCategory[];
  brandList:Brand[];
  recommend:Products[];


  refreshList(){
    return this.http.get(this.url,{headers:this.httpOptions})
    .toPromise()
    .then(res=> this.list = res as Products[]);
  }
  getAllCategories(){
    return this.http.get(this.cat)
    .toPromise()
    .then(res=>this.catList=res as ProductCategory[])
  }
  getAllProducts(){
    return this.http.get(this.url,{headers:this.httpOptions});
  }

  getAllBrands(){
    return this.http.get(this.brands)
    .toPromise()
    .then(res=>this.brandList=res as Brand[])
  }

  getProductsByCategoryId(id:number){
    var params = new HttpParams();
    params = params.set('productCategoryId', id.toString());
    return this.http.get(this.url,{params:params})
    .toPromise()
    .then(res=>this.list=res as Products[]);

  }
  getProductById(id:number){
    return this.http.get(`${this.url}/${id}`,{headers:this.httpOptions});
  }

  getProductsByFilter(filter:string){
    var params = new HttpParams();
    params = params.set('filterName', filter);
    return this.http.get(this.url,{params:params})
    .toPromise()
    .then(res=>this.list=res as Products[]);
  }

  getProductsBySubCategoryId(id:number){
    var params = new HttpParams();

    params = params.set('productSubCategoryId', id.toString());
    return this.http.get(this.url,{params:params})
    .toPromise()
    .then(res=>this.list=res as Products[]);

  }

  getProductsByBrandId(id:number){
    var params = new HttpParams();

    params = params.set('brandId', id.toString());
    return this.http.get(this.url,{params:params})
    .toPromise()
    .then(res=>this.list=res as Products[]);

  }
  getProductsByName(name:string){
    var params = new HttpParams();

    params =params.set('name', name);
    return this.http.get(this.url,{params:params})
    .toPromise()
    .then(res=>this.list=res as Products[]);

  }

  getRecommendedProducts(id:number){
    var params = new HttpParams();
    params = params.set('productId', id.toString());
    params = params.set('filterName', 'recommend');

    return this.http.get(this.url,{params:params});
  }
}
