export class Products{
    productId:number;
    name:string;
    unitPrice:number;
    unitInStock:number;
    unitOnOrder:number;
    discount:number;
    totalPrice:number;
    brandId:number;
    counter:number;
    description:string;
    photoAsBase64:string="";
    productCategoryId:number;
    productSubCategoryId:number;

    avgRating:number;
    filterName:string;
}