import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import { application } from 'src/app/server/server.service';
import { Brand } from '../brands/brand.model';
import { ProductCategory } from '../productCategories/product-categories.model';
import { ProductsubCategoriesService } from '../productCategories/productsub-categories.service';
import { Products } from './product.model';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  constructor(private http:HttpClient,
    private service:ProductsubCategoriesService) { }

  readonly url = application.baseUrl + "/Products";
  readonly cat = application.baseUrl + "/ProductCategory";
  readonly sub = application.baseUrl + "/ProductSubCategory";
  readonly brands = application.baseUrl + "/Brands";
   

  formData:Products=new Products();
  list:Products[];
  catList:ProductCategory[];
  brandList:Brand[];

  postProduct(form:FormGroup){
    return this.http.post(this.url,form);
  }

  putProduct(){
    return this.http.put(`${this.url}/${this.formData.brandId}`,this.formData);
  }

  deleteProduct(id:number){
    return this.http.delete(`${this.url}/${id}`);
  }

  refreshList(){
    this.http.get(this.url)
    .toPromise()
    .then(res=> this.list = res as Products[]);
  }

  getAllProducts(){
    return this.http.get(this.url);
  }
   getCategory(){
     return this.http.get(this.cat);
   }
  
  getSubCategoryByCategoryId(id:number){
   return this.service.getSubCategoryByCategoryId(id);
  }
  
  getBrands(){
    return this.http.get(this.brands);
  }
  getAllCategories(){
    return this.http.get(this.cat)
    .toPromise()
    .then(res=>this.catList=res as ProductCategory[])
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
}
