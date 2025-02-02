﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Raefftec.CatchEmAll.DAL;
using System;

namespace Raefftec.CatchEmAll.DAL.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20180116154941_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Raefftec.CatchEmAll.DAL.Category", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int?>("Number");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId", "Number")
                        .IsUnique()
                        .HasFilter("[Number] IS NOT NULL");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Raefftec.CatchEmAll.DAL.Query", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AutoFilterDeletedDuplicates");

                    b.Property<long>("CategoryId");

                    b.Property<decimal?>("DesiredPrice");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsLocked");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("NotificationMode");

                    b.Property<int>("Priority");

                    b.Property<DateTimeOffset>("Updated");

                    b.Property<bool>("UseDescription");

                    b.Property<string>("WithAllTheseWords");

                    b.Property<string>("WithExactlyTheseWords");

                    b.Property<string>("WithNoneOfTheseWords");

                    b.Property<string>("WithOneOfTheseWords");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Queries");
                });

            modelBuilder.Entity("Raefftec.CatchEmAll.DAL.Result", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal?>("BidPrice");

                    b.Property<string>("Description");

                    b.Property<DateTimeOffset?>("Ends");

                    b.Property<long>("ExternalId");

                    b.Property<decimal?>("FinalPrice");

                    b.Property<bool>("IsClosed");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsFavorite");

                    b.Property<bool>("IsHidden");

                    b.Property<bool>("IsLocked");

                    b.Property<bool>("IsNew");

                    b.Property<bool>("IsNotified");

                    b.Property<bool>("IsSold");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<decimal?>("PurchasePrice");

                    b.Property<long>("QueryId");

                    b.Property<DateTimeOffset>("Updated");

                    b.HasKey("Id");

                    b.HasIndex("QueryId");

                    b.ToTable("Results");
                });

            modelBuilder.Entity("Raefftec.CatchEmAll.DAL.Subscription", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("HighPriorityQuota");

                    b.Property<bool>("IsDefault");

                    b.Property<int>("LowPriotityQuota");

                    b.Property<string>("Name");

                    b.Property<int>("NormalPriotiryQuota");

                    b.HasKey("Id");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("Raefftec.CatchEmAll.DAL.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AlternativeEmail");

                    b.Property<bool>("AutoFilterDeletedDuplicatesDefault");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<bool>("EnableEmailNotification");

                    b.Property<bool>("EnableIftttNotification");

                    b.Property<string>("IftttMakerEventName");

                    b.Property<string>("IftttMakerKey");

                    b.Property<bool>("IsAdmin");

                    b.Property<bool>("IsEnabled");

                    b.Property<string>("PasswordHash")
                        .IsRequired();

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Raefftec.CatchEmAll.DAL.Category", b =>
                {
                    b.HasOne("Raefftec.CatchEmAll.DAL.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Raefftec.CatchEmAll.DAL.Query", b =>
                {
                    b.HasOne("Raefftec.CatchEmAll.DAL.Category", "Category")
                        .WithMany("Queries")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Raefftec.CatchEmAll.DAL.Result", b =>
                {
                    b.HasOne("Raefftec.CatchEmAll.DAL.Query", "Query")
                        .WithMany("Results")
                        .HasForeignKey("QueryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
