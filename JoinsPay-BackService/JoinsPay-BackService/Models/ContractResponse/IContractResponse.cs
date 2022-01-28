
namespace JoinsPay_BackService.Models.ContractResponse
{
    public class IContractResponse<T>: IContractResponse
        where T :  class
    {
        public T data { get; set; }
    }

    public class IContractResponse
    {
        public bool success { get; set; }
        public string message { get; set; }

        public int statusCode { get; set; }
    }
}
