using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestNetCore.Data.Migrations
{
    public partial class add_ClaimsDataUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClaimsDataUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserProviderKey = table.Column<string>(nullable: true),
                    UserFirstName = table.Column<string>(nullable: true),
                    UserSecondName = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    UserEmail = table.Column<string>(nullable: true),
                    UserLinkGooglePlus = table.Column<string>(nullable: true),
                    UserLinkPicasa = table.Column<string>(nullable: true),
                    UserAvatar = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimsDataUsers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClaimsDataUsers");
        }
    }
}
