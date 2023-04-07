using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingWebApi.DAL;
using ShoppingWebApi.DAL.Entities;

namespace ShoppingWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly DataBaseContext _context;
        //inyeccion de dependencias
        public CountriesController(DataBaseContext context)
        {
            /* con una sola instancia se puede llamar el
            contexto cuantas veces sea necesario
            con esto ya se puede hacer el crud pertinenete en la base
            de datos*/

            _context = context;
        }
        //Decorador
        [HttpGet,ActionName("Get")]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries() 
        {
            var countries = await _context.Countries.ToListAsync();

            if (countries == null) return NotFound();
            return countries;
        }

        [HttpGet, ActionName("Get")]
        [Route("Get({id}")]
        public  async Task<ActionResult<Country>> GetCountryById(Guid? id)
        {
            var country =await  _context.Countries.FirstOrDefaultAsync(c =>c.Id==id);

            if (country == null) return NotFound();
            return Ok(country);
        }

        [HttpPost, ActionName("Create")]
        [Route("Create")]
        public async Task<ActionResult> CreateCountry(Country country) {
            try 
            {
                country.Id= Guid.NewGuid();
                country.CreatedDate = DateTime.Now;
                _context.Countries.Add(country);
                await _context.SaveChangesAsync();// aqui se hace el insert
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe", country.Name));
            }
            catch(Exception ex) 
            {
                return Conflict(ex.Message);
            }

            return Ok(country);

        }

        [HttpPut, ActionName("Edit")]
        [Route("Edit/{id}")]
        public async Task<ActionResult> EditCountry(Guid? id,Country country)
        {
            try
            {
                if(id != country.Id) return NotFound("Country not found");
               
                country.ModifiedDate = DateTime.Now;
                _context.Countries.Update(country);
                await _context.SaveChangesAsync();// aqui se hace el update 
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe", country.Name));
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

            return Ok(country);

        }

        [HttpDelete, ActionName("Delete")]
        [Route("Delete/{id}")]
        public async Task<ActionResult> DeleteCountry(Guid? id) 
        {
            if (_context.Countries == null) {
                return Problem("Entity  set 'DataBaseContext.Countries' is null");
            }
            var country = await _context.Countries.FirstOrDefaultAsync(c=> c.Id==id);
        
            if(country == null)
            {
                return NotFound("Country not found");
            }
            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();//hace las veces de DELETE
            
            return Ok(String.Format("El pais {0} fue eliminado",country.Name));  
        }

    }
}
