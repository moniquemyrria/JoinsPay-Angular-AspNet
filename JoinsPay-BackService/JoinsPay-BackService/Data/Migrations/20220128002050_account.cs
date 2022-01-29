using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JoinsPay_BackService.Data.Migrations
{
    public partial class account : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Register.Account",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idAccountCategory = table.Column<long>(nullable: false),
                    code = table.Column<string>(maxLength: 10, nullable: false),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    agency = table.Column<string>(maxLength: 10, nullable: false),
                    accountNumber = table.Column<string>(maxLength: 10, nullable: false),
                    deleted = table.Column<string>(maxLength: 1, nullable: true),
                    dateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Register.Account", x => x.id);
                    table.ForeignKey(
                        name: "FK_Register.Account_Register.Account_Category_idAccountCategory",
                        column: x => x.idAccountCategory,
                        principalTable: "Register.Account_Category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Register.Account_idAccountCategory",
                table: "Register.Account",
                column: "idAccountCategory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Register.Account");
        }
    }
}
