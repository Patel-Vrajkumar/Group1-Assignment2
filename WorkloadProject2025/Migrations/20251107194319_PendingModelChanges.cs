using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkloadProject2025.Migrations
{
    /// <inheritdoc />
    public partial class PendingModelChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateHired",
                table: "Faculty",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Faculty",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Faculty",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Faculty_DepartmentId",
                table: "Faculty",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Faculty_Departments_DepartmentId",
                table: "Faculty",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faculty_Departments_DepartmentId",
                table: "Faculty");

            migrationBuilder.DropIndex(
                name: "IX_Faculty_DepartmentId",
                table: "Faculty");

            migrationBuilder.DropColumn(
                name: "DateHired",
                table: "Faculty");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Faculty");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Faculty");
        }
    }
}
