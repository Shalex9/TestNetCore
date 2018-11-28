using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestNetCore.Data.Migrations
{
    public partial class add_ALL_WIDGET_TABLES : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AllUserWidgets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    WidgetId = table.Column<int>(nullable: false),
                    WidgetUrl = table.Column<string>(nullable: true),
                    RunTestMessage = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllUserWidgets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GalleryFilesImgs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FileName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Size = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalleryFilesImgs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GalleryFilesSounds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FileName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Size = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalleryFilesSounds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HotelInformations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NumberOfRoom = table.Column<int>(nullable: false),
                    PriceForRoom = table.Column<int>(nullable: false),
                    ComfortableOfRoom = table.Column<string>(nullable: true),
                    HasToilet = table.Column<bool>(nullable: false),
                    HasTV = table.Column<bool>(nullable: false),
                    HasBigBed = table.Column<bool>(nullable: false),
                    IsFreeNow = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelInformations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HotelReservations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NumberOfRoom = table.Column<int>(nullable: false),
                    StartReserv = table.Column<DateTime>(nullable: false),
                    EndReserv = table.Column<DateTime>(nullable: false),
                    GuestGuid = table.Column<string>(nullable: true),
                    GuestName = table.Column<string>(nullable: true),
                    GuestEmail = table.Column<string>(nullable: true),
                    DateReserv = table.Column<DateTime>(nullable: false),
                    SummReserv = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelReservations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SettingsDonationNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    Token = table.Column<string>(nullable: true),
                    AnimationVisibleDon = table.Column<bool>(nullable: false),
                    BgBoxDon = table.Column<string>(nullable: true),
                    BgMessageDon = table.Column<string>(nullable: true),
                    BgMessageOpacityDon = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    AnimationDurationDon = table.Column<int>(nullable: false),
                    AnimationDelayDon = table.Column<int>(nullable: false),
                    LayoutMessageDon = table.Column<string>(nullable: true),
                    AnimationImageVisibleDon = table.Column<bool>(nullable: false),
                    NameImageDon = table.Column<string>(nullable: true),
                    PathImageDon = table.Column<string>(nullable: true),
                    StartEffectAnimationDon = table.Column<string>(nullable: true),
                    FinishEffectAnimationDon = table.Column<string>(nullable: true),
                    CaliberImageDon = table.Column<int>(nullable: false),
                    SoundVisibleDon = table.Column<bool>(nullable: false),
                    PathSoundDon = table.Column<string>(nullable: true),
                    NameSoundDon = table.Column<string>(nullable: true),
                    SoundVolumeDon = table.Column<int>(nullable: false),
                    MinAmountDon = table.Column<int>(nullable: false),
                    TextDelayDon = table.Column<int>(nullable: false),
                    TemplateTextDon = table.Column<string>(nullable: true),
                    StartEffectAnimationTextDon = table.Column<string>(nullable: true),
                    FinishEffectAnimationTextDon = table.Column<string>(nullable: true),
                    FontFamilyTitleDon = table.Column<string>(nullable: true),
                    FontColorTitleDon = table.Column<string>(nullable: true),
                    FontColorTitleOpacityDon = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    FontHighlightColorTitleDon = table.Column<string>(nullable: true),
                    FontSizeTitleDon = table.Column<string>(nullable: true),
                    FontWeightTitleDon = table.Column<string>(nullable: true),
                    FontItalicTitleDon = table.Column<string>(nullable: true),
                    FontUnderlineTitleDon = table.Column<string>(nullable: true),
                    LetterSpacingTitleDon = table.Column<string>(nullable: true),
                    WordSpacingTitleDon = table.Column<string>(nullable: true),
                    ShadowTitleDon = table.Column<string>(nullable: true),
                    FontAnimationTitleDon = table.Column<string>(nullable: true),
                    FontFamilyTextDon = table.Column<string>(nullable: true),
                    FontColorTextDon = table.Column<string>(nullable: true),
                    FontColorTextOpacityDon = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    FontSizeTextDon = table.Column<string>(nullable: true),
                    FontWeightTextDon = table.Column<string>(nullable: true),
                    FontItalicTextDon = table.Column<string>(nullable: true),
                    FontUnderlineTextDon = table.Column<string>(nullable: true),
                    LetterSpacingTextDon = table.Column<string>(nullable: true),
                    WordSpacingTextDon = table.Column<string>(nullable: true),
                    ShadowTextDon = table.Column<string>(nullable: true),
                    FontAnimationTextDon = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettingsDonationNotifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TableIncomes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateOfReceipt = table.Column<DateTime>(nullable: false),
                    NameDonator = table.Column<string>(nullable: true),
                    MessageText = table.Column<string>(nullable: true),
                    PaymentSystem = table.Column<string>(nullable: true),
                    Income = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    DonatCurrency = table.Column<string>(nullable: true),
                    DonatorId = table.Column<string>(nullable: true),
                    VoiceName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableIncomes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UploadFilesImgs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FileName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Size = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadFilesImgs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UploadFilesSounds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FileName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Size = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadFilesSounds", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllUserWidgets");

            migrationBuilder.DropTable(
                name: "GalleryFilesImgs");

            migrationBuilder.DropTable(
                name: "GalleryFilesSounds");

            migrationBuilder.DropTable(
                name: "HotelInformations");

            migrationBuilder.DropTable(
                name: "HotelReservations");

            migrationBuilder.DropTable(
                name: "SettingsDonationNotifications");

            migrationBuilder.DropTable(
                name: "TableIncomes");

            migrationBuilder.DropTable(
                name: "UploadFilesImgs");

            migrationBuilder.DropTable(
                name: "UploadFilesSounds");
        }
    }
}
