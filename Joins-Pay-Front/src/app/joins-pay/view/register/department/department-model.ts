export interface IDepartment{
    id: number
    idDepartmentCategory: number
    name: string
    dateCreated: Date
    deleted: string

    departmentCategory?: string
}