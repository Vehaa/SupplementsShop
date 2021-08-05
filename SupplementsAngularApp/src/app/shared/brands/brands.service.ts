import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import { application } from 'src/app/server/server.service';
import { Brand } from './brand.model';

@Injectable({
  providedIn: 'root'
})
export class BrandsService {

  constructor(private http:HttpClient) {}
  readonly url = application.baseUrl + "/Brands";
   

  formData:Brand=new Brand();
  list:Brand[];

  postBrand(form:FormGroup){
    return this.http.post(this.url,form);
  }

  putBrand(){
    return this.http.put(`${this.url}/${this.formData.brandId}`,this.formData);
  }

  deleteBrand(id:number){
    return this.http.delete(`${this.url}/${id}`);
  }

  refreshList(){
    this.http.get(this.url)
    .toPromise()
    .then(res=> this.list = res as Brand[]);
  }

  public GetAllBrands=():Observable<any>=>{
    return this.http.get(this.url);
  }
}
