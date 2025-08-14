using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgendaEstudiantil.Data.Migrations
{
    /// <inheritdoc />
    public partial class NuevoCampo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Completado",
                table: "Eventos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Completado",
                table: "Eventos");
        }
    }
}
