import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ProductCategoriesService } from '../shared/productCategories/product-categories.service';
import { ProductsService } from '../shared/products/products.service';
import { ReportRequest } from '../shared/reportsModels/reportrequest.model';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: [

  ]
})
export class ProductsComponent implements OnInit {

  base = "data:image/png;charset=utf-8;base64,";
  image;
  constructor(public service: ProductsService,
    private toastr: ToastrService,
    private _router: Router,
    private sanitizer: DomSanitizer) { }
  productName = "";


  ngOnInit(): void {
    this.service.refreshList();
    this.service.getAllCategories();
    this.service.getAllBrands();
  }


  onDelete(id: number) {
    if (confirm('Da li ste sigurni da želite izbrisati?')) {
      this.service.deleteProduct(id)
        .subscribe(
          res => {
            this.service.refreshList();
            this.toastr.error("Brisanje uspješno!", "Proizvodi");
          },
          err => { console.log(err) }
        )
    }

  }
  onEditProduct(id: number) {
    this._router.navigate(['/Products/Edit', id]);
  }
  sanitize(url: string) {
    //return url;
    return this.sanitizer.bypassSecurityTrustUrl(url);
  }

  catProducts(id: number) {
    this.service.getProductsByCategoryId(id);
  }

  allProducts() {
    this.service.refreshList();
  }

  subProducts(id: number) {
    this.service.getProductsBySubCategoryId(id);
  }

  brandProducts(id: number) {
    this.service.getProductsByBrandId(id);
  }

  productsByName(name: string) {
    this.service.getProductsByName(name);
  }

}
