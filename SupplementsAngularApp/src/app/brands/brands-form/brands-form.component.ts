import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable, Subscriber } from 'rxjs';
import { Brand } from 'src/app/shared/brands/brand.model';
import { BrandsService } from 'src/app/shared/brands/brands.service';

@Component({
  selector: 'app-brands-form',
  templateUrl: './brands-form.component.html',
  styles: [
  ]
})
export class BrandsFormComponent implements OnInit {
  MAX_SIZE: number = 3048576;
  theFile: any = null;
  messages: string[] = [];
  logo64: string;
  myImage: Observable<any>;

  form = new FormGroup({
    name: new FormControl(),
    description: new FormControl(),
    LogoAsBase64: new FormControl()
  });

  constructor(public service: BrandsService,
    private toastr: ToastrService,
    private router: Router) { }

  ngOnInit(): void {
  }

  insertRecord(form: FormGroup) {
    this.form.patchValue({
      LogoAsBase64: this.logo64,
      name: form.controls['name'].value,
      description: form.controls['description'].value
    });
    this.service.postBrand(this.form.value).subscribe(
      res => {
        this.reloadCurrentRoute();
        this.service.refreshList();
        this.toastr.success('Brend uspješno dodan!', 'Brendovi')
      },
      err => { console.log(err); }
    )
  }

  resetForm(form: FormGroup) {
    form.reset();
    this.service.formData = new Brand();
  }

  onSubmit(form: FormGroup) {
    this.insertRecord(form);
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

  reloadCurrentRoute() {
    const currentUrl = this.router.url;
    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
    this.router.onSameUrlNavigation = 'reload';
    this.router.navigate([currentUrl]);

  };
  convertToBase64(file: File) {
    this.myImage = new Observable((subscriber: Subscriber<any>) => {
      this.readFile(file, subscriber);
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
