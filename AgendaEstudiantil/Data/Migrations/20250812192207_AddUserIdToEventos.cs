using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgendaEstudiantil.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToEventos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Eventos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Eventos");
        }
    }
}
