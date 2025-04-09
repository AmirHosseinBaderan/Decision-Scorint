﻿// <auto-generated />
using System;
using App.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace App.Migrations
{
    [DbContext(typeof(DecisionTreeDbContext))]
    [Migration("20250409231947_UpdateMath")]
    partial class UpdateMath
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("App.Domain.DecisionNode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Condition")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FalseBranchId")
                        .HasColumnType("int");

                    b.Property<string>("Formula")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TreeId")
                        .HasColumnType("int");

                    b.Property<int?>("TrueBranchId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FalseBranchId");

                    b.HasIndex("TrueBranchId");

                    b.ToTable("DecisionNodes");
                });

            modelBuilder.Entity("App.Domain.DecisionNode", b =>
                {
                    b.HasOne("App.Domain.DecisionNode", "FalseBranch")
                        .WithMany()
                        .HasForeignKey("FalseBranchId");

                    b.HasOne("App.Domain.DecisionNode", "TrueBranch")
                        .WithMany()
                        .HasForeignKey("TrueBranchId");

                    b.Navigation("FalseBranch");

                    b.Navigation("TrueBranch");
                });
#pragma warning restore 612, 618
        }
    }
}
