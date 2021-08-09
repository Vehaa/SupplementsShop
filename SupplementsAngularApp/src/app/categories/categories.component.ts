import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ProductCategoriesService } from '../shared/productCategories/product-categories.service';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styles: [
  ]
})
export class CategoriesComponent implements OnInit {

  subVisible:boolean=false;
  catVisible:boolean=false;
  constructor(public service: ProductCategoriesService,
    private toastr:ToastrService) { }

  ngOnInit(): void {
    this.service.refreshList();
  }

  onDeleteCategory(id:number){
    if(confirm('Da li ste sigurni da želite izbrisati?'))
    {
      this.service.deleteCategory(id)
    .subscribe(
      res=>{
        this.service.refreshList();
        this.toastr.error("Brisanje uspješno!","Kategorije proizvoda");
      },
      err=>{console.log(err)}
    )
    }
    
  }

  public visibleCatDiv(div:boolean){
    this.catVisible=div;
    this.subVisible=false;

  }
  public visibleSubDiv(div:boolean){
    this.subVisible=div;
    this.catVisible=false;
  }
  public odustani(){
    this.subVisible=false;
    this.catVisible=false;
  }
}
