import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule,ReactiveFormsModule  } from "@angular/forms";
import { RouterModule, Routes } from '@angular/router';



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
import { CategoriesComponent } from './categories/categories.component';
import { CategoriesFormComponent } from './categories/categories-form/categories-form.component';
import { CategoriesSubFormComponent } from './categories/categories-sub-form/categories-sub-form.component';
import { ProductsComponent } from './products/products.component';
import { ProductsFormComponent } from './products/products-form/products-form.component';
import { ProductsEditFormComponent } from './products/products-edit-form/products-edit-form.component';


const appRoutes:Routes=[
  {path:'Users', component:ClientsComponent},
  {path:'Users/Add', component:ClientsFormComponent},
  {path:'Users/Edit/:id', component:ClientsEditFormComponent},
  {path:'Cities', component:CitiesComponent},
  {path:'Employees', component:EmployeesComponent},
  {path:'Employees/Add', component:EmployeesFormComponent},
  {path:'Employees/Edit/:id', component:EmployeesEditFormComponent},
  {path:'Brands', component:BrandsComponent},
  {path:'ProductCategory', component:CategoriesComponent},
  {path:'Products', component:ProductsComponent},
  {path:'Products/Add', component:ProductsFormComponent},
  {path:'Products/Edit/:id', component:ProductsEditFormComponent}



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
    CategoriesComponent,
    CategoriesFormComponent,
    CategoriesSubFormComponent,
    ProductsComponent,
    ProductsFormComponent,
    ProductsEditFormComponent

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
