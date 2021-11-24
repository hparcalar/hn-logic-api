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
    [Migration("20211011123345_PrintersAndItems2")]
    partial class PrintersAndItems2
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

                    b.Property<bool>("CanRepeat")
                        .HasColumnType("boolean");

                    b.Property<string>("ConnectionResetMessage")
                        .HasColumnType("text");

                    b.Property<int>("ConnectionResetMessageDelay")
                        .HasColumnType("integer");

                    b.Property<int>("DelayAfter")
                        .HasColumnType("integer");

                    b.Property<int>("DelayBefore")
                        .HasColumnType("integer");

                    b.Property<int>("HnAppId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeviceConnected")
                        .HasColumnType("boolean");

                    b.Property<string>("LiveCondition")
                        .HasColumnType("text");

                    b.Property<bool>("MustBeStopped")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("ProcStatus")
                        .HasColumnType("integer");

                    b.HasKey("HnProcessId");

                    b.HasIndex("HnAppId");

                    b.ToTable("HnProcesses");
                });

            modelBuilder.Entity("HekaNodes.DataAccess.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("ItemCode")
                        .HasColumnType("text");

                    b.Property<string>("ItemName")
                        .HasColumnType("text");

                    b.HasKey("ItemId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("HekaNodes.DataAccess.PrintQueue", b =>
                {
                    b.Property<int>("PrintQueueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool?>("IsPrinted")
                        .HasColumnType("boolean");

                    b.Property<string>("ItemCode")
                        .HasColumnType("text");

                    b.Property<int?>("ItemId")
                        .HasColumnType("integer");

                    b.HasKey("PrintQueueId");

                    b.ToTable("PrintQueues");
                });

            modelBuilder.Entity("HekaNodes.DataAccess.ProcessResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("DurationInSeconds")
                        .HasColumnType("integer");

                    b.Property<bool?>("IsOk")
                        .HasColumnType("boolean");

                    b.Property<int?>("ItemId")
                        .HasColumnType("integer");

                    b.Property<float?>("NumResult")
                        .HasColumnType("real");

                    b.Property<int>("ProcessStepId")
                        .HasColumnType("integer");

                    b.Property<string>("StrResult")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

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

                    b.Property<int>("ConditionRealizeTimeout")
                        .HasColumnType("integer");

                    b.Property<int>("ConditionSatisfiedTime")
                        .HasColumnType("integer");

                    b.Property<int?>("DelayAfter")
                        .HasColumnType("integer");

                    b.Property<int?>("DelayBefore")
                        .HasColumnType("integer");

                    b.Property<string>("ElseAction")
                        .HasColumnType("text");

                    b.Property<string>("Explanation")
                        .HasColumnType("text");

                    b.Property<int>("HnProcessId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsTestResult")
                        .HasColumnType("boolean");

                    b.Property<int>("OrderNo")
                        .HasColumnType("integer");

                    b.Property<string>("ParallelAction")
                        .HasColumnType("text");

                    b.Property<string>("ResultAction")
                        .HasColumnType("text");

                    b.Property<bool>("WaitUntilConditionRealized")
                        .HasColumnType("boolean");

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
                    b.HasOne("HekaNodes.DataAccess.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId");

                    b.HasOne("HekaNodes.DataAccess.ProcessStep", "ProcessStep")
                        .WithMany()
                        .HasForeignKey("ProcessStepId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

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