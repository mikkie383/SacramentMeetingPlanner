﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SacramentMeetingPlanner.Data;

namespace SacramentMeetingPlanner.Migrations
{
    [DbContext(typeof(SacramentContext))]
    partial class SacramentContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SacramentMeetingPlanner.Models.Calling", b =>
                {
                    b.Property<int>("CallingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CallingName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CallingId");

                    b.ToTable("Calling");
                });

            modelBuilder.Entity("SacramentMeetingPlanner.Models.Calling_Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CallingId")
                        .HasColumnType("int");

                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CallingId");

                    b.HasIndex("MemberId");

                    b.ToTable("CallingMember");
                });

            modelBuilder.Entity("SacramentMeetingPlanner.Models.Member", b =>
                {
                    b.Property<int>("MemberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Birth")
                        .HasColumnType("datetime2");

                    b.Property<string>("MemberFull")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("MemberFull");

                    b.HasKey("MemberId");

                    b.ToTable("Member");
                });

            modelBuilder.Entity("SacramentMeetingPlanner.Models.Planner", b =>
                {
                    b.Property<int>("PlannerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Benediction")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClosingHymn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Conducting")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Invocation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OpeningHymn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PlannedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SacramentHymn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Topic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Topic1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Topic2")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PlannerId");

                    b.ToTable("Planner");
                });

            modelBuilder.Entity("SacramentMeetingPlanner.Models.Planner_Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.Property<int>("PlannerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.HasIndex("PlannerId");

                    b.ToTable("Planner_Member");
                });

            modelBuilder.Entity("SacramentMeetingPlanner.Models.Calling_Member", b =>
                {
                    b.HasOne("SacramentMeetingPlanner.Models.Calling", "Calling")
                        .WithMany("Calling_Members")
                        .HasForeignKey("CallingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SacramentMeetingPlanner.Models.Member", "Member")
                        .WithMany("Calling_Members")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Calling");

                    b.Navigation("Member");
                });

            modelBuilder.Entity("SacramentMeetingPlanner.Models.Planner_Member", b =>
                {
                    b.HasOne("SacramentMeetingPlanner.Models.Member", "Member")
                        .WithMany("Planner_Members")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SacramentMeetingPlanner.Models.Planner", "Planner")
                        .WithMany("Planner_Members")
                        .HasForeignKey("PlannerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Member");

                    b.Navigation("Planner");
                });

            modelBuilder.Entity("SacramentMeetingPlanner.Models.Calling", b =>
                {
                    b.Navigation("Calling_Members");
                });

            modelBuilder.Entity("SacramentMeetingPlanner.Models.Member", b =>
                {
                    b.Navigation("Calling_Members");

                    b.Navigation("Planner_Members");
                });

            modelBuilder.Entity("SacramentMeetingPlanner.Models.Planner", b =>
                {
                    b.Navigation("Planner_Members");
                });
#pragma warning restore 612, 618
        }
    }
}
