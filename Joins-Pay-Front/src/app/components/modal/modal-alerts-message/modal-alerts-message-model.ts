
export class AlertMessageModel {
    title: string = "Titulo"
    text: string = "Texto"
    sendRouter: boolean = false
    confirmButtonShow: boolean = true
    statusCode?: number
    errorTitleShow?: boolean
    confirmButtonText?: string;
    confirmButtonEventEmmiter?: any = {}
    dangerButtonText?: string = ""
    dangerButtonEventEmmiter?: any = {}

}