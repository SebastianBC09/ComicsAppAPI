namespace ComicsAPI.Models {
  public class Producto {
    public int Id { get; set; }
    public required string Titulo { get; set; }
    public required string Autor { get; set; }
    public required string Editorial { get; set; }
    public DateTime FechaPublicacion { get; set; }
    public required string Genero { get; set; }
    public decimal Precio { get; set; }
    public required string Imagen { get; set; }
    public required string Descripcion { get; set; }
  }
}
