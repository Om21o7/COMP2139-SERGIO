﻿// <auto-generated />
using System;
using COMP2139_Labs.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace COMP2139_Labs.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240323181445_AddingComments")]
    partial class AddingComments
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("COMP2139_Labs.Areas.ProjectManagement.Models.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProjectId"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProjectId");

                    b.ToTable("projects");
                });

            modelBuilder.Entity("COMP2139_Labs.Areas.ProjectManagement.Models.ProjectComment", b =>
                {
                    b.Property<int>("ProjectCommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProjectCommentId"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("DatePosted")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.HasKey("ProjectCommentId");

                    b.HasIndex("ProjectId");

                    b.ToTable("comments");
                });

            modelBuilder.Entity("COMP2139_Labs.Areas.ProjectManagement.Models.ProjectTask", b =>
                {
                    b.Property<int>("ProjectTaskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProjectTaskId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProjectTaskId");

                    b.HasIndex("ProjectId")
                        .IsUnique();

                    b.ToTable("tasks");
                });

            modelBuilder.Entity("COMP2139_Labs.Areas.ProjectManagement.Models.ProjectComment", b =>
                {
                    b.HasOne("COMP2139_Labs.Areas.ProjectManagement.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("COMP2139_Labs.Areas.ProjectManagement.Models.ProjectTask", b =>
                {
                    b.HasOne("COMP2139_Labs.Areas.ProjectManagement.Models.Project", "Project")
                        .WithOne("Tasks")
                        .HasForeignKey("COMP2139_Labs.Areas.ProjectManagement.Models.ProjectTask", "ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("COMP2139_Labs.Areas.ProjectManagement.Models.Project", b =>
                {
                    b.Navigation("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}