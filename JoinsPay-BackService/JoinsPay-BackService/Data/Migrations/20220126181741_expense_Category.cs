using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JoinsPay_BackService.Data.Migrations
{
    public partial class expense_Category : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "delete",
                table: "Register.Revenue_Category");

            migrationBuilder.AddColumn<string>(
                name: "deleted",
                table: "Register.Revenue_Category",
                maxLength: 1,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Register.Expense_Category",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    initials = table.Column<string>(maxLength: 6, nullable: false),
                    description = table.Column<string>(maxLength: 30, nullable: false),
                    color = table.Column<string>(maxLength: 10, nullable: true),
                    deleted = table.Column<string>(maxLength: 1, nullable: true),
                    dateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Register.Expense_Category", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Register.Expense_Category");

            migrationBuilder.DropColumn(
                name: "deleted",
                table: "Register.Revenue_Category");

            migrationBuilder.AddColumn<string>(
                name: "delete",
                table: "Register.Revenue_Category",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: true);
        }
    }
}
