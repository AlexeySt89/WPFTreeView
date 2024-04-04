using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WPFTreeView.Model;

namespace WPFTreeView;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Model.Attribute> Attributes { get; set; }

    public virtual DbSet<Model.Link> Links { get; set; }

    public virtual DbSet<Model.Object> Objects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=your_server;Database=your_database;Trusted_Connection=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Model.Attribute>(entity =>
        {
            entity.HasKey(e => new { e.ObjectId, e.Name });

            entity.Property(e => e.ObjectId).HasColumnName("objectId");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Value)
                .HasMaxLength(255)
                .HasColumnName("value");

            entity.HasOne(d => d.Object).WithMany(p => p.Attributes)
                .HasForeignKey(d => d.ObjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Attribute__objec__29572725");
        });

        modelBuilder.Entity<Model.Link>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.ChildId).HasColumnName("childId");
            entity.Property(e => e.ParentId).HasColumnName("parentId");

            entity.HasOne(d => d.Child).WithMany()
                .HasForeignKey(d => d.ChildId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Links__childId__267ABA7A");

            entity.HasOne(d => d.Parent).WithMany()
                .HasForeignKey(d => d.ParentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Links__parentId__25869641");
        });

        modelBuilder.Entity<Model.Object>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Objects__3213E83F6F8BA072");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Product)
                .HasMaxLength(255)
                .HasColumnName("product");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .HasColumnName("type");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
