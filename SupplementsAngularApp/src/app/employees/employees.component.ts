import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { EmployeeService } from '../shared/employees/employees.service';

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styles: [
  ]
})
export class EmployeesComponent implements OnInit {

  constructor(public service:EmployeeService,
    private toastr:ToastrService,
    private _router:Router) {
    }

  ngOnInit(): void {
    this.service.getEmployees();
  }
  cityID:number;
  

  onDelete(id:number){
    if(confirm('Da li ste sigurni da želite izbrisati?'))
    {
      this.service.deleteEmployee(id)
    .subscribe(
      res=>{
        this.service.getEmployees();
        this.toastr.error("Brisanje uspješno!","Uposlenici");
      },
      err=>{console.log(err)}
    )
    }
    
  }
  onEditEmployee(id:number){
    this._router.navigate(['/Employees/Edit',id]);
  }

}
