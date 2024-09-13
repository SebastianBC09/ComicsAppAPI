using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComicsAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductoImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Imagen",
                table: "Productos",
                newName: "ImageURL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageURL",
                table: "Productos",
                newName: "Imagen");
        }
    }
}
