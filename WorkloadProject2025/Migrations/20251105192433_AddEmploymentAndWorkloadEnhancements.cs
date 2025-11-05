using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkloadProject2025.Migrations
{
    /// <inheritdoc />
    public partial class AddEmploymentAndWorkloadEnhancements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppliesToEmploymentCategory",
                table: "WorkloadCategories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "WorkloadCategories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPrimaryInstructor",
                table: "FacultyWorkLoads",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "FacultyWorkLoads",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PercentShare",
                table: "FacultyWorkLoads",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProgramOfStudyId",
                table: "FacultyWorkLoads",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmploymentCategory",
                table: "Faculty",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Faculty",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Faculty",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FacultyWorkLoads_ProgramOfStudyId",
                table: "FacultyWorkLoads",
                column: "ProgramOfStudyId");

            migrationBuilder.AddForeignKey(
                name: "FK_FacultyWorkLoads_ProgramsOfStudy_ProgramOfStudyId",
                table: "FacultyWorkLoads",
                column: "ProgramOfStudyId",
                principalTable: "ProgramsOfStudy",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FacultyWorkLoads_ProgramsOfStudy_ProgramOfStudyId",
                table: "FacultyWorkLoads");

            migrationBuilder.DropIndex(
                name: "IX_FacultyWorkLoads_ProgramOfStudyId",
                table: "FacultyWorkLoads");

            migrationBuilder.DropColumn(
                name: "AppliesToEmploymentCategory",
                table: "WorkloadCategories");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "WorkloadCategories");

            migrationBuilder.DropColumn(
                name: "IsPrimaryInstructor",
                table: "FacultyWorkLoads");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "FacultyWorkLoads");

            migrationBuilder.DropColumn(
                name: "PercentShare",
                table: "FacultyWorkLoads");

            migrationBuilder.DropColumn(
                name: "ProgramOfStudyId",
                table: "FacultyWorkLoads");

            migrationBuilder.DropColumn(
                name: "EmploymentCategory",
                table: "Faculty");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Faculty");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Faculty");
        }
    }
}
