using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotasEnsolvers.Models;

namespace NotasEnsolvers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotasCategoriaController : ControllerBase
    {
        private readonly NotasContext _dbContext;
        #region dbContext
        public NotasCategoriaController(NotasContext Context)
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
                List<NotasCategoria> lista = _dbContext.NotasCategorias.OrderByDescending(t => t.IdCategoria).ToList();
                return StatusCode(StatusCodes.Status200OK, lista);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] NotasCategoria request)
        {
            try
            {
                await _dbContext.NotasCategorias.AddAsync(request);
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
                
                var notasCategoria = _dbContext.NotasCategorias.Where(nc => nc.IdNota == id);
                _dbContext.NotasCategorias.RemoveRange(notasCategoria);

                
                var nota = _dbContext.Notas.Find(id);
                if (nota == null)
                {
                    return NotFound();
                }

                _dbContext.Notas.Remove(nota);

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
