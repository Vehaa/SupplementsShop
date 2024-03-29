import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
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
  httpOptions=new HttpHeaders().set('Authorization', 'Bearer '+ localStorage.getItem('token'));


  formData:ProductSubCategory=new ProductSubCategory();
  list:ProductSubCategory[];
  params = new HttpParams();

  postSubCategory(){
    return this.http.post(this.url,this.formData,{headers:this.httpOptions});
  }

  putSubCategory(){
    return this.http.put(`${this.url}/${this.formData.productSubCategoryId}`,this.formData,{headers:this.httpOptions});
  }

  deleteSubCCategory(id:number){
    return this.http.delete(`${this.url}/${id}`,{headers:this.httpOptions});
  }

  refreshList(){
    this.http.get(this.url)
    .toPromise()
    .then(res=> this.list = res as ProductSubCategory[]);
  }

  public GetAllSubCCategories=():Observable<any>=>{
    return this.http.get(this.url);
  }

  getSubCategoryByCategoryId(id:number){
    this.params = this.params.set('productCategoryId', id.toString());
    return this.http.get(this.url,{params:this.params});
  }
  getCategories():Observable<any[]>{
    return this.http.get<any[]>(this.categoryUrl);
  }
}
