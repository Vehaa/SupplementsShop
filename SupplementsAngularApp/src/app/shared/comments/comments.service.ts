import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { application } from 'src/app/server/server.service';
import { ClientsService } from '../clients/clients.service';
import { Comments } from './comments.model';

@Injectable({
  providedIn: 'root'
})
export class CommentsService {

  readonly url = application.baseUrl + "/Comments";
  httpOptions=new HttpHeaders().set('Authorization', 'Bearer '+ localStorage.getItem('token'));
  payLoad = JSON.parse(atob(localStorage.getItem('token').split('.')[1]));
  userId=this.payLoad.unique_name;
  constructor(private http:HttpClient,
    private clientService: ClientsService) { }


  list:Comments[];

  postComment(comment:Comments){
    return this.http.post(this.url,comment,{headers:this.httpOptions});
  }

  putComment(id:number,params:Comments){
    return this.http.put(`${this.url}/${id}`,params,{headers:this.httpOptions}).subscribe();
  }

  deleteComment(id:number){
    return this.http.delete(`${this.url}/${id}`,{headers:this.httpOptions});
  }

  getCommentsByProductId(id:number){
    var params = new HttpParams();
    params = params.set('productId', id.toString());
    return this.http.get(this.url,{params:params})
    .toPromise()
    .then(res=>this.list=res as Comments[]);
  }

  refreshList(id:number){
    var params = new HttpParams();
    params = params.set('productId', id.toString());
    this.http.get(this.url,{params:params})
    .toPromise()
    .then(res=>this.list=res as Comments[]);
  }

  getClientById(){
    return this.clientService.getClientById(this.userId);
  }
}
