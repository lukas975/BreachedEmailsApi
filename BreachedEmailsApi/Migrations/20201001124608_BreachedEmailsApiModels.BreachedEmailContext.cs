using Microsoft.EntityFrameworkCore.Migrations;

namespace BreachedEmailsApi.Migrations
{
    public partial class BreachedEmailsApiModelsBreachedEmailContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BreachedEmails",
                columns: table => new
                {
                    Email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BreachedEmails", x => x.Email);
                });

            migrationBuilder.InsertData(
                table: "BreachedEmails",
                column: "Email",
                value: "email1@email.com");

            migrationBuilder.InsertData(
                table: "BreachedEmails",
                column: "Email",
                value: "email2@email.com");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BreachedEmails");
        }
    }
}
