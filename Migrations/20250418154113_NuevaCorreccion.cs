using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppViajesWirsolut.Migrations
{
    /// <inheritdoc />
    public partial class NuevaCorreccion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdVehiculo",
                table: "Viajes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdVehiculo",
                table: "Viajes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
