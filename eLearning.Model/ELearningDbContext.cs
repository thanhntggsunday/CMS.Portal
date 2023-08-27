namespace eLearning.Model
{
    using eLearning.Model.Entities;
    using System.Data.Entity;

    public partial class ELearningDbContext : DbContext
    {
        public ELearningDbContext()
            : base("name=ApplicationDbContext")
        {
            Database.SetInitializer<ELearningDbContext>(null);
        }

        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<ContentCategory> Category { get; set; }

        public virtual DbSet<Content> Content { get; set; }
        public virtual DbSet<ContentTag> ContentTag { get; set; }

        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }

        public virtual DbSet<Trainners> Trainners { get; set; }

        public virtual DbSet<CourseCategory> CourseCategories { get; set; }
        public virtual DbSet<CourseLessons> CourseLessons { get; set; }
        public virtual DbSet<Courses> Courses { get; set; }
        public virtual DbSet<CoursesStudents> CoursesStudents { get; set; }
        public virtual DbSet<LessonComments> LessonComments { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContentCategory>()
                .Property(e => e.MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<ContentCategory>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<ContentCategory>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<ContentCategory>()
                .Property(e => e.MetaDescriptions)
                .IsFixedLength();

            modelBuilder.Entity<ContentCategory>()
                .Property(e => e.Language)
                .IsUnicode(false);

            modelBuilder.Entity<Content>()
                .Property(e => e.MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<Content>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Content>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Content>()
                .Property(e => e.MetaDescriptions)
                .IsFixedLength();

            modelBuilder.Entity<Content>()
                .Property(e => e.Language)
                .IsUnicode(false);

            modelBuilder.Entity<ContentTag>()
                .Property(e => e.TagID)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Product>()
                .Property(e => e.PromotionPrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Product>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.MetaDescriptions)
                .IsFixedLength();

            modelBuilder.Entity<ProductCategory>()
                .Property(e => e.MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<ProductCategory>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<ProductCategory>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<ProductCategory>()
                .Property(e => e.MetaDescriptions)
                .IsFixedLength();

            modelBuilder.Entity<CourseCategory>()
                .Property(e => e.SeoAlias)
                .IsUnicode(false);

            modelBuilder.Entity<CourseCategory>()
                .HasMany(e => e.Courses)
                .WithOptional(e => e.CourseCategory)
                .HasForeignKey(e => e.CategoryId);

            modelBuilder.Entity<CourseLessons>()
                .Property(e => e.VideoPath)
                .IsUnicode(false);

            modelBuilder.Entity<CourseLessons>()
                .HasMany(e => e.LessonComments)
                .WithOptional(e => e.CourseLessons)
                .HasForeignKey(e => e.LessonId);

            modelBuilder.Entity<Courses>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Courses>()
                .Property(e => e.PromotionPrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Courses>()
                .HasMany(e => e.CourseLessons)
                .WithOptional(e => e.Courses)
                .HasForeignKey(e => e.CourseId);

            modelBuilder.Entity<Courses>()
                .HasMany(e => e.CoursesStudents)
                .WithOptional(e => e.Courses)
                .HasForeignKey(e => e.CourseId);
        }
    }
}