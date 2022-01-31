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

namespace JoinsPay_BackService.Controllers.Register.Revenue
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevenueController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RevenueController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Revenue
        [HttpGet]
        public async Task<List<RevenueModelView>> GetRevenues()
        {
            var incomes = await _context.Incomes
                                    .Where(t => t.deleted == "N")
                                    .Include(t => t.revenueCategory)
                                    .Include(t => t.account)
                                    .Include(t => t.department)
                                    .Include(t => t.department)
                                    .ToListAsync();

            List<RevenueModelView> accountModelView = new List<RevenueModelView>();


            if (incomes != null)
            {
                foreach (var income in incomes)
                {
                    accountModelView.Add(
                        new RevenueModelView
                        {
                            id                  = income.id,
                            idRevenueCategory   = income.idRevenueCategory,
                            idAccount           = income.idAccount,
                            idDepartment        = income.idDepartment,
                            amount              = income.amount,
                            amountFormatted     = income.amount.ToString("C", CultureInfo.CurrentCulture),
                            description         = income.description,
                            deleted             = income.deleted,
                            dateCreated         = income.dateCreated,
                            revenueCategory     = income.revenueCategory.initials + " - " + income.revenueCategory.description,
                            account             = income.account.name + " - Ag." + income.account.agency + " / Conta: " + income.account.accountNumber,
                            department          = income.department.name
                        }
                    );; ;
                }
            }

            return accountModelView;
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
        public long id { get; set; }
        public long idRevenueCategory { get; set; }
        public long idAccount { get; set; }
        public long idDepartment { get; set; }
        public Double amount { get; set; }
        public string amountFormatted { get; set; }
        public string description { get; set; }
        public string deleted { get; set; }
        public DateTime dateCreated { get; set; }
        public string revenueCategory { get; set; }
        public string account { get; set; }
        public string department { get; set; }

    }
}
