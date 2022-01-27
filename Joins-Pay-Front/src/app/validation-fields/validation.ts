import { IContractResponse } from "../../app/contract-response/contract-response";

export class Validation {

    /**
     * Valid Schema
     */
    public ValidSchema(schema: ValidationSchema[], json: any): IContractResponse {
        let cResponse = new IContractResponse();

        cResponse.success = true;
        cResponse.message = "";

        schema = schema.reverse()

        schema.forEach((validador: ValidationSchema) => {
            
            let value = json[validador.name];
            
            if (value) {

                
                if (typeof value !== validador.type) {
                    cResponse.success = false;
                    cResponse.message = "Tipo do campo " + validador.textName + " inváilido!";
                    return cResponse;
                }


                if (validador.required && !value) {
                    
                    cResponse.success = false;
                    cResponse.message = (validador.msg == undefined || validador.msg == null) ? "O Campo " + validador.textName + " é obrigatório!" :  validador.msg;
                    return cResponse;
                }


                if (validador.type == "string" && validador.maxLength > 0 && (value + '').length > validador.maxLength) {
                    cResponse.success = false;
                    cResponse.message = "Campo " + validador.textName + " ultrapassou o tamanho permitido para o campo!";
                    return cResponse;
                }
                if (validador.valid) {
                    let result = validador.valid(value);
                    if (!result.success) {
                        cResponse.success = false;
                        cResponse.message = result.message;
                        return cResponse;
                    }
                }
               
            } else {

                if (validador.required) {
                    cResponse.success = false;
                    cResponse.message = (validador.msg == undefined || validador.msg == null) ? "O Campo " + validador.textName + " é obrigatório!" :  validador.msg;
                    return cResponse;
                }
            }
            
        })


        return cResponse;
    }
}

export class ValidationSchema {
    name: string
    textName: string
    type: string
    required: boolean
    maxLength: number
    msg?: string
    valid?: (item: any) => (IContractResponse)

    constructor(name: string, textName: string, type: string = "string", required: boolean = false, maxLength: number = 0, msg?: string, valid?: (item: any) => IContractResponse) {
        this.name = name
        this.textName = textName
        this.type = type
        this.required = required
        this.maxLength = maxLength
        this.msg = msg
        this.valid = valid

    }
}