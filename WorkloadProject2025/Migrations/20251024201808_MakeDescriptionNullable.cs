using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkloadProject2025.Migrations
{
    /// <inheritdoc />
    public partial class MakeDescriptionNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FacultyWorkLoads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacultyEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: true),
                    TermId = table.Column<int>(type: "int", nullable: false),
                    WorkloadCategoryId = table.Column<int>(type: "int", nullable: true),
                    Workload = table.Column<int>(type: "int", nullable: false),
                    HoursAssigned = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAssigned = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultyWorkLoads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacultyWorkLoads_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FacultyWorkLoads_Faculty_FacultyEmail",
                        column: x => x.FacultyEmail,
                        principalTable: "Faculty",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacultyWorkLoads_Terms_TermId",
                        column: x => x.TermId,
                        principalTable: "Terms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacultyWorkLoads_WorkloadCategories_WorkloadCategoryId",
                        column: x => x.WorkloadCategoryId,
                        principalTable: "WorkloadCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FacultyWorkLoads_CourseId",
                table: "FacultyWorkLoads",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_FacultyWorkLoads_FacultyEmail",
                table: "FacultyWorkLoads",
                column: "FacultyEmail");

            migrationBuilder.CreateIndex(
                name: "IX_FacultyWorkLoads_TermId",
                table: "FacultyWorkLoads",
                column: "TermId");

            migrationBuilder.CreateIndex(
                name: "IX_FacultyWorkLoads_WorkloadCategoryId",
                table: "FacultyWorkLoads",
                column: "WorkloadCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FacultyWorkLoads");
        }
    }
}
