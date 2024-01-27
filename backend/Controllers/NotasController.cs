using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotasEnsolvers.Models;
using System.Threading;

namespace NotasEnsolvers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotasController : ControllerBase
    {
        private readonly NotasContext _dbContext;
        #region dbContext
        public NotasController(NotasContext Context)
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
                List<Nota> listaNotas = _dbContext.Notas.Include(n => n.NotasCategoria).ThenInclude(nc => nc.IdCategoriaNavigation).OrderByDescending(t => t.IdNota).ToList();

                var notasDto = listaNotas.Select(n => new
                {
                    IdNota = n.IdNota,
                    Descripcion = n.Descripcion,
                    Archivada = n.Archivada,
                    Categorias = n.NotasCategoria.Select(nc => nc.IdCategoriaNavigation.Descripcion).ToList()
                });

                return StatusCode(StatusCodes.Status200OK, notasDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] Nota request)
        {
            try
            {
                await _dbContext.Notas.AddAsync(request);
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
                // Eliminar registros de NotasCategoria asociados a la nota
                var notasCategoria = _dbContext.NotasCategorias.Where(nc => nc.IdNota == id);
                _dbContext.NotasCategorias.RemoveRange(notasCategoria);

                // Eliminar la nota
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

        [HttpPut]
        [Route("Modificar/{id:int}")]
        public async Task<IActionResult> Modificar(int id, [FromBody] Nota nuevaTarea)
        {
            try
            {
                Nota nota = await _dbContext.Notas.FindAsync(id);

                if (nota == null)
                {
                    return NotFound();
                }

                nota.Descripcion = nuevaTarea.Descripcion;


                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("ModificarArchivada/{id:int}")]
        public async Task<IActionResult> ModificarArchivada(int id, [FromBody] Nota nuevaTarea)
        {
            try
            {
                Nota nota = await _dbContext.Notas.FindAsync(id);

                if (nota == null)
                {
                    return NotFound();
                }

                nota.Archivada = !nuevaTarea.Archivada;


                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("ModificarConCategoria/{id:int}")]
        public async Task<IActionResult> ModificarConCategoria(int id, [FromBody] NotaConCategoriasRequest request)
        {
            try
            {
                Nota nota = await _dbContext.Notas.FindAsync(id);

                    nota.Descripcion = request.Descripcion;

                

                await _dbContext.Notas.AddAsync(nota);
                await _dbContext.SaveChangesAsync();

                if (request.IdCategorias != null && request.IdCategorias.Any())
                {
                    foreach (int idCategoria in request.IdCategorias)
                    {
                        NotasCategoria notasCategoria = new NotasCategoria
                        {
                            IdNota = nota.IdNota,
                            IdCategoria = idCategoria
                        };
                      //  nota.NotasCategoria.Add(notasCategoria);
                       // await _dbContext.NotasCategorias.AddAsync(notasCategoria);
                    }

                    await _dbContext.SaveChangesAsync();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("GuardarConCategoria")]
        public async Task<IActionResult> GuardarConCategoria([FromBody] NotaConCategoriasRequest request)
        {
            try
            {                
                Nota nuevaNota = new Nota
                {
                    Descripcion = request.Descripcion,
                    Archivada = request.Archivada,
                    
                };
                      
                await _dbContext.Notas.AddAsync(nuevaNota);
                await _dbContext.SaveChangesAsync();
                              
                if (request.IdCategorias != null && request.IdCategorias.Any())
                {
                    foreach (int idCategoria in request.IdCategorias)
                    {
                        NotasCategoria notasCategoria = new NotasCategoria
                        {
                            IdNota = nuevaNota.IdNota,
                            IdCategoria = idCategoria
                        };
                        nuevaNota.NotasCategoria.Add(notasCategoria);
                        await _dbContext.NotasCategorias.AddAsync(notasCategoria);
                    }

                    await _dbContext.SaveChangesAsync();
                }
               return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        [HttpDelete]

        [Route("EliminarCategorias/{id:int}")]
        public async Task<IActionResult> EliminarCategorias(int id)
        {
            try
            {
                
                var notasCategoria = _dbContext.NotasCategorias.Where(nc => nc.IdNota == id);
                _dbContext.NotasCategorias.RemoveRange(notasCategoria);

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
