using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkloadProject2025.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureAssignmentPercentagePrecision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Annotations",
                table: "FacultyWorkLoads",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovedBy",
                table: "FacultyWorkLoads",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedDate",
                table: "FacultyWorkLoads",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClonedFromId",
                table: "FacultyWorkLoads",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CoveringForFacultyEmail",
                table: "FacultyWorkLoads",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "FacultyWorkLoads",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "FacultyWorkLoads",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsCoverage",
                table: "FacultyWorkLoads",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "FacultyWorkLoads",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "FacultyWorkLoads",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RejectionReason",
                table: "FacultyWorkLoads",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SourceYear",
                table: "FacultyWorkLoads",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "FacultyWorkLoads",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmittedDate",
                table: "FacultyWorkLoads",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "Faculty",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
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
                name: "EmploymentCategory",
                table: "Faculty",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "HireDate",
                table: "Faculty",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPinned",
                table: "Faculty",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Faculty",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "Faculty",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxCourseLoad",
                table: "Faculty",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "MaxTeachingHours",
                table: "Faculty",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "CourseCode",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPinned",
                table: "Courses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "CourseAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstructorEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    TermId = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    AssignmentPercentage = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseAssignments_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseAssignments_Faculty_InstructorEmail",
                        column: x => x.InstructorEmail,
                        principalTable: "Faculty",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseAssignments_Terms_TermId",
                        column: x => x.TermId,
                        principalTable: "Terms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FacultyPreferredCourses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacultyEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultyPreferredCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacultyPreferredCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacultyPreferredCourses_Faculty_FacultyEmail",
                        column: x => x.FacultyEmail,
                        principalTable: "Faculty",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FacultyQualifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacultyEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    AwardedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultyQualifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacultyQualifications_Faculty_FacultyEmail",
                        column: x => x.FacultyEmail,
                        principalTable: "Faculty",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstructorAvailabilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacultyEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructorAvailabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstructorAvailabilities_Faculty_FacultyEmail",
                        column: x => x.FacultyEmail,
                        principalTable: "Faculty",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstructorPreferences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacultyEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PreferredDeliveryMode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxTravelDistanceKm = table.Column<int>(type: "int", nullable: true),
                    PreferredCampus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructorPreferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstructorPreferences_Faculty_FacultyEmail",
                        column: x => x.FacultyEmail,
                        principalTable: "Faculty",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstructorProgramAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacultyEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProgramOfStudyId = table.Column<int>(type: "int", nullable: false),
                    MaxCourseLoadOverride = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructorProgramAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstructorProgramAssignments_Faculty_FacultyEmail",
                        column: x => x.FacultyEmail,
                        principalTable: "Faculty",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstructorProgramAssignments_ProgramsOfStudy_ProgramOfStudyId",
                        column: x => x.ProgramOfStudyId,
                        principalTable: "ProgramsOfStudy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Faculty_DepartmentId",
                table: "Faculty",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseAssignments_CourseId",
                table: "CourseAssignments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseAssignments_InstructorEmail",
                table: "CourseAssignments",
                column: "InstructorEmail");

            migrationBuilder.CreateIndex(
                name: "IX_CourseAssignments_TermId",
                table: "CourseAssignments",
                column: "TermId");

            migrationBuilder.CreateIndex(
                name: "IX_FacultyPreferredCourses_CourseId",
                table: "FacultyPreferredCourses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_FacultyPreferredCourses_FacultyEmail",
                table: "FacultyPreferredCourses",
                column: "FacultyEmail");

            migrationBuilder.CreateIndex(
                name: "IX_FacultyQualifications_FacultyEmail",
                table: "FacultyQualifications",
                column: "FacultyEmail");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorAvailabilities_FacultyEmail",
                table: "InstructorAvailabilities",
                column: "FacultyEmail");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorPreferences_FacultyEmail",
                table: "InstructorPreferences",
                column: "FacultyEmail");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorProgramAssignments_FacultyEmail",
                table: "InstructorProgramAssignments",
                column: "FacultyEmail");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorProgramAssignments_ProgramOfStudyId",
                table: "InstructorProgramAssignments",
                column: "ProgramOfStudyId");

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

            migrationBuilder.DropTable(
                name: "CourseAssignments");

            migrationBuilder.DropTable(
                name: "FacultyPreferredCourses");

            migrationBuilder.DropTable(
                name: "FacultyQualifications");

            migrationBuilder.DropTable(
                name: "InstructorAvailabilities");

            migrationBuilder.DropTable(
                name: "InstructorPreferences");

            migrationBuilder.DropTable(
                name: "InstructorProgramAssignments");

            migrationBuilder.DropIndex(
                name: "IX_Faculty_DepartmentId",
                table: "Faculty");

            migrationBuilder.DropColumn(
                name: "Annotations",
                table: "FacultyWorkLoads");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "FacultyWorkLoads");

            migrationBuilder.DropColumn(
                name: "ApprovedDate",
                table: "FacultyWorkLoads");

            migrationBuilder.DropColumn(
                name: "ClonedFromId",
                table: "FacultyWorkLoads");

            migrationBuilder.DropColumn(
                name: "CoveringForFacultyEmail",
                table: "FacultyWorkLoads");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "FacultyWorkLoads");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "FacultyWorkLoads");

            migrationBuilder.DropColumn(
                name: "IsCoverage",
                table: "FacultyWorkLoads");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "FacultyWorkLoads");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "FacultyWorkLoads");

            migrationBuilder.DropColumn(
                name: "RejectionReason",
                table: "FacultyWorkLoads");

            migrationBuilder.DropColumn(
                name: "SourceYear",
                table: "FacultyWorkLoads");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "FacultyWorkLoads");

            migrationBuilder.DropColumn(
                name: "SubmittedDate",
                table: "FacultyWorkLoads");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Faculty");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Faculty");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Faculty");

            migrationBuilder.DropColumn(
                name: "EmploymentCategory",
                table: "Faculty");

            migrationBuilder.DropColumn(
                name: "HireDate",
                table: "Faculty");

            migrationBuilder.DropColumn(
                name: "IsPinned",
                table: "Faculty");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Faculty");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "Faculty");

            migrationBuilder.DropColumn(
                name: "MaxCourseLoad",
                table: "Faculty");

            migrationBuilder.DropColumn(
                name: "MaxTeachingHours",
                table: "Faculty");

            migrationBuilder.DropColumn(
                name: "CourseCode",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "IsPinned",
                table: "Courses");
        }
    }
}
