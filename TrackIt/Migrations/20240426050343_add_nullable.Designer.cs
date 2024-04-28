﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TrackIt.Data;

#nullable disable

namespace TrackIt.Migrations
{
    [DbContext(typeof(Applicationdbcontext))]
    [Migration("20240426050343_add_nullable")]
    partial class add_nullable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TrackIt.Models.ClinetClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ClientTable");
                });

            modelBuilder.Entity("TrackIt.Models.OrderClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Arival")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<int>("Product_id")
                        .HasColumnType("int");

                    b.Property<int?>("Quantity")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("Rate")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int>("vendor_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Product_id");

                    b.HasIndex("vendor_id");

                    b.ToTable("OrderTable");
                });

            modelBuilder.Entity("TrackIt.Models.ProductClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("company")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ProductTable");
                });

            modelBuilder.Entity("TrackIt.Models.SalesClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Client_id")
                        .HasColumnType("int");

                    b.Property<int?>("Clinent_id")
                        .HasColumnType("int");

                    b.Property<int>("Product_id")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("Rate")
                        .HasColumnType("int");

                    b.Property<DateTime>("Sales_Date")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Clinent_id");

                    b.HasIndex("Product_id");

                    b.ToTable("SalesTable");
                });

            modelBuilder.Entity("TrackIt.Models.StockClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Client_id")
                        .HasColumnType("int");

                    b.Property<string>("InStock")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.Property<int>("Order_id")
                        .HasColumnType("int");

                    b.Property<int>("Product_id")
                        .HasColumnType("int");

                    b.Property<int>("Sales_id")
                        .HasColumnType("int");

                    b.Property<string>("serial_number")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasAlternateKey("serial_number");

                    b.HasIndex("Client_id");

                    b.HasIndex("Order_id");

                    b.HasIndex("Product_id");

                    b.HasIndex("Sales_id");

                    b.ToTable("StockTable");
                });

            modelBuilder.Entity("TrackIt.Models.VendorClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<long>("PhoneNumber")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("VendorTable");
                });

            modelBuilder.Entity("TrackIt.Models.OrderClass", b =>
                {
                    b.HasOne("TrackIt.Models.ProductClass", "Product")
                        .WithMany()
                        .HasForeignKey("Product_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrackIt.Models.VendorClass", "vendor")
                        .WithMany()
                        .HasForeignKey("vendor_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("vendor");
                });

            modelBuilder.Entity("TrackIt.Models.SalesClass", b =>
                {
                    b.HasOne("TrackIt.Models.ClinetClass", "Clinet")
                        .WithMany()
                        .HasForeignKey("Clinent_id");

                    b.HasOne("TrackIt.Models.ProductClass", "Product")
                        .WithMany()
                        .HasForeignKey("Product_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Clinet");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("TrackIt.Models.StockClass", b =>
                {
                    b.HasOne("TrackIt.Models.ClinetClass", "Client")
                        .WithMany()
                        .HasForeignKey("Client_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrackIt.Models.OrderClass", "Order")
                        .WithMany()
                        .HasForeignKey("Order_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrackIt.Models.ProductClass", "Product")
                        .WithMany()
                        .HasForeignKey("Product_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrackIt.Models.SalesClass", "Sales")
                        .WithMany()
                        .HasForeignKey("Sales_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Order");

                    b.Navigation("Product");

                    b.Navigation("Sales");
                });
#pragma warning restore 612, 618
        }
    }
}
