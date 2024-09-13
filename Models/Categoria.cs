namespace ComicsAPI.Models
{
  public class Categoria
  {
    public int Id { get; set; }
    public required string Nombre { get; set; }

    public required List<Producto> Productos { get; set; }
  }
}
