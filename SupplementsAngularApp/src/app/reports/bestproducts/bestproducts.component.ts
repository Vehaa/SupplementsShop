import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { ToastrService } from 'ngx-toastr';
import { Products } from 'src/app/shared/products/product.model';
import { ReportsService } from 'src/app/shared/reports/reports.service';

@Component({
  selector: 'app-bestproducts',
  templateUrl: './bestproducts.component.html',
  styleUrls: [

  ]
})
export class BestproductsComponent implements OnInit {

  form=new FormGroup({
    dateFrom:new FormControl(''),
    dateTo:new FormControl('')
  });
  dateFrom:Date;
  dateTo:Date;
  div:boolean=false;
  list:Products[];
  base="data:image/png;charset=utf-8;base64,";

  constructor(private service:ReportsService,
    private toastr:ToastrService,
    private sanitizer: DomSanitizer) { }

  ngOnInit(): void {
  }


  onSubmit(){
    this.form.patchValue({
      dateFrom:this.form.controls['dateFrom'].value,
      dateTo:this.form.controls['dateTo'].value
    });
    
    this.dateFrom=this.form.controls['dateFrom'].value;
    this.dateTo=this.form.controls['dateTo'].value
    this.service.getTopProducts(this.form.value).toPromise().then(res=>{
      this.list=res as Products[];
      this.div=true;
    },
    err=>{
      this.toastr.warning(err.error);
    }
    );
    
  }

  sanitize(url: string) {
    //return url;
    return this.sanitizer.bypassSecurityTrustUrl(url);
  }

  onPrint() {
    window.print();
  }
}
