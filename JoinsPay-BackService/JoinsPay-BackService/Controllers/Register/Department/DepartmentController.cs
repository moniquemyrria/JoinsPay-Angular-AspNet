using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JoinsPay_BackService.Data;
using JoinsPay_BackService.Models.ContractResponse;
using JoinsPay_BackService.Models.Register.Department;

namespace JoinsPay_BackService.Controllers.Register.Department
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Department
        [HttpGet]
        public async Task<List<DepartmentModelView>> GetDepartments()
        {

            var departmentCategory = HttpContext.Request.Headers["departmentCategory"];

            var departments = await _context.Departments
                                    .Include(t => t.departmentCategory)
                                    .Where(t => t.deleted == "N" && t.departmentCategory.description.ToLower() == departmentCategory.ToString().ToLower())
                                    .ToListAsync();

            List<DepartmentModelView> departmentModelView = new List<DepartmentModelView>();


            if (departments != null)
            {
                foreach (var department in departments)
                {
                    departmentModelView.Add(
                        new DepartmentModelView
                        {
                            id                      = department.id,
                            idDepartmentCategory    = department.idDepartamentCategory,
                            name                    = department.name,
                            deleted                 = department.deleted,
                            dateCreated             = department.dateCreated,
                            departmentCategory      = department.departmentCategory.description
                        }
                    ); ;
                }
            }


            return departmentModelView;
        }

        // GET: api/Department/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentDTO>> GetDepartmentDTO(long id)
        {
            var departmentDTO = await _context.Departments.FindAsync(id);

            if (departmentDTO == null)
            {
                return NotFound();
            }

            return departmentDTO;
        }

        static string checkMessageDepartamentCategory(string type, string departamentCategory, string? peopleName)
        {
            string departamentType = "";
            switch (departamentCategory)
            {
                case "STORE":
                    departamentType = type == "edit" ? "Dados da Loja alterado com sucesso." : type == "delete" ? "Loja excluída com sucesso." : "Dados da Nova Loja cadastrada com sucesso.";
                    break;
                case "COMPANY":
                    departamentType = type == "edit" ? "Dados da Empresa alterado com sucesso." : type == "delete" ? "Empresa excluída com sucesso." : "Dados da Nova Empresa cadastrada com sucesso.";
                    break;
                case "PEOPLE":
                    departamentType = type == "edit" ? "Dados de " + peopleName + " alterado com sucesso." : type == "delete" ? "Dadode de " + peopleName +" excluído com sucesso." : "Dados de " + peopleName + " cadastrado com sucesso.";
                    break;
            };
            return departamentType;
        }

        // PUT: api/Department/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IContractResponse> PutDepartmentDTO(long id, DepartmentDTO departmentDTO)
        {
            var iContractResponse = new IContractResponse<DepartmentDTO>();
            
            if (id != departmentDTO.id)
            {
                iContractResponse.success = false;
                iContractResponse.statusCode = this.HttpContext.Response.StatusCode;
                iContractResponse.message = "Id não localizado";
            }

            try
            {
                var departmentCategory = _context.DepartmentCategories.FirstOrDefault(t => t.id == departmentDTO.idDepartamentCategory);

                if (departmentCategory != null)
                {
                    departmentDTO.name = departmentDTO.name.ToUpper();
                    departmentDTO.departmentCategory = departmentCategory;
                    _context.Entry(departmentDTO).State = EntityState.Modified;

                    iContractResponse.success = true;
                    iContractResponse.data = departmentDTO;
                    iContractResponse.statusCode = this.HttpContext.Response.StatusCode; 

                    iContractResponse.message = checkMessageDepartamentCategory("edit", departmentCategory.description, departmentDTO.name);

                    await _context.SaveChangesAsync();
                }
                else
                {
                    iContractResponse.success = false;
                    iContractResponse.statusCode = this.HttpContext.Response.StatusCode;
                    iContractResponse.message = "Não foi possível localizar os dados para realizar a alteração do cadastro.";
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentDTOExists(id))
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

        // POST: api/Department
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<IContractResponse> PostDepartmentDTO(DepartmentModelView departmentModelView)
        {
            var iContractResponse = new IContractResponse<DepartmentDTO>();

            try
            {
                var departmentCategory = _context.DepartmentCategories.FirstOrDefault(t => t.description.ToUpper() == departmentModelView.departmentCategory.ToUpper());


                if (departmentCategory != null)
                {
                    DepartmentDTO departmentDTO = new DepartmentDTO();

                    departmentDTO.name                  = departmentModelView.name.ToUpper();
                    departmentDTO.deleted               = departmentModelView.deleted;
                    departmentDTO.dateCreated           = departmentModelView.dateCreated;
                    departmentDTO.departmentCategory    = departmentCategory;
                    _context.Departments.Add(departmentDTO);
                    await _context.SaveChangesAsync();

                    iContractResponse.success = true;
                    iContractResponse.data = departmentDTO;
                    iContractResponse.statusCode = this.HttpContext.Response.StatusCode;
                    iContractResponse.message = checkMessageDepartamentCategory("new", departmentCategory.description, departmentModelView.name); ;
                   
                }
                else
                {
                    iContractResponse.success = false;
                    iContractResponse.statusCode = this.HttpContext.Response.StatusCode;
                    iContractResponse.message = "Não foi possível localizar os dados para realizar o cadastro.";
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

        // DELETE: api/Department/5
        [HttpDelete("{id}")]
        public async Task<IContractResponse> DeleteDepartmentDTO(long id)
        {
            var departmentDTO = await _context.Departments.FindAsync(id);

            var iContractResponse = new IContractResponse();

            if (departmentDTO == null)
            {
                iContractResponse.success = false;
                iContractResponse.statusCode = this.HttpContext.Response.StatusCode;
                iContractResponse.message = "Id não localizado";
            }


            try
            {
                var department = _context.Departments
                                   .Where(t => t.id == id)
                                   .Include(t => t.departmentCategory)
                                   .FirstOrDefault();

                if (department != null)
                {
                    departmentDTO.deleted = "Y";
                    _context.Entry(departmentDTO).State = EntityState.Modified;
                    iContractResponse.success = true;
                    iContractResponse.statusCode = this.HttpContext.Response.StatusCode;
                    iContractResponse.message = checkMessageDepartamentCategory("delete", department.departmentCategory.description, departmentDTO.name); ;

                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentDTOExists(id))
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

        private bool DepartmentDTOExists(long id)
        {
            return _context.Departments.Any(e => e.id == id);
        }
    }

    public class DepartmentModelView
    {
        public long id { get; set; }
        public long idDepartmentCategory { get; set; }
        public string name { get; set; }
        public string deleted { get; set; }
        public DateTime dateCreated { get; set; }

        public string departmentCategory { get; set; }

    }
}
