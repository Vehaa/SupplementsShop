import { Component, HostListener } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './shared/auth/auth.service';
import { CartService } from './shared/cart/cart.service';
import { CookieService } from 'ngx-cookie-service';
import * as moment from "moment";
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'SupplementsAngularApp';

  public totalItem:number;
  constructor(private router: Router,
    private cartService:CartService,
    private authService:AuthService,
    private cookieService: CookieService) {

  }
//   @HostListener('window:beforeunload', ['$event'])
// beforeunloadHandler(event) {
//     this.closeWindow();
// }
  ngOnInit():void{
    this.cartService.getProducts()
    .subscribe(res=>{
      this.totalItem=res.length;
    })
}

  isLogged(){
    return this.authService.IsLoggedIn();
  }

  onLogout() {
    localStorage.removeItem('token');
    localStorage.clear();
    this.cartService.removeAllCart();
    this.router.navigate(['/User/Login']).then(() => {
      window.location.reload();
    });
  }
  onLogin() {
    this.router.navigate(['/User/Login']);
  }
  closeWindow() {
    localStorage.removeItem('token');

  }
  isClient(){
    return this.authService.isClient();
  }
  isAdmin() {
    return this.authService.isAdmin();
  }

  isEmployee() {
    return this.authService.isEmployee();
  }
  reloadCurrentRoute() {
    const currentUrl = this.router.url;
    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
    this.router.onSameUrlNavigation = 'reload';
    this.router.navigate([currentUrl]);
  
    };

    refresh(): void {
      window.location.reload();
  }
}