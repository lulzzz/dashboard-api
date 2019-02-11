﻿// <auto-generated />
using HdProduction.Dashboard.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HdProduction.Dashboard.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20190211091824_UserFullName")]
    partial class UserFullName
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("HdProduction.Dashboard.Domain.Entities.Projects.Project", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<string>("SelfHostSettings")
                        .HasColumnType("Json");

                    b.HasKey("Id");

                    b.ToTable("project");
                });

            modelBuilder.Entity("HdProduction.Dashboard.Domain.Entities.Relational.UserProjectRights", b =>
                {
                    b.Property<long>("UserId");

                    b.Property<long>("ProjectId");

                    b.Property<int>("Right");

                    b.HasKey("UserId", "ProjectId");

                    b.HasIndex("ProjectId");

                    b.ToTable("user_project");
                });

            modelBuilder.Entity("HdProduction.Dashboard.Domain.Entities.Users.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(254);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<string>("PwdSalt")
                        .IsFixedLength(true)
                        .HasMaxLength(32);

                    b.Property<string>("RefreshToken")
                        .IsFixedLength(true)
                        .HasMaxLength(44);

                    b.Property<string>("SaltedPwd")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("user");
                });

            modelBuilder.Entity("HdProduction.Dashboard.Domain.Entities.Relational.UserProjectRights", b =>
                {
                    b.HasOne("HdProduction.Dashboard.Domain.Entities.Projects.Project", "Project")
                        .WithMany("UserRights")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HdProduction.Dashboard.Domain.Entities.Users.User", "User")
                        .WithMany("ProjectRights")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
