using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimeTech.DataAccess.Data;
using PrimeTech.DataAccess.Model;
using PrimeTech.Mapping;
using PrimeTech.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PrimeTech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        ApplicationDbContext _DbContext;
        private readonly IMapper _mapper;
        public CompanyController(ApplicationDbContext DbContext, IMapper mapper)
        {
            _DbContext = DbContext;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<List<VmCompany>>> GetList()
        {
            try
            {
               
                var data=  await _DbContext.Company.ToListAsync();
                var vm=_mapper.Map<List<Company>, List<VmCompany>>((List<Company>)data);
                return vm;
            }

            catch (Exception ex)
            {
                return StatusCode(417, ex.Message);
            }
        }
        [HttpGet]
        [Route("/GetAttributeList")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<List<VmAttributes>>> GetAttributeList()
        {
            try
            {

                var data = await _DbContext.Attributes.ToListAsync();
                var vm = _mapper.Map<List<Attributes>, List<VmAttributes>>((List<Attributes>)data);
                return vm;
            }

            catch (Exception ex)
            {
                return StatusCode(417, ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<VmCompany>> GetById(int id)
        {
            try
            {
                var data= await _DbContext.Company.Include(x => x.AttributesValue).ThenInclude(a => a.Attributes).SingleOrDefaultAsync(a=>a.Id==id);
                var model = _mapper.Map<VmCompany>(data);
                return model;
            }

            catch (Exception ex)
            {
                return StatusCode(417, ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<VmCompany>> Post([FromBody] VmCompany vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    var data = _mapper.Map<Company>(vm);
                    _DbContext.Add(data);
                    await _DbContext.SaveChangesAsync();
                    return vm;

                }
                else
                {
                    return StatusCode(400, "Invalid Parameter");
                    return null;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(417, ex.Message);
            }

        }
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> Put(int id,[FromBody] VmCompany vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (await _DbContext.Company.AnyAsync(c => c.Id == id))
                    {
                        var data = _mapper.Map<Company>(vm);
                        _DbContext.Update(data);
                        await _DbContext.SaveChangesAsync();
                        return Ok(1);
                    }
                    return StatusCode(401, "Id Not Wxist");

                }
                else
                {
                    return StatusCode(400, "Invalid Parameter");
                    return null;
                }

            }
            catch (Exception ex)
            {
                return StatusCode(417, ex.Message);
            }
        }
        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var data = await _DbContext.Company.FindAsync(id);
                if (data != null)
                {
                    _DbContext.Remove(data);
                    await _DbContext.SaveChangesAsync();
                    return Ok(1);
                }
                return StatusCode(401, "Id Not Wxist");
            }
            catch (Exception ex)
            {
                return StatusCode(417, ex.Message);
            }
        }
    }
}
