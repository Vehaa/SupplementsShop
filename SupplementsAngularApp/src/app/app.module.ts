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
import { RegistrationComponent } from './users/registration/registration.component';
import { UserComponent } from './users/user.component';
import { ForbiddenComponent } from './forbidden/forbidden.component';
import { JwtHelperService,JWT_OPTIONS  } from '@auth0/angular-jwt';
import { LoginComponent } from './users/login/login.component';
import { RoleGuard } from './shared/auth/role.guard';
import { HomeComponent } from './home/home.component';
import { ProductDetailsComponent } from './home/product-details/product-details.component';
import { CartComponent } from './cart/cart/cart.component';
import { OrdersComponent } from './orders/orders.component';
import { MyordersComponent } from './myorders/myorders.component';
import { MyOrderDetailsComponent } from './myorders/my-order-details/my-order-details.component';
import { OrderDetailsComponent } from './orders/order-details/order-details.component';
import { NavHeaderComponent } from './nav-header/nav-header.component';
import { ProfileComponent } from './users/profile/profile.component';
import { ReportsComponent } from './reports/reports.component';

const appRoutes:Routes=[
  {path:'Users', component:ClientsComponent,canActivate:[RoleGuard],data:{permittedRoles:['Administrator','Uposlenik']} },
  {path:'Users/Add', component:ClientsFormComponent,canActivate:[RoleGuard],data:{permittedRoles:['Administrator','Uposlenik']}},
  {path:'Users/Edit/:id', component:ClientsEditFormComponent,canActivate:[RoleGuard],data:{permittedRoles:['Administrator','Uposlenik']}},
  {path:'Cities', component:CitiesComponent,canActivate:[RoleGuard],data:{permittedRoles:['Administrator']} },
  {path:'Employees', component:EmployeesComponent,canActivate:[RoleGuard],data:{permittedRoles:['Administrator']} },
  {path:'Employees/Add', component:EmployeesFormComponent,canActivate:[RoleGuard],data:{permittedRoles:['Administrator']}},
  {path:'Employees/Edit/:id', component:EmployeesEditFormComponent,canActivate:[RoleGuard],data:{permittedRoles:['Administrator']}},
  {path:'Brands', component:BrandsComponent,canActivate:[RoleGuard],data:{permittedRoles:['Administrator','Uposlenik']}},
  {path:'ProductCategory', component:CategoriesComponent,canActivate:[RoleGuard],data:{permittedRoles:['Administrator','Uposlenik']}},
  {path:'Products', component:ProductsComponent,canActivate:[RoleGuard],data:{permittedRoles:['Administrator','Uposlenik']}},
  {path:'Products/Add', component:ProductsFormComponent,canActivate:[RoleGuard],data:{permittedRoles:['Administrator','Uposlenik']}},
  {path:'Products/Edit/:id', component:ProductsEditFormComponent,canActivate:[RoleGuard],data:{permittedRoles:['Administrator','Uposlenik']}},
  {path:'User/Login', component:LoginComponent},
  {path:'User/Register', component:RegistrationComponent},
  {path:'User/Account', component:ProfileComponent,canActivate:[RoleGuard],data:{permittedRoles:['Administrator','Uposlenik','Klijent']}},
  {path:'Forbidden', component:ForbiddenComponent},
  {path:'', component:HomeComponent},
  {path:'ProductDetails/:id', component:ProductDetailsComponent},
  {path:'Cart', component:CartComponent,canActivate:[RoleGuard],data:{permittedRoles:['Klijent']}},
  {path:'Orders', component:OrdersComponent,canActivate:[RoleGuard],data:{permittedRoles:['Administrator','Uposlenik']}},
  {path:'OrderDetails/:id', component:OrderDetailsComponent,canActivate:[RoleGuard],data:{permittedRoles:['Administrator','Uposlenik']}},
  {path:'MyOrders', component:MyordersComponent,canActivate:[RoleGuard],data:{permittedRoles:['Klijent']}},
  {path:'MyOrderDetails/:id', component:MyOrderDetailsComponent,canActivate:[RoleGuard],data:{permittedRoles:['Klijent']}},
  {path:'Reports', component:ReportsComponent,canActivate:[RoleGuard],data:{permittedRoles:['Administrator','Uposlenik']}}





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
    ProductsEditFormComponent,
    LoginComponent,
    RegistrationComponent,
    UserComponent,
    ForbiddenComponent,
    HomeComponent,
    ProductDetailsComponent,
    CartComponent,
    OrdersComponent,
    MyordersComponent,
    MyOrderDetailsComponent,
    OrderDetailsComponent,
    NavHeaderComponent,
    ProfileComponent,
    ReportsComponent,

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
    NgbModule,
    RouterModule.forRoot(appRoutes,{ onSameUrlNavigation: 'reload' })
  ],
  exports:[RouterModule],

  providers: [
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
