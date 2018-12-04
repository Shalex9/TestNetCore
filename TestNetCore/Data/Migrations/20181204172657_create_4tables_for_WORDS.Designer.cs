﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestNetCore.Data;

namespace TestNetCore.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20181204172657_create_4tables_for_WORDS")]
    partial class create_4tables_for_WORDS
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("TestNetCore.Models.ClaimsDataUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("UserAvatar");

                    b.Property<string>("UserEmail");

                    b.Property<string>("UserFirstName");

                    b.Property<string>("UserLinkGooglePlus");

                    b.Property<string>("UserLinkPicasa");

                    b.Property<string>("UserName");

                    b.Property<string>("UserProviderKey");

                    b.Property<string>("UserSecondName");

                    b.HasKey("Id");

                    b.ToTable("ClaimsDataUsers");
                });

            modelBuilder.Entity("TestNetCore.Models.EFViewModels.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Country");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("TestNetCore.Models.EFViewModels.CompanySort", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("CompaniesSort");
                });

            modelBuilder.Entity("TestNetCore.Models.EFViewModels.Phone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Diagonal")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int?>("ManufacturerId");

                    b.Property<string>("Name");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id");

                    b.HasIndex("ManufacturerId");

                    b.ToTable("Phones");
                });

            modelBuilder.Entity("TestNetCore.Models.EFViewModels.UserSort", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age");

                    b.Property<int>("CompanyId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("UsersSort");
                });

            modelBuilder.Entity("TestNetCore.Models.Files.ForbiddenWord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Word");

                    b.HasKey("Id");

                    b.ToTable("ForbiddenWords");
                });

            modelBuilder.Entity("TestNetCore.Models.Files.ForbiddenWordUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("UserId");

                    b.Property<string>("Word");

                    b.HasKey("Id");

                    b.ToTable("ForbiddenWordUsers");
                });

            modelBuilder.Entity("TestNetCore.Models.Files.RussianDictionary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Definition");

                    b.Property<string>("Word");

                    b.HasKey("Id");

                    b.ToTable("RussianDictionaries");
                });

            modelBuilder.Entity("TestNetCore.Models.Files.RussianWord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Word");

                    b.HasKey("Id");

                    b.ToTable("RussianWords");
                });

            modelBuilder.Entity("TestNetCore.Models.Files.SettingsPageGallery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AnimationDelay");

                    b.Property<int>("AnimationDuration");

                    b.Property<string>("BgBox");

                    b.Property<string>("BgMessage");

                    b.Property<decimal>("BgMessageOpacity")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("CaliberImage");

                    b.Property<string>("FinishEffectAnimation");

                    b.Property<string>("LayoutMessage");

                    b.Property<string>("NameImage");

                    b.Property<string>("NameSound");

                    b.Property<string>("PathImage");

                    b.Property<string>("PathSound");

                    b.Property<int>("SoundVolume");

                    b.Property<string>("StartEffectAnimation");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("SettingsGalleryPages");
                });

            modelBuilder.Entity("TestNetCore.Models.TableIncome", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOfReceipt");

                    b.Property<string>("DonatCurrency");

                    b.Property<string>("DonatorId");

                    b.Property<decimal>("Income")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("MessageText");

                    b.Property<string>("NameDonator");

                    b.Property<string>("PaymentSystem");

                    b.Property<string>("UserId");

                    b.Property<string>("VoiceName");

                    b.HasKey("Id");

                    b.ToTable("TableIncomes");
                });

            modelBuilder.Entity("TestNetCore.Models.WidgetsViewModels.AllUserWidget", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("RunTestMessage");

                    b.Property<Guid>("UserId");

                    b.Property<int>("WidgetId");

                    b.Property<string>("WidgetUrl");

                    b.HasKey("Id");

                    b.ToTable("AllUserWidgets");
                });

            modelBuilder.Entity("TestNetCore.Models.WidgetsViewModels.GalleryFileImg", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FileName");

                    b.Property<string>("Size");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("GalleryFilesImgs");
                });

            modelBuilder.Entity("TestNetCore.Models.WidgetsViewModels.GalleryFileSound", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FileName");

                    b.Property<string>("Size");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("GalleryFilesSounds");
                });

            modelBuilder.Entity("TestNetCore.Models.WidgetsViewModels.HotelInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ComfortableOfRoom");

                    b.Property<bool>("HasBigBed");

                    b.Property<bool>("HasTV");

                    b.Property<bool>("HasToilet");

                    b.Property<bool>("IsFreeNow");

                    b.Property<int>("NumberOfRoom");

                    b.Property<int>("PriceForRoom");

                    b.HasKey("Id");

                    b.ToTable("HotelInformations");
                });

            modelBuilder.Entity("TestNetCore.Models.WidgetsViewModels.HotelReservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateReserv");

                    b.Property<DateTime>("EndReserv");

                    b.Property<string>("GuestEmail");

                    b.Property<string>("GuestGuid");

                    b.Property<string>("GuestName");

                    b.Property<int>("NumberOfRoom");

                    b.Property<DateTime>("StartReserv");

                    b.Property<int>("SummReserv");

                    b.HasKey("Id");

                    b.ToTable("HotelReservations");
                });

            modelBuilder.Entity("TestNetCore.Models.WidgetsViewModels.SettingsDonationNotification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AnimationDelayDon");

                    b.Property<int>("AnimationDurationDon");

                    b.Property<bool>("AnimationImageVisibleDon");

                    b.Property<bool>("AnimationVisibleDon");

                    b.Property<string>("BgBoxDon");

                    b.Property<string>("BgMessageDon");

                    b.Property<decimal>("BgMessageOpacityDon")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("CaliberImageDon");

                    b.Property<string>("FinishEffectAnimationDon");

                    b.Property<string>("FinishEffectAnimationTextDon");

                    b.Property<string>("FontAnimationTextDon");

                    b.Property<string>("FontAnimationTitleDon");

                    b.Property<string>("FontColorTextDon");

                    b.Property<decimal>("FontColorTextOpacityDon")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("FontColorTitleDon");

                    b.Property<decimal>("FontColorTitleOpacityDon")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("FontFamilyTextDon");

                    b.Property<string>("FontFamilyTitleDon");

                    b.Property<string>("FontHighlightColorTitleDon");

                    b.Property<string>("FontItalicTextDon");

                    b.Property<string>("FontItalicTitleDon");

                    b.Property<string>("FontSizeTextDon");

                    b.Property<string>("FontSizeTitleDon");

                    b.Property<string>("FontUnderlineTextDon");

                    b.Property<string>("FontUnderlineTitleDon");

                    b.Property<string>("FontWeightTextDon");

                    b.Property<string>("FontWeightTitleDon");

                    b.Property<string>("LayoutMessageDon");

                    b.Property<string>("LetterSpacingTextDon");

                    b.Property<string>("LetterSpacingTitleDon");

                    b.Property<int>("MinAmountDon");

                    b.Property<string>("NameImageDon");

                    b.Property<string>("NameSoundDon");

                    b.Property<string>("PathImageDon");

                    b.Property<string>("PathSoundDon");

                    b.Property<string>("ShadowTextDon");

                    b.Property<string>("ShadowTitleDon");

                    b.Property<bool>("SoundVisibleDon");

                    b.Property<int>("SoundVolumeDon");

                    b.Property<string>("StartEffectAnimationDon");

                    b.Property<string>("StartEffectAnimationTextDon");

                    b.Property<string>("TemplateTextDon");

                    b.Property<int>("TextDelayDon");

                    b.Property<string>("Token");

                    b.Property<string>("UserId");

                    b.Property<string>("WordSpacingTextDon");

                    b.Property<string>("WordSpacingTitleDon");

                    b.HasKey("Id");

                    b.ToTable("SettingsDonationNotifications");
                });

            modelBuilder.Entity("TestNetCore.Models.WidgetsViewModels.UploadFileImg", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FileName");

                    b.Property<string>("Size");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("UploadFilesImgs");
                });

            modelBuilder.Entity("TestNetCore.Models.WidgetsViewModels.UploadFileSound", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FileName");

                    b.Property<string>("Size");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("UploadFilesSounds");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TestNetCore.Models.EFViewModels.Phone", b =>
                {
                    b.HasOne("TestNetCore.Models.EFViewModels.Company", "Manufacturer")
                        .WithMany()
                        .HasForeignKey("ManufacturerId");
                });

            modelBuilder.Entity("TestNetCore.Models.EFViewModels.UserSort", b =>
                {
                    b.HasOne("TestNetCore.Models.EFViewModels.CompanySort", "Company")
                        .WithMany("UsersSort")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
