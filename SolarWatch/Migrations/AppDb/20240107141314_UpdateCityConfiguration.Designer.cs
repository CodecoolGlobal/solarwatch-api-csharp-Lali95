﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SolarWatch.Data;

#nullable disable

namespace SolarWatch.Migrations.AppDb
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240107141314_UpdateCityConfiguration")]
    partial class UpdateCityConfiguration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SolarWatch.Data.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Country = "UK",
                            Name = "London",
                            State = "SomeState"
                        },
                        new
                        {
                            Id = 2,
                            Country = "Hungary",
                            Name = "Budapest",
                            State = "SomeState"
                        },
                        new
                        {
                            Id = 3,
                            Country = "France",
                            Name = "Paris",
                            State = "SomeState"
                        });
                });

            modelBuilder.Entity("SolarWatch.Data.SunriseSunset.SunriseSunset", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Sunrise")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Sunset")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("SunriseSunsets");
                });

            modelBuilder.Entity("SolarWatch.Data.SunriseSunset.SunriseSunset", b =>
                {
                    b.HasOne("SolarWatch.Data.City", "City")
                        .WithMany("SunriseSunsets")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("SolarWatch.Data.City", b =>
                {
                    b.Navigation("SunriseSunsets");
                });
#pragma warning restore 612, 618
        }
    }
}
