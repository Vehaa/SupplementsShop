import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ProductCategory } from 'src/app/shared/productCategories/product-categories.model';
import { ProductCategoriesService } from 'src/app/shared/productCategories/product-categories.service';

@Component({
  selector: 'app-categories-form',
  templateUrl: './categories-form.component.html',
  styleUrls: [
    
  ]
})
export class CategoriesFormComponent implements OnInit {

  constructor(public service: ProductCategoriesService,
    private toastr:ToastrService) { }

  ngOnInit(): void {
  }

  insertCategory(form:NgForm){
    this.service.postCategory().subscribe(
      res=>{
        this.resetForm(form);
        this.service.refreshList();
        this.toastr.success('Kategorija uspješno dodana!','Kategorije proizvoda')
      },
      err=>{console.log(err);}
    )
  }
 

  resetForm(form:NgForm){
    form.form.reset();
    this.service.formData=new ProductCategory();
  }

  onSubmitCategory(form:NgForm){
    this.insertCategory(form);
  }

  
}
