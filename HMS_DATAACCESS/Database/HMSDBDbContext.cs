using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace HMS_DATAACCESS.Database
{
    public partial class HMSDBDbContext : DbContext
    {
        public HMSDBDbContext()
        {
        }

        public HMSDBDbContext(DbContextOptions<HMSDBDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<FoodItem> FoodItems { get; set; }
        public virtual DbSet<LoginMaster> LoginMasters { get; set; }
        public virtual DbSet<OrderItemstbl> OrderItemstbls { get; set; }
        public virtual DbSet<OrderTbl> OrderTbls { get; set; }
        public virtual DbSet<TableMaster> TableMasters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=GURAV\\SQLEXPRESS;Database=HMSDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<FoodItem>(entity =>
            {
                entity.HasOne(d => d.Category)
                    .WithMany(p => p.FoodItems)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__FoodItem__catego__1367E606");
            });

            modelBuilder.Entity<OrderItemstbl>(entity =>
            {
                entity.HasOne(d => d.Item)
                    .WithMany(p => p.OrderItemstbls)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("FK__OrderItem__ItemI__1920BF5C");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItemstbls)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__OrderItem__Order__182C9B23");
            });

            modelBuilder.Entity<OrderTbl>(entity =>
            {
                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.OrderTbls)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__orderTbl__Employ__09DE7BCC");

                entity.HasOne(d => d.Table)
                    .WithMany(p => p.OrderTbls)
                    .HasForeignKey(d => d.TableId)
                    .HasConstraintName("FK__orderTbl__TableI__0AD2A005");
            });

            modelBuilder.Entity<TableMaster>(entity =>
            {
                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TableMasters)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__TableMast__Emplo__0519C6AF");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
