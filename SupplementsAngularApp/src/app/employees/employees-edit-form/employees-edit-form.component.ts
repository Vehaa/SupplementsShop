import { formatDate } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { City } from 'src/app/shared/cities/city.model';
import { Employee } from 'src/app/shared/employees/employee.model';
import { EmployeeService } from 'src/app/shared/employees/employees.service';

@Component({
  selector: 'app-employees-edit-form',
  templateUrl: './employees-edit-form.component.html',
  styles: [
  ]
})
export class EmployeesEditFormComponent implements OnInit {

  cityList: City[];
  selectedCity: number;
  userId = 0;
  constructor(public service: EmployeeService,
    private toastr: ToastrService,
    private route: ActivatedRoute,
    private router: Router) {
    this.service.getCities().subscribe(data => this.cityList = data);

  }

  public ADComment = false;
  public ClientStatus = false;
  public isVisible = false;

  form = new FormGroup({
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
    picture: new FormControl(''),
    pictureThumb: new FormControl(''),
    userName: new FormControl(''),
    roleId: new FormControl(''),
    userId: new FormControl('')
  })
  form2 = new FormGroup({
    password: new FormControl(''),
    passwordConfirmation: new FormControl('')
  })

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const userId = +params.get('id');
      if (userId) {
        this.getEmployee(userId);
      }
      this.userId = userId;
    });

  }
  visibleDiv(div: boolean) {
    this.isVisible = div;
  }

  onSubmit(form: FormGroup) {
    this.updateRecord(form);
  }

  getEmployee(id: number) {
    this.service.getEmployeeById(id).subscribe(
      (employee: Employee) => this.editEmployee(employee),
      (err: any) => console.log(err)
    );
  }

  editEmployee(client: Employee) {
    this.ADComment = client.comments;
    this.ClientStatus = client.status;
    this.form.patchValue({
      firstName: client.firstName,
      lastName: client.lastName,
      email: client.email,
      birthDate: formatDate(client.birthDate, 'yyyy-MM-dd', 'en'),
      phone: client.phone,
      address: client.address,
      cityId: client.cityId,
      passwordHash: client.passwordHash,
      passwordSalt: client.passwordSalt,
      registrationDate: client.registrationDate,
      status: client.status,
      comments: client.comments,
      picture: client.picture,
      pictureThumb: client.pictureThumb,
      roleId: client.roleId,
      userName: client.userName,
      userId: client.userId


    })

  }
  reloadCurrentRoute() {
    const currentUrl = this.router.url;
    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
    this.router.onSameUrlNavigation = 'reload';
    this.router.navigate([currentUrl]);

  };

  setComments(AD: boolean) {
    if (confirm("Da li ste sigurni da želite spremiti izmjene?")) {
      this.form.patchValue({
        comments: AD
      });
      this.service.putEmployee(this.userId, this.form.value)
        .subscribe(res => {
          this.service.getEmployees();
          this.reloadCurrentRoute();

          if (AD)
            this.toastr.success('Uposlenik uspješno odobreni!', 'Uposlenici');
          else
            this.toastr.warning('Uposlenik uspješno zabranjeni!', 'Uposlenici');
        },
          err => { this.toastr.error(err.error); });
    }
  }

  setStatus(ST: boolean) {
    if (confirm("Da li ste sigurni da želite spremiti izmjene?")) {

      this.form.patchValue({
        status: ST
      });
      this.service.putEmployee(this.userId, this.form.value)
        .subscribe(res => {
          this.service.getEmployees();
          this.reloadCurrentRoute();
          if (ST) {
            this.toastr.success('Uposlenik uspješno AKTIVIRAN!', 'Uposlenici');
            this.ClientStatus = true;
          }
          else {
            this.toastr.warning('Uposlenik uspješno DEAKTIVIRAN!', 'Uposlenici');
            this.ClientStatus = false;
          }
        },
          err => { this.toastr.error(err.error); });
    }
  }

  updatePassword(form2: FormGroup) {
    if (confirm("Da li ste sigurni da želite spremiti izmjene?")) {
      this.form.patchValue({
        password: this.form2.controls['password'].value,
        passwordConfirmation: this.form2.controls['passwordConfirmation'].value
      });
      this.service.putEmployee(this.userId, this.form.value).
        subscribe(res => {
          this.service.getEmployees();
          this.toastr.success('Nova lozinka uspješno postavljena!', 'Uposlenici');
          this.form2.reset();

        },
          err => { this.toastr.error(err.error); });
    }
  }
  updateRecord(form: FormGroup) {
    this.service.putEmployee(this.userId, this.form.value)
      .subscribe(res => {
        this.service.getEmployees();
        this.toastr.success('Podaci o uposleniku uspješno izmjenjeni!', 'Uposlenici');
        this.router.navigate(['/Employees']);
      },
        err => { this.toastr.error(err.error); });

  }
}
