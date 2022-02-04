import { IPaymentMethodCategory } from "./payment-method-category-model";

export interface IPaymentMethod {
   id: number
   idAccount: number
   name: string
   acceptInstallment: boolean
   numberInstallments: number
   intervalDaysInstallments: number
   deleted: string
   dateCreated: Date
   account: string
   paymentMethodCategory: IPaymentMethodCategory[];
}
