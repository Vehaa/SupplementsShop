import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/shared/auth/auth.service';
import { CartService } from 'src/app/shared/cart/cart.service';
import { Client } from 'src/app/shared/clients/client.model';
import { Comments } from 'src/app/shared/comments/comments.model';
import { CommentsService } from 'src/app/shared/comments/comments.service';
import { HomeService } from 'src/app/shared/home/home.service';
import { Products } from 'src/app/shared/products/product.model';
import { RatingService } from 'src/app/shared/rating/rating.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: [

  ]
})
export class ProductDetailsComponent implements OnInit {

  constructor(public service:HomeService,
    private toastr:ToastrService,
    private _router:Router,
    private sanitizer: DomSanitizer,
    private route:ActivatedRoute,
    private authService:AuthService,
    private cartService:CartService,
    public commentService:CommentsService,
    private ratingService:RatingService) { }

    
  base="data:image/png;charset=utf-8;base64,";
  rate:number;
  product:Products;
  productId:number;
  comment=new Comments;
  client: Client;
  discount:boolean=false;
  listComments:Comments[];
  photo;
  text=new FormControl('');
  public qty=new FormControl('1');
  canComment=false;

  form = new FormGroup({
    userId:new FormControl(),
    productId:new FormControl(),
    rate:new FormControl()
  })

  ngOnInit(): void {
    this.route.paramMap.subscribe(params =>{
      const productId=+params.get('id');
      if(productId){
        this.productId=productId;
        this.getProduct(productId);
      }
    });
      this.commentService.getCommentsByProductId(this.productId);
      this.commentService.getClientById().subscribe(res=>{
        this.client=res as Client;
        this.canComment=this.client.comments;
      });
    
    
  }

  setStar(rating:number){
    this.form.patchValue({
      userId:this.client.userId,
      productId:this.product.productId,
      rate:rating
    });
    this.ratingService.postStar(this.form.value).subscribe(res=>{
      this.toastr.success("Uspješno ste ocjenili proizvod!");
    },
    err=>{
      this.toastr.error(err.error);
    });
  }

  isClient(){
    return this.authService.isClient()
  }

  isEmployee(){
    return this.authService.isEmployee();
  }

  isAdmin(){
    return this.authService.isAdmin();
  }

  isLoggedIn(){
    return this.authService.IsLoggedIn();
  }

  removeComment(comment){
    var canDelete=false;
    if(comment.userId==this.client.userId)
      canDelete=true;
    
    if(this.isEmployee())
      canDelete=true;
    
    if(canDelete){
    if(confirm('Da li ste sigurni da želite izbrisati?'))
      {
        this.commentService.deleteComment(comment.commentId).subscribe(
          res=>{
            this.commentService.refreshList(this.product.productId);
        this.toastr.error("Komentar uspješno uklonjen!");
      },
        err=>{console.log(err)})
      }
      
  }
    else
      this.toastr.error("Komentar nije uklonjen!");
  }

  sanitize(url: string) {
    //return url;
    return this.sanitizer.bypassSecurityTrustUrl(url);

  }

  getProduct(id:number){
    this.service.getProductById(id).subscribe(
      res=>{
        this.product=res as Products;
        this.rate=this.product.avgRating;
      });


  }
  canActivate(){
    return this.authService.isClient();
  }

  addtoCart(item:any){
    item.unitOnOrder=this.qty.value;
    this.cartService.addtoCart(item);
    this.toastr.success('Proizvod uspješno dodan u korpu!', 'Korpa');
  }
  addComment(){
    if(this.text.value!=''){
      this.comment.text=this.text.value;
      this.comment.userId=this.client.userId;
      this.comment.productId=this.product.productId;

      this.commentService.postComment(this.comment).subscribe(res=>{
      this.commentService.refreshList(this.productId);
      },
      err=>{console.log(err);}

        );
      

      this.text.setValue("");
      this.toastr.success("Uspješno ste komentarisali proizvod!");

    }
    else{
      this.toastr.error("Vaš komentar je prazan!");
    }

  }



}
