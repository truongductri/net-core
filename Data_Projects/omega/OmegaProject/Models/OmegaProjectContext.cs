using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OmegaProject.Models
{
    public partial class OmegaProjectContext : DbContext
    {
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Branch> Branch { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<ContactUs> ContactUs { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Duty> Duty { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<Photo> Photo { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Status> Status { get; set; }

        public OmegaProjectContext(DbContextOptions<OmegaProjectContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId)
                    .HasName("IX_AspNetRoleClaims_RoleId");

                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex");

                entity.Property(e => e.Id).HasMaxLength(450);

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId)
                    .HasName("IX_AspNetUserClaims_UserId");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey })
                    .HasName("PK_AspNetUserLogins");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(450);

                entity.Property(e => e.ProviderKey).HasMaxLength(450);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId })
                    .HasName("PK_AspNetUserRoles");

                entity.HasIndex(e => e.RoleId)
                    .HasName("IX_AspNetUserRoles_RoleId");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_AspNetUserRoles_UserId");

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.Property(e => e.RoleId).HasMaxLength(450);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name })
                    .HasName("PK_AspNetUserTokens");

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.Property(e => e.LoginProvider).HasMaxLength(450);

                entity.Property(e => e.Name).HasMaxLength(450);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id).HasMaxLength(450);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Branch>(entity =>
            {
                entity.HasKey(e => e.BraId)
                    .HasName("PK_Branch");

                entity.Property(e => e.BraId).HasColumnName("BraID");

                entity.Property(e => e.BraDisabled).HasDefaultValueSql("0");

                entity.Property(e => e.BraName).HasMaxLength(500);

                entity.Property(e => e.BraNo).HasMaxLength(50);

                entity.Property(e => e.BraNotes).HasMaxLength(500);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CateId)
                    .HasName("PK_Category");

                entity.Property(e => e.CateId).HasColumnName("CateID");

                entity.Property(e => e.CateDisabled).HasDefaultValueSql("0");

                entity.Property(e => e.CateName).HasMaxLength(500);

                entity.Property(e => e.CateNotes).HasMaxLength(500);

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Category)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Category_Status");
            });

            modelBuilder.Entity<ContactUs>(entity =>
            {
                entity.HasKey(e => e.ConId)
                    .HasName("PK_Contract");

                entity.Property(e => e.ConId).HasColumnName("ConID");

                entity.Property(e => e.ConContent).HasMaxLength(1000);

                entity.Property(e => e.ConEmail).HasMaxLength(500);

                entity.Property(e => e.ConName).HasMaxLength(500);

                entity.Property(e => e.ConPhone).HasMaxLength(50);

                entity.Property(e => e.ConTitle).HasMaxLength(500);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CusId)
                    .HasName("PK_Customer");

                entity.Property(e => e.CusId).HasColumnName("CusID");

                entity.Property(e => e.BraId).HasColumnName("BraID");

                entity.Property(e => e.CusAddress).HasMaxLength(500);

                entity.Property(e => e.CusDateCreate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.CusLastUpdate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.CusName).HasMaxLength(500);

                entity.Property(e => e.CusNo).HasMaxLength(250);

                entity.Property(e => e.CusWeb).HasMaxLength(100);

                entity.Property(e => e.PhotoId).HasColumnName("PhotoID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.HasOne(d => d.Bra)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.BraId)
                    .HasConstraintName("FK_Customer_Branch");

                entity.HasOne(d => d.Photo)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.PhotoId)
                    .HasConstraintName("FK_Customer_Photo");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Customer_Status");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.DepId)
                    .HasName("PK_Department");

                entity.Property(e => e.DepId).HasColumnName("DepID");

                entity.Property(e => e.DepDisabled).HasDefaultValueSql("0");

                entity.Property(e => e.DepName).HasMaxLength(500);

                entity.Property(e => e.DepNo).HasMaxLength(50);

                entity.Property(e => e.DepNotes).HasMaxLength(500);
            });

            modelBuilder.Entity<Duty>(entity =>
            {
                entity.Property(e => e.DutyId).HasColumnName("DutyID");

                entity.Property(e => e.DutyDisabled).HasDefaultValueSql("0");

                entity.Property(e => e.DutyName).HasMaxLength(500);

                entity.Property(e => e.DutyNo).HasMaxLength(50);

                entity.Property(e => e.DutyNotes).HasMaxLength(500);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmpId)
                    .HasName("PK_Employee");

                entity.Property(e => e.EmpId).HasColumnName("EmpID");

                entity.Property(e => e.DepId).HasColumnName("DepID");

                entity.Property(e => e.DutyId).HasColumnName("DutyID");

                entity.Property(e => e.Email).HasColumnType("varchar(50)");

                entity.Property(e => e.EmpDisabled).HasDefaultValueSql("0");

                entity.Property(e => e.EmpGender).HasDefaultValueSql("0");

                entity.Property(e => e.EmpName).HasMaxLength(500);

                entity.Property(e => e.EmpNo).HasMaxLength(50);

                entity.Property(e => e.Phone).HasColumnType("varchar(50)");

                entity.Property(e => e.Skype).HasColumnType("varchar(50)");

                entity.Property(e => e.PhotoId).HasColumnName("PhotoID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.HasOne(d => d.Dep)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.DepId)
                    .HasConstraintName("FK_Employee_Department");

                entity.HasOne(d => d.Duty)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.DutyId)
                    .HasConstraintName("FK_Employee_Duty");

                entity.HasOne(d => d.Photo)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.PhotoId)
                    .HasConstraintName("FK_Employee_Photo");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Employee_Status");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.EventId).HasColumnName("EventID");

                entity.Property(e => e.EventInfo1).HasMaxLength(500);

                entity.Property(e => e.EventInfo2).HasMaxLength(500);

                entity.Property(e => e.EventInfo3).HasMaxLength(500);

                entity.Property(e => e.EventInfo4).HasMaxLength(500);

                entity.Property(e => e.EventInfo5).HasMaxLength(500);

                entity.Property(e => e.EventName).HasMaxLength(500);

                entity.Property(e => e.PhotoId).HasColumnName("PhotoID");

                entity.HasOne(d => d.Photo)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.PhotoId)
                    .HasConstraintName("FK_Event_Photo");
            });

            modelBuilder.Entity<Photo>(entity =>
            {
                entity.Property(e => e.PhotoId).HasColumnName("PhotoID");

                entity.Property(e => e.PhotoDisabled).HasDefaultValueSql("0");

                entity.Property(e => e.PhotoLarge01).HasMaxLength(1000);

                entity.Property(e => e.PhotoLarge02).HasMaxLength(1000);

                entity.Property(e => e.PhotoLarge03).HasMaxLength(1000);

                entity.Property(e => e.PhotoLarge04).HasMaxLength(1000);

                entity.Property(e => e.PhotoLarge05).HasMaxLength(1000);

                entity.Property(e => e.PhotoName).HasMaxLength(500);

                entity.Property(e => e.PhotoNotes).HasMaxLength(500);

                entity.Property(e => e.PhotoThumbNail).HasMaxLength(1000);
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.Property(e => e.PostId).HasColumnName("PostID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.PhotoId).HasColumnName("PhotoID");

                entity.Property(e => e.PostContent).HasColumnType("ntext");

                entity.Property(e => e.PostDateCreate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.PostDescription).HasColumnType("ntext");

                entity.Property(e => e.PostDisabled).HasDefaultValueSql("0");

                entity.Property(e => e.PostLabel).HasMaxLength(500);

                entity.Property(e => e.PostLastUpdate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.PostTitle).HasMaxLength(500);

                entity.Property(e => e.PostUserCreate).HasMaxLength(450);

                entity.Property(e => e.PostUserUpdate).HasMaxLength(450);

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Post_Category");

                entity.HasOne(d => d.Photo)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.PhotoId)
                    .HasConstraintName("FK_Post_Photo");

                entity.HasOne(d => d.PostUserCreateNavigation)
                    .WithMany(p => p.PostPostUserCreateNavigation)
                    .HasForeignKey(d => d.PostUserCreate)
                    .HasConstraintName("FK_Post_AspNetUsersCreate");

                entity.HasOne(d => d.PostUserUpdateNavigation)
                    .WithMany(p => p.PostPostUserUpdateNavigation)
                    .HasForeignKey(d => d.PostUserUpdate)
                    .HasConstraintName("FK_Post_AspNetUsersUpdate");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Post_Status");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProdId)
                    .HasName("PK_Product");

                entity.Property(e => e.ProdId).HasColumnName("ProdID");

                entity.Property(e => e.PhotoId).HasColumnName("PhotoID");

                entity.Property(e => e.ProdDescription).HasMaxLength(500);

                entity.Property(e => e.ProdDescription1).HasColumnType("ntext");

                entity.Property(e => e.ProdDescription2).HasColumnType("ntext");

                entity.Property(e => e.ProdDescription3).HasColumnType("ntext");

                entity.Property(e => e.ProdDescription4).HasColumnType("ntext");

                entity.Property(e => e.ProdDisabled).HasDefaultValueSql("0");

                entity.Property(e => e.ProdName).HasMaxLength(500);

                entity.Property(e => e.ProdNo).HasMaxLength(500);

                entity.Property(e => e.ProdNotes).HasMaxLength(500);

                entity.HasOne(d => d.Photo)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.PhotoId)
                    .HasConstraintName("FK_Product_Photo");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.StatusDisabled).HasDefaultValueSql("0");

                entity.Property(e => e.StatusName).HasMaxLength(500);

                entity.Property(e => e.StatusNotes).HasMaxLength(500);

                entity.Property(e => e.TableName).HasColumnType("varchar(50)");
            });
        }
    }
}