import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Client } from 'src/app/shared/clients/client.model';
import { ReportsService } from 'src/app/shared/reports/reports.service';

@Component({
  selector: 'app-bestcustomers',
  templateUrl: './bestcustomers.component.html',
  styleUrls: [

  ]
})
export class BestcustomersComponent implements OnInit {

  form=new FormGroup({
    dateFrom:new FormControl(''),
    dateTo:new FormControl('')
  });
  div:boolean=false;
  list:Client[];

  dateFrom:Date;
  dateTo:Date;
  constructor(public service:ReportsService,
    private toastr:ToastrService) { }

  ngOnInit(): void {
  }

  onSubmit(){
    this.form.patchValue({
      dateFrom:this.form.controls['dateFrom'].value,
      dateTo:this.form.controls['dateTo'].value
    });
    this.dateFrom=this.form.controls['dateFrom'].value;
    this.dateTo=this.form.controls['dateTo'].value;
    this.service.getTopCustomers(this.form.value).toPromise().then(res=>{
      this.list=res as Client[];
      this.div=true;
    },
    err=>{
      this.toastr.warning(err.error);
    }
    );
  }
}
