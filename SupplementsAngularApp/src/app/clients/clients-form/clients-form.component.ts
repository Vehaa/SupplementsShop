import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Client } from 'src/app/shared/clients/client.model';
import { ClientsService } from 'src/app/shared/clients/clients.service';
import { City } from "src/app/shared/cities/city.model";
import { Router } from '@angular/router';

@Component({
  selector: 'app-clients-form',
  templateUrl: './clients-form.component.html',
  styles: [
  ]
})
export class ClientsFormComponent implements OnInit {

  constructor(public service:ClientsService,
    private toastr:ToastrService,
    private router:Router) {
      this.service.getCities().subscribe(data=>this.cityList=data);
    }

    cityList:City[];
    selectedCity:number;

  ngOnInit(): void {

  }

  onSubmit(form:NgForm){
      this.insertRecord(form);
  }
 

  insertRecord(form:NgForm){
    this.service.postClient().subscribe(
      res=>{
        this.resetForm(form);
        this.service.getClient();
        this.toastr.success('Klijent uspješno dodan!','Klijenti');
        this.router.navigate(['/Users']);

      },
      err=>{console.log(err);}
    )
  }

  resetForm(form:NgForm){
    form.form.reset();
    this.service.formData=new Client();
  }
  

}
