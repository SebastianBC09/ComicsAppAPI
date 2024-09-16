using ComicsAPI.Data;
using ComicsAPI.Models;
using ComicsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComicsAPI.Controllers
{
  [Route("api/auth")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    private readonly ApplicationDbContext _context;
    private readonly PasswordService _passwordService;

    public AuthController(ApplicationDbContext context, PasswordService passwordService)
    {
      _context = context;
      _passwordService = passwordService;
    }

    // REGISTRO
    [HttpPost("registro")]
    public async Task<IActionResult> Register([FromBody] Usuario usuario)
    {
      if (await _context.Usuarios.AnyAsync(u => u.Email == usuario.Email))
      {
        return BadRequest("El email ya está registrado");
      }

      usuario.PasswordHash = _passwordService.HashPassword((usuario.PasswordHash));

      _context.Usuarios.Add(usuario);
      await _context.SaveChangesAsync();

      return Ok(usuario);
    }

    // LOGIN
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] Login loginRequest)
    {
      var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == loginRequest.Email);
      if (usuario == null)
      {
        return Unauthorized("El correo electronico o la contrasena son incorrectos");
      }

      var isPasswordValid = _passwordService.VerifyPassword(usuario.PasswordHash, loginRequest.Password);
      if (!isPasswordValid)
      {
        return Unauthorized("El correo electronico o la contrasena son incorrectos");
      }

      var userResponse = new
      {
        Id = usuario.Id,
        NombreUsuario = usuario.NombreUsuario,
        Email = usuario.Email,
      };

      return Ok(new { user = userResponse, message = "Inicio de sesión exitoso!" });
    }
  }
}
