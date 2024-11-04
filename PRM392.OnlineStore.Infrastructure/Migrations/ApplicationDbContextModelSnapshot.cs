﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PRM392.OnlineStore.Infrastructure.Persistence;

#nullable disable

namespace PRM392.OnlineStore.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PRM392.OnlineStore.Domain.Entities.Models.Cart", b =>
                {
                    b.Property<int>("CartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CartID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartId"));

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("CartId")
                        .HasName("PK__Carts__51BCD79715BE33A0");

                    b.HasIndex("UserId");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("PRM392.OnlineStore.Domain.Entities.Models.CartItem", b =>
                {
                    b.Property<int>("CartItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CartItemID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartItemId"));

                    b.Property<int?>("CartId")
                        .HasColumnType("int")
                        .HasColumnName("CartID");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("ProductID");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("CartItemId")
                        .HasName("PK__CartItem__488B0B2ACFA5F688");

                    b.HasIndex("CartId");

                    b.HasIndex("ProductId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("PRM392.OnlineStore.Domain.Entities.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CategoryID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("CategoryId")
                        .HasName("PK__Categori__19093A2BA147EEE1");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("PRM392.OnlineStore.Domain.Entities.Models.ChatMessage", b =>
                {
                    b.Property<int>("ChatMessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ChatMessageID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ChatMessageId"));

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RecipientId")
                        .HasColumnType("int")
                        .HasColumnName("RecipientID");

                    b.Property<DateTime>("SentAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("ChatMessageId")
                        .HasName("PK__ChatMess__9AB61055FB5E89DB");

                    b.HasIndex("UserId");

                    b.ToTable("ChatMessages");
                });

            modelBuilder.Entity("PRM392.OnlineStore.Domain.Entities.Models.Notification", b =>
                {
                    b.Property<int>("NotificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("NotificationID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NotificationId"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<string>("Message")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("NotificationId")
                        .HasName("PK__Notifica__20CF2E320E7EBCA6");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("PRM392.OnlineStore.Domain.Entities.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("OrderID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<string>("BillingAddress")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("CartId")
                        .HasColumnType("int")
                        .HasColumnName("CartID");

                    b.Property<int?>("LocationId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("OrderStatus")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("OrderId")
                        .HasName("PK__Orders__C3905BAF7427A975");

                    b.HasIndex("CartId");

                    b.HasIndex("LocationId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("PRM392.OnlineStore.Domain.Entities.Models.Payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PaymentID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentId"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("OrderID");

                    b.Property<DateTime>("PaymentDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("PaymentStatus")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("PaymentId")
                        .HasName("PK__Payments__9B556A5816F52A86");

                    b.HasIndex("OrderId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("PRM392.OnlineStore.Domain.Entities.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ProductID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<string>("BriefDescription")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int")
                        .HasColumnName("CategoryID");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("FullDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("ImageURL");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("TechnicalSpecifications")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("ProductId")
                        .HasName("PK__Products__B40CC6ED098ACCE2");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("PRM392.OnlineStore.Domain.Entities.Models.StoreLocation", b =>
                {
                    b.Property<int>("LocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("LocationID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LocationId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<decimal>("Latitude")
                        .HasColumnType("decimal(9, 6)");

                    b.Property<decimal>("Longitude")
                        .HasColumnType("decimal(9, 6)");

                    b.HasKey("LocationId")
                        .HasName("PK__StoreLoc__E7FEA4779CA8FC05");

                    b.ToTable("StoreLocations");
                });

            modelBuilder.Entity("PRM392.OnlineStore.Domain.Entities.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RefreshTokenExpiryTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RefreshTokenIssuedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UserId")
                        .HasName("PK__Users__1788CCAC21F76469");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PRM392.OnlineStore.Domain.Entities.Models.Cart", b =>
                {
                    b.HasOne("PRM392.OnlineStore.Domain.Entities.Models.User", "User")
                        .WithMany("Carts")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__Carts__UserID__3E52440B");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PRM392.OnlineStore.Domain.Entities.Models.CartItem", b =>
                {
                    b.HasOne("PRM392.OnlineStore.Domain.Entities.Models.Cart", "Cart")
                        .WithMany("CartItems")
                        .HasForeignKey("CartId")
                        .HasConstraintName("FK__CartItems__CartI__412EB0B6");

                    b.HasOne("PRM392.OnlineStore.Domain.Entities.Models.Product", "Product")
                        .WithMany("CartItems")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK__CartItems__Produ__4222D4EF");

                    b.Navigation("Cart");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("PRM392.OnlineStore.Domain.Entities.Models.ChatMessage", b =>
                {
                    b.HasOne("PRM392.OnlineStore.Domain.Entities.Models.User", "User")
                        .WithMany("ChatMessages")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__ChatMessa__UserI__534D60F1");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PRM392.OnlineStore.Domain.Entities.Models.Notification", b =>
                {
                    b.HasOne("PRM392.OnlineStore.Domain.Entities.Models.User", "User")
                        .WithMany("Notifications")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__Notificat__UserI__4F7CD00D");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PRM392.OnlineStore.Domain.Entities.Models.Order", b =>
                {
                    b.HasOne("PRM392.OnlineStore.Domain.Entities.Models.Cart", "Cart")
                        .WithMany("Orders")
                        .HasForeignKey("CartId")
                        .HasConstraintName("FK__Orders__CartID__45F365D3");

                    b.HasOne("PRM392.OnlineStore.Domain.Entities.Models.StoreLocation", "StoreLocation")
                        .WithMany("Orders")
                        .HasForeignKey("LocationId")
                        .HasConstraintName("FK__Orders__LocationID__45F365D3");

                    b.HasOne("PRM392.OnlineStore.Domain.Entities.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__Orders__UserID__46E78A0C");

                    b.Navigation("Cart");

                    b.Navigation("StoreLocation");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PRM392.OnlineStore.Domain.Entities.Models.Payment", b =>
                {
                    b.HasOne("PRM392.OnlineStore.Domain.Entities.Models.Order", "Order")
                        .WithMany("Payments")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("FK__Payments__OrderI__4AB81AF0");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("PRM392.OnlineStore.Domain.Entities.Models.Product", b =>
                {
                    b.HasOne("PRM392.OnlineStore.Domain.Entities.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("FK__Products__Catego__3B75D760");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("PRM392.OnlineStore.Domain.Entities.Models.Cart", b =>
                {
                    b.Navigation("CartItems");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("PRM392.OnlineStore.Domain.Entities.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("PRM392.OnlineStore.Domain.Entities.Models.Order", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("PRM392.OnlineStore.Domain.Entities.Models.Product", b =>
                {
                    b.Navigation("CartItems");
                });

            modelBuilder.Entity("PRM392.OnlineStore.Domain.Entities.Models.StoreLocation", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("PRM392.OnlineStore.Domain.Entities.Models.User", b =>
                {
                    b.Navigation("Carts");

                    b.Navigation("ChatMessages");

                    b.Navigation("Notifications");

                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
