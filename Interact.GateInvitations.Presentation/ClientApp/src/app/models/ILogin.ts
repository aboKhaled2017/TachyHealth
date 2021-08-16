export interface ILogin{
    username:string 
    password:string 
    userType:UserType
}
export enum UserType{
    Admin=2,
    Customer=0,
    SecurityKeeper=1
}