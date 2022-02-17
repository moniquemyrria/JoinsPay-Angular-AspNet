import { IPaymentMethodCategory } from "../register/payment-method/payment-method-category-model";

export interface IExpense {

    id: number
    idExpenseCategory: number
    idPaymentMethod: number
    idDepartment: number
    idAccount: number
    idExpenseStatus: number
    idExpenseType: number


    amount: number//valor
    fine: number//multa
    interest: number//juros
    discount: number//desconto
    qtyInstallment: number//quantidade de parcelas
    installment: number//parcela

    description: string

    dateCreated: Date
    dueDate: Date//vencimento / primeiro vencimento
    paymentDate?: Date//data de pagamento

    paymentMethodCategory: IPaymentMethodCategory[]

    status: string;
    expenseTypeDescription: string;

}

export interface IExpenseStatus {
    id: number
    description: string
    dateCreated: Date
    deleted: string
}

export interface IExpenseType {
    id: number
    description: string
    routerLink: string
    icon: string
    dateCreated: Date
    deleted: string
}
