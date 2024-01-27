using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotasEnsolvers.Models;

namespace NotasEnsolvers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
            private readonly NotasContext _dbContext;
        #region dbContext
        public CategoriaController(NotasContext Context)
            {
                _dbContext = Context;
            }
        #endregion
        #region endpoints
        [HttpGet]
        [Route("Lista")]
         public async Task<IActionResult> Lista()
            {
                try
                {
                    List<Categorium> lista = _dbContext.Categoria.OrderByDescending(t => t.IdCategoria).ToList();
                    return StatusCode(StatusCodes.Status200OK, lista);
                }
                catch (Exception ex)
                {

                    return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
                }
            }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] Categorium request)
         {
                try
                {
                    await _dbContext.Categoria.AddAsync(request);
                    await _dbContext.SaveChangesAsync();
                    return StatusCode(StatusCodes.Status200OK, "OK");
                }
                catch (Exception ex)
                {

                    return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
                }
         }

         [HttpDelete]
         [Route("Cerrar/{id:int}")]
         public async Task<IActionResult> Cerrar(int id)
         {
                try
                {
                    Categorium categoria = _dbContext.Categoria.Find(id);

                    if (categoria == null)
                    {
                        return NotFound();
                    }

                    _dbContext.Categoria.Remove(categoria);

                    await _dbContext.SaveChangesAsync();
                    return StatusCode(StatusCodes.Status200OK, "OK");
                }
                catch (Exception ex)
                {

                    return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
                }
         }
        #endregion




    }
}
