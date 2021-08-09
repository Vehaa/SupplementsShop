import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { application } from '../../server/server.service';
import { ProductSubCategory } from './product-subcategories.model';

@Injectable({
  providedIn: 'root'
})
export class ProductsubCategoriesService {

  constructor(private http:HttpClient) { }

  readonly url = application.baseUrl + "/ProductSubCategory";
  readonly categoryUrl=application.baseUrl + "/ProductCategory";


  formData:ProductSubCategory=new ProductSubCategory();
  list:ProductSubCategory[];

  postSubCategory(){
    return this.http.post(this.url,this.formData);
  }

  putSubCCategory(){
    return this.http.put(`${this.url}/${this.formData.productSubCategoryId}`,this.formData);
  }

  deleteSubCCategory(id:number){
    return this.http.delete(`${this.url}/${id}`);
  }

  refreshList(){
    this.http.get(this.url)
    .toPromise()
    .then(res=> this.list = res as ProductSubCategory[]);
  }

  public GetAllSubCCategories=():Observable<any>=>{
    return this.http.get(this.url);
  }

  getCategories():Observable<any[]>{
    return this.http.get<any[]>(this.categoryUrl);
  }
}
