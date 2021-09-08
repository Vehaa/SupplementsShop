import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UsersService } from 'src/app/shared/users/users.service';

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
  constructor(private service: UsersService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {
    if (localStorage.getItem('token') != null)
          this.router.navigate(['/Products']);

  }

  onSubmit(form: NgForm) {
    if(this.service.login(form.value)!=null)
    this.isLoged=true;
    this.service.login(form.value).subscribe(
      (res: any) => {
        localStorage.setItem('token', res);
        this.router.navigateByUrl('/Products');
      },
      err => {
        if (err.status == 400){
          var mes= err.status.message;
          this.toastr.error(mes, 'Authentication failed.');

        }
        else
          console.log(err);
      }
    );
  }

}
