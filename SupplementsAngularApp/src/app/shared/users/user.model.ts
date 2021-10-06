import { Byte } from "@angular/compiler/src/util";

export class User{
    userId:number;
    firstName:string="";
    lastName:string="";
    userName:string="";
    password:string="";
    oldpassword:string="";
    passwordHash:string="";
    passwordSalt:string="";
    passwordConfirmation:string="";
    email:string="";
    phone:string="";
    address:string="";
    cityName:string="";
    birthDate:Date;
    registrationDate:Date;
    cityId:number;
    status:boolean;
    comments:boolean;
    photoAsBase64:string="";
    roleId:number;

}