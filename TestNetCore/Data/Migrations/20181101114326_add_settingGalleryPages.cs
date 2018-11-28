using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestNetCore.Data.Migrations
{
    public partial class add_settingGalleryPages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SettingsGalleryPages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    BgBox = table.Column<string>(nullable: true),
                    BgMessage = table.Column<string>(nullable: true),
                    BgMessageOpacity = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    AnimationDuration = table.Column<int>(nullable: false),
                    AnimationDelay = table.Column<int>(nullable: false),
                    LayoutMessage = table.Column<string>(nullable: true),
                    NameImage = table.Column<string>(nullable: true),
                    PathImage = table.Column<string>(nullable: true),
                    StartEffectAnimation = table.Column<string>(nullable: true),
                    FinishEffectAnimation = table.Column<string>(nullable: true),
                    CaliberImage = table.Column<int>(nullable: false),
                    PathSound = table.Column<string>(nullable: true),
                    NameSound = table.Column<string>(nullable: true),
                    SoundVolume = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettingsGalleryPages", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SettingsGalleryPages");
        }
    }
}
