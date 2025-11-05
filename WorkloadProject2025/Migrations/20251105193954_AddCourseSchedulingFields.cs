using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkloadProject2025.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseSchedulingFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoveredForFacultyEmail",
                table: "FacultyWorkLoads",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCoverage",
                table: "FacultyWorkLoads",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "BlockSlot",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "EndTime",
                table: "Courses",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Enrollment",
                table: "Courses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MeetingDays",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Room",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "StartTime",
                table: "Courses",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TermId",
                table: "Courses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TermId",
                table: "Courses",
                column: "TermId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Terms_TermId",
                table: "Courses",
                column: "TermId",
                principalTable: "Terms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Terms_TermId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_TermId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CoveredForFacultyEmail",
                table: "FacultyWorkLoads");

            migrationBuilder.DropColumn(
                name: "IsCoverage",
                table: "FacultyWorkLoads");

            migrationBuilder.DropColumn(
                name: "BlockSlot",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Enrollment",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "MeetingDays",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Room",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "TermId",
                table: "Courses");
        }
    }
}
