using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cy.IdentityServerAspNetIdentity.Data.Migrations.AspNetIdentity.ApplicationDb
{
    /// <inheritdoc />
    public partial class InitialApplicationDb2Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FavoriteColor",
                table: "AspNetUsers",
                type: "longtext",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FavoriteColor",
                table: "AspNetUsers");
        }
    }
}
