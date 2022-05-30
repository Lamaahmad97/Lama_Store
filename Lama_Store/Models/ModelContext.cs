using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Lama_Store.Models
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EmpDeptView> EmpDeptViews { get; set; }
        public virtual DbSet<LlAboutu> LlAboutus { get; set; }
        public virtual DbSet<LlCategory> LlCategories { get; set; }
        public virtual DbSet<LlConatctu> LlConatctus { get; set; }
        public virtual DbSet<LlHome> LlHomes { get; set; }
        public virtual DbSet<LlLogin> LlLogins { get; set; }
        public virtual DbSet<LlOrder> LlOrders { get; set; }
        public virtual DbSet<LlProduct> LlProducts { get; set; }
        public virtual DbSet<LlProductOrder> LlProductOrders { get; set; }
        public virtual DbSet<LlRole> LlRoles { get; set; }
        public virtual DbSet<LlStore> LlStores { get; set; }
        public virtual DbSet<LlTestimonial> LlTestimonials { get; set; }
        public virtual DbSet<LlUser> LlUsers { get; set; }
        public virtual DbSet<LlVisa> LlVisas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
    #warning       To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseOracle("USER ID=tah14_User185;PASSWORD=Lama.1997;DATA SOURCE=94.56.229.181:3488/traindb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TAH14_USER185")
                .HasAnnotation("Relational:Collation", "USING_NLS_COMP");

            modelBuilder.Entity<EmpDeptView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("EMP_DEPT_VIEW");

                entity.Property(e => e.Depname)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("DEPNAME");

                entity.Property(e => e.Empid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("EMPID");

                entity.Property(e => e.Fname)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("FNAME");
            });

            modelBuilder.Entity<LlAboutu>(entity =>
            {
                entity.HasKey(e => e.AboutusId)
                    .HasName("SYS_C00224261");

                entity.ToTable("LL_ABOUTUS");

                entity.Property(e => e.AboutusId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ABOUTUS_ID");

                entity.Property(e => e.Descriptionn)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTIONN");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USER_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.LlAboutus)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("USERABOUT_FK");
            });

            modelBuilder.Entity<LlCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("SYS_C00224278");

                entity.ToTable("LL_CATEGORY");

                entity.Property(e => e.CategoryId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CATEGORY_ID");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CATEGORY_NAME");

                entity.Property(e => e.ImagePath)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE_PATH");
            });

            modelBuilder.Entity<LlConatctu>(entity =>
            {
                entity.HasKey(e => e.ContactusId)
                    .HasName("SYS_C00224258");

                entity.ToTable("LL_CONATCTUS");

                entity.Property(e => e.ContactusId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CONTACTUS_ID");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Message)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("MESSAGE");

                entity.Property(e => e.Namee)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NAMEE");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USER_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.LlConatctus)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("USERCONTACT_FK");
            });

            modelBuilder.Entity<LlHome>(entity =>
            {
                entity.HasKey(e => e.HomeId)
                    .HasName("SYS_C00224269");

                entity.ToTable("LL_HOME");

                entity.Property(e => e.HomeId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("HOME_ID");

                entity.Property(e => e.BbgImagePath)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("BBG_IMAGE_PATH");

                entity.Property(e => e.BgImagePath)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("BG_IMAGE_PATH");

                entity.Property(e => e.BggImagePath)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("BGG_IMAGE_PATH");

                entity.Property(e => e.Texitt)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("TEXITT");

                entity.Property(e => e.Texittt)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("TEXITTT");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USER_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.LlHomes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("USERHOME_FK");
            });

            modelBuilder.Entity<LlLogin>(entity =>
            {
                entity.HasKey(e => e.LoginId)
                    .HasName("SYS_C00224222");

                entity.ToTable("LL_LOGIN");

                entity.HasIndex(e => e.UserName, "UNI_USERNAME")
                    .IsUnique();

                entity.Property(e => e.LoginId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("LOGIN_ID");

                entity.Property(e => e.LoginPass)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LOGIN_PASS");

                entity.Property(e => e.RoleId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ROLE_ID");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USER_ID");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("USER_NAME");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.LlLogins)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("EROLE_LOGIN_FK");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.LlLogins)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("EUSER_LOGIN_FK");
            });

            modelBuilder.Entity<LlOrder>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("SYS_C00224315");

                entity.ToTable("LL_ORDER");

                entity.Property(e => e.OrderId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ORDER_ID");

                entity.Property(e => e.OrderName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ORDER_NAME");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USER_ID");

                entity.Property(e => e.VisaId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("VISA_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.LlOrders)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("USERORDER_FK");

                entity.HasOne(d => d.Visa)
                    .WithMany(p => p.LlOrders)
                    .HasForeignKey(d => d.VisaId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("USERSVISA_FK");
            });

            modelBuilder.Entity<LlProduct>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("SYS_C00224294");

                entity.ToTable("LL_PRODUCT");

                entity.Property(e => e.ProductId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PRODUCT_ID");

                entity.Property(e => e.ImagePathP)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE_PATH_P");

                entity.Property(e => e.ProductCost)
                    .HasColumnType("NUMBER(30)")
                    .HasColumnName("PRODUCT_COST");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PRODUCT_NAME");

                entity.Property(e => e.ProductPrice)
                    .HasColumnType("FLOAT")
                    .HasColumnName("PRODUCT_PRICE");

                entity.Property(e => e.StoreId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("STORE_ID");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.LlProducts)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("S_STORE_FK2");
            });

            modelBuilder.Entity<LlProductOrder>(entity =>
            {
                entity.HasKey(e => e.ProductOrderId)
                    .HasName("SYS_C00224377");

                entity.ToTable("LL_PRODUCT_ORDER");

                entity.Property(e => e.ProductOrderId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PRODUCT_ORDER_ID");

                entity.Property(e => e.DateFrom)
                    .HasColumnType("DATE")
                    .HasColumnName("DATE_FROM");

                entity.Property(e => e.DateTo)
                    .HasColumnType("DATE")
                    .HasColumnName("DATE_TO");

                entity.Property(e => e.OrderId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ORDER_ID");

                entity.Property(e => e.ProductId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PRODUCT_ID");

                entity.Property(e => e.Quantity)
                    .HasColumnType("NUMBER")
                    .HasColumnName("QUANTITY");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.LlProductOrders)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("EORDER_FK");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.LlProductOrders)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("EPRODUCT_FK");
            });

            modelBuilder.Entity<LlRole>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("SYS_C00224216");

                entity.ToTable("LL_ROLE");

                entity.Property(e => e.RoleId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ROLE_ID");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ROLE_NAME");
            });

            modelBuilder.Entity<LlStore>(entity =>
            {
                entity.HasKey(e => e.StoreId)
                    .HasName("SYS_C00224282");

                entity.ToTable("LL_STORE");

                entity.Property(e => e.StoreId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("STORE_ID");

                entity.Property(e => e.CategoryId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CATEGORY_ID");

                entity.Property(e => e.ImagePathS)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE_PATH_S");

                entity.Property(e => e.StoreName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("STORE_NAME");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.LlStores)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("S_PRODUCT_FK2");
            });

            modelBuilder.Entity<LlTestimonial>(entity =>
            {
                entity.HasKey(e => e.TestimonialId)
                    .HasName("SYS_C00224275");

                entity.ToTable("LL_TESTIMONIAL");

                entity.Property(e => e.TestimonialId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("TESTIMONIAL_ID");

                entity.Property(e => e.BgImagePath)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("BG_IMAGE_PATH");

                entity.Property(e => e.Message)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("MESSAGE");

                entity.Property(e => e.Status)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USER_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.LlTestimonials)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("USERTEST_FK");
            });

            modelBuilder.Entity<LlUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("SYS_C00224218");

                entity.ToTable("LL_USER");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("USER_ID");

                entity.Property(e => e.ImagePath)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE_PATH");

                entity.Property(e => e.UserFname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("USER_FNAME");

                entity.Property(e => e.UserLname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("USER_LNAME");
            });

            modelBuilder.Entity<LlVisa>(entity =>
            {
                entity.HasKey(e => e.VisaId)
                    .HasName("SYS_C00224307");

                entity.ToTable("LL_VISA");

                entity.Property(e => e.VisaId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("VISA_ID");

                entity.Property(e => e.VisaBalance)
                    .HasColumnType("FLOAT")
                    .HasColumnName("VISA_BALANCE");

                entity.Property(e => e.VisaName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("VISA_NAME");

                entity.Property(e => e.VisaPass)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("VISA_PASS");
            });

            modelBuilder.HasSequence("SEQ");

            modelBuilder.HasSequence("SEQ11");

            modelBuilder.HasSequence("SEQUN");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
