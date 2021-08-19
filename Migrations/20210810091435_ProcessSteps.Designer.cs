﻿// <auto-generated />
using System;
using HekaNodes.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace hn_logic_api.Migrations
{
    [DbContext(typeof(NodesContext))]
    [Migration("20210810091435_ProcessSteps")]
    partial class ProcessSteps
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            modelBuilder.Entity("HekaNodes.DataAccess.HnApp", b =>
                {
                    b.Property<int>("HnAppId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("AppName")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.HasKey("HnAppId");

                    b.ToTable("HnApps");
                });

            modelBuilder.Entity("HekaNodes.DataAccess.HnProcess", b =>
                {
                    b.Property<int>("HnProcessId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<int>("HnAppId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("HnProcessId");

                    b.HasIndex("HnAppId");

                    b.ToTable("HnProcesses");
                });

            modelBuilder.Entity("HekaNodes.DataAccess.ProcessResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<float?>("NumResult")
                        .HasColumnType("real");

                    b.Property<int>("ProcessStepId")
                        .HasColumnType("integer");

                    b.Property<string>("StrResult")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ProcessStepId");

                    b.ToTable("ProcessResults");
                });

            modelBuilder.Entity("HekaNodes.DataAccess.ProcessStep", b =>
                {
                    b.Property<int>("ProcessStepId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("Comparison")
                        .HasColumnType("text");

                    b.Property<int?>("DelayAfter")
                        .HasColumnType("integer");

                    b.Property<int?>("DelayBefore")
                        .HasColumnType("integer");

                    b.Property<string>("Explanation")
                        .HasColumnType("text");

                    b.Property<int>("HnProcessId")
                        .HasColumnType("integer");

                    b.Property<string>("ResultAction")
                        .HasColumnType("text");

                    b.HasKey("ProcessStepId");

                    b.HasIndex("HnProcessId");

                    b.ToTable("ProcessSteps");
                });

            modelBuilder.Entity("HekaNodes.DataAccess.HnProcess", b =>
                {
                    b.HasOne("HekaNodes.DataAccess.HnApp", "HnApp")
                        .WithMany()
                        .HasForeignKey("HnAppId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HnApp");
                });

            modelBuilder.Entity("HekaNodes.DataAccess.ProcessResult", b =>
                {
                    b.HasOne("HekaNodes.DataAccess.ProcessStep", "ProcessStep")
                        .WithMany()
                        .HasForeignKey("ProcessStepId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProcessStep");
                });

            modelBuilder.Entity("HekaNodes.DataAccess.ProcessStep", b =>
                {
                    b.HasOne("HekaNodes.DataAccess.HnProcess", "HnProcess")
                        .WithMany()
                        .HasForeignKey("HnProcessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HnProcess");
                });
#pragma warning restore 612, 618
        }
    }
}
