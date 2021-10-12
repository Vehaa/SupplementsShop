import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { application } from 'src/app/server/server.service';
import { Products } from '../products/product.model';
import { ReportRequest } from '../reportsModels/reportrequest.model';

@Injectable({
  providedIn: 'root'
})
export class ReportsService {

  readonly rep = application.baseUrl + "/Reports/Earning";
  readonly pro = application.baseUrl + "/Reports/TopProducts";
  readonly cus = application.baseUrl + "/Reports/TopCustomers";
 
  list:Products[];



  constructor(private http:HttpClient) { }


  getEarningReport(p:ReportRequest){
    const httpOptions=new HttpHeaders().set('Authorization', 'Bearer '+ localStorage.getItem('token'));
    
    var params = new HttpParams();
    params = params.set('dateFrom',p.dateFrom.toString());
    params = params.set('dateTo',p.dateTo.toString());
    return this.http.get(this.rep,{headers:httpOptions,params:params});

  }

  getTopProducts(p:ReportRequest){
    const httpOptions=new HttpHeaders().set('Authorization', 'Bearer '+ localStorage.getItem('token'));
    
    var params = new HttpParams();
    params = params.set('dateFrom',p.dateFrom.toString());
    params = params.set('dateTo',p.dateTo.toString());
    return this.http.get(this.pro,{headers:httpOptions,params:params});

  }

  getTopCustomers(p:ReportRequest){
    const httpOptions=new HttpHeaders().set('Authorization', 'Bearer '+ localStorage.getItem('token'));
    
    var params = new HttpParams();
    params = params.set('dateFrom',p.dateFrom.toString());
    params = params.set('dateTo',p.dateTo.toString());
    return this.http.get(this.cus,{headers:httpOptions,params:params});

  }
}
