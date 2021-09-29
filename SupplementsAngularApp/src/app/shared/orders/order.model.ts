import { Client } from "../clients/client.model";
import { Products } from "../products/product.model";
import { OrderDetails } from "./orderDetails.model";

export class Orders{
    orderId:number;
    orderProductList:Products[];
    customerId:number;
    shippingPrice:number;
    orderDate:Date;
    shippedDate:Date;


    orderStatusName:string;
    orderDetailsList:OrderDetails[];
    client:Client;
    reason:string;


}