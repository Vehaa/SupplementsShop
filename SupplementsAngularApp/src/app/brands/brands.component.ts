import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { BrandsService } from '../shared/brands/brands.service';

@Component({
  selector: 'app-brands',
  templateUrl: './brands.component.html',
  styles: [
  ]
})
export class BrandsComponent implements OnInit {

 

  constructor(public service: BrandsService,
    private toastr:ToastrService) { }

  ngOnInit(): void {
    this.service.refreshList();
  }

  onDelete(id:number){
    if(confirm('Da li ste sigurni da želite izbrisati?'))
    {
      this.service.deleteBrand(id)
    .subscribe(
      res=>{
        this.service.refreshList();
        this.toastr.error("Brisanje uspješno!","Gradovi");
      },
      err=>{console.log(err)}
    )
    }
    
  }

}
