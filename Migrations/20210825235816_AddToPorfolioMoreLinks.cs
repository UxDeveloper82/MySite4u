using Microsoft.EntityFrameworkCore.Migrations;

namespace MySite4u.Migrations
{
    public partial class AddToPorfolioMoreLinks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodeLink",
                table: "Portfolios",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodeLink",
                table: "Portfolios");
        }
    }
}
