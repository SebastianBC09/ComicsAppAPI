namespace ComicsAPI.Models {
  public class ProductoDeseado {
    public int Id { get; set; }
    public int ProductoId { get; set; }
    public int UserId { get; set; }
    public required Producto Producto { get; set; }
  }
}
