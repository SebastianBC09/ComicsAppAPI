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

    // CATEGORIAS
    [HttpGet("categorias")]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
    {
      return await _context.Categorias.ToListAsync();
    }

    // WISHLIST
    [HttpPost("deseados")]
    public async Task<ActionResult> AddProductoDeseado(int userId, int productoId)
    {
      var producto = await _context.Productos.FindAsync(productoId);
      if (producto == null)
      {
        return NotFound("Producto no existe!");
      }

      var productoDeseadoExistente = await _context.ProductosDeseados
          .AnyAsync(pd => pd.UserId == userId && pd.ProductoId == productoId);

      if (productoDeseadoExistente)
      {
        return BadRequest("Producto ya está en la lista de deseados!");
      }

      var productoDeseado = new ProductoDeseado
      {
        UserId = userId,
        ProductoId = productoId,
        Producto = producto
      };

      _context.ProductosDeseados.Add(productoDeseado);
      await _context.SaveChangesAsync();

      return Ok(new { Message = "Producto añadido a la lista de deseados!", Producto = producto });
    }

    [HttpGet("deseados/usuario/{userId}")]
    public async Task<ActionResult<IEnumerable<Producto>>> GetProductoDeseados(int userId)
    {
      var productosDeseados = await _context.ProductosDeseados
        .Where(pd => pd.UserId == userId)
        .Include(pd => pd.Producto)
        .Select(pd => pd.Producto)
        .ToListAsync();

      return Ok(productosDeseados);
    }

    [HttpDelete("deseados/{productoId}")]
    public async Task<ActionResult> RemoveProductoDeseado(int userId, int productoId)
    {
      var productoDeseado = await _context.ProductosDeseados
        .FirstOrDefaultAsync(pd => pd.UserId == userId && pd.ProductoId == productoId);

      if (productoDeseado == null)
      {
        return NotFound("El producto no está en la lista de deseados.");
      }

      _context.ProductosDeseados.Remove(productoDeseado);
      await _context.SaveChangesAsync();

      return Ok("Producto eliminado de la lista de deseados.");
    }
  }
}
