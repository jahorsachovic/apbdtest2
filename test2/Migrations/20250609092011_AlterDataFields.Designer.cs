﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using test2.DAL;

#nullable disable

namespace test2.Migrations
{
    [DbContext(typeof(ConcertsDbContext))]
    [Migration("20250609092011_AlterDataFields")]
    partial class AlterDataFields
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("test2.Models.Concert", b =>
                {
                    b.Property<int>("ConcertId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ConcertId"));

                    b.Property<int>("AvailableTickets")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ConcertId");

                    b.ToTable("Concert");

                    b.HasData(
                        new
                        {
                            ConcertId = 1,
                            AvailableTickets = 0,
                            Date = new DateTime(2025, 6, 7, 9, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Concert 1"
                        },
                        new
                        {
                            ConcertId = 14,
                            AvailableTickets = 0,
                            Date = new DateTime(2025, 6, 10, 9, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Concert 14"
                        });
                });

            modelBuilder.Entity("test2.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customer");

                    b.HasData(
                        new
                        {
                            CustomerId = 1,
                            FirstName = "John",
                            LastName = "Doe"
                        });
                });

            modelBuilder.Entity("test2.Models.PurchasedTicket", b =>
                {
                    b.Property<int>("TicketConcertId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.HasKey("TicketConcertId", "CustomerId");

                    b.HasIndex("CustomerId");

                    b.ToTable("PurchasedTicket");

                    b.HasData(
                        new
                        {
                            TicketConcertId = 1,
                            CustomerId = 1,
                            PurchaseDate = new DateTime(2025, 6, 3, 9, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            TicketConcertId = 2,
                            CustomerId = 1,
                            PurchaseDate = new DateTime(2025, 6, 3, 9, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("test2.Models.Ticket", b =>
                {
                    b.Property<int>("TicketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TicketId"));

                    b.Property<int>("SeatNumber")
                        .HasColumnType("int");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("TicketId");

                    b.ToTable("Ticket");

                    b.HasData(
                        new
                        {
                            TicketId = 1,
                            SeatNumber = 124,
                            SerialNumber = "TK2034/S4531/12"
                        },
                        new
                        {
                            TicketId = 2,
                            SeatNumber = 330,
                            SerialNumber = "TK2027/S4831/133"
                        });
                });

            modelBuilder.Entity("test2.Models.TicketConcert", b =>
                {
                    b.Property<int>("TicketConcertId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TicketConcertId"));

                    b.Property<int>("ConcertId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasPrecision(10)
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("TicketId")
                        .HasColumnType("int");

                    b.HasKey("TicketConcertId");

                    b.HasIndex("ConcertId");

                    b.HasIndex("TicketId");

                    b.ToTable("TicketConcert");

                    b.HasData(
                        new
                        {
                            TicketConcertId = 1,
                            ConcertId = 1,
                            Price = 33.4m,
                            TicketId = 1
                        },
                        new
                        {
                            TicketConcertId = 2,
                            ConcertId = 14,
                            Price = 48.4m,
                            TicketId = 2
                        });
                });

            modelBuilder.Entity("test2.Models.PurchasedTicket", b =>
                {
                    b.HasOne("test2.Models.Customer", "Customer")
                        .WithMany("purchases")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("test2.Models.TicketConcert", "TicketConcert")
                        .WithMany()
                        .HasForeignKey("TicketConcertId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("TicketConcert");
                });

            modelBuilder.Entity("test2.Models.TicketConcert", b =>
                {
                    b.HasOne("test2.Models.Concert", "Concert")
                        .WithMany()
                        .HasForeignKey("ConcertId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("test2.Models.Ticket", "Ticket")
                        .WithMany()
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Concert");

                    b.Navigation("Ticket");
                });

            modelBuilder.Entity("test2.Models.Customer", b =>
                {
                    b.Navigation("purchases");
                });
#pragma warning restore 612, 618
        }
    }
}
