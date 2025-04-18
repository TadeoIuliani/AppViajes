using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppViajesWirsolut.Migrations
{
    /// <inheritdoc />
    public partial class BaseDatosFinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Patente",
                table: "Vehiculos",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_Patente",
                table: "Vehiculos",
                column: "Patente",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ciudades_IdApiCiudad",
                table: "Ciudades",
                column: "IdApiCiudad",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vehiculos_Patente",
                table: "Vehiculos");

            migrationBuilder.DropIndex(
                name: "IX_Ciudades_IdApiCiudad",
                table: "Ciudades");

            migrationBuilder.AlterColumn<string>(
                name: "Patente",
                table: "Vehiculos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
