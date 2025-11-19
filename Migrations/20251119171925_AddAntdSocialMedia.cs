using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminHubApi.Migrations
{
    /// <inheritdoc />
    public partial class AddAntdSocialMedia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AntdScheduledPosts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Content = table.Column<string>(type: "text", nullable: true),
                    ScheduledDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ScheduledTime = table.Column<int>(type: "integer", nullable: false),
                    Author = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Category = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Tags = table.Column<string>(type: "text", nullable: true),
                    LikesCount = table.Column<int>(type: "integer", nullable: false),
                    CommentsCount = table.Column<int>(type: "integer", nullable: false),
                    SharesCount = table.Column<int>(type: "integer", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    Link = table.Column<string>(type: "text", nullable: true),
                    Location = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Hashtags = table.Column<string>(type: "text", nullable: true),
                    Platform = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntdScheduledPosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AntdSocialMediaActivities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Author = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    UserId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ActivityType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PostContent = table.Column<string>(type: "text", nullable: true),
                    Platform = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    UserLocation = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    UserAge = table.Column<int>(type: "integer", nullable: false),
                    UserGender = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    UserInterests = table.Column<string>(type: "text", nullable: true),
                    UserFriendsCount = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntdSocialMediaActivities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AntdSocialMediaStats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Followers = table.Column<int>(type: "integer", nullable: false),
                    Following = table.Column<int>(type: "integer", nullable: false),
                    Posts = table.Column<int>(type: "integer", nullable: false),
                    Likes = table.Column<int>(type: "integer", nullable: false),
                    Comments = table.Column<int>(type: "integer", nullable: false),
                    EngagementRate = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntdSocialMediaStats", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AntdScheduledPosts_Author",
                table: "AntdScheduledPosts",
                column: "Author");

            migrationBuilder.CreateIndex(
                name: "IX_AntdScheduledPosts_Category",
                table: "AntdScheduledPosts",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_AntdScheduledPosts_Platform",
                table: "AntdScheduledPosts",
                column: "Platform");

            migrationBuilder.CreateIndex(
                name: "IX_AntdScheduledPosts_ScheduledDate",
                table: "AntdScheduledPosts",
                column: "ScheduledDate");

            migrationBuilder.CreateIndex(
                name: "IX_AntdSocialMediaActivities_ActivityType",
                table: "AntdSocialMediaActivities",
                column: "ActivityType");

            migrationBuilder.CreateIndex(
                name: "IX_AntdSocialMediaActivities_Platform",
                table: "AntdSocialMediaActivities",
                column: "Platform");

            migrationBuilder.CreateIndex(
                name: "IX_AntdSocialMediaActivities_Timestamp",
                table: "AntdSocialMediaActivities",
                column: "Timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_AntdSocialMediaActivities_UserGender",
                table: "AntdSocialMediaActivities",
                column: "UserGender");

            migrationBuilder.CreateIndex(
                name: "IX_AntdSocialMediaStats_Title",
                table: "AntdSocialMediaStats",
                column: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AntdScheduledPosts");

            migrationBuilder.DropTable(
                name: "AntdSocialMediaActivities");

            migrationBuilder.DropTable(
                name: "AntdSocialMediaStats");
        }
    }
}
