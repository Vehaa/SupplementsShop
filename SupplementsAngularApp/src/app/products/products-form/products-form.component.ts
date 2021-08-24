import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable, Subscriber } from 'rxjs';
import { Brand } from 'src/app/shared/brands/brand.model';
import { ProductCategory } from 'src/app/shared/productCategories/product-categories.model';
import { ProductSubCategory } from 'src/app/shared/productCategories/product-subcategories.model';
import { Products } from 'src/app/shared/products/product.model';
import { ProductsService } from 'src/app/shared/products/products.service';

@Component({
  selector: 'app-products-form',
  templateUrl: './products-form.component.html',
  styleUrls: [

  ]
})
export class ProductsFormComponent implements OnInit {

  constructor(public service: ProductsService,
    private toastr: ToastrService,
    private router: Router) {
    this.service.getCategory().subscribe(data => this.catList = <ProductCategory[]>data);
    this.service.getBrands().subscribe(res => this.brandsList = <Brand[]>res);

  }

  ngOnInit(): void {

  }

  MAX_SIZE: number = 3048576;
  theFile: any = null;
  messages: string[] = [];
  logo64: string;
  myImage: Observable<any>;
  catId: number;
  catList: ProductCategory[];
  subList: ProductSubCategory[];
  brandsList: Brand[];
  cat: ProductCategory;

  form = new FormGroup({
    name: new FormControl(),
    description: new FormControl(),
    LogoAsBase64: new FormControl(),
    unitPrice: new FormControl(),
    unitInStock: new FormControl(),
    brandId: new FormControl(),
    productCategoryId: new FormControl(),
    productSubCategoryId: new FormControl(),
    discount: new FormControl(),

  });

  onSubmit(form: FormGroup) {
    this.insertRecord(form);
  }

  insertRecord(form: FormGroup) {
    if(form.controls['productSubCategoryId'].value==null){
      this.form.patchValue({
        productSubCategoryId:null
      })
    };
    this.form.patchValue({
      LogoAsBase64: this.logo64,
      name: form.controls['name'].value,
      description: form.controls['description'].value,
      unitPrice: form.controls['unitPrice'].value,
      unitInStock: form.controls['unitInStock'].value,
      brandId: form.controls['brandId'].value,
      productCategoryId: form.controls['productCategoryId'].value,
      productSubCategoryId: form.controls['productSubCategoryId'].value,
      discount: form.controls['discount'].value
    });

    this.service.postProduct(this.form.value).subscribe(
      res => {
        this.resetForm(form);
        this.service.refreshList();
        this.toastr.success('Proizvod uspješno dodan!', 'Proizvodi');
        this.router.navigate(['/Products']);
      },
      err => { console.log(err); }
    )
  }
  resetForm(form: FormGroup) {
    form.reset();
    this.service.formData = new Products();
  }


  getSubs(value: number) {
    this.service.getSubCategoryByCategoryId(value).subscribe(res => this.subList = <ProductSubCategory[]>res);
  }

  onFileChange(event) {
    this.theFile = null;
    if (event.target.files && event.target.files.length > 0) {
        // Don't allow file sizes over 3MB
        if (event.target.files[0].size < this.MAX_SIZE) {
            // Set theFile property
            this.theFile = event.target.files[0];
            this.convertToBase64(this.theFile);
            console.log(this.theFile);
            
        }
        else {
            // Display error message
            this.messages.push("File: " + event.target.files[0].name + " is too large to upload.");
        }
    }
  }

  convertToBase64(file:File){
    this.myImage= new Observable((subscriber:Subscriber<any>)=>{
      this.readFile(file,subscriber);
    });
  }

  readFile(file: File, subscriber: Subscriber<any>) {
    const filereader = new FileReader();
    filereader.readAsDataURL(file);
    filereader.onload = () => {
      subscriber.next(filereader.result);
      this.logo64 = filereader.result.toString();
      subscriber.complete();


    }


  }

}
