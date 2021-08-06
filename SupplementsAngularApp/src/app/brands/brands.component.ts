import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { BrandsService } from '../shared/brands/brands.service';
import {DomSanitizer} from '@angular/platform-browser';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-brands',
  templateUrl: './brands.component.html',
  styles: [
  ]
})
export class BrandsComponent implements OnInit {
  base="data:image/png;charset=utf-8;base64,";
  image;
  Brands:Observable<any>;

  constructor(public service: BrandsService,
    private toastr:ToastrService,
    private sanitizer: DomSanitizer) { }

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
        this.toastr.error("Brisanje uspješno!","Brendovi");
      },
      err=>{console.log(err)}
    )
    }
    
  }
  sanitize(url: string) {
    //return url;
    return this.sanitizer.bypassSecurityTrustUrl(url);
  }

}
