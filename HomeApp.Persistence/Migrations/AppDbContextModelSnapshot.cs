﻿// <auto-generated />
using System;
using HomeApp.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HomeApp.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HomeApp.Library.Entities.DataPoint", b =>
                {
                    b.Property<DateTime>("Id")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RoomId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float>("Value")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("DataPoint");

                    b.HasDiscriminator<string>("Discriminator").HasValue("DataPoint");
                });

            modelBuilder.Entity("HomeApp.Library.Entities.Room", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("HomeApp.Library.Entities.Battery", b =>
                {
                    b.HasBaseType("HomeApp.Library.Entities.DataPoint");

                    b.HasIndex("RoomId");

                    b.HasDiscriminator().HasValue("Battery");
                });

            modelBuilder.Entity("HomeApp.Library.Entities.Co2", b =>
                {
                    b.HasBaseType("HomeApp.Library.Entities.DataPoint");

                    b.HasIndex("RoomId");

                    b.HasDiscriminator().HasValue("Co2");
                });

            modelBuilder.Entity("HomeApp.Library.Entities.Humidity", b =>
                {
                    b.HasBaseType("HomeApp.Library.Entities.DataPoint");

                    b.HasIndex("RoomId");

                    b.HasDiscriminator().HasValue("Humidity");
                });

            modelBuilder.Entity("HomeApp.Library.Entities.Temperature", b =>
                {
                    b.HasBaseType("HomeApp.Library.Entities.DataPoint");

                    b.HasIndex("RoomId");

                    b.HasDiscriminator().HasValue("Temperature");
                });

            modelBuilder.Entity("HomeApp.Library.Entities.Battery", b =>
                {
                    b.HasOne("HomeApp.Library.Entities.Room", "Room")
                        .WithMany("BatteryLevels")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");
                });

            modelBuilder.Entity("HomeApp.Library.Entities.Co2", b =>
                {
                    b.HasOne("HomeApp.Library.Entities.Room", "Room")
                        .WithMany("Co2Levels")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");
                });

            modelBuilder.Entity("HomeApp.Library.Entities.Humidity", b =>
                {
                    b.HasOne("HomeApp.Library.Entities.Room", "Room")
                        .WithMany("HumidityLevels")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");
                });

            modelBuilder.Entity("HomeApp.Library.Entities.Temperature", b =>
                {
                    b.HasOne("HomeApp.Library.Entities.Room", "Room")
                        .WithMany("TemperatureLevels")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");
                });

            modelBuilder.Entity("HomeApp.Library.Entities.Room", b =>
                {
                    b.Navigation("BatteryLevels");

                    b.Navigation("Co2Levels");

                    b.Navigation("HumidityLevels");

                    b.Navigation("TemperatureLevels");
                });
#pragma warning restore 612, 618
        }
    }
}
