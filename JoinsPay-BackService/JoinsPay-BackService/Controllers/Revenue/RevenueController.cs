using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JoinsPay_BackService.Data;
using JoinsPay_BackService.Models.ContractResponse;
using JoinsPay_BackService.Models.Register.Revenue;
using System.Globalization;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace JoinsPay_BackService.Controllers.Register.Revenue
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevenueController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public RevenueController(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: api/Revenue
        [HttpGet]
        public async Task<RevenueModelView> GetRevenues()
        {
            var incomes = await _context.Incomes
                                    .Where(t => t.deleted == "N")
                                    .Include(t => t.revenueCategory)
                                    .Include(t => t.account)
                                    .Include(t => t.department)
                                    .Include(t => t.department)
                                    .OrderByDescending(t => t.id)
                                    .ToListAsync();

            RevenueModelView revenueModelView = new RevenueModelView();
            List<DataIncomes> dataIncomes = new List<DataIncomes>();

            revenueModelView.dataTop3SumByCurrentMonthRevenueCategory = await showTop3SumByCurrentMonthRevenueCategory();


            if (incomes != null)
            {
                foreach (var income in incomes)
                {

                    if (income.dateCreated.Month == DateTime.Now.Month)
                    {
                        revenueModelView.totalAmountCurrentMounth = revenueModelView.totalAmountCurrentMounth + income.amount;
                    }

                    if (income.dateCreated.Year == DateTime.Now.Year)
                    {
                        revenueModelView.totalAmountCurrentYear = revenueModelView.totalAmountCurrentYear + income.amount;
                    }


                    dataIncomes.Add(
                            new DataIncomes
                            {
                                id = income.id,
                                idRevenueCategory = income.idRevenueCategory,
                                idAccount = income.idAccount,
                                idDepartment = income.idDepartment,
                                amount = income.amount,
                                amountFormatted = income.amount.ToString("C", CultureInfo.CurrentCulture),
                                description = income.description,
                                deleted = income.deleted,
                                dateCreated = income.dateCreated,
                                dateCreatedFormatted = income.dateCreated.Date.ToString("dd/MM/yyyy").ToString(new CultureInfo("pt-BR")).ToUpper(),
                                revenueCategory = income.revenueCategory.initials + " - " + income.revenueCategory.description,
                                account = income.account.name + " - Ag." + income.account.agency + " / Conta: " + income.account.accountNumber,
                                department = income.department.name,
                                color = income.revenueCategory.color
                            }
                        );
                }

                revenueModelView.dataIncomes = dataIncomes;
                revenueModelView.currentMounth = DateTime.Now.ToString("MMMM").ToString(new CultureInfo("pt-BR")).ToUpper();
                revenueModelView.currentYear = DateTime.Now.ToString("yyyy").ToString();

            }

            return revenueModelView;
        }

        private async Task<List<DataTop3SumByCurrentMonthRevenueCategory>> showTop3SumByCurrentMonthRevenueCategory()
        {
            List <DataTop3SumByCurrentMonthRevenueCategory> dataTop3SumByCurrentMonthRevenueCategory = new List<DataTop3SumByCurrentMonthRevenueCategory>();


            string xSql =
                " DECLARE @monthCurrent AS int                                                              " +
                " SET @monthCurrent = DATEPART(MONTH, GETDATE());                                           " +
                " SELECT                                                                                    " +
                "   TOP(3) R.idRevenueCategory                      ,                                       " +
                "   RRC.description	AS [descriptionRevenueCategory] ,                                       " +
                "   SUM(R.amount)   AS [totalAmountRevenueCategory]                                         " +
                " FROM          Revenue                     R   (NOLOCK)                                    " +
                " INNER JOIN    [Register.Revenue_Category] RRC (NOLOCK) ON (RRC.id = R.idRevenueCategory)  " +
                " WHERE DATEPART(MONTH, R.dateCreated) = @monthCurrent                                      " +
                " GROUP BY RRC.description, R.idRevenueCategory                                             " +
                " ORDER BY [totalAmountRevenueCategory] DESC                                                ";

            try
            {
                using SqlConnection conexao = new SqlConnection(
                    _config.GetConnectionString("DefaultConnection"));
                dataTop3SumByCurrentMonthRevenueCategory = conexao.Query<DataTop3SumByCurrentMonthRevenueCategory>(xSql, commandTimeout: 600).ToList();

            }
            catch (Exception Ex)
            {

            }

            return dataTop3SumByCurrentMonthRevenueCategory;
        }

        // GET: api/Revenue/PeriodDate
        [HttpGet("PeriodDate/{dateInitial}/{dateFinal}")]
        public async Task<RevenueModelView> GetRevenuePeriodDate(DateTime dateInitial, DateTime dateFinal)
        {
            var incomes = await _context.Incomes
                                    .Where(
                                        t => t.deleted == "N" &&
                                        t.dateCreated.Date >= dateInitial.Date && 
                                        t.dateCreated.Date <= dateFinal.Date
                                     )
                                    .Include(t => t.revenueCategory)
                                    .Include(t => t.account)
                                    .Include(t => t.department)
                                    .Include(t => t.department)
                                    .ToListAsync();

            RevenueModelView revenueModelView = new RevenueModelView();
            List<DataIncomes> dataIncomes = new List<DataIncomes>();

            if (incomes != null)
            {
                foreach (var income in incomes)
                {

                    revenueModelView.totalAmount = revenueModelView.totalAmount + income.amount;

                    dataIncomes.Add(
                        new DataIncomes
                        {
                            id = income.id,
                            idRevenueCategory = income.idRevenueCategory,
                            idAccount = income.idAccount,
                            idDepartment = income.idDepartment,
                            amount = income.amount,
                            amountFormatted = income.amount.ToString("C", CultureInfo.CurrentCulture),
                            description = income.description,
                            deleted = income.deleted,
                            dateCreated = income.dateCreated,
                            dateCreatedFormatted = income.dateCreated.Date.ToString("dd/MM/yyyy").ToString(new CultureInfo("pt-BR")).ToUpper(),
                            revenueCategory = income.revenueCategory.initials + " - " + income.revenueCategory.description,
                            account = income.account.name + " - Ag." + income.account.agency + " / Conta: " + income.account.accountNumber,
                            department = income.department.name,
                            color = income.revenueCategory.color
                        }
                    ); 
                }

                revenueModelView.dataIncomes = dataIncomes;
                revenueModelView.currentMounth = DateTime.Now.ToString("MMMM").ToString(new CultureInfo("pt-BR")).ToUpper();
                revenueModelView.currentYear = DateTime.Now.ToString("yyyy").ToString();

            }

            return revenueModelView;
        }

        // GET: api/Revenue/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RevenueDTO>> GetRevenueDTO(long id)
        {
            var accountDTO = await _context.Incomes.FindAsync(id);

            if (accountDTO == null)
            {
                return NotFound();
            }

            return accountDTO;
        }

        // PUT: api/Revenue/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IContractResponse> PutRevenueDTO(long id, RevenueDTO revenueDTO)
        {
            var iContractResponse = new IContractResponse<RevenueDTO>();
            
            if (id != revenueDTO.id)
            {
                iContractResponse.success = false;
                iContractResponse.statusCode = this.HttpContext.Response.StatusCode;
                iContractResponse.message = "Id não localizado";
            }

            try
            {
                var revenueCategory = _context.RevenueCategories.FirstOrDefault(t => t.id == revenueDTO.idRevenueCategory);
                var account         = _context.Accounts.FirstOrDefault(t => t.id == revenueDTO.idAccount);
                var department      = _context.Departments.FirstOrDefault(t => t.id == revenueDTO.idDepartment);

                if (revenueCategory != null && account != null && department != null)
                {
                    revenueDTO.description      = revenueDTO.description.ToUpper();
                    revenueDTO.revenueCategory  = revenueCategory;
                    revenueDTO.department       = department;
                    _context.Entry(revenueDTO).State = EntityState.Modified;

                    iContractResponse.success = true;
                    iContractResponse.data = revenueDTO;
                    iContractResponse.statusCode = this.HttpContext.Response.StatusCode;
                    iContractResponse.message = "Receita alterada com sucesso.";

                    await _context.SaveChangesAsync();
                }
                else
                {
                    iContractResponse.success = false;
                    iContractResponse.statusCode = this.HttpContext.Response.StatusCode;
                    iContractResponse.message = "Não foi possível localizar os dados da Receita selecionada para realizar a alteração dos dados.";
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RevenueDTOExists(id))
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

        // POST: api/Revenue
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<IContractResponse> PostRevenueDTO(RevenueDTO revenueDTO)
        {
            var iContractResponse = new IContractResponse<RevenueDTO>();

            try
            {
                var revenueCategory = _context.RevenueCategories.FirstOrDefault(t => t.id == revenueDTO.idRevenueCategory);
                var account = _context.Accounts.FirstOrDefault(t => t.id == revenueDTO.idAccount);
                var department = _context.Departments.FirstOrDefault(t => t.id == revenueDTO.idDepartment);


                if (revenueCategory != null && account != null && department != null)
                {
                    revenueDTO.description = revenueDTO.description != null ? revenueDTO.description.ToUpper() : "";
                    revenueDTO.revenueCategory = revenueCategory;
                    revenueDTO.department = department;
                    _context.Incomes.Add(revenueDTO);
                    await _context.SaveChangesAsync();

                    iContractResponse.success = true;
                    iContractResponse.data = revenueDTO;
                    iContractResponse.statusCode = this.HttpContext.Response.StatusCode;
                    iContractResponse.message = "Nova Receita inserida com sucesso.";
                   
                }
                else
                {
                    iContractResponse.success = false;
                    iContractResponse.statusCode = this.HttpContext.Response.StatusCode;
                    iContractResponse.message = "Não foi possível localizar os dados da Receita selecionada para realizar a inserção dos dados.";
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

        // DELETE: api/Revenue/5
        [HttpDelete("{id}")]
        public async Task<IContractResponse> DeleteRevenueDTO(long id)
        {
            var accountDTO = await _context.Incomes.FindAsync(id);

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
                iContractResponse.message = "Receita cancelada com sucesso.";

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RevenueDTOExists(id))
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

        private bool RevenueDTOExists(long id)
        {
            return _context.Incomes.Any(e => e.id == id);
        }
    }

    public class RevenueModelView
    {
        public Double totalAmount { get; set; }
        public Double totalAmountCurrentMounth { get; set; }
        public Double totalAmountCurrentYear { get; set; }
        public string currentMounth { get; set; }
        public string currentYear { get; set; }

        public List<DataIncomes> dataIncomes { get; set; }

        public List<DataTop3SumByCurrentMonthRevenueCategory> dataTop3SumByCurrentMonthRevenueCategory { get; set; }
    }

    public class DataIncomes
    {
        public long id { get; set; }
        public long idRevenueCategory { get; set; }
        public long idAccount { get; set; }
        public long idDepartment { get; set; }
        public Double amount { get; set; }
        public string amountFormatted { get; set; }
        public string description { get; set; }
        public string deleted { get; set; }
        public DateTime dateCreated { get; set; }
        public string dateCreatedFormatted { get; set; }
        public string revenueCategory { get; set; }
        public string account { get; set; }
        public string department { get; set; }
        public string color { get; set; }

    }

    public class DataTop3SumByCurrentMonthRevenueCategory
    {
        public long idRevenueCategory { get; set; }
        public string descriptionRevenueCategory { get; set; }
        public Double totalAmountRevenueCategory { get; set; }
    }
}
