using Microsoft.EntityFrameworkCore.Migrations;

namespace JoinsPay_BackService.Data.Migrations
{
    public partial class add_field_icon_expense_type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "routerLink",
                table: "Expense.Expense_Type",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "icon",
                table: "Expense.Expense_Type",
                maxLength: 30,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "icon",
                table: "Expense.Expense_Type");

            migrationBuilder.AlterColumn<string>(
                name: "routerLink",
                table: "Expense.Expense_Type",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
