using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JoinsPay_BackService.Data;
using JoinsPay_BackService.Models.Register.RevenueCategory;
using JoinsPay_BackService.Models.ContractResponse;

namespace JoinsPay_BackService.Controllers.Register.RevenueCategory
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevenueCategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RevenueCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/RevenueCategory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RevenueCategoryDTO>>> GetRevenueCategories()
        {
            return await _context.RevenueCategories.Where(t => t.deleted == "N").ToListAsync();
        }

        // GET: api/RevenueCategory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RevenueCategoryDTO>> GetRevenueCategoryDTO(long id)
        {
            var revenueCategoryDTO = await _context.RevenueCategories.FindAsync(id);

            if (revenueCategoryDTO == null)
            {
                return NotFound();
            }

            return revenueCategoryDTO;
        }

        // PUT: api/RevenueCategory/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IContractResponse> PutRevenueCategoryDTO(long id, RevenueCategoryDTO revenueCategoryDTO)
        {
            var iContractResponse = new IContractResponse<RevenueCategoryDTO>();
            
            if (id != revenueCategoryDTO.id)
            {
                iContractResponse.success = false;
                iContractResponse.statusCode = this.HttpContext.Response.StatusCode;
                iContractResponse.message = "Id não localizado";
            }

            try
            {
                revenueCategoryDTO.initials = revenueCategoryDTO.initials.ToUpper();
                revenueCategoryDTO.description = revenueCategoryDTO.description.ToUpper();
                _context.Entry(revenueCategoryDTO).State = EntityState.Modified;

                iContractResponse.success = true;
                iContractResponse.data = revenueCategoryDTO;
                iContractResponse.statusCode = this.HttpContext.Response.StatusCode;
                iContractResponse.message = "Categoria de Receita alterada com sucesso.";

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RevenueCategoryDTOExists(id))
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

        // POST: api/RevenueCategory
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<IContractResponse> PostRevenueCategoryDTO(RevenueCategoryDTO revenueCategoryDTO)
        {
            var iContractResponse = new IContractResponse<RevenueCategoryDTO>();

            try
            {
                revenueCategoryDTO.initials = revenueCategoryDTO.initials.ToUpper();
                revenueCategoryDTO.description = revenueCategoryDTO.description.ToUpper();
                _context.RevenueCategories.Add(revenueCategoryDTO);
                await _context.SaveChangesAsync();

                iContractResponse.success = true;
                iContractResponse.data = revenueCategoryDTO;
                iContractResponse.statusCode = this.HttpContext.Response.StatusCode;
                iContractResponse.message = "Nova categoria de Receita cadastrada com sucesso.";

            }
            catch (Exception e)
            {
                iContractResponse.success = false;
                iContractResponse.statusCode = this.HttpContext.Response.StatusCode;
                iContractResponse.message = e.Message;
            }

           
            return iContractResponse;

        }

        // DELETE: api/RevenueCategory/5
        [HttpDelete("{id}")]
        public async Task<IContractResponse> DeleteRevenueCategoryDTO(long id)
        {
            var revenueCategoryDTO = await _context.RevenueCategories.FindAsync(id);

            var iContractResponse = new IContractResponse();

            if (revenueCategoryDTO == null)
            {
                iContractResponse.success = false;
                iContractResponse.statusCode = this.HttpContext.Response.StatusCode;
                iContractResponse.message = "Id não localizado";
            }


            try
            {
                revenueCategoryDTO.deleted = "Y";
                _context.Entry(revenueCategoryDTO).State = EntityState.Modified;

                iContractResponse.success = true;
                iContractResponse.statusCode = this.HttpContext.Response.StatusCode;
                iContractResponse.message = "Categoria de Receita excluída com sucesso.";

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RevenueCategoryDTOExists(id))
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

        private bool RevenueCategoryDTOExists(long id)
        {
            return _context.RevenueCategories.Any(e => e.id == id);
        }
    }
}
