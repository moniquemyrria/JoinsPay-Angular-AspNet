using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JoinsPay_BackService.Data;
using JoinsPay_BackService.Models.ContractResponse;
using JoinsPay_BackService.Models.Register.ExpenseCategory;

namespace JoinsPay_BackService.Controllers.Register.ExpenseCategory
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseCategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ExpenseCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ExpenseCategory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExpenseCategoryDTO>>> GetExpenseCategories()
        {
            return await _context.ExpenseCategories.Where(t => t.deleted == "N").ToListAsync();
        }

        // GET: api/ExpenseCategory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseCategoryDTO>> GetExpenseCategoryDTO(long id)
        {
            var expenseCategoryDTO = await _context.ExpenseCategories.FindAsync(id);

            if (expenseCategoryDTO == null)
            {
                return NotFound();
            }

            return expenseCategoryDTO;
        }

        // PUT: api/ExpenseCategory/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IContractResponse> PutExpenseCategoryDTO(long id, ExpenseCategoryDTO expenseCategoryDTO)
        {
            var iContractResponse = new IContractResponse<ExpenseCategoryDTO>();
            
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
                iContractResponse.message = "Categoria de Despesa alterada com sucesso.";

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseCategoryDTOExists(id))
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

        // POST: api/ExpenseCategory
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<IContractResponse> PostExpenseCategoryDTO(ExpenseCategoryDTO expenseCategoryDTO)
        {
            var iContractResponse = new IContractResponse<ExpenseCategoryDTO>();

            try
            {
                expenseCategoryDTO.initials = expenseCategoryDTO.initials.ToUpper();
                expenseCategoryDTO.description = expenseCategoryDTO.description.ToUpper();
                _context.ExpenseCategories.Add(expenseCategoryDTO);
                await _context.SaveChangesAsync();

                iContractResponse.success = true;
                iContractResponse.data = expenseCategoryDTO;
                iContractResponse.statusCode = this.HttpContext.Response.StatusCode;
                iContractResponse.message = "Nova categoria de Despesa cadastrada com sucesso.";

            }
            catch (Exception e)
            {
                iContractResponse.success = false;
                iContractResponse.statusCode = this.HttpContext.Response.StatusCode;
                iContractResponse.message = e.Message;
            }

           
            return iContractResponse;

        }

        // DELETE: api/ExpenseCategory/5
        [HttpDelete("{id}")]
        public async Task<IContractResponse> DeleteExpenseCategoryDTO(long id)
        {
            var expenseCategoryDTO = await _context.ExpenseCategories.FindAsync(id);

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
                iContractResponse.message = "Categoria de Despesa excluída com sucesso.";

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseCategoryDTOExists(id))
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

        private bool ExpenseCategoryDTOExists(long id)
        {
            return _context.ExpenseCategories.Any(e => e.id == id);
        }
    }
}
