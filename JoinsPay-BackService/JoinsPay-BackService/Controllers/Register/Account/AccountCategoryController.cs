using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JoinsPay_BackService.Data;
using JoinsPay_BackService.Models.ContractResponse;
using JoinsPay_BackService.Models.Register.Account;

namespace JoinsPay_BackService.Controllers.Register.AccountCategory
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountCategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AccountCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/AccountCategory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountCategoryDTO>>> GetAccountCategories()
        {
            return await _context.AccountCategories.Where(t => t.deleted == "N").ToListAsync();
        }

        // GET: api/AccountCategory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountCategoryDTO>> GetAccountCategoryDTO(long id)
        {
            var expenseCategoryDTO = await _context.AccountCategories.FindAsync(id);

            if (expenseCategoryDTO == null)
            {
                return NotFound();
            }

            return expenseCategoryDTO;
        }

        // PUT: api/AccountCategory/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IContractResponse> PutAccountCategoryDTO(long id, AccountCategoryDTO expenseCategoryDTO)
        {
            var iContractResponse = new IContractResponse<AccountCategoryDTO>();
            
            if (id != expenseCategoryDTO.id)
            {
                iContractResponse.success = false;
                iContractResponse.statusCode = this.HttpContext.Response.StatusCode;
                iContractResponse.message = "Id não localizado";
            }

            try
            {
                expenseCategoryDTO.initials = expenseCategoryDTO.initials.ToUpper();
                expenseCategoryDTO.description = expenseCategoryDTO.description.ToUpper();
                _context.Entry(expenseCategoryDTO).State = EntityState.Modified;

                iContractResponse.success = true;
                iContractResponse.data = expenseCategoryDTO;
                iContractResponse.statusCode = this.HttpContext.Response.StatusCode;
                iContractResponse.message = "Tipo de Conta alterada com sucesso.";

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountCategoryDTOExists(id))
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

        // POST: api/AccountCategory
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<IContractResponse> PostAccountCategoryDTO(AccountCategoryDTO expenseCategoryDTO)
        {
            var iContractResponse = new IContractResponse<AccountCategoryDTO>();

            try
            {
                expenseCategoryDTO.initials = expenseCategoryDTO.initials.ToUpper();
                expenseCategoryDTO.description = expenseCategoryDTO.description.ToUpper();
                _context.AccountCategories.Add(expenseCategoryDTO);
                await _context.SaveChangesAsync();

                iContractResponse.success = true;
                iContractResponse.data = expenseCategoryDTO;
                iContractResponse.statusCode = this.HttpContext.Response.StatusCode;
                iContractResponse.message = "Novo Tipo de Conta cadastrada com sucesso.";

            }
            catch (Exception e)
            {
                iContractResponse.success = false;
                iContractResponse.statusCode = this.HttpContext.Response.StatusCode;
                iContractResponse.message = e.Message;
            }

           
            return iContractResponse;

        }

        // DELETE: api/AccountCategory/5
        [HttpDelete("{id}")]
        public async Task<IContractResponse> DeleteAccountCategoryDTO(long id)
        {
            var expenseCategoryDTO = await _context.AccountCategories.FindAsync(id);

            var iContractResponse = new IContractResponse();

            if (expenseCategoryDTO == null)
            {
                iContractResponse.success = false;
                iContractResponse.statusCode = this.HttpContext.Response.StatusCode;
                iContractResponse.message = "Id não localizado";
            }


            try
            {
                expenseCategoryDTO.deleted = "Y";
                _context.Entry(expenseCategoryDTO).State = EntityState.Modified;

                iContractResponse.success = true;
                iContractResponse.statusCode = this.HttpContext.Response.StatusCode;
                iContractResponse.message = "Tipo de Conta excluída com sucesso.";

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountCategoryDTOExists(id))
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

        private bool AccountCategoryDTOExists(long id)
        {
            return _context.AccountCategories.Any(e => e.id == id);
        }
    }
}
