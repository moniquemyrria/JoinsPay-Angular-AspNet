using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JoinsPay_BackService.Data;
using JoinsPay_BackService.Models.ContractResponse;
using JoinsPay_BackService.Models.Register.Account;

namespace JoinsPay_BackService.Controllers.Register.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Account
        [HttpGet]
        public async Task<List<AccountModelView>> GetAccounts()
        {
            var accounts = await _context.Accounts
                                    .Where(t => t.deleted == "N")
                                    .Include(t => t.accountCategory)
                                    .Include(t => t.department)
                                    .ToListAsync();

            List<AccountModelView> accountModelView = new List<AccountModelView>();


            if (accounts != null)
            {
                foreach (var account in accounts)
                {
                    accountModelView.Add(
                        new AccountModelView
                        {
                            id                  = account.id,
                            idAccountCategory   = account.idAccountCategory,
                            code                = account.code,
                            name                = account.name,
                            agency              = account.agency,
                            accountNumber       = account.accountNumber,
                            deleted             = account.deleted,
                            dateCreated         = account.dateCreated,
                            accountCategory     = account.accountCategory.initials + " - " + account.accountCategory.description,
                            department          = account.department.name
                        }
                    ); ;
                }
            }

            return accountModelView;
        }

        // GET: api/Account/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountDTO>> GetAccountDTO(long id)
        {
            var accountDTO = await _context.Accounts.FindAsync(id);

            if (accountDTO == null)
            {
                return NotFound();
            }

            return accountDTO;
        }

        // PUT: api/Account/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IContractResponse> PutAccountDTO(long id, AccountDTO accountDTO)
        {
            var iContractResponse = new IContractResponse<AccountDTO>();
            
            if (id != accountDTO.id)
            {
                iContractResponse.success = false;
                iContractResponse.statusCode = this.HttpContext.Response.StatusCode;
                iContractResponse.message = "Id não localizado";
            }

            try
            {
                var accountCategory = _context.AccountCategories.FirstOrDefault(t => t.id == accountDTO.idAccountCategory);

                if (accountCategory != null)
                {
                    accountDTO.code             = accountDTO.code.ToUpper();
                    accountDTO.name             = accountDTO.name.ToUpper();
                    accountDTO.agency           = accountDTO.agency.ToUpper();
                    accountDTO.accountNumber    = accountDTO.accountNumber.ToUpper();
                    accountDTO.accountCategory  = accountCategory;
                    _context.Entry(accountDTO).State = EntityState.Modified;

                    iContractResponse.success = true;
                    iContractResponse.data = accountDTO;
                    iContractResponse.statusCode = this.HttpContext.Response.StatusCode;
                    iContractResponse.message = "Conta alterada com sucesso.";

                    await _context.SaveChangesAsync();
                }
                else
                {
                    iContractResponse.success = false;
                    iContractResponse.statusCode = this.HttpContext.Response.StatusCode;
                    iContractResponse.message = "Não foi possível localizar os dados do Tipo da Conta selecionada para realizar a alteração dos dados.";
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountDTOExists(id))
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
        public async Task<IContractResponse> PostAccountDTO(AccountDTO accountDTO)
        {
            var iContractResponse = new IContractResponse<AccountDTO>();

            try
            {
                var accountCategory = _context.AccountCategories.FirstOrDefault(t => t.id == accountDTO.idAccountCategory);


                if (accountCategory != null)
                {
                    accountDTO.code             = accountDTO.code.ToUpper();
                    accountDTO.name             = accountDTO.name.ToUpper();
                    accountDTO.agency           = accountDTO.agency.ToUpper();
                    accountDTO.accountNumber    = accountDTO.accountNumber.ToUpper();
                    accountDTO.accountCategory  = accountCategory;
                    _context.Accounts.Add(accountDTO);
                    await _context.SaveChangesAsync();

                    iContractResponse.success = true;
                    iContractResponse.data = accountDTO;
                    iContractResponse.statusCode = this.HttpContext.Response.StatusCode;
                    iContractResponse.message = "Nova Conta cadastrada com sucesso.";
                   
                }
                else
                {
                    iContractResponse.success = false;
                    iContractResponse.statusCode = this.HttpContext.Response.StatusCode;
                    iContractResponse.message = "Não foi possível localizar os dados do Tipo da Conta selecionada para realizar o cadastro da nova conta.";
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
        public async Task<IContractResponse> DeleteAccountDTO(long id)
        {
            var accountDTO = await _context.Accounts.FindAsync(id);

            var iContractResponse = new IContractResponse();

            if (accountDTO == null)
            {
                iContractResponse.success = false;
                iContractResponse.statusCode = this.HttpContext.Response.StatusCode;
                iContractResponse.message = "Id não localizado";
            }


            try
            {
                accountDTO.deleted = "Y";
                _context.Entry(accountDTO).State = EntityState.Modified;

                iContractResponse.success = true;
                iContractResponse.statusCode = this.HttpContext.Response.StatusCode;
                iContractResponse.message = "Conta excluída com sucesso.";

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountDTOExists(id))
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

        private bool AccountDTOExists(long id)
        {
            return _context.Accounts.Any(e => e.id == id);
        }
    }

    public class AccountModelView
    {
        public long id { get; set; }
        public long idAccountCategory { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string agency { get; set; }
        public string accountNumber { get; set; }
        public string deleted { get; set; }
        public DateTime dateCreated { get; set; }

        public string accountCategory { get; set; }
        
        public string department { get; set; }

    }
}
