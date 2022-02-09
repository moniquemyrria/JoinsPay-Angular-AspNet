using Microsoft.EntityFrameworkCore.Migrations;

namespace JoinsPay_BackService.Data.Migrations
{
    public partial class add_field_routerLink_expense_type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "routerLink",
                table: "Expense.Expense_Type",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "routerLink",
                table: "Expense.Expense_Type");
        }
    }
}
