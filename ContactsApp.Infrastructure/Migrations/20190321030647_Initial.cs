using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactsApp.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 200, nullable: false),
                    LastName = table.Column<string>(maxLength: 200, nullable: false),
                    Birthday = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(maxLength: 500, nullable: false),
                    Gender = table.Column<string>(maxLength: 1, nullable: false),
                    IsFavorite = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactsInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Company = table.Column<string>(maxLength: 200, nullable: true),
                    Avatar = table.Column<string>(maxLength: 1000, nullable: true),
                    Address = table.Column<string>(maxLength: 500, nullable: true),
                    Phone = table.Column<string>(maxLength: 100, nullable: true),
                    Comments = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactsInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactsInfos_Contacts_Id",
                        column: x => x.Id,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_Email",
                table: "Contacts",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactsInfos");

            migrationBuilder.DropTable(
                name: "Contacts");
        }
    }
}
