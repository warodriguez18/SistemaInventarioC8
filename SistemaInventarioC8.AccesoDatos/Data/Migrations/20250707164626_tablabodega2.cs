using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaInventarioC8.AccesoDatos.Data.Migrations
{
    /// <inheritdoc />
    public partial class tablabodega2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Bodegas",
                table: "Bodegas");

            migrationBuilder.RenameTable(
                name: "Bodegas",
                newName: "Bodega");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bodega",
                table: "Bodega",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Bodega",
                table: "Bodega");

            migrationBuilder.RenameTable(
                name: "Bodega",
                newName: "Bodegas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bodegas",
                table: "Bodegas",
                column: "Id");
        }
    }
}
