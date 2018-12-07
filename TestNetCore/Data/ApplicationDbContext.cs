using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestNetCore.Models;
using TestNetCore.Models.WidgetsViewModels;
using TestNetCore.Models.EFViewModels;
using TestNetCore.Models.Files;

namespace TestNetCore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<ClaimsDataUser> ClaimsDataUsers { get; set; }
        public DbSet<SettingsDonationNotification> SettingsDonationNotifications { get; set; }
        public DbSet<GalleryFileImg> GalleryFilesImgs { get; set; }
        public DbSet<GalleryFileSound> GalleryFilesSounds { get; set; }
        public DbSet<UploadFileImg> UploadFilesImgs { get; set; }
        public DbSet<UploadFileSound> UploadFilesSounds { get; set; }
        public DbSet<AllUserWidget> AllUserWidgets { get; set; }
        public DbSet<TableIncome> TableIncomes { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<UserSort> UsersSort { get; set; }
        public DbSet<CompanySort> CompaniesSort { get; set; }
        public DbSet<HotelReservation> HotelReservations { get; set; }
        public DbSet<HotelInformation> HotelInformations { get; set; }
        public DbSet<SettingsPageGallery> SettingsGalleryPages { get; set; }
        public DbSet<RussianDictionary> RussianDictionaries { get; set; }
        public DbSet<ForbiddenWord> ForbiddenWords { get; set; }
        public DbSet<ForbiddenWordUser> ForbiddenWordUsers { get; set; }
        public DbSet<SavedUserAssociation> SavedUserAssociations { get; set; }
        public DbSet<CRUDfileUser> CRUDfileUsers { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }
    }
}
