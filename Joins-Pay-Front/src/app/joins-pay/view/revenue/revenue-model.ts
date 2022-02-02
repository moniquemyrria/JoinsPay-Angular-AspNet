export interface IRevenue{
    totalAmount: number
    totalAmountCurrentMounth: number
    currentMounth: string
    totalAmountCurrentYear: number
    currentYear: string
    dataIncomes: DataIncomes[];
    dataTop3SumByCurrentMonthRevenueCategory: DataTop3SumByCurrentMonthRevenueCategory[]
}

export interface DataIncomes {
    id: number
    idRevenueCategory: number
    idAccount: number
    idDepartment: number
    amount: number
    description: string
    dateCreated: Date
    deleted: string
    revenueCategory: string
    account: string
    department: string
}

export interface DataTop3SumByCurrentMonthRevenueCategory{
   idRevenueCategory: number
   descriptionRevenueCategory: string
   totalAmountRevenueCategory: number
}