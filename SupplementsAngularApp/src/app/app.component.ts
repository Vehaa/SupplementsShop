import { Component, HostListener } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './shared/auth/auth.service';
import { CartService } from './shared/cart/cart.service';

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
    private authService:AuthService) {

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
    localStorage.clear();
    this.cartService.removeAllCart();
    this.router.navigate(['/User/Login']);
  }
  onLogin() {
    this.router.navigate(['/User/Login']);
  }
  closeWindow() {
    localStorage.removeItem('token');

  }
}