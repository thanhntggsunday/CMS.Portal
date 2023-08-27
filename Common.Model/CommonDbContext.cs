namespace Common.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Common.Model.Entities;
    using Action = Entities.Action;
    using Microsoft.AspNet.Identity.EntityFramework;

    public partial class CommonDbContext : IdentityDbContext<AppUser>
    {
        public CommonDbContext()
            : base("name=ApplicationDbContext")
        {
            Database.SetInitializer<CommonDbContext>(null);
        }

        public virtual DbSet<Role_Permission> Role_Permission { get; set; }
        public virtual DbSet<Function_Action> Function_Action { get; set; }
        public virtual DbSet<AppRole> AppRoles { get; set; }
        public DbSet<IdentityUserRole> UserRoles { set; get; }

        public virtual DbSet<About> Abouts { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Credential> Credentials { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Footer> Footers { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<MenuType> MenuTypes { get; set; }
        public virtual DbSet<Slide> Slides { get; set; }
        public virtual DbSet<SystemConfig> SystemConfigs { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id).ToTable("AppRoles");
            //modelBuilder.Entity<IdentityUserRole>().HasKey(i => new { i.UserId, i.RoleId }).ToTable("AppUserRoles");
            //modelBuilder.Entity<IdentityUserLogin>().HasKey(i => i.UserId).ToTable("AppUserLogins");
            //modelBuilder.Entity<IdentityUserClaim>().HasKey(i => i.UserId).ToTable("AppUserClaims");
            modelBuilder.CreateAspNetModel();
        }

        public static CommonDbContext Create()
        {
            return new CommonDbContext();
        }
    }
}