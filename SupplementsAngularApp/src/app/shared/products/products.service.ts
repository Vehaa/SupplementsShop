import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import { application } from 'src/app/server/server.service';
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
  params = new HttpParams();

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

  public GetAllProducts=():Observable<any>=>{
    return this.http.get(this.url);
  }
   getCategory(){
     return this.http.get(this.cat);
   }
   getSubCategory(){
    return this.http.get(this.sub);
  }
  getSubCategoryByCategoryId(id:number){
   return this.service.getSubCategoryByCategoryId(id);
  }
  
  getBrands(){
    return this.http.get(this.brands);
  }
}
