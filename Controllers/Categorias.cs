using ComicsAPI.Data;
using ComicsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComicsAPI.Controllers
{
  [Route("api/categorias")]
  [ApiController]

  public class CategoriasController : ControllerBase
  {
    private readonly ApplicationDbContext _context;
    public CategoriasController(ApplicationDbContext context)
    {
      _context = context;
    }

    // CATEGORIAS
    [HttpGet()]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
    {
      return await _context.Categorias.ToListAsync();
    }
  }
}
