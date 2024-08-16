using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AuctionAce.Api;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Auction> Auctions { get; set; }

    public virtual DbSet<AuctionItem> AuctionItems { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=MICHAť\\SQLEXPRESS;initial catalog=AuctionAce;MultipleActiveResultSets=True;App=EntityFramework;TrustServerCertificate=True;Trusted_Connection=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.ToTable("Address");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BuildingNumber).HasColumnName("building_number");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .HasColumnName("city");
            entity.Property(e => e.Street)
                .HasMaxLength(50)
                .HasColumnName("street");
            entity.Property(e => e.ZipCode)
                .HasMaxLength(50)
                .HasColumnName("zip_code");
        });

        modelBuilder.Entity<Auction>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AuctionName).HasColumnName("auction_name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("end_date");
            entity.Property(e => e.IdCategory).HasColumnName("id_category");
            entity.Property(e => e.IdData).HasColumnName("id_data");
            entity.Property(e => e.IdUsers).HasColumnName("id_users");
            entity.Property(e => e.ImagePath).HasColumnName("image_path");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("start_date");

            entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.Auctions)
                .HasForeignKey(d => d.IdCategory)
                .HasConstraintName("FK_Auctions_Category");

            entity.HasOne(d => d.IdUsersNavigation).WithMany(p => p.Auctions)
                .HasForeignKey(d => d.IdUsers)
                .HasConstraintName("FK_Auctions_Users");
        });

        modelBuilder.Entity<AuctionItem>(entity =>
        {
            entity.ToTable("Auction_Items");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("amount");
            entity.Property(e => e.BuyNowPrice)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("buy_now_price");
            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .HasColumnName("category");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IdAuctions).HasColumnName("id_auctions");
            entity.Property(e => e.IdStatus).HasColumnName("id_status");
            entity.Property(e => e.ImagePath).HasColumnName("image_path");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.NewUsed).HasColumnName("new_used");
            entity.Property(e => e.StartingPrice)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("starting_price");

            entity.HasOne(d => d.IdAuctionsNavigation).WithMany(p => p.AuctionItems)
                .HasForeignKey(d => d.IdAuctions)
                .HasConstraintName("FK_Auction_Items_Auctions");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripton).HasColumnName("descripton");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.IdAddress).HasColumnName("id_address");
            entity.Property(e => e.IdRoles).HasColumnName("id_roles");
            entity.Property(e => e.IdStatus).HasColumnName("id_status");
            entity.Property(e => e.IsLogined).HasColumnName("isLogined");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .HasColumnName("surname");

            entity.HasOne(d => d.IdAddressNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdAddress)
                .HasConstraintName("FK_Users_Address");

            entity.HasOne(d => d.IdRolesNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRoles)
                .HasConstraintName("FK_Users_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
