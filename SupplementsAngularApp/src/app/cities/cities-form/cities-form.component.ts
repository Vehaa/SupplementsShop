import { Component, OnInit } from '@angular/core';
import {  ToastrService } from 'ngx-toastr';
import { CitiesService } from 'src/app/shared/cities/cities.service';
import { NgForm } from '@angular/forms';
import { City } from "src/app/shared/cities/city.model";

@Component({
  selector: 'app-cities-form',
  templateUrl: './cities-form.component.html',
  styles: [
  ]
})
export class CitiesFormComponent implements OnInit {

  constructor(public service: CitiesService,
    private toastr:ToastrService) { }

  ngOnInit(): void {
  }

  insertRecord(form:NgForm){
    this.service.postCity().subscribe(
      res=>{
        this.resetForm(form);
        this.service.refreshList();
        this.toastr.success('Grad uspješno dodan!','Gradovi')
      },
      err=>{console.log(err);}
    )
  }

  resetForm(form:NgForm){
    form.form.reset();
    this.service.formData=new City();
  }

  onSubmit(form:NgForm){
    this.insertRecord(form);
  }
}
