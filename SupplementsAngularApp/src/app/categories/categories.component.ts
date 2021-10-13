import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ProductCategoriesService } from '../shared/productCategories/product-categories.service';
import { ProductSubCategory } from '../shared/productCategories/product-subcategories.model';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styles: [
  ]
})
export class CategoriesComponent implements OnInit {

  subVisible: boolean = false;
  catVisible: boolean = false;
  constructor(public service: ProductCategoriesService,
    private toastr: ToastrService,
    private router: Router) { }

  ngOnInit(): void {
    this.service.refreshList();
  }

  onDeleteCategory(id: number) {
    if (confirm('Da li ste sigurni da želite izbrisati?')) {
      this.service.deleteCategory(id)
        .subscribe(
          res => {
            this.service.refreshList();
            this.reloadCurrentRoute();
            this.toastr.error("Brisanje uspješno!", "Kategorije proizvoda");
          },
          err => { this.toastr.error(err.error); }
        );
    }

  }
  onDeleteSubCategory(id: number) {
    if (confirm('Da li ste sigurni da želite izbrisati?')) {
      this.service.deleteSubCategory(id)
        .subscribe(
          res => {
            this.service.refreshList();
            this.toastr.error("Brisanje uspješno!", "Kategorije proizvoda");
          },
          err => { this.toastr.error(err.error); }
        );
    }

  }

  public visibleCatDiv(div: boolean) {
    this.catVisible = div;
    this.subVisible = false;

  }
  public visibleSubDiv(div: boolean) {
    this.subVisible = div;
    this.catVisible = false;
  }
  public odustani() {
    this.subVisible = false;
    this.catVisible = false;
  }
  reloadCurrentRoute() {
    const currentUrl = this.router.url;
    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
    this.router.onSameUrlNavigation = 'reload';
    this.router.navigate([currentUrl]);

  };
}
