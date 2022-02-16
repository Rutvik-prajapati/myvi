export interface IRecharge {
   id?: number,
   userId:number,
   planId:number,
   price:number,
   rzpOrderId?:string,
   rzpPaymentId?:string,
   rzpSignature?:string
}
