using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestNetCore.Data.Migrations
{
    public partial class cleanDB_and_create_EmailMessages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Phones");

            migrationBuilder.DropTable(
                name: "TableIncomes");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "GalleryFilesSounds");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "GalleryFilesImgs");

            migrationBuilder.DropColumn(
                name: "LastChange",
                table: "CRUDfileUsers");

            migrationBuilder.CreateTable(
                name: "EmailMessages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    SendTo = table.Column<string>(nullable: true),
                    NameFrom = table.Column<string>(nullable: true),
                    TitleMessage = table.Column<string>(nullable: true),
                    TextMessage = table.Column<string>(nullable: true),
                    AttachFile = table.Column<string>(nullable: true),
                    VoiceName = table.Column<string>(nullable: true),
                    DateMessage = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailMessages", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailMessages");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "GalleryFilesSounds",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "GalleryFilesImgs",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastChange",
                table: "CRUDfileUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Country = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TableIncomes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateOfReceipt = table.Column<DateTime>(nullable: false),
                    DonatCurrency = table.Column<string>(nullable: true),
                    DonatorId = table.Column<string>(nullable: true),
                    Income = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    MessageText = table.Column<string>(nullable: true),
                    NameDonator = table.Column<string>(nullable: true),
                    PaymentSystem = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    VoiceName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableIncomes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Phones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Diagonal = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    ManufacturerId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Phones_Companies_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Phones_ManufacturerId",
                table: "Phones",
                column: "ManufacturerId");
        }
    }
}
