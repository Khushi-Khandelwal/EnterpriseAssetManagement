﻿// <auto-generated />
using System;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(AssetManagmentContext))]
    partial class AssetManagmentContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DataAccessLayer.Entities.Asset", b =>
                {
                    b.Property<int>("AssetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AssetId"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("date");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("Price")
                        .HasColumnType("int");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Source")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Type")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("AssetId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Assets");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.AssetAssignment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AssetId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("AssignDate")
                        .HasColumnType("date");

                    b.Property<string>("Status")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("UnAssignDate")
                        .HasColumnType("date");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AssetId");

                    b.HasIndex("UserId");

                    b.ToTable("AssetAssignments");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.AssetCategory", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CategoryId");

                    b.ToTable("AssetCategories");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.UserDetail", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("PhoneNumber")
                        .HasColumnType("bigint");

                    b.HasKey("UserId");

                    b.ToTable("UserDetails");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.Asset", b =>
                {
                    b.HasOne("DataAccessLayer.Entities.AssetCategory", "AssetCategory")
                        .WithMany("Assets")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssetCategory");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.AssetAssignment", b =>
                {
                    b.HasOne("DataAccessLayer.Entities.Asset", "Asset")
                        .WithMany("AssetAssignments")
                        .HasForeignKey("AssetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Entities.UserDetail", "UserDetail")
                        .WithMany("AssetAssignments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Asset");

                    b.Navigation("UserDetail");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.Asset", b =>
                {
                    b.Navigation("AssetAssignments");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.AssetCategory", b =>
                {
                    b.Navigation("Assets");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.UserDetail", b =>
                {
                    b.Navigation("AssetAssignments");
                });
#pragma warning restore 612, 618
        }
    }
}
