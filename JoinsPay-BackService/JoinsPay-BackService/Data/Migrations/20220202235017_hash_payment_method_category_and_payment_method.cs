using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JoinsPay_BackService.Data.Migrations
{
    public partial class hash_payment_method_category_and_payment_method : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Register.Payment_Method_Payment_Method_Category",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idPaymentMethod = table.Column<long>(nullable: false),
                    idPaymentMethodCategory = table.Column<long>(nullable: false),
                    dateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Register.Payment_Method_Payment_Method_Category", x => x.id);
                    table.ForeignKey(
                        name: "FK_Register.Payment_Method_Payment_Method_Category_Register.Payment_Method_idPaymentMethod",
                        column: x => x.idPaymentMethod,
                        principalTable: "Register.Payment_Method",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Register.Payment_Method_Payment_Method_Category_Register.Payment_Method_Category_idPaymentMethodCategory",
                        column: x => x.idPaymentMethodCategory,
                        principalTable: "Register.Payment_Method_Category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Register.Payment_Method_Payment_Method_Category_idPaymentMethod",
                table: "Register.Payment_Method_Payment_Method_Category",
                column: "idPaymentMethod");

            migrationBuilder.CreateIndex(
                name: "IX_Register.Payment_Method_Payment_Method_Category_idPaymentMethodCategory",
                table: "Register.Payment_Method_Payment_Method_Category",
                column: "idPaymentMethodCategory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Register.Payment_Method_Payment_Method_Category");
        }
    }
}
