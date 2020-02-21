using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PortfolioPage.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ConceptionDate = table.Column<DateTime>(nullable: false),
                    CompletionDate = table.Column<DateTime>(nullable: false),
                    CompletionDeadline = table.Column<DateTime>(nullable: false),
                    numberOfComponents = table.Column<int>(nullable: false),
                    currentProjectStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Project");
        }
    }
}
