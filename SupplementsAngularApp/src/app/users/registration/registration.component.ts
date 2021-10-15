import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { City } from 'src/app/shared/cities/city.model';
import { Client } from 'src/app/shared/clients/client.model';
import { ClientsService } from 'src/app/shared/clients/clients.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styles: [
  ]
})
export class RegistrationComponent implements OnInit {

  constructor(public service:ClientsService,
    private toastr:ToastrService,
    private router:Router) {
      this.service.getCities().subscribe(data=>this.cityList=data);
     }

  ngOnInit(): void {
    
  }

  cityList:City[];
  selectedCity:number;

  onSubmit(form:NgForm){
    this.insertRecord(form);
}


insertRecord(form:NgForm){
  this.service.registerClient().subscribe(
    res=>{
      this.resetForm(form);
      this.toastr.success('Uspješno ste se registrovali!');
      this.router.navigate(['/User/Login']);

    },
    err=>{
      console.log(err.error);
      this.toastr.error(err.errors);
    }
  )
}

resetForm(form:NgForm){
  form.form.reset();
  this.service.formData=new Client();
}

}
