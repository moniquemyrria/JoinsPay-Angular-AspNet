using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JoinsPay_BackService.Data;
using JoinsPay_BackService.Models.Expense;
using JoinsPay_BackService.Models.ContractResponse;
using Microsoft.Extensions.Configuration;
using Dapper;
using Microsoft.Data.SqlClient;

namespace JoinsPay_BackService.Controllers.Expense
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public ExpenseController(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: api/Expense
        [HttpGet]
        public async Task<List<ExpenseModelView>> GetExpenses()
        {
            List<ExpenseModelView> listexpenseViewModel = new List<ExpenseModelView>();
            
            string xSql =
               " SELECT                                                                                                                   " +
               "    E.id																												, " +
               "    FORMAT(E.amount, 'C', 'pt-br')																	[amountFormat]		, " +
               "    E.fine																												, " +
               "    E.interest																											, " +
               "    E.discount																											, " +
               "    E.qtyInstallment																									, " +
               "    E.description																										, " +
               "    E.installment																										, " +
               "    CONVERT(varchar(10),E.dateCreated,103)															[dateCreatedFormat]	, " +
               "    CASE WHEN E.dueDate IS NOT NULL THEN CONVERT(varchar(10),E.dueDate,103) ELSE '-' END			[dueDateFormat]		, " +
               "    CASE WHEN e.paymentDate IS NOT NULL THEN CONVERT(varchar(10),e.paymentDate,103) ELSE '-' END	[paymentDateFormat]	, " +
               "    REC.description																					[expenseCategory]	, " +
               "    REC.color																											, " +
               "    RPM.name																						[paymentMethod]		, " +
               "    RD.name																							[department]		, " +
               "    RA.name																							[account]			, " +
               "    ES.description																					[status]			, " +
               "    UPPER(ET.description)																			[expenseType]         " +
               " FROM		[Expense]					E	 (NOLOCK)                                                                     " +
               " INNER JOIN	[Register.Expense_Category]	REC	 (NOLOCK) ON (REC.id  = E.idExpenseCategory)                                  " +
               " INNER JOIN	[Register.Payment_Method]	RPM  (NOLOCK) ON (RPM.id  = E.idPaymentMethod)                                    " +
               " INNER JOIN	[Register.Department]		RD	(NOLOCK) ON (RD.id	  = E.idDepartment)                                       " +
               " INNER JOIN	[Register.Account]			RA	(NOLOCK) ON (RA.id	  = E.idAccount)                                          " +
               " INNER JOIN	[Expense.Expense_Status]	ES	(NOLOCK) ON (ES.id	  = E.idExpenseStatus)                                    " +
               " INNER JOIN	[Expense.Expense_Type]		ET	(NOLOCK) ON (ET.id	  = E.idExpenseType)                                      ";

            try
            {
                using SqlConnection conexao = new SqlConnection(
                    _config.GetConnectionString("DefaultConnection"));
                listexpenseViewModel = conexao.Query<ExpenseModelView>(xSql, commandTimeout: 600).ToList();

            }
            catch (Exception e)
            {

            }

            return listexpenseViewModel;
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

        // GET: api/Expense/ExpenseStatus/NewExpense
        [HttpGet("ExpenseStatus/NewExpense")]
        public async Task<ActionResult<IEnumerable<ExpenseStatusDTO>>> GetExpenseStatusNew()
        {
            return await _context.ExpenseStatus.Where(t => t.description.ToUpper() == "EM ABERTO" || t.description.ToUpper() == "PAGO").ToListAsync();
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
        public async Task<IContractResponse> PostExpenseDTO(ExpenseDTO expenseDTO)
        {
            var expenseType = _context.ExpenseType.Where(t => t.description == expenseDTO.expenseTypeDescription).FirstOrDefault();
            var iContractResponse = new IContractResponse<ExpenseDTO>();

            try
            {

                if (expenseType != null)
                {
                    expenseDTO.idExpenseType = expenseType.id;
                    _context.Expenses.Add(expenseDTO);
                    await _context.SaveChangesAsync();
                    iContractResponse.success = true;
                    iContractResponse.data = expenseDTO;
                    iContractResponse.statusCode = this.HttpContext.Response.StatusCode;
                    iContractResponse.message = "Nova " + expenseType.description + " cadastrada com sucesso.";

                }
                else
                {
                    iContractResponse.success = false;
                    iContractResponse.statusCode = this.HttpContext.Response.StatusCode;
                    iContractResponse.message = "Id do Tipo de Despesa não localizado";

                }

            }
            catch (Exception e)
            {
                iContractResponse.success = false;
                iContractResponse.statusCode = this.HttpContext.Response.StatusCode;
                iContractResponse.message = "Não foi possível cadastrar a Despesa. Error: " + e;

            }

            return iContractResponse;
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

        public class ExpenseModelView
        {
            public long id { get; set; }
            public string amountFormat { get; set; }        //valor
            public Double fine { get; set; }                //multa
            public Double interest { get; set; }            //juros
            public Double discount { get; set; }            //desconto
            public int qtyInstallment { get; set; }         //quantidade de parcelas
            public int installment { get; set; }            //parcela
            public string description { get; set; }
            public string dateCreatedFormat { get; set; }
            public string  dueDateFormat { get; set; }    //vencimento / primeiro vencimento
            public string  paymentDateFormat { get; set; }//data de pagamento
            public string expenseCategory { get; set; }
            public string color { get; set; }
            public string paymentMethod { get; set; }
            public string department { get; set; }
            public string account { get; set; }
            public string status { get; set; }
            public string expenseType { get; set; }

        }

    }
}
