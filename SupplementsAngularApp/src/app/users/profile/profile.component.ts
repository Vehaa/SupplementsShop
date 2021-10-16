import { formatDate } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable, Subscriber } from 'rxjs';
import { City } from 'src/app/shared/cities/city.model';
import { User } from 'src/app/shared/users/user.model';
import { UsersService } from 'src/app/shared/users/users.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: [

  ]
})
export class ProfileComponent implements OnInit {

  myImage: Observable<any>;
  theFile: any = null;
  messages: string[] = [];
  logo64: string;
  photo: string;
  user:User;
  MAX_SIZE: number = 3048576;
  base="data:image/png;charset=utf-8;base64,";


  constructor(public userService: UsersService,
    private toastr: ToastrService,
    private router: Router,
    private sanitizer: DomSanitizer) {
    this.userService.getCities().subscribe(data => this.cityList = data);
  }

  ngOnInit(): void {
    this.userService.getProfile().subscribe(res=>{
      this.user=res as User;
      this.editClient(this.user);
    })
  }

  form = new FormGroup({
    userName: new FormControl(''),
    firstName: new FormControl(''),
    lastName: new FormControl(''),
    email: new FormControl(''),
    phone: new FormControl(''),
    birthDate: new FormControl(''),
    address: new FormControl(''),
    cityId: new FormControl(''),
    passwordHash: new FormControl(''),
    passwordSalt: new FormControl(''),
    photoAsBase64: new FormControl(''),
    userId: new FormControl(''),  
    status: new FormControl(''),
    comments: new FormControl('')
  });
  form2 = new FormGroup({
    oldpassword: new FormControl(''),
    password: new FormControl(''),
    passwordConfirmation: new FormControl('')
  });
  public isVisible = false;
  cityList: City[];


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
  visibleDiv(div: boolean) {
    this.isVisible = div;
  }

  updatePassword(form2: FormGroup) {
    if (confirm("Da li ste sigurni da želite spremiti izmjene?")) {    
      this.form2.patchValue({
        oldpassword:form2.controls['oldpassword'].value,
        password:form2.controls['password'].value,
        passwordConfirmation:form2.controls['passwordConfirmation'].value
      });
      this.userService.putPassword(this.form2.value).
        subscribe(res => {
          this.toastr.success('Nova lozinka uspješno postavljena!', 'Account');
          this.form2.reset();

        },
        err=>{
          console.log(err);
          if (err.status >= 400){
            var mes= err.error;
            this.toastr.error(mes, 'GREŠKA');
  
          }
        });
    }
  }

  updateRecord(form: FormGroup) {
    if (confirm("Da li ste sigurni da želite spremiti izmjene?")){
      if(this.logo64!=null){
        this.form.patchValue({
          photoAsBase64:this.logo64
        });
      }
      this.userService.putUser(form.value)
        .subscribe((res: any) => {
          this.toastr.success('Podaci uspješno izmjenjeni!', 'Account');
          this.router.navigate(['/User/Account']);
        },
        err => {
          console.log(err);
          if (err.status >= 400){
            var mes= err.error;
            this.toastr.error(mes, 'GREŠKA');
  
          }
        }
        );
    }
    

  }

  onSubmit(form: FormGroup) {
    this.updateRecord(form);
  }


  editClient(client:User){
    this.form.patchValue({
      firstName:client.firstName,
      lastName:client.lastName,
      email:client.email,
      birthDate:formatDate(client.birthDate,'yyyy-MM-dd','en'),
      phone:client.phone,
      address:client.address,
      cityId:client.cityId,
      passwordHash:client.passwordHash,
      passwordSalt:client.passwordSalt,
      registrationDate:client.registrationDate,
      status:client.status,
      comments:client.comments,
      photoAsBase64:client.photoAsBase64,
      roleId:client.roleId,
      userName:client.userName,
      userId:client.userId
  
  
    })
    
  }

  sanitize(url: string) {
    //return url;
    return this.sanitizer.bypassSecurityTrustUrl(url);
  }
}
