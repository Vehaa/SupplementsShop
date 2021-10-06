import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Employee } from 'src/app/shared/employees/employee.model';
import { EmployeeService } from 'src/app/shared/employees/employees.service';
import { City } from "src/app/shared/cities/city.model";
import { Router } from '@angular/router';


@Component({
  selector: 'app-employees-form',
  templateUrl: './employees-form.component.html',
  styleUrls: [

  ]
})
export class EmployeesFormComponent implements OnInit {

  constructor(public service:EmployeeService,
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
    this.service.postEmployee().subscribe(
      res=>{
        this.resetForm(form);
        this.service.getEmployees();
        this.toastr.success('Uposlenik uspješno dodan!','Uposlenik');
        this.router.navigate(['/Employees']);
      },
      err=>{
        this.toastr.error(err.error);
      }
    )
  }

  resetForm(form:NgForm){
    form.form.reset();
    this.service.formData=new Employee();
  }
  
  

}
