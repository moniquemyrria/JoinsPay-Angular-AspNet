using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JoinsPay_BackService.Data.Migrations
{
    public partial class insert_department_in_account_and_payment_method_category_and_payment_method : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "idDepartment",
                table: "Register.Account",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Register.Payment_Method_Category",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(maxLength: 30, nullable: false),
                    deleted = table.Column<string>(maxLength: 1, nullable: true),
                    dateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Register.Payment_Method_Category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Register.Payment_Method",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idPaymentMethodCategory = table.Column<long>(nullable: false),
                    idAccount = table.Column<long>(nullable: false),
                    name = table.Column<string>(maxLength: 30, nullable: false),
                    acceptInstallment = table.Column<bool>(nullable: false),
                    numberInstallments = table.Column<int>(nullable: false),
                    intervalDaysInstallments = table.Column<int>(nullable: false),
                    deleted = table.Column<string>(maxLength: 1, nullable: true),
                    dateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Register.Payment_Method", x => x.id);
                    table.ForeignKey(
                        name: "FK_Register.Payment_Method_Register.Account_idAccount",
                        column: x => x.idAccount,
                        principalTable: "Register.Account",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Register.Payment_Method_Register.Payment_Method_Category_idPaymentMethodCategory",
                        column: x => x.idPaymentMethodCategory,
                        principalTable: "Register.Payment_Method_Category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Register.Account_idDepartment",
                table: "Register.Account",
                column: "idDepartment");

            migrationBuilder.CreateIndex(
                name: "IX_Register.Payment_Method_idAccount",
                table: "Register.Payment_Method",
                column: "idAccount");

            migrationBuilder.CreateIndex(
                name: "IX_Register.Payment_Method_idPaymentMethodCategory",
                table: "Register.Payment_Method",
                column: "idPaymentMethodCategory");

            migrationBuilder.AddForeignKey(
                name: "FK_Register.Account_Register.Department_idDepartment",
                table: "Register.Account",
                column: "idDepartment",
                principalTable: "Register.Department",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Register.Account_Register.Department_idDepartment",
                table: "Register.Account");

            migrationBuilder.DropTable(
                name: "Register.Payment_Method");

            migrationBuilder.DropTable(
                name: "Register.Payment_Method_Category");

            migrationBuilder.DropIndex(
                name: "IX_Register.Account_idDepartment",
                table: "Register.Account");

            migrationBuilder.DropColumn(
                name: "idDepartment",
                table: "Register.Account");
        }
    }
}
