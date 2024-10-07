using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AuctionAce.Infrastructure.Data.Models;

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

    public virtual DbSet<AuctionsItemsPhoto> AuctionsItemsPhotos { get; set; }

    public virtual DbSet<BidHistory> BidHistories { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<ChatHistory> ChatHistories { get; set; }

    public virtual DbSet<Leaderboard> Leaderboards { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserBoughtItem> UserBoughtItems { get; set; }

    public virtual DbSet<Wishlist> Wishlists { get; set; }

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
            entity.Property(e => e.BuyNowPrice)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("buy_now_price");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Guid).HasColumnName("guid");
            entity.Property(e => e.IdAuctions).HasColumnName("id_auctions");
            entity.Property(e => e.IdStatus).HasColumnName("id_status");
            entity.Property(e => e.IsBought).HasColumnName("isBought");
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

        modelBuilder.Entity<AuctionsItemsPhoto>(entity =>
        {
            entity.ToTable("Auctions_Items_Photos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AuctionItemId).HasColumnName("auction_item_id");
            entity.Property(e => e.AuctionsId).HasColumnName("auctions_id");
            entity.Property(e => e.FileName).HasColumnName("file_name");
            entity.Property(e => e.Path).HasColumnName("path");

            entity.HasOne(d => d.AuctionItem).WithMany(p => p.AuctionsItemsPhotos)
                .HasForeignKey(d => d.AuctionItemId)
                .HasConstraintName("FK_Auctions_Items_Photos_Auction_Items");

            entity.HasOne(d => d.Auctions).WithMany(p => p.AuctionsItemsPhotos)
                .HasForeignKey(d => d.AuctionsId)
                .HasConstraintName("FK_Auctions_Items_Photos_Auctions");
        });

        modelBuilder.Entity<BidHistory>(entity =>
        {
            entity.ToTable("Bid_History");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.IdAuctionItems).HasColumnName("id_auction_items");
            entity.Property(e => e.IdUsers).HasColumnName("id_users");
            entity.Property(e => e.IsWin).HasColumnName("isWin");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("price");
            entity.Property(e => e.UserEmail).HasColumnName("user_email");

            entity.HasOne(d => d.IdAuctionItemsNavigation).WithMany(p => p.BidHistories)
                .HasForeignKey(d => d.IdAuctionItems)
                .HasConstraintName("FK_Bid_History_Auction_Items");

            entity.HasOne(d => d.IdUsersNavigation).WithMany(p => p.BidHistories)
                .HasForeignKey(d => d.IdUsers)
                .HasConstraintName("FK_Bid_History_Users");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripton).HasColumnName("descripton");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<ChatHistory>(entity =>
        {
            entity.ToTable("Chat_History");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AuctionItemId).HasColumnName("auction_item_id");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.UserEmail).HasColumnName("user_email");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.AuctionItem).WithMany(p => p.ChatHistories)
                .HasForeignKey(d => d.AuctionItemId)
                .HasConstraintName("FK_Chat_History_Auction_Items");

            entity.HasOne(d => d.User).WithMany(p => p.ChatHistories)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Chat_History_Users");
        });

        modelBuilder.Entity<Leaderboard>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AuctionItemId).HasColumnName("auction_item_id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.IsFinal).HasColumnName("isFinal");
            entity.Property(e => e.Price).HasColumnName("price");

            entity.HasOne(d => d.AuctionItem).WithMany(p => p.Leaderboards)
                .HasForeignKey(d => d.AuctionItemId)
                .HasConstraintName("FK_Leaderboards_Auction_Items");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Leaderboards)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_Leaderboards_Users");
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

        modelBuilder.Entity<UserBoughtItem>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdAuctionItem).HasColumnName("id_auction_item");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Prize).HasColumnName("prize");

            entity.HasOne(d => d.IdAuctionItemNavigation).WithMany(p => p.UserBoughtItems)
                .HasForeignKey(d => d.IdAuctionItem)
                .HasConstraintName("FK_UserBoughtItems_Auction_Items");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.UserBoughtItems)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_UserBoughtItems_Users");
        });

        modelBuilder.Entity<Wishlist>(entity =>
        {
            entity.ToTable("Wishlist");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdAuction).HasColumnName("id_auction");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.IsLiked).HasColumnName("isLiked");

            entity.HasOne(d => d.IdAuctionNavigation).WithMany(p => p.Wishlists)
                .HasForeignKey(d => d.IdAuction)
                .HasConstraintName("FK_Wishlist_Auctions");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Wishlists)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_Wishlist_Users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
