using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JoinsPay_BackService.Data;
using JoinsPay_BackService.Models.Expense;

namespace JoinsPay_BackService.Controllers.Expense
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ExpenseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Expense
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExpenseDTO>>> GetExpenses()
        {
            return await _context.Expenses.ToListAsync();
        }

        // GET: api/Expense/ExpenseType
        [HttpGet("ExpenseType")]
        public async Task<ActionResult<IEnumerable<ExpenseTypeDTO>>> GetExpenseTypes()
        {
            return await _context.ExpenseType.ToListAsync();
        }

        // GET: api/Expense/ExpenseStatus
        [HttpGet("ExpenseStatus")]
        public async Task<ActionResult<IEnumerable<ExpenseStatusDTO>>> GetExpenseStatus()
        {
            return await _context.ExpenseStatus.ToListAsync();
        }

        // GET: api/Expense/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseDTO>> GetExpenseDTO(long id)
        {
            var expenseDTO = await _context.Expenses.FindAsync(id);

            if (expenseDTO == null)
            {
                return NotFound();
            }

            return expenseDTO;
        }

        // PUT: api/Expense/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpenseDTO(long id, ExpenseDTO expenseDTO)
        {
            if (id != expenseDTO.id)
            {
                return BadRequest();
            }

            _context.Entry(expenseDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseDTOExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Expense
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ExpenseDTO>> PostExpenseDTO(ExpenseDTO expenseDTO)
        {
            _context.Expenses.Add(expenseDTO);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExpenseDTO", new { id = expenseDTO.id }, expenseDTO);
        }

        // DELETE: api/Expense/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ExpenseDTO>> DeleteExpenseDTO(long id)
        {
            var expenseDTO = await _context.Expenses.FindAsync(id);
            if (expenseDTO == null)
            {
                return NotFound();
            }

            _context.Expenses.Remove(expenseDTO);
            await _context.SaveChangesAsync();

            return expenseDTO;
        }

        private bool ExpenseDTOExists(long id)
        {
            return _context.Expenses.Any(e => e.id == id);
        }
    }
}
