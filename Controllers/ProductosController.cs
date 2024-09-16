using ComicsAPI.Data;
using ComicsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComicsAPI.Controllers
{
  [Route("api/productos")]
  [ApiController]
  public class ProductosController : ControllerBase
  {
    private readonly ApplicationDbContext _context;
    public ProductosController(ApplicationDbContext context)
    {
      _context = context;
    }

    // PRODUCTOS
    [HttpGet()]
    public async Task<ActionResult<IEnumerable<Producto>>> GetProductos(string? titulo, string? autor, string? editorial, string? genero, decimal? precioMin, decimal? precioMax
    )
    {
      var productos = _context.Productos.AsQueryable();
      if (!string.IsNullOrEmpty(titulo))
      {
        productos = productos.Where(p => p.Titulo.Contains(titulo));
      }
      if (!string.IsNullOrEmpty(autor))
      {
        productos = productos.Where(p => p.Autor.Contains(autor));
      }
      if (!string.IsNullOrEmpty(editorial))
      {
        productos = productos.Where(p => p.Editorial.Contains(editorial));
      }
      if (!string.IsNullOrEmpty(genero))
      {
        productos = productos.Where(p => p.Genero.Contains(genero));
      }
      if (precioMin.HasValue)
      {
        productos = productos.Where(p => p.Precio >= precioMin);
      }
      if (precioMax.HasValue)
      {
        productos = productos.Where(p => p.Precio <= precioMax);
      }
      return await productos.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Producto>> GetProducto(int id)
    {
      var producto = await _context.Productos.FirstOrDefaultAsync(p => p.Id == id);
      if (producto == null)
      {
        return NotFound();
      }
      return Ok(producto);
    }
  }
}
