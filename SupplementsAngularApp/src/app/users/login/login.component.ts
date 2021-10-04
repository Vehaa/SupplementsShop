import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UsersService } from 'src/app/shared/users/users.service';
import * as moment from "moment";
import { OrdersService } from 'src/app/shared/orders/orders.service';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styles: [
  ]
})
export class LoginComponent implements OnInit {

  formModel = {
    UserName: '',
    Password: ''
  }
    isLoged=false;
  constructor(private service: UsersService, private router: Router, private toastr: ToastrService,
    private orderService:OrdersService) { }

  ngOnInit(): void {
    if (localStorage.getItem('token') != null)
          this.router.navigate(['']);

  }

  onSubmit(form: NgForm) {
    if(this.service.login(form.value)!=null)
    this.isLoged=true;
    this.service.login(form.value).subscribe(
      (res: any) => {
        localStorage.setItem('token', res);
        this.router.navigateByUrl('').then(() => {
          window.location.reload();
        });
      },
      err => {
        if (err.status >= 400){
          var mes= err.error;
          this.toastr.error(mes, 'Authentication failed.');

        }
        else
          console.log(err);
      }
    );
  }


}
