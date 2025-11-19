using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminHubApi.Migrations
{
    /// <inheritdoc />
    public partial class AddAntdBidding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AntdBiddingTopSellers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Artist = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Volume = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    OwnersCount = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Edition = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Owner = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Collection = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Verified = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntdBiddingTopSellers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AntdBiddingTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: true),
                    ProductId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    TransactionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Seller = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Buyer = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PurchasePrice = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    SalePrice = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Profit = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    ShippingAddress = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    State = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Country = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    TransactionType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntdBiddingTransactions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AntdBiddingTopSellers_Artist",
                table: "AntdBiddingTopSellers",
                column: "Artist");

            migrationBuilder.CreateIndex(
                name: "IX_AntdBiddingTopSellers_Collection",
                table: "AntdBiddingTopSellers",
                column: "Collection");

            migrationBuilder.CreateIndex(
                name: "IX_AntdBiddingTopSellers_Verified",
                table: "AntdBiddingTopSellers",
                column: "Verified");

            migrationBuilder.CreateIndex(
                name: "IX_AntdBiddingTopSellers_Volume",
                table: "AntdBiddingTopSellers",
                column: "Volume");

            migrationBuilder.CreateIndex(
                name: "IX_AntdBiddingTransactions_Country",
                table: "AntdBiddingTransactions",
                column: "Country");

            migrationBuilder.CreateIndex(
                name: "IX_AntdBiddingTransactions_TransactionDate",
                table: "AntdBiddingTransactions",
                column: "TransactionDate");

            migrationBuilder.CreateIndex(
                name: "IX_AntdBiddingTransactions_TransactionType",
                table: "AntdBiddingTransactions",
                column: "TransactionType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AntdBiddingTopSellers");

            migrationBuilder.DropTable(
                name: "AntdBiddingTransactions");
        }
    }
}
