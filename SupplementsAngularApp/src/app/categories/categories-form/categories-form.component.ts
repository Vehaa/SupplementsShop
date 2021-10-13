import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
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
    private toastr: ToastrService,
    private router: Router) { }

  ngOnInit(): void {
    this.service.refreshList();
  }

  insertCategory(form: NgForm) {
    this.service.postCategory().subscribe(
      res => {
        this.resetForm(form);
        this.service.refreshList();
        this.reloadCurrentRoute();
        this.toastr.success('Kategorija uspješno dodana!', 'Kategorije proizvoda')
      },
      err => {
        this.toastr.error(err.error);
      }
    );
  }


  resetForm(form: NgForm) {
    form.form.reset();
    this.service.formData = new ProductCategory();
  }

  onSubmitCategory(form: NgForm) {
    this.insertCategory(form);
  }

  reloadCurrentRoute() {
    const currentUrl = this.router.url;
    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
    this.router.onSameUrlNavigation = 'reload';
    this.router.navigate([currentUrl]);

  };
}
