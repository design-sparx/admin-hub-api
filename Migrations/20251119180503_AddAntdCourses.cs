using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminHubApi.Migrations
{
    /// <inheritdoc />
    public partial class AddAntdCourses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AntdCourses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    InstructorName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreditHours = table.Column<int>(type: "integer", nullable: false),
                    Department = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Prerequisites = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CourseLocation = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    TotalLessons = table.Column<int>(type: "integer", nullable: false),
                    CurrentLessons = table.Column<int>(type: "integer", nullable: false),
                    FavoriteColor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntdCourses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AntdCourses_Code",
                table: "AntdCourses",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_AntdCourses_Department",
                table: "AntdCourses",
                column: "Department");

            migrationBuilder.CreateIndex(
                name: "IX_AntdCourses_InstructorName",
                table: "AntdCourses",
                column: "InstructorName");

            migrationBuilder.CreateIndex(
                name: "IX_AntdCourses_StartDate",
                table: "AntdCourses",
                column: "StartDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AntdCourses");
        }
    }
}
