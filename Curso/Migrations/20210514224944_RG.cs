using Microsoft.EntityFrameworkCore.Migrations;

namespace DominandoEfCore.Migrations
{
    public partial class RG : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Rg",
                table: "Funcionarios",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rg",
                table: "Funcionarios");
        }
    }
}
