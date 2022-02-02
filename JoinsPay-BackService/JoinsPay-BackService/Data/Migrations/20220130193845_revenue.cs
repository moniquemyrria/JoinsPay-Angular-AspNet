using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JoinsPay_BackService.Data.Migrations
{
    public partial class revenue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Revenue",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idRevenueCategory = table.Column<long>(nullable: false),
                    idAccount = table.Column<long>(nullable: false),
                    idDepartment = table.Column<long>(nullable: false),
                    amount = table.Column<double>(nullable: false),
                    description = table.Column<string>(maxLength: 200, nullable: false),
                    deleted = table.Column<string>(maxLength: 1, nullable: true),
                    dateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Revenue", x => x.id);
                    table.ForeignKey(
                        name: "FK_Revenue_Register.Account_idAccount",
                        column: x => x.idAccount,
                        principalTable: "Register.Account",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Revenue_Register.Department_idDepartment",
                        column: x => x.idDepartment,
                        principalTable: "Register.Department",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Revenue_Register.Revenue_Category_idRevenueCategory",
                        column: x => x.idRevenueCategory,
                        principalTable: "Register.Revenue_Category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Revenue_idAccount",
                table: "Revenue",
                column: "idAccount");

            migrationBuilder.CreateIndex(
                name: "IX_Revenue_idDepartment",
                table: "Revenue",
                column: "idDepartment");

            migrationBuilder.CreateIndex(
                name: "IX_Revenue_idRevenueCategory",
                table: "Revenue",
                column: "idRevenueCategory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Revenue");
        }
    }
}
