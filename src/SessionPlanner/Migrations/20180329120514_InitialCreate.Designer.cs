﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using SessionPlanner.Domain;
using SessionPlanner.Repositories;

namespace SessionPlanner.Migrations
{
    [DbContext(typeof(SessionPlannerDbContext))]
    [Migration("20180329120514_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-preview1-28290")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SessionPlanner.Domain.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("Location");

                    b.Property<string>("Name");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("SessionPlanner.Domain.Session", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Abstract");

                    b.Property<string>("Description");

                    b.Property<string>("Title");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("SessionPlanner.Domain.Speaker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Biography");

                    b.Property<string>("Name");

                    b.Property<string>("Photo");

                    b.Property<int?>("SessionId");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("SessionId");

                    b.ToTable("Speakers");
                });

            modelBuilder.Entity("SessionPlanner.Domain.Submission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<int?>("EventId");

                    b.Property<int?>("SessionId");

                    b.Property<int>("Status");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("SessionId");

                    b.ToTable("Submissions");
                });

            modelBuilder.Entity("SessionPlanner.Domain.Speaker", b =>
                {
                    b.HasOne("SessionPlanner.Domain.Session")
                        .WithMany("Speakers")
                        .HasForeignKey("SessionId");
                });

            modelBuilder.Entity("SessionPlanner.Domain.Submission", b =>
                {
                    b.HasOne("SessionPlanner.Domain.Event")
                        .WithMany("Submissions")
                        .HasForeignKey("EventId");

                    b.HasOne("SessionPlanner.Domain.Session", "Session")
                        .WithMany()
                        .HasForeignKey("SessionId");
                });
#pragma warning restore 612, 618
        }
    }
}
