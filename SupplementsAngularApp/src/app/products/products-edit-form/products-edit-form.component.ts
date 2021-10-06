import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable, Subscriber } from 'rxjs';
import { Brand } from 'src/app/shared/brands/brand.model';
import { ProductCategory } from 'src/app/shared/productCategories/product-categories.model';
import { ProductSubCategory } from 'src/app/shared/productCategories/product-subcategories.model';
import { ProductsubCategoriesService } from 'src/app/shared/productCategories/productsub-categories.service';
import { Products } from 'src/app/shared/products/product.model';
import { ProductsService } from 'src/app/shared/products/products.service';

@Component({
  selector: 'app-products-edit-form',
  templateUrl: './products-edit-form.component.html',
  styleUrls: [
    
  ]
})
export class ProductsEditFormComponent implements OnInit {

  base="data:image/png;charset=utf-8;base64,";
  MAX_SIZE: number = 3048576;
  theFile: any = null;
  messages: string[] = [];
  logo64: string;
  photo64: string;
  myImage: Observable<any>;
  constructor(public service: ProductsService,
    private subServ:ProductsubCategoriesService,
    private toastr: ToastrService,
    private route:ActivatedRoute,
    private router: Router,
    private sanitizer: DomSanitizer) {
      this.service.getCategory().subscribe(data => this.catList = <ProductCategory[]>data);
      this.service.getBrands().subscribe(res => this.brandsList = <Brand[]>res);

     }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params =>{
      const productId=+params.get('id');
      if(productId){
        this.getProduct(productId);
      }
      this.productId=productId;
    });

  }

  catId: number;
  subId:number=0;
  catList: ProductCategory[];
  subList: ProductSubCategory[];
  brandsList: Brand[];
  cat: ProductCategory;
  productId:number=0;
  val:number;
  Product:Products;

  
  form = new FormGroup({
    productId: new FormControl(),
    name: new FormControl(),
    description: new FormControl(),
    photoAsBase64: new FormControl(),
    unitPrice: new FormControl(),
    unitInStock: new FormControl(),
    brandId: new FormControl(),
    productCategoryId: new FormControl(),
    productSubCategoryId: new FormControl(),
    discount: new FormControl(),

  });


  getSubs(value:number) {
    this.service.getSubCategoryByCategoryId(value).subscribe(res => this.subList = <ProductSubCategory[]>res);
  }
  sanitize(url: string) {
    //return url;
    return this.sanitizer.bypassSecurityTrustUrl(url);
  }
  onFileChange(event) {
    this.theFile = null;
    if (event.target.files && event.target.files.length > 0) {
        // Don't allow file sizes over 3MB
        if (event.target.files[0].size < this.MAX_SIZE) {
            // Set theFile property
            this.theFile = event.target.files[0];
            this.convertToBase64(this.theFile);
            
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


  getProduct(id:number){
    this.service.getProductById(id).subscribe(
      (product:Products)=>this.editProduct(product)
    );
    this.service.getProductById(id).subscribe(res=>
      this.Product=res as Products);
      this.editProduct(this.Product);
  }

  editProduct(product:Products){
    if(product?.productCategoryId!=null){
    this.service.getSubCategoryByCategoryId(product.productCategoryId).subscribe(res => this.subList = <ProductSubCategory[]>res);
    }
    this.photo64=product?.photoAsBase64;
    this.form.patchValue({
      productId:product?.productId,
      name:product?.name,
      description:product?.description,
      photoAsBase64:product?.photoAsBase64,
      unitPrice:product?.unitPrice,
      unitInStock:product?.unitInStock,
      brandId:product?.brandId,
      productCategoryId:product?.productCategoryId,
      productSubCategoryId:product?.productSubCategoryId,
      discount:product?.discount 
    });   
  }

  updateRecord(form:FormGroup) {
    if(this.logo64!=null){
     this.form.patchValue({
      photoAsBase64:this.logo64
     });
    }
    this.service.putProduct(this.productId,this.form.value)
    .subscribe(res=>{
      this.service.getAllProducts();
      this.toastr.success('Podaci o proizvodu uspješno izmjenjeni!','Proizvodi');
      this.router.navigate(['/Products']);
    })      
  }

  onSubmit(form:FormGroup){
    this.updateRecord(form);
  }
}
