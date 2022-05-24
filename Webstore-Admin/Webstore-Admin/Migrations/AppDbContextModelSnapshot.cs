﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Webstore_Admin.Data.Contexts;

namespace Webstore_Admin.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Webstore_Admin.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Fruit"
                        },
                        new
                        {
                            Id = 2,
                            Name = "GB"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Other"
                        });
                });

            modelBuilder.Entity("Webstore_Admin.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Kneippbyn 15",
                            City = "Visby",
                            Email = "pippi@langstrump.se",
                            Name = "Pippi Långstrump",
                            PhoneNumber = "0701112233",
                            ZipCode = "622 61"
                        },
                        new
                        {
                            Id = 2,
                            Address = "Rörstrandsgatan 25",
                            City = "Stockholm",
                            Email = "karlsson@taket.se",
                            Name = "Karlsson på taket",
                            PhoneNumber = "0704445566",
                            ZipCode = "113 41"
                        },
                        new
                        {
                            Id = 3,
                            Address = "Katthult",
                            City = "Katthult",
                            Email = "emil@svensson.se",
                            Name = "Emil Svensson",
                            PhoneNumber = "0707778899",
                            ZipCode = "598 92"
                        },
                        new
                        {
                            Id = 4,
                            Address = "Stränge 16",
                            City = "Mellerud",
                            Email = "ronja@rovardotter.se",
                            Name = "Ronja Rövardotter",
                            PhoneNumber = "0701234567",
                            ZipCode = "464 82"
                        });
                });

            modelBuilder.Entity("Webstore_Admin.Models.Discount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Percent")
                        .HasColumnType("decimal(3,2)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("Webstore_Admin.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderCreated")
                        .HasColumnType("datetime2");

                    b.Property<double>("Temperature")
                        .HasColumnType("float");

                    b.Property<string>("WeatherType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("WindSpeed")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Webstore_Admin.Models.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("Webstore_Admin.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UnitsInStock")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            Name = "Royal Gala",
                            Price = 4.95m,
                            UnitsInStock = 100
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 1,
                            Name = "Pink Lady",
                            Price = 4.95m,
                            UnitsInStock = 15
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 1,
                            Name = "Granny Smith",
                            Price = 3.95m,
                            UnitsInStock = 100
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 1,
                            Name = "Red Delicious",
                            Price = 2.95m,
                            UnitsInStock = 70
                        },
                        new
                        {
                            Id = 18,
                            CategoryId = 1,
                            Name = "Jonagold",
                            Price = 1.95m,
                            UnitsInStock = 50
                        },
                        new
                        {
                            Id = 19,
                            CategoryId = 1,
                            Name = "Golden Delicious",
                            Price = 2.95m,
                            UnitsInStock = 50
                        },
                        new
                        {
                            Id = 20,
                            CategoryId = 1,
                            Name = "Royal Gala EKO",
                            Price = 8.95m,
                            UnitsInStock = 40
                        },
                        new
                        {
                            Id = 21,
                            CategoryId = 1,
                            Name = "Discovery",
                            Price = 3.95m,
                            UnitsInStock = 50
                        },
                        new
                        {
                            Id = 22,
                            CategoryId = 1,
                            Name = "Ingrid Marie",
                            Price = 3.95m,
                            UnitsInStock = 5
                        },
                        new
                        {
                            Id = 23,
                            CategoryId = 1,
                            Name = "Rubinstar",
                            Price = 4.95m,
                            UnitsInStock = 12
                        },
                        new
                        {
                            Id = 24,
                            CategoryId = 1,
                            Name = "Cox Orange",
                            Price = 4.95m,
                            UnitsInStock = 60
                        },
                        new
                        {
                            Id = 25,
                            CategoryId = 1,
                            Name = "Äppelkorg",
                            Price = 34.95m,
                            UnitsInStock = 8
                        },
                        new
                        {
                            Id = 26,
                            CategoryId = 1,
                            Name = "Äppelpåse 1Kg",
                            Price = 19.95m,
                            UnitsInStock = 20
                        },
                        new
                        {
                            Id = 27,
                            CategoryId = 1,
                            Name = "Honey Crunch",
                            Price = 12.95m,
                            UnitsInStock = 33
                        },
                        new
                        {
                            Id = 28,
                            CategoryId = 1,
                            Name = "Granny Smith EKO",
                            Price = 7.95m,
                            UnitsInStock = 12
                        },
                        new
                        {
                            Id = 5,
                            CategoryId = 2,
                            Name = "Calippo Cola",
                            Price = 8.95m,
                            UnitsInStock = 10
                        },
                        new
                        {
                            Id = 6,
                            CategoryId = 2,
                            Name = "Twister",
                            Price = 9.95m,
                            UnitsInStock = 3
                        },
                        new
                        {
                            Id = 7,
                            CategoryId = 2,
                            Name = "Piggelin",
                            Price = 5.95m,
                            UnitsInStock = 13
                        },
                        new
                        {
                            Id = 8,
                            CategoryId = 2,
                            Name = "Nogger",
                            Price = 12.95m,
                            UnitsInStock = 10
                        },
                        new
                        {
                            Id = 9,
                            CategoryId = 2,
                            Name = "88",
                            Price = 12.95m,
                            UnitsInStock = 5
                        },
                        new
                        {
                            Id = 10,
                            CategoryId = 2,
                            Name = "Sandwich",
                            Price = 12.95m,
                            UnitsInStock = 12
                        },
                        new
                        {
                            Id = 11,
                            CategoryId = 2,
                            Name = "Cornetto Jordgubb",
                            Price = 13.95m,
                            UnitsInStock = 10
                        },
                        new
                        {
                            Id = 12,
                            CategoryId = 2,
                            Name = "Daim",
                            Price = 18.95m,
                            UnitsInStock = 18
                        },
                        new
                        {
                            Id = 13,
                            CategoryId = 2,
                            Name = "Magnum Strawberry White",
                            Price = 18.95m,
                            UnitsInStock = 18
                        },
                        new
                        {
                            Id = 14,
                            CategoryId = 2,
                            Name = "Lakrits Puck",
                            Price = 12.95m,
                            UnitsInStock = 19
                        },
                        new
                        {
                            Id = 15,
                            CategoryId = 3,
                            Name = "Apple Juice",
                            Price = 16.95m,
                            UnitsInStock = 20
                        },
                        new
                        {
                            Id = 16,
                            CategoryId = 3,
                            Name = "Apple Cider",
                            Price = 18.95m,
                            UnitsInStock = 35
                        },
                        new
                        {
                            Id = 17,
                            CategoryId = 3,
                            Name = "Sweet Soda",
                            Price = 13.95m,
                            UnitsInStock = 5
                        });
                });

            modelBuilder.Entity("Webstore_Admin.Models.Discount", b =>
                {
                    b.HasOne("Webstore_Admin.Models.Product", "Product")
                        .WithMany("Discounts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Webstore_Admin.Models.Order", b =>
                {
                    b.HasOne("Webstore_Admin.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Webstore_Admin.Models.OrderDetail", b =>
                {
                    b.HasOne("Webstore_Admin.Models.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Webstore_Admin.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Webstore_Admin.Models.Product", b =>
                {
                    b.HasOne("Webstore_Admin.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
