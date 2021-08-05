import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule,ReactiveFormsModule  } from "@angular/forms";
import { RouterModule, Routes } from '@angular/router';
import { RouteNames } from "./route-nav";



import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { ToastrModule } from "ngx-toastr";
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';



import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CitiesComponent } from './cities/cities.component';
import { CitiesFormComponent } from './cities/cities-form/cities-form.component';
import { ClientsComponent } from './clients/clients.component';
import { ClientsFormComponent } from './clients/clients-form/clients-form.component';
import { EmployeesComponent } from './employees/employees.component';
import { EmployeesFormComponent } from './employees/employees-form/employees-form.component';
import { ClientsEditFormComponent } from './clients/clients-edit-form/clients-edit-form.component';
import { EmployeesEditFormComponent } from './employees/employees-edit-form/employees-edit-form.component';
import { BrandsComponent } from './brands/brands.component';
import { BrandsFormComponent } from './brands/brands-form/brands-form.component';



const appRoutes:Routes=[
  {path:'Users', component:ClientsComponent},
  {path:'Users/Add', component:ClientsFormComponent},
  {path:'Users/Edit/:id', component:ClientsEditFormComponent},
  {path:'Cities', component:CitiesComponent},
  {path:'Employees', component:EmployeesComponent},
  {path:'Employees/Add', component:EmployeesFormComponent},
  {path:'Employees/Edit/:id', component:EmployeesEditFormComponent},
  {path:'Brands', component:BrandsComponent}


];

@NgModule({
  declarations: [
    AppComponent,
    CitiesComponent,
    CitiesFormComponent,
    ClientsComponent,
    ClientsFormComponent,
    EmployeesComponent,
    EmployeesFormComponent,
    ClientsEditFormComponent,
    EmployeesEditFormComponent,
    BrandsComponent,
    BrandsFormComponent,

  ],
  imports: [
    BrowserModule,
    NgbModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    MatDatepickerModule,
    MatNativeDateModule,
    MatFormFieldModule,
    MatInputModule ,
    RouterModule.forRoot(appRoutes,{ onSameUrlNavigation: 'reload' })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
