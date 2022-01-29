using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JoinsPay_BackService.Data.Migrations
{
    public partial class department_Category_and_department : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Register.Department_Category",
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
                    table.PrimaryKey("PK_Register.Department_Category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Register.Department",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idDepartamentCategory = table.Column<long>(nullable: false),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    deleted = table.Column<string>(maxLength: 1, nullable: true),
                    dateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Register.Department", x => x.id);
                    table.ForeignKey(
                        name: "FK_Register.Department_Register.Department_Category_idDepartamentCategory",
                        column: x => x.idDepartamentCategory,
                        principalTable: "Register.Department_Category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Register.Department_idDepartamentCategory",
                table: "Register.Department",
                column: "idDepartamentCategory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Register.Department");

            migrationBuilder.DropTable(
                name: "Register.Department_Category");
        }
    }
}
