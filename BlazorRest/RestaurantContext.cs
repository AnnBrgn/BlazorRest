using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BlazorRest;

public partial class RestaurantContext : DbContext
{
    public RestaurantContext()
    {
    }

    public RestaurantContext(DbContextOptions<RestaurantContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CrossProductUser> CrossProductUsers { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-G4Q8TTS;Database=Restaurant;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CrossProductUser>(entity =>
        {
            entity.HasKey(e => e.IdProduct);

            entity.ToTable("CrossProductUser");

            entity.Property(e => e.IdProduct).ValueGeneratedNever();

            entity.HasOne(d => d.IdProductNavigation).WithOne(p => p.CrossProductUser)
                .HasForeignKey<CrossProductUser>(d => d.IdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CrossProductUser_Product");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.CrossProductUsers)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_CrossProductUser_User");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Menu");

            entity.ToTable("Product");

            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.Icon).HasColumnType("image");
            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.NameUser).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
