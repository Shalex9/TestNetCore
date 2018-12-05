using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestNetCore.Data.Migrations
{
    public partial class create_savedUserAssociations_and_CRUDfilesUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CRUDfileUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    FileName1 = table.Column<string>(nullable: true),
                    FilePath1 = table.Column<string>(nullable: true),
                    FileSize1 = table.Column<string>(nullable: true),
                    FileName2 = table.Column<string>(nullable: true),
                    FilePath2 = table.Column<string>(nullable: true),
                    FileSize2 = table.Column<string>(nullable: true),
                    LastChange = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CRUDfileUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SavedUserAssociations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    Word = table.Column<string>(nullable: true),
                    AllWordsAss = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedUserAssociations", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CRUDfileUsers");

            migrationBuilder.DropTable(
                name: "SavedUserAssociations");
        }
    }
}
