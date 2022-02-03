using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JoinsPay_BackService.Data;
using JoinsPay_BackService.Models.ContractResponse;
using JoinsPay_BackService.Models.Register.PaymentMethod;

namespace JoinsPay_BackService.Controllers.Register.PaymentMethod
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PaymentMethodController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Account
        [HttpGet]
        public async Task<List<PaymentMethodModelView>> GetAccounts()
        {
            var paymentMethod = await _context.PaymentMethods
                                    .Where(t => t.deleted == "N")
                                    .Include(t => t.account)
                                    .ToListAsync();

            List<PaymentMethodModelView> paymentMethodModelView = new List<PaymentMethodModelView>();


            if (paymentMethod != null)
            {
                foreach (var payment in paymentMethod)
                {
                    paymentMethodModelView.Add(
                        new PaymentMethodModelView
                        {
                            id                          = payment.id,
                            idAccount                   = payment.idAccount,
                            name                        = payment.name,
                            acceptInstallment           = payment.acceptInstallment,
                            numberInstallments          = payment.numberInstallments,
                            deleted                     = payment.deleted,
                            dateCreated                 = payment.dateCreated,
                            account                     = payment.account.name
                        }
                    ); ;
                }
            }

            return paymentMethodModelView;
        }

        // GET: api/Account/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentMethodDTO>> GetPaymentMethodDTO(long id)
        {
            var paymentMethodDTO = await _context.PaymentMethods.FindAsync(id);

            if (paymentMethodDTO == null)
            {
                return NotFound();
            }

            return paymentMethodDTO;
        }

        // PUT: api/Account/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IContractResponse> PutPaymentMethodDTO(long id, PaymentMethodDTO paymentMethodDTO)
        {
            var iContractResponse = new IContractResponse<PaymentMethodDTO>();
            
            if (id != paymentMethodDTO.id)
            {
                iContractResponse.success = false;
                iContractResponse.statusCode = this.HttpContext.Response.StatusCode;
                iContractResponse.message = "Id não localizado";
            }

            try
            {
                var account = _context.Accounts.FirstOrDefault(t => t.id == paymentMethodDTO.idAccount);

                if (account != null)
                {
                    paymentMethodDTO.name                   = paymentMethodDTO.name.ToUpper();
                    paymentMethodDTO.acceptInstallment      = paymentMethodDTO.acceptInstallment;
                    paymentMethodDTO.numberInstallments     = paymentMethodDTO.numberInstallments;
                    paymentMethodDTO.account                = account;
                    _context.Entry(paymentMethodDTO).State = EntityState.Modified;

                    iContractResponse.success = true;
                    iContractResponse.data = paymentMethodDTO;
                    iContractResponse.statusCode = this.HttpContext.Response.StatusCode;
                    iContractResponse.message = "Forma de Pagemento alterada com sucesso.";

                    await _context.SaveChangesAsync();
                }
                else
                {
                    iContractResponse.success = false;
                    iContractResponse.statusCode = this.HttpContext.Response.StatusCode;
                    iContractResponse.message = "Não foi possível localizar os dados da Forma de Pagemento selecionada para realizar a alteração dos dados.";
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentMethodDTOExists(id))
                {
                    iContractResponse.success = false;
                    iContractResponse.statusCode = this.HttpContext.Response.StatusCode;
                }
                else
                {
                    throw;
                }
            }

            return iContractResponse;
        }

        // POST: api/Account
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<IContractResponse> PostPaymentMethodDTO(PaymentMethodDTO paymentMethodDTO)
        {
            var iContractResponse = new IContractResponse<PaymentMethodDTO>();

            try
            {
                var account = _context.Accounts.FirstOrDefault(t => t.id == paymentMethodDTO.idAccount);

                if (account != null)
                {
                    paymentMethodDTO.name               = paymentMethodDTO.name.ToUpper();
                    paymentMethodDTO.acceptInstallment  = paymentMethodDTO.acceptInstallment;
                    paymentMethodDTO.numberInstallments = paymentMethodDTO.numberInstallments;
                    paymentMethodDTO.account            = account;
                    _context.PaymentMethods.Add(paymentMethodDTO);

                    foreach (var aux in paymentMethodDTO.paymentMethodsPaymentMethodCategories)
                    {
                        var paymentMethodCategory = _context.PaymentMethodCategories.FirstOrDefault(t => t.id == aux.idPaymentMethodCategory);

                        _context.PaymentMethodsPaymentMethodCategories.Add(
                            new PaymentMethod_PaymentMethodCategoryDTO
                            {
                                paymentMethod           = paymentMethodDTO,
                                paymentMethodCategory   = paymentMethodCategory
                            }
                        );
                        
                    }

                  
                    await _context.SaveChangesAsync();

                    iContractResponse.success = true;
                    iContractResponse.data = paymentMethodDTO;
                    iContractResponse.statusCode = this.HttpContext.Response.StatusCode;
                    iContractResponse.message = "Nova Forma de Pagamento cadastrada com sucesso.";
                   
                }
                else
                {
                    iContractResponse.success = false;
                    iContractResponse.statusCode = this.HttpContext.Response.StatusCode;
                    iContractResponse.message = "Não foi possível localizar os dados da Forma de Pagemento selecionada para realizar o cadastro da nova conta.";
                }

            }
            catch (Exception e)
            {
                iContractResponse.success = false;
                iContractResponse.statusCode = this.HttpContext.Response.StatusCode;
                iContractResponse.message = e.Message;
            }

           
            return iContractResponse;

        }

        // DELETE: api/Account/5
        [HttpDelete("{id}")]
        public async Task<IContractResponse> DeletePaymentMethodDTO(long id)
        {
            var paymentMethodDTO = await _context.Accounts.FindAsync(id);

            var iContractResponse = new IContractResponse();

            if (paymentMethodDTO == null)
            {
                iContractResponse.success = false;
                iContractResponse.statusCode = this.HttpContext.Response.StatusCode;
                iContractResponse.message = "Id não localizado";
            }


            try
            {
                paymentMethodDTO.deleted = "Y";
                _context.Entry(paymentMethodDTO).State = EntityState.Modified;

                iContractResponse.success = true;
                iContractResponse.statusCode = this.HttpContext.Response.StatusCode;
                iContractResponse.message = "Forma de Pagemento excluída com sucesso.";

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentMethodDTOExists(id))
                {
                    iContractResponse.success = false;
                    iContractResponse.statusCode = this.HttpContext.Response.StatusCode;
                }
                else
                {
                    throw;
                }
            }

            return iContractResponse;
        }

        private bool PaymentMethodDTOExists(long id)
        {
            return _context.Accounts.Any(e => e.id == id);
        }
    }

    public class PaymentMethodModelView
    {
        public long id { get; set; }
        public long idPaymentMethodCategory { get; set; }
        public long idAccount { get; set; }
        public string name { get; set; }
        public Boolean acceptInstallment { get; set; }
        public int numberInstallments { get; set; }
        public int intervalDaysInstallments { get; set; }
        public string deleted { get; set; }
        public DateTime dateCreated { get; set; } 
        public string paymentMethodCategory { get; set; }
        public string account { get; set; }

    }
}
