using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingWebApi.DAL;
using ShoppingWebApi.DAL.Entities;

namespace ShoppingWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly DataBaseContext _context;
        //inyeccion de dependencias
        public CategoriesController(DataBaseContext context)
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
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories() 
        {
            var categories = await _context.Categories.ToListAsync();

            if (categories == null) return NotFound();
            return categories;
        }

        [HttpGet, ActionName("Get")]
        [Route("Get({id}")]
        public  async Task<ActionResult<Category>> GetCategoryById(Guid? id)
        {
            var category =await  _context.Categories.FirstOrDefaultAsync(c =>c.Id==id);

            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpPost, ActionName("Create")]
        [Route("Create")]
        public async Task<ActionResult> CreateCategory(Category category) {
            try 
            {
                category.Id= Guid.NewGuid();
                category.CreatedDate = DateTime.Now;
                
                
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();// aqui se hace el insert
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe", category.Name));
            }
            catch(Exception ex) 
            {
                return Conflict(ex.Message);
            }

            return Ok(category);

        }

        [HttpPut, ActionName("Edit")]
        [Route("Edit/{id}")]
        public async Task<ActionResult> EditCategory(Guid? id,Category category)
        {
            try
            {
                if(id != category.Id) return NotFound("Category not found");
               
                category.ModifiedDate = DateTime.Now;
                _context.Categories.Update(category);
                await _context.SaveChangesAsync();// aqui se hace el update 
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe", category.Name));
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

            return Ok(category);

        }

        [HttpDelete, ActionName("Delete")]
        [Route("Delete/{id}")]
        public async Task<ActionResult> DeleteCategory(Guid? id) 
        {
            if (_context.Categories == null) {
                return Problem("Entity  set 'DataBaseContext.Categories' is null");
            }
            var category = await _context.Categories.FirstOrDefaultAsync(c=> c.Id==id);
        
            if(category == null)
            {
                return NotFound("category not found");
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();//hace las veces de DELETE
            
            return Ok(String.Format("La categoria {0} fue eliminada",category.Name));  
        }

    }
}
