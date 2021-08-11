import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { application } from 'src/app/server/server.service';
import { ProductCategory } from './product-categories.model';
import { ProductSubCategory } from './product-subcategories.model';

@Injectable({
  providedIn: 'root'
})
export class ProductCategoriesService {

  constructor(private http:HttpClient) { }

  readonly url = application.baseUrl + "/ProductCategory";
  readonly sub = application.baseUrl + "/ProductSubCategory";

  formData:ProductCategory=new ProductCategory();
  list:ProductCategory[];

  postCategory(){
    return this.http.post(this.url,this.formData);
  }

  putCategory(){
    return this.http.put(`${this.url}/${this.formData.productCategoryId}`,this.formData);
  }

  deleteCategory(id:number){
    return this.http.delete(`${this.url}/${id}`);
  }
  deleteSubCategory(id:number){
    return this.http.delete(`${this.sub}/${id}`);
  }

  refreshList(){
    this.http.get(this.url)
    .toPromise()
    .then(res=> this.list = res as ProductCategory[]);
  }

  public GetAllCategories=():Observable<any>=>{
    return this.http.get(this.url);
  }

  
}
