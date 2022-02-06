using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JoinsPay_BackService.Data.Migrations
{
    public partial class expense_expense_status_expense_type_payment_method_category : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Expense.Expense_Status",
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
                    table.PrimaryKey("PK_Expense.Expense_Status", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Expense.Expense_Type",
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
                    table.PrimaryKey("PK_Expense.Expense_Type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Expense",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idExpenseCategory = table.Column<long>(nullable: false),
                    idPaymentMethod = table.Column<long>(nullable: false),
                    idDepartment = table.Column<long>(nullable: false),
                    idAccount = table.Column<long>(nullable: false),
                    idExpenseStatus = table.Column<long>(nullable: false),
                    idExpenseType = table.Column<long>(nullable: false),
                    amount = table.Column<double>(nullable: false),
                    fine = table.Column<double>(nullable: false),
                    interest = table.Column<double>(nullable: false),
                    discount = table.Column<double>(nullable: false),
                    qtyInstallment = table.Column<int>(nullable: false),
                    installment = table.Column<int>(nullable: false),
                    description = table.Column<string>(maxLength: 200, nullable: true),
                    dateCreated = table.Column<DateTime>(nullable: false),
                    dueDate = table.Column<DateTime>(nullable: true),
                    paymentDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expense", x => x.id);
                    table.ForeignKey(
                        name: "FK_Expense_Register.Account_idAccount",
                        column: x => x.idAccount,
                        principalTable: "Register.Account",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Expense_Register.Department_idDepartment",
                        column: x => x.idDepartment,
                        principalTable: "Register.Department",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Expense_Register.Expense_Category_idExpenseCategory",
                        column: x => x.idExpenseCategory,
                        principalTable: "Register.Expense_Category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Expense_Expense.Expense_Status_idExpenseStatus",
                        column: x => x.idExpenseStatus,
                        principalTable: "Expense.Expense_Status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Expense_Expense.Expense_Type_idExpenseType",
                        column: x => x.idExpenseType,
                        principalTable: "Expense.Expense_Type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Expense_Register.Payment_Method_idPaymentMethod",
                        column: x => x.idPaymentMethod,
                        principalTable: "Register.Payment_Method",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Expense.Expense_Payment_Method_Category",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idExpense = table.Column<long>(nullable: false),
                    idPaymentMethodCategory = table.Column<long>(nullable: false),
                    dateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expense.Expense_Payment_Method_Category", x => x.id);
                    table.ForeignKey(
                        name: "FK_Expense.Expense_Payment_Method_Category_Expense_idExpense",
                        column: x => x.idExpense,
                        principalTable: "Expense",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Expense.Expense_Payment_Method_Category_Register.Payment_Method_Category_idPaymentMethodCategory",
                        column: x => x.idPaymentMethodCategory,
                        principalTable: "Register.Payment_Method_Category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expense_idAccount",
                table: "Expense",
                column: "idAccount");

            migrationBuilder.CreateIndex(
                name: "IX_Expense_idDepartment",
                table: "Expense",
                column: "idDepartment");

            migrationBuilder.CreateIndex(
                name: "IX_Expense_idExpenseCategory",
                table: "Expense",
                column: "idExpenseCategory");

            migrationBuilder.CreateIndex(
                name: "IX_Expense_idExpenseStatus",
                table: "Expense",
                column: "idExpenseStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Expense_idExpenseType",
                table: "Expense",
                column: "idExpenseType");

            migrationBuilder.CreateIndex(
                name: "IX_Expense_idPaymentMethod",
                table: "Expense",
                column: "idPaymentMethod");

            migrationBuilder.CreateIndex(
                name: "IX_Expense.Expense_Payment_Method_Category_idExpense",
                table: "Expense.Expense_Payment_Method_Category",
                column: "idExpense");

            migrationBuilder.CreateIndex(
                name: "IX_Expense.Expense_Payment_Method_Category_idPaymentMethodCategory",
                table: "Expense.Expense_Payment_Method_Category",
                column: "idPaymentMethodCategory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expense.Expense_Payment_Method_Category");

            migrationBuilder.DropTable(
                name: "Expense");

            migrationBuilder.DropTable(
                name: "Expense.Expense_Status");

            migrationBuilder.DropTable(
                name: "Expense.Expense_Type");
        }
    }
}
