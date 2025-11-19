using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminHubApi.Migrations
{
    /// <inheritdoc />
    public partial class AddAntdLearningDashboard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AntdCommunityGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Image = table.Column<string>(type: "text", nullable: true),
                    Category = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Location = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Size = table.Column<int>(type: "integer", nullable: false),
                    Leader = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MeetingTime = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    MemberAgeRange = table.Column<int>(type: "integer", nullable: false),
                    MemberInterests = table.Column<string>(type: "text", nullable: true),
                    FavoriteColor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntdCommunityGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AntdExams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Course = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CourseCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ExamDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExamTime = table.Column<int>(type: "integer", nullable: false),
                    ExamDuration = table.Column<int>(type: "integer", nullable: false),
                    ExamScore = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntdExams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AntdRecommendedCourses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Duration = table.Column<int>(type: "integer", nullable: false),
                    Level = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Category = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Instructor = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CourseLanguage = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    FavoriteColor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Lessons = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntdRecommendedCourses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AntdStudyStatistics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    Category = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Month = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntdStudyStatistics", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AntdCommunityGroups_Category",
                table: "AntdCommunityGroups",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_AntdCommunityGroups_Leader",
                table: "AntdCommunityGroups",
                column: "Leader");

            migrationBuilder.CreateIndex(
                name: "IX_AntdCommunityGroups_Location",
                table: "AntdCommunityGroups",
                column: "Location");

            migrationBuilder.CreateIndex(
                name: "IX_AntdCommunityGroups_StartDate",
                table: "AntdCommunityGroups",
                column: "StartDate");

            migrationBuilder.CreateIndex(
                name: "IX_AntdExams_Course",
                table: "AntdExams",
                column: "Course");

            migrationBuilder.CreateIndex(
                name: "IX_AntdExams_CourseCode",
                table: "AntdExams",
                column: "CourseCode");

            migrationBuilder.CreateIndex(
                name: "IX_AntdExams_ExamDate",
                table: "AntdExams",
                column: "ExamDate");

            migrationBuilder.CreateIndex(
                name: "IX_AntdExams_StudentId",
                table: "AntdExams",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_AntdRecommendedCourses_Category",
                table: "AntdRecommendedCourses",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_AntdRecommendedCourses_Instructor",
                table: "AntdRecommendedCourses",
                column: "Instructor");

            migrationBuilder.CreateIndex(
                name: "IX_AntdRecommendedCourses_Level",
                table: "AntdRecommendedCourses",
                column: "Level");

            migrationBuilder.CreateIndex(
                name: "IX_AntdRecommendedCourses_StartDate",
                table: "AntdRecommendedCourses",
                column: "StartDate");

            migrationBuilder.CreateIndex(
                name: "IX_AntdStudyStatistics_Category",
                table: "AntdStudyStatistics",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_AntdStudyStatistics_Month",
                table: "AntdStudyStatistics",
                column: "Month");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AntdCommunityGroups");

            migrationBuilder.DropTable(
                name: "AntdExams");

            migrationBuilder.DropTable(
                name: "AntdRecommendedCourses");

            migrationBuilder.DropTable(
                name: "AntdStudyStatistics");
        }
    }
}
