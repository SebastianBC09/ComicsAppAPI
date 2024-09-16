using ComicsAPI.Data;
using ComicsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComicsAPI.Controllers
{
  [Route("api/deseados")]
  [ApiController]
  public class WishlistController : ControllerBase
  {
    private readonly ApplicationDbContext _context;
    public WishlistController(ApplicationDbContext context)
    {
      _context = context;
    }

    [HttpGet()]
    public async Task<ActionResult<IEnumerable<Producto>>> GetProductoDeseados(int userId)
    {
      var productosDeseados = await _context.ProductosDeseados
        .Where(pd => pd.UserId == userId)
        .Include(pd => pd.Producto)
        .Select(pd => pd.Producto)
        .ToListAsync();

      return Ok(productosDeseados);
    }

    [HttpPost("agregar")]
    public async Task<IActionResult> AddToWishlist([FromBody] Wishlist request)
    {
      if (await _context.ProductosDeseados.AnyAsync(w => w.UserId == request.UserId && w.ProductoId == request.ProductoId))
      {
        return BadRequest("El producto ya está en la wishlist");
      }

      var producto = await _context.Productos.FindAsync(request.ProductoId);
      if (producto == null)
      {
        return NotFound("Producto no encontrado");
      }

      var wishlistItem = new ProductoDeseado
      {
        UserId = request.UserId,
        ProductoId = request.ProductoId
      };

      _context.ProductosDeseados.Add(wishlistItem);
      await _context.SaveChangesAsync();

      return Ok("Producto añadido a la lista de deseos");
    }


    [HttpDelete("eliminar")]
    public async Task<IActionResult> RemoveFromWishlist([FromBody] Wishlist request)
    {
      var wishlistItem = await _context.ProductosDeseados.FirstOrDefaultAsync(w => w.UserId == request.UserId && w.ProductoId == request.ProductoId);

      if (wishlistItem == null)
      {
        return NotFound("El producto no está en la wishlist");
      }

      _context.ProductosDeseados.Remove(wishlistItem);
      await _context.SaveChangesAsync();

      return Ok("Producto eliminado de la lista de deseos");
    }
  }
}
