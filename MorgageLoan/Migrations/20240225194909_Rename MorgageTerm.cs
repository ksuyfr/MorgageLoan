using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MorgageLoan.Migrations
{
    /// <inheritdoc />
    public partial class RenameMorgageTerm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreditTerm",
                table: "Credit",
                newName: "MorgageTerm");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MorgageTerm",
                table: "Credit",
                newName: "CreditTerm");
        }
    }
}
