using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MyVi.API.Entities
{
    public partial class MyVIDBContext : DbContext
    {
        public MyVIDBContext()
        {
        }

        public MyVIDBContext(DbContextOptions<MyVIDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Plan> Plan { get; set; }
        public virtual DbSet<PlanType> PlanType { get; set; }
        public virtual DbSet<PortNumber> PortNumber { get; set; }
        public virtual DbSet<RoamingPlan> RoamingPlan { get; set; }
        public virtual DbSet<Simtype> Simtype { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRechargeHistory> UserRechargeHistory { get; set; }
        public virtual DbSet<Vipnumber> Vipnumber { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=MyVIDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.OnCreated).HasColumnType("datetime");

                entity.Property(e => e.OnUpdated).HasColumnType("datetime");

                entity.Property(e => e.PincodeNo)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.Property(e => e.ContactNo)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.OnCreated).HasColumnType("datetime");

                entity.Property(e => e.OnUpdated).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.OnCreated).HasColumnType("datetime");

                entity.Property(e => e.OnUpdated).HasColumnType("datetime");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.AlternateContactNo)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.OnCreated).HasColumnType("datetime");

                entity.Property(e => e.OnUpdated).HasColumnType("datetime");

                entity.Property(e => e.SimtypeId)
                    .HasColumnName("SIMTypeId")
                    .HasComment("Prepaid = 1, Postpaid = 2");

                entity.Property(e => e.VipnumberId).HasColumnName("VIPNumberId");

                entity.HasOne(d => d.Addess)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.AddessId)
                    .HasConstraintName("FK_Order_Address");

                entity.HasOne(d => d.Plan)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.PlanId)
                    .HasConstraintName("FK_Order_Plan");

                entity.HasOne(d => d.PortNumber)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.PortNumberId)
                    .HasConstraintName("FK_Order_PortNumber");

                entity.HasOne(d => d.Simtype)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.SimtypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_SIMType");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Order");

                entity.HasOne(d => d.Vipnumber)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.VipnumberId)
                    .HasConstraintName("FK_Order_VIPNumber");
            });

            modelBuilder.Entity<Plan>(entity =>
            {
                entity.Property(e => e.Benefits).IsUnicode(false);

                entity.Property(e => e.Call)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Data)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.OnCreated).HasColumnType("datetime");

                entity.Property(e => e.OnUpdated).HasColumnType("datetime");

                entity.Property(e => e.Sms)
                    .HasColumnName("SMS")
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PlanType>(entity =>
            {
                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasComment("Prepaid = 1, Postpaid = 2 ");

                entity.Property(e => e.OnCreated).HasColumnType("datetime");

                entity.Property(e => e.OnUpdated).HasColumnType("datetime");

                entity.HasOne(d => d.SimType)
                    .WithMany(p => p.PlanType)
                    .HasForeignKey(d => d.SimTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIMType_PlanType");
            });

            modelBuilder.Entity<PortNumber>(entity =>
            {
                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.OnCreated).HasColumnType("datetime");

                entity.Property(e => e.OnUpdated).HasColumnType("datetime");

                entity.Property(e => e.SimtypeId).HasColumnName("SIMTypeId");

                entity.HasOne(d => d.Simtype)
                    .WithMany(p => p.PortNumber)
                    .HasForeignKey(d => d.SimtypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIMType_PortNumber");
            });

            modelBuilder.Entity<RoamingPlan>(entity =>
            {
                entity.Property(e => e.OnCreated).HasColumnType("datetime");

                entity.Property(e => e.OnUpdated).HasColumnType("datetime");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.RoamingPlan)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Country_RoamingPlan");

                entity.HasOne(d => d.Plan)
                    .WithMany(p => p.RoamingPlan)
                    .HasForeignKey(d => d.PlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Plan_RoamingPlan");
            });

            modelBuilder.Entity<Simtype>(entity =>
            {
                entity.ToTable("SIMType");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.OnCreated).HasColumnType("datetime");

                entity.Property(e => e.OnUpdated).HasColumnType("datetime");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.ContactNo)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.OnCreated).HasColumnType("datetime");

                entity.Property(e => e.OnUpdated).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserRechargeHistory>(entity =>
            {
                entity.Property(e => e.OnCreated).HasColumnType("datetime");

                entity.Property(e => e.OnUpdated).HasColumnType("datetime");

                entity.Property(e => e.RzpOrderId).HasMaxLength(25);

                entity.Property(e => e.RzpPaymentId).HasMaxLength(25);

                entity.HasOne(d => d.Plan)
                    .WithMany(p => p.UserRechargeHistory)
                    .HasForeignKey(d => d.PlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Plan_UserRechargeHistory");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRechargeHistory)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_UserRechargeHistory");
            });

            modelBuilder.Entity<Vipnumber>(entity =>
            {
                entity.ToTable("VIPNumber");

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.OnCreated).HasColumnType("datetime");

                entity.Property(e => e.OnUpdated).HasColumnType("datetime");

                entity.Property(e => e.SimtypeId).HasColumnName("SIMTypeId");

                entity.HasOne(d => d.Simtype)
                    .WithMany(p => p.Vipnumber)
                    .HasForeignKey(d => d.SimtypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIMType_VIPNumber");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
