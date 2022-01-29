using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JoinsPay_BackService.Data.Migrations
{
    public partial class account_Category : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Register.Account_Category",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    initials = table.Column<string>(maxLength: 6, nullable: false),
                    description = table.Column<string>(maxLength: 30, nullable: false),
                    deleted = table.Column<string>(maxLength: 1, nullable: true),
                    standard = table.Column<string>(maxLength: 1, nullable: false),
                    dateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Register.Account_Category", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Register.Account_Category");
        }
    }
}
