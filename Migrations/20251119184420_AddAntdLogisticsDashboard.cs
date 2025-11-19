using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AdminHubApi.Migrations
{
    /// <inheritdoc />
    public partial class AddAntdLogisticsDashboard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AntdDeliveryAnalytics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    Month = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntdDeliveryAnalytics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AntdTruckDeliveries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ShipmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    TruckId = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    DriverName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    OriginCity = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    DestinationCity = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ShipmentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeliveryTime = table.Column<int>(type: "integer", nullable: false),
                    ShipmentWeight = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    DeliveryStatus = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ShipmentCost = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    FavoriteColor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntdTruckDeliveries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AntdTruckDeliveryRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    PickupLocation = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    DeliveryLocation = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeliveryTime = table.Column<int>(type: "integer", nullable: false),
                    TruckType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CargoWeight = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    DeliveryStatus = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DriverName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ContactNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntdTruckDeliveryRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AntdTrucks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TruckId = table.Column<Guid>(type: "uuid", nullable: false),
                    Make = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Model = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Mileage = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Color = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Availability = table.Column<bool>(type: "boolean", nullable: false),
                    Origin = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Destination = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Progress = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntdTrucks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AntdDeliveryAnalytics_Month",
                table: "AntdDeliveryAnalytics",
                column: "Month");

            migrationBuilder.CreateIndex(
                name: "IX_AntdDeliveryAnalytics_Status",
                table: "AntdDeliveryAnalytics",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_AntdTruckDeliveries_CustomerId",
                table: "AntdTruckDeliveries",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_AntdTruckDeliveries_DeliveryStatus",
                table: "AntdTruckDeliveries",
                column: "DeliveryStatus");

            migrationBuilder.CreateIndex(
                name: "IX_AntdTruckDeliveries_ShipmentDate",
                table: "AntdTruckDeliveries",
                column: "ShipmentDate");

            migrationBuilder.CreateIndex(
                name: "IX_AntdTruckDeliveries_ShipmentId",
                table: "AntdTruckDeliveries",
                column: "ShipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AntdTruckDeliveries_TruckId",
                table: "AntdTruckDeliveries",
                column: "TruckId");

            migrationBuilder.CreateIndex(
                name: "IX_AntdTruckDeliveryRequests_DeliveryDate",
                table: "AntdTruckDeliveryRequests",
                column: "DeliveryDate");

            migrationBuilder.CreateIndex(
                name: "IX_AntdTruckDeliveryRequests_DeliveryStatus",
                table: "AntdTruckDeliveryRequests",
                column: "DeliveryStatus");

            migrationBuilder.CreateIndex(
                name: "IX_AntdTruckDeliveryRequests_TruckType",
                table: "AntdTruckDeliveryRequests",
                column: "TruckType");

            migrationBuilder.CreateIndex(
                name: "IX_AntdTrucks_Availability",
                table: "AntdTrucks",
                column: "Availability");

            migrationBuilder.CreateIndex(
                name: "IX_AntdTrucks_Make",
                table: "AntdTrucks",
                column: "Make");

            migrationBuilder.CreateIndex(
                name: "IX_AntdTrucks_Status",
                table: "AntdTrucks",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_AntdTrucks_TruckId",
                table: "AntdTrucks",
                column: "TruckId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AntdDeliveryAnalytics");

            migrationBuilder.DropTable(
                name: "AntdTruckDeliveries");

            migrationBuilder.DropTable(
                name: "AntdTruckDeliveryRequests");

            migrationBuilder.DropTable(
                name: "AntdTrucks");
        }
    }
}
