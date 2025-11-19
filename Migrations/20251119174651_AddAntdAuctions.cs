using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminHubApi.Migrations
{
    /// <inheritdoc />
    public partial class AddAntdAuctions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AntdAuctionCreators",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Age = table.Column<int>(type: "integer", nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Country = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PostalCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    FavoriteColor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    SalesCount = table.Column<int>(type: "integer", nullable: false),
                    TotalSales = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntdAuctionCreators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AntdLiveAuctions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NftName = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    NftImage = table.Column<string>(type: "text", nullable: true),
                    SellerUsername = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    BuyerUsername = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    StartPrice = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    EndPrice = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsHighestBidMine = table.Column<bool>(type: "boolean", nullable: false),
                    WinningBid = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    TimeLeft = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntdLiveAuctions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AntdAuctionCreators_Country",
                table: "AntdAuctionCreators",
                column: "Country");

            migrationBuilder.CreateIndex(
                name: "IX_AntdAuctionCreators_Email",
                table: "AntdAuctionCreators",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_AntdAuctionCreators_SalesCount",
                table: "AntdAuctionCreators",
                column: "SalesCount");

            migrationBuilder.CreateIndex(
                name: "IX_AntdLiveAuctions_EndDate",
                table: "AntdLiveAuctions",
                column: "EndDate");

            migrationBuilder.CreateIndex(
                name: "IX_AntdLiveAuctions_SellerUsername",
                table: "AntdLiveAuctions",
                column: "SellerUsername");

            migrationBuilder.CreateIndex(
                name: "IX_AntdLiveAuctions_StartDate",
                table: "AntdLiveAuctions",
                column: "StartDate");

            migrationBuilder.CreateIndex(
                name: "IX_AntdLiveAuctions_Status",
                table: "AntdLiveAuctions",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AntdAuctionCreators");

            migrationBuilder.DropTable(
                name: "AntdLiveAuctions");
        }
    }
}
