using System.ComponentModel.DataAnnotations;

namespace ComicsAPI.Models
{
  public class Usuario
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string NombreUsuario { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(12)]
    public string PasswordHash { get; set; }

  }
}
