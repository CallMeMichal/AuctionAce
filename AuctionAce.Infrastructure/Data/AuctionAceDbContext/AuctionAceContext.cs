using System;
using System.Collections.Generic;
using AuctionAce.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AuctionAce.Infrastructure.Data.AuctionAceDbContext;

public partial class AuctionAceContext : DbContext
{
    public AuctionAceContext()
    {
    }

    public AuctionAceContext(DbContextOptions<AuctionAceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Auction> Auctions { get; set; }

    public virtual DbSet<AuctionItem> AuctionItems { get; set; }

    public virtual DbSet<Calendar> Calendars { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<ItemHistory> ItemHistories { get; set; }

    public virtual DbSet<MessageHistory> MessageHistories { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Shipping> Shippings { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Watchlist> Watchlists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=MICHAť\\SQLEXPRESS;database=AuctionAce;Integrated Security=True;encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.ToTable("Address");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BuildingNumber).HasColumnName("building_number");
            entity.Property(e => e.City)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("city");
            entity.Property(e => e.Street)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("street");
            entity.Property(e => e.ZipCode)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("zip_code");
        });

        modelBuilder.Entity<Auction>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AuctionName).HasColumnName("auction_name");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("end_date");
            entity.Property(e => e.IdCategory).HasColumnName("id_category");
            entity.Property(e => e.IdData).HasColumnName("id_data");
            entity.Property(e => e.IdPayments).HasColumnName("id_payments");
            entity.Property(e => e.IdStatus).HasColumnName("id_status");
            entity.Property(e => e.IdUsers).HasColumnName("id_users");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("start_date");

            entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.Auctions)
                .HasForeignKey(d => d.IdCategory)
                .HasConstraintName("FK_Auctions_Category");

            entity.HasOne(d => d.IdDataNavigation).WithMany(p => p.Auctions)
                .HasForeignKey(d => d.IdData)
                .HasConstraintName("FK_Auctions_Calendar1");

            entity.HasOne(d => d.IdStatusNavigation).WithMany(p => p.Auctions)
                .HasForeignKey(d => d.IdStatus)
                .HasConstraintName("FK_Auctions_Status");

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
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.NewUsed).HasColumnName("new_used");
            entity.Property(e => e.Photo)
                .HasMaxLength(50)
                .HasColumnName("photo");
            entity.Property(e => e.StartingPrice)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("starting_price");

            entity.HasOne(d => d.IdAuctionsNavigation).WithMany(p => p.AuctionItems)
                .HasForeignKey(d => d.IdAuctions)
                .HasConstraintName("FK_Auction_Items_Auctions");

            entity.HasOne(d => d.IdStatusNavigation).WithMany(p => p.AuctionItems)
                .HasForeignKey(d => d.IdStatus)
                .HasConstraintName("FK_Auction_Items_Status");
        });

        modelBuilder.Entity<Calendar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Calendar_1");

            entity.ToTable("Calendar");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.EventDate)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("eventDate");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripton)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("descripton");
            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("name");
        });

        modelBuilder.Entity<ItemHistory>(entity =>
        {
            entity.ToTable("Item_History");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Data)
                .HasColumnType("datetime")
                .HasColumnName("data");
            entity.Property(e => e.IdAuctionItems).HasColumnName("id_auction_items");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("price");

            entity.HasOne(d => d.IdAuctionItemsNavigation).WithMany(p => p.ItemHistories)
                .HasForeignKey(d => d.IdAuctionItems)
                .HasConstraintName("FK_Item_History_Auction_Items");
        });

        modelBuilder.Entity<MessageHistory>(entity =>
        {
            entity.ToTable("Message_History");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Data).HasColumnName("data");
            entity.Property(e => e.IdAuctionItems).HasColumnName("id_auction_items");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Message)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("message");

            entity.HasOne(d => d.IdAuctionItemsNavigation).WithMany(p => p.MessageHistories)
                .HasForeignKey(d => d.IdAuctionItems)
                .HasConstraintName("FK_Message_History_Auction_Items");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.MessageHistories)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_Message_History_Users");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdAuctionItems).HasColumnName("id_auction_items");
            entity.Property(e => e.IdUser).HasColumnName("id_user");

            entity.HasOne(d => d.IdAuctionItemsNavigation).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.IdAuctionItems)
                .HasConstraintName("FK_Notifications_Auction_Items");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_Notifications_Users");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Type)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("type");

            entity.HasOne(d => d.IdNavigation).WithMany()
                .HasForeignKey(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payments_Auctions");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Shipping>(entity =>
        {
            entity.ToTable("Shipping");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdAddress).HasColumnName("id_address");
            entity.Property(e => e.IdAuctionItems).HasColumnName("id_auction_items");
            entity.Property(e => e.IdStatus).HasColumnName("id_status");

            entity.HasOne(d => d.IdAddressNavigation).WithMany(p => p.Shippings)
                .HasForeignKey(d => d.IdAddress)
                .HasConstraintName("FK_Shipping_Address");

            entity.HasOne(d => d.IdAuctionItemsNavigation).WithMany(p => p.Shippings)
                .HasForeignKey(d => d.IdAuctionItems)
                .HasConstraintName("FK_Shipping_Auction_Items");

            entity.HasOne(d => d.IdStatusNavigation).WithMany(p => p.Shippings)
                .HasForeignKey(d => d.IdStatus)
                .HasConstraintName("FK_Shipping_Status");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.ToTable("Status");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.StatusName)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("status_name");
            entity.Property(e => e.StatusType)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("status_type");
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

            entity.HasOne(d => d.IdStatusNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdStatus)
                .HasConstraintName("FK_Users_Status");
        });

        modelBuilder.Entity<Watchlist>(entity =>
        {
            entity.ToTable("Watchlist");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdAuction).HasColumnName("id_auction");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.WatchDate).HasColumnName("watch_date");

            entity.HasOne(d => d.IdAuctionNavigation).WithMany(p => p.Watchlists)
                .HasForeignKey(d => d.IdAuction)
                .HasConstraintName("FK_Watchlist_Auctions");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Watchlists)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_Watchlist_Users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
