﻿// <auto-generated />
using System;
using CedarWebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CedarWebApp.Migrations
{
    [DbContext(typeof(CedarContext))]
    partial class CedarContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CedarWebApp.Models.Cart", b =>
                {
                    b.Property<int>("CartId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("UserId");

                    b.HasKey("CartId");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("CedarWebApp.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description");

                    b.Property<string>("ImageUrl");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<int>("UserId");

                    b.HasKey("CategoryId");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("CedarWebApp.Models.CategoryJoined", b =>
                {
                    b.Property<int>("CategoryJoinedId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<int>("FoodItemId");

                    b.HasKey("CategoryJoinedId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("FoodItemId");

                    b.ToTable("CategoriesJoined");
                });

            modelBuilder.Entity("CedarWebApp.Models.FoodItem", b =>
                {
                    b.Property<int>("FoodItemId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description");

                    b.Property<string>("ImageUrl");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<int>("UserId");

                    b.HasKey("FoodItemId");

                    b.ToTable("Foods");
                });

            modelBuilder.Entity("CedarWebApp.Models.FoodJoined", b =>
                {
                    b.Property<int>("FoodJoinedId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CartId");

                    b.Property<int>("FoodId");

                    b.HasKey("FoodJoinedId");

                    b.HasIndex("CartId");

                    b.HasIndex("FoodId");

                    b.ToTable("FoodItemsJoined");
                });

            modelBuilder.Entity("CedarWebApp.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("RestaurantName")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CedarWebApp.Models.CategoryJoined", b =>
                {
                    b.HasOne("CedarWebApp.Models.Category", "Category")
                        .WithMany("FoodsJoined")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CedarWebApp.Models.FoodItem", "FoodItem")
                        .WithMany()
                        .HasForeignKey("FoodItemId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CedarWebApp.Models.FoodJoined", b =>
                {
                    b.HasOne("CedarWebApp.Models.Cart", "Cart")
                        .WithMany("FoodItemsJoined")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CedarWebApp.Models.FoodItem", "Food")
                        .WithMany()
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
