using Microsoft.EntityFrameworkCore.Migrations;

namespace casln.Infrastructure.Persistence.Migrations
{
    public partial class ListCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "TodoLists",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "TodoLists");
        }
    }
}
