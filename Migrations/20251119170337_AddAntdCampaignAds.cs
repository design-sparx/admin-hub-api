using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminHubApi.Migrations
{
    /// <inheritdoc />
    public partial class AddAntdCampaignAds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AntdCampaignAds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AdSource = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    AdCampaign = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    AdGroup = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    AdType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Impressions = table.Column<int>(type: "integer", nullable: false),
                    Clicks = table.Column<int>(type: "integer", nullable: false),
                    Conversions = table.Column<int>(type: "integer", nullable: false),
                    Cost = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ConversionRate = table.Column<decimal>(type: "numeric(6,4)", precision: 6, scale: 4, nullable: false),
                    Revenue = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Roi = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntdCampaignAds", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AntdCampaignAds_AdCampaign",
                table: "AntdCampaignAds",
                column: "AdCampaign");

            migrationBuilder.CreateIndex(
                name: "IX_AntdCampaignAds_AdSource",
                table: "AntdCampaignAds",
                column: "AdSource");

            migrationBuilder.CreateIndex(
                name: "IX_AntdCampaignAds_StartDate",
                table: "AntdCampaignAds",
                column: "StartDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AntdCampaignAds");
        }
    }
}
