using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkloadProject2025.Migrations
{
    /// <inheritdoc />
    public partial class AddHRProcessingFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HRNotes",
                table: "FacultyWorkLoads",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HRProcessed",
                table: "FacultyWorkLoads",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "HRProcessedDate",
                table: "FacultyWorkLoads",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HRNotes",
                table: "FacultyWorkLoads");

            migrationBuilder.DropColumn(
                name: "HRProcessed",
                table: "FacultyWorkLoads");

            migrationBuilder.DropColumn(
                name: "HRProcessedDate",
                table: "FacultyWorkLoads");
        }
    }
}
