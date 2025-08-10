using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgendaEstudiantil.Data.Migrations
{
    /// <inheritdoc />
    public partial class CrearTablaEventos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Eventos",
                newName: "Fecha");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Fecha",
                table: "Eventos",
                newName: "Date");
        }
    }
}
