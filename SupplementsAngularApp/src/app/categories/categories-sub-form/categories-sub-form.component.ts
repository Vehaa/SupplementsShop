import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ProductCategory } from 'src/app/shared/productCategories/product-categories.model';
import { ProductCategoriesService } from 'src/app/shared/productCategories/product-categories.service';
import { ProductSubCategory } from 'src/app/shared/productCategories/product-subcategories.model';
import { ProductsubCategoriesService } from 'src/app/shared/productCategories/productsub-categories.service';

@Component({
  selector: 'app-categories-sub-form',
  templateUrl: './categories-sub-form.component.html',
  styles: [
  ]
})
export class CategoriesSubFormComponent implements OnInit {

  categoryList:ProductCategory[];

  constructor(public service: ProductsubCategoriesService,
    public service2: ProductCategoriesService,
    private toastr:ToastrService) {
      this.service.getCategories().subscribe(data=>this.categoryList=data);

     }

  ngOnInit(): void {
    this.service.refreshList();
  }

  insertSubCategory(form:NgForm){
    this.service.postSubCategory().subscribe(
      res=>{
        this.resetForm(form);
        this.service.refreshList();
        this.service2.refreshList();
        this.toastr.success('Potkategorija uspješno dodana!','Kategorije proizvoda')
      },
      err=>{console.log(err);}
    )
  }
 

  resetForm(form:NgForm){
    form.form.reset();
    this.service.formData=new ProductSubCategory();
  }

  onSubmitSubCategory(form:NgForm){
    this.insertSubCategory(form);
  }
  
}
