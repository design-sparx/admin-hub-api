using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminHubApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewCreatorAndModifiedColumnsToProductCategoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "ProductCategories",
                newName: "Modified");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "ProductCategories",
                newName: "Created");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "ProductCategories",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedById",
                table: "ProductCategories",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_CreatedById",
                table: "ProductCategories",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_ModifiedById",
                table: "ProductCategories",
                column: "ModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_AspNetUsers_CreatedById",
                table: "ProductCategories",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_AspNetUsers_ModifiedById",
                table: "ProductCategories",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_AspNetUsers_CreatedById",
                table: "ProductCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_AspNetUsers_ModifiedById",
                table: "ProductCategories");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategories_CreatedById",
                table: "ProductCategories");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategories_ModifiedById",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "ProductCategories");

            migrationBuilder.RenameColumn(
                name: "Modified",
                table: "ProductCategories",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "ProductCategories",
                newName: "CreatedAt");
        }
    }
}
