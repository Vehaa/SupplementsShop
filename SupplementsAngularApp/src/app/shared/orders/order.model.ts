import { Products } from "../products/product.model";

export class Orders{
    orderId:number;
    orderProductList:Products[];
    customerId:number;
    shippingPrice:number;

}