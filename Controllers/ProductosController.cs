using ComicsAPI.Data;
using ComicsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComicsAPI.Controllers {
  [Route("api/[controller]")]
  [ApiController]

  public class ProductosController: ControllerBase {
    private readonly ApplicationDbContext _context;
    public ProductosController(ApplicationDbContext context) {
      _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Producto>>> GetProductos() {
      return await _context.Productos.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Producto>> GetProducto(int id) {
      var Producto = await _context.Productos.FindAsync(id);
      if(Producto == null) {
        return NotFound();
      }
      return Producto;
    }

    [HttpPost]
    public async Task<ActionResult<Producto>> PostProducto(Producto producto) {
      _context.Productos.Add(producto);
      await _context.SaveChangesAsync();
      return CreatedAtAction(nameof(GetProducto), new { id = producto.Id }, producto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProducto(int id) {
      var Producto = await _context.Productos.FindAsync(id);
      if(Producto == null) {
        return NotFound();
      }
      _context.Productos.Remove(Producto);
      await _context.SaveChangesAsync();
      return NoContent();
    }

    private bool ProductoExists(int id) {
      return _context.Productos.Any(e => e.Id == id);
    }
  }
}
