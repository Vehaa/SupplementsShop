import { Byte } from "@angular/compiler/src/util";

export class Brand{
    brandId:number;
    name:string;
    description:string;
    logoAsBase64:string="";
    logoAsByteArray:Byte;
}