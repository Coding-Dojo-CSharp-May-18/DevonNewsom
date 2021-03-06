﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MoreEFunizes.Models;
using MySql.Data.EntityFrameworkCore.Storage.Internal;
using System;

namespace MoreEFunizes.Migrations
{
    [DbContext(typeof(QuoteContext))]
    partial class QuoteContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026");

            modelBuilder.Entity("MoreEFunizes.Models.Quote", b =>
                {
                    b.Property<int>("QuoteId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Byline");

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("UserId");

                    b.HasKey("QuoteId");

                    b.HasIndex("UserId");

                    b.ToTable("Quotes");
                });

            modelBuilder.Entity("MoreEFunizes.Models.QuoteUser", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("Username");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MoreEFunizes.Models.Quote", b =>
                {
                    b.HasOne("MoreEFunizes.Models.QuoteUser", "Creator")
                        .WithMany("CreatedQuotes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
