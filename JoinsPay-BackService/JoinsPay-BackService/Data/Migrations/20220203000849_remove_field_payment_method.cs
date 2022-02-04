using Microsoft.EntityFrameworkCore.Migrations;

namespace JoinsPay_BackService.Data.Migrations
{
    public partial class remove_field_payment_method : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Register.Payment_Method_Register.Payment_Method_Category_idPaymentMethodCategory",
                table: "Register.Payment_Method");

            migrationBuilder.DropIndex(
                name: "IX_Register.Payment_Method_idPaymentMethodCategory",
                table: "Register.Payment_Method");

            migrationBuilder.DropColumn(
                name: "idPaymentMethodCategory",
                table: "Register.Payment_Method");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "idPaymentMethodCategory",
                table: "Register.Payment_Method",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Register.Payment_Method_idPaymentMethodCategory",
                table: "Register.Payment_Method",
                column: "idPaymentMethodCategory");

            migrationBuilder.AddForeignKey(
                name: "FK_Register.Payment_Method_Register.Payment_Method_Category_idPaymentMethodCategory",
                table: "Register.Payment_Method",
                column: "idPaymentMethodCategory",
                principalTable: "Register.Payment_Method_Category",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
