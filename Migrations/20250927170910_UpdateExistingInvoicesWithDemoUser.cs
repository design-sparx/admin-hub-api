using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminHubApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateExistingInvoicesWithDemoUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Update existing invoices to have the demo user as creator
            // First get the demo user ID
            migrationBuilder.Sql(@"
                UPDATE ""Invoices""
                SET ""CreatedById"" = (
                    SELECT ""Id"" FROM ""AspNetUsers""
                    WHERE ""Email"" = 'demo@adminhub.com'
                    LIMIT 1
                ),
                ""CreatedByEmail"" = 'demo@adminhub.com',
                ""CreatedByName"" = 'Demo User'
                WHERE ""CreatedById"" IS NULL;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
