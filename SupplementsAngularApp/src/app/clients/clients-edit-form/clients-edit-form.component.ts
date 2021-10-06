import { Component, OnInit } from '@angular/core';
import { ClientsService } from 'src/app/shared/clients/clients.service';
import { ToastrService } from 'ngx-toastr';
import { City } from 'src/app/shared/cities/city.model';
import { FormControl, FormGroup, NgForm } from '@angular/forms';
import { Client } from 'src/app/shared/clients/client.model';
import { ActivatedRoute, Router } from '@angular/router';
import { formatDate } from '@angular/common' ;
import { first } from 'rxjs/operators';
import { HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-clients-edit-form',
  templateUrl: './clients-edit-form.component.html',
  styles: [
  ]
})


export class ClientsEditFormComponent implements OnInit {

  public ADComment = false;
  public ClientStatus=false;
  public isVisible = false;

  form=new FormGroup({
    firstName: new FormControl(''),
    lastName: new FormControl(''),
    email: new FormControl(''),
    phone: new FormControl(''),
    birthDate: new FormControl(''),
    address: new FormControl(''),
    cityId: new FormControl(''),
    password: new FormControl(''),
    passwordConfirmation: new FormControl(''),
    passwordHash: new FormControl(''),
    passwordSalt: new FormControl(''),
    registrationDate: new FormControl(''),
    status: new FormControl(''),
    comments: new FormControl(''),
    photoAsBase64: new FormControl(''),
    userName: new FormControl(''),
    roleId: new FormControl(''),
    userId: new FormControl('')
  })
  form2=new FormGroup({ 
    password: new FormControl(''),
    passwordConfirmation: new FormControl('')
  })
  constructor(public service:ClientsService,
    private toastr:ToastrService,
    private route:ActivatedRoute,
    private router:Router) {
      this.service.getCities().subscribe(data=>this.cityList=data);
    }

    cityList:City[];
    selectedCity:number;
    userId=0;


  ngOnInit(): void {
    this.route.paramMap.subscribe(params =>{
      const userId=+params.get('id');
      if(userId){
        this.getClient(userId);
      }
      this.userId=userId;
    });

    
  }
  visibleDiv(div:boolean){
    this.isVisible=div;
  }
  
  onSubmit(form:FormGroup){
    this.updateRecord(form);
}



updateRecord(form:FormGroup) {
  this.service.putClient(this.userId,this.form.value)
  .subscribe(res=>{
    this.service.getClient();
    this.toastr.success('Podaci o klijentu uspješno izmjenjeni!','Klijenti');
    this.router.navigate(['/Users']);
  })
    
}

updatePassword(form2:FormGroup){
  if(confirm("Da li ste sigurni da želite spremiti izmjene?")){
  this.form.patchValue({
    password:this.form2.controls['password'].value,
    passwordConfirmation:this.form2.controls['passwordConfirmation'].value
  });
    this.service.putClient(this.userId,this.form.value).
    subscribe(res=>{
      this.service.getClient();
      this.toastr.success('Nova lozinka uspješno postavljena!','Klijenti');
      this.form2.reset();

    })
  }
}

setComments(AD:boolean){
  if(confirm("Da li ste sigurni da želite spremiti izmjene?")){
  this.form.patchValue({
    comments:AD
  });
  this.service.putClient(this.userId,this.form.value)
  .subscribe(res=>{
    this.service.getClient();
    this.reloadCurrentRoute();

    if(AD){
      this.toastr.success('Komentari uspješno odobreni!','Klijenti');
      this.ADComment=true;
    }
      else{
        this.toastr.warning('Komentari uspješno zabranjeni!','Klijenti');
        this.ADComment=false;
      }
  });
  }
}

setStatus(ST:boolean){
  if(confirm("Da li ste sigurni da želite spremiti izmjene?")){

  this.form.patchValue({
    status:ST
  });
  this.service.putClient(this.userId,this.form.value)
  .subscribe(res=>{
    this.service.getClient();
    this.reloadCurrentRoute();
    if(ST){
      this.toastr.success('Klijent uspješno AKTIVIRAN!','Klijenti');
      this.ClientStatus=true;
    }
    else{
      this.toastr.warning('Klijent uspješno DEAKTIVIRAN!','Klijenti');
      this.ClientStatus=false;
    }
  });
  }
}

reloadCurrentRoute() {
  const currentUrl = this.router.url;
  this.router.routeReuseStrategy.shouldReuseRoute = () => false;
  this.router.onSameUrlNavigation = 'reload';
  this.router.navigate([currentUrl]);

  };



getClient(id:number){
  this.service.getClientById(id).subscribe(
    (client:Client)=>this.editClient(client),
    (err:any)=>console.log(err)
  );
}

editClient(client:Client){
  this.ADComment=client.comments;
  this.ClientStatus=client.status;
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

}
