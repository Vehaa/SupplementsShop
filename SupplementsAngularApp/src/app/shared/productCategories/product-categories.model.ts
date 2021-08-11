import { Observable } from "rxjs";
import { ProductSubCategory } from "./product-subcategories.model";

export class ProductCategory {
    productCategoryId:number;
    name:string="";
    subCategory:ProductSubCategory[];
}
