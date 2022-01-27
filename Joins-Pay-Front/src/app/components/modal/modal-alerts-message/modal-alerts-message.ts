import { AlertMessageModel } from "./modal-alerts-message-model";

export function GetAlertMessage(
    title: string,
    text: string,
    sendRouter: boolean,
    confirmButtonShow: boolean,
    statusCode?: number,
    errorTitleShow?: boolean,

    confirmButtonText?: string,
    confirmButtonEventEmmiter?: object,
    dangerButtonText?: string,
    dangerButtonEventEmmiter?: object


) {

    let alertMesssage: AlertMessageModel = {} as AlertMessageModel

    if (statusCode !== undefined) {
        switch (statusCode) {
            case 500:
                alertMesssage = {
                    title: title,
                    text: text,
                    sendRouter: sendRouter ? true: false,
                    confirmButtonShow: true,
                    errorTitleShow: true,
                    confirmButtonText: 'OK',
                    confirmButtonEventEmmiter: confirmButtonEventEmmiter,
                }

                break;
            default:
                break;
        }
    } else {
        switch (confirmButtonShow) {
            case true:
                alertMesssage = {
                    title: title,
                    text: text,
                    sendRouter: sendRouter ? true: false,
                    confirmButtonShow: true,
                    errorTitleShow: errorTitleShow == undefined ? false : errorTitleShow,
                    confirmButtonText: confirmButtonText == undefined ? 'OK' : confirmButtonText,
                    confirmButtonEventEmmiter: confirmButtonEventEmmiter,
                }

                break;
            case false:

                alertMesssage = {
                    title: title,
                    text: text,
                    confirmButtonShow: false,
                    sendRouter: sendRouter ? true: false,
                    errorTitleShow: errorTitleShow == undefined ? false : errorTitleShow,
                    confirmButtonText: confirmButtonText == undefined ? 'Sim' : confirmButtonText,
                    confirmButtonEventEmmiter: confirmButtonEventEmmiter,
                    dangerButtonText: "Não",
                    dangerButtonEventEmmiter: dangerButtonEventEmmiter,
                    
                }


                break;
        }
    }

    return alertMesssage

}