using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminHubApi.Migrations
{
    /// <inheritdoc />
    public partial class AddAntdProjectsAndAddAntdTasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AntdProjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectName = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Budget = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ProjectManager = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ClientName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Priority = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    TeamSize = table.Column<int>(type: "integer", nullable: false),
                    ProjectDescription = table.Column<string>(type: "text", nullable: true),
                    ProjectLocation = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ProjectType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ProjectCategory = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ProjectDuration = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntdProjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AntdTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    Priority = table.Column<int>(type: "integer", nullable: false),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AssignedTo = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Notes = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    Category = table.Column<int>(type: "integer", nullable: false),
                    Duration = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    CompletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Color = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntdTasks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AntdProjects_ClientName",
                table: "AntdProjects",
                column: "ClientName");

            migrationBuilder.CreateIndex(
                name: "IX_AntdProjects_Priority",
                table: "AntdProjects",
                column: "Priority");

            migrationBuilder.CreateIndex(
                name: "IX_AntdProjects_ProjectManager",
                table: "AntdProjects",
                column: "ProjectManager");

            migrationBuilder.CreateIndex(
                name: "IX_AntdProjects_StartDate_EndDate",
                table: "AntdProjects",
                columns: new[] { "StartDate", "EndDate" });

            migrationBuilder.CreateIndex(
                name: "IX_AntdProjects_Status",
                table: "AntdProjects",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_AntdTasks_AssignedTo",
                table: "AntdTasks",
                column: "AssignedTo");

            migrationBuilder.CreateIndex(
                name: "IX_AntdTasks_Category",
                table: "AntdTasks",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_AntdTasks_DueDate",
                table: "AntdTasks",
                column: "DueDate");

            migrationBuilder.CreateIndex(
                name: "IX_AntdTasks_Priority",
                table: "AntdTasks",
                column: "Priority");

            migrationBuilder.CreateIndex(
                name: "IX_AntdTasks_Status",
                table: "AntdTasks",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AntdProjects");

            migrationBuilder.DropTable(
                name: "AntdTasks");
        }
    }
}
