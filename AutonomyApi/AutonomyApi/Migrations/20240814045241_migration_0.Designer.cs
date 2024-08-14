﻿// <auto-generated />
using System;
using AutonomyApi.Database;
using AutonomyApi.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AutonomyApi.Migrations
{
    [DbContext(typeof(AutonomyDbContext))]
    [Migration("20240814045241_migration_0")]
    partial class migration_0
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "document_type", new[] { "cpf", "cnpj" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "time_unit", new[] { "minute", "hour", "half_day", "day", "week", "month" });
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AutonomyApi.Models.Entities.Budget", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Footer")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("footer");

                    b.Property<string>("Header")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("header");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)")
                        .HasColumnName("name");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_budgets");

                    b.HasIndex("UserId", "Name")
                        .IsUnique()
                        .HasDatabaseName("ix_budgets_user_id_name");

                    b.ToTable("budgets", (string)null);
                });

            modelBuilder.Entity("AutonomyApi.Models.Entities.BudgetItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BudgetId")
                        .HasColumnType("integer")
                        .HasColumnName("budget_id");

                    b.Property<int>("Duration")
                        .HasColumnType("integer")
                        .HasColumnName("duration");

                    b.Property<TimeUnit?>("DurationTimeUnit")
                        .HasColumnType("time_unit")
                        .HasColumnName("duration_time_unit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)")
                        .HasColumnName("name");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer")
                        .HasColumnName("quantity");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("numeric")
                        .HasColumnName("unit_price");

                    b.HasKey("Id")
                        .HasName("pk_budget_items");

                    b.HasIndex("BudgetId", "Name")
                        .IsUnique()
                        .HasDatabaseName("ix_budget_items_budget_id_name");

                    b.ToTable("budget_items", (string)null);
                });

            modelBuilder.Entity("AutonomyApi.Models.Entities.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)")
                        .HasColumnName("name");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("registration_date");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_clients");

                    b.HasIndex("UserId", "Name")
                        .IsUnique()
                        .HasDatabaseName("ix_clients_user_id_name");

                    b.ToTable("clients", (string)null);
                });

            modelBuilder.Entity("AutonomyApi.Models.Entities.ClientDocument", b =>
                {
                    b.Property<int>("ClientId")
                        .HasColumnType("integer")
                        .HasColumnName("client_id");

                    b.Property<DocumentType>("Type")
                        .HasColumnType("document_type")
                        .HasColumnName("type");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("value");

                    b.HasKey("ClientId", "Type")
                        .HasName("pk_client_documents");

                    b.ToTable("client_documents", (string)null);
                });

            modelBuilder.Entity("AutonomyApi.Models.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)")
                        .HasColumnName("password");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("registration_date");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("ix_users_name");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("AutonomyApi.Models.Entities.Budget", b =>
                {
                    b.HasOne("AutonomyApi.Models.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_budgets_users_user_id");
                });

            modelBuilder.Entity("AutonomyApi.Models.Entities.BudgetItem", b =>
                {
                    b.HasOne("AutonomyApi.Models.Entities.Budget", null)
                        .WithMany("Items")
                        .HasForeignKey("BudgetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_budget_items_budgets_budget_id");
                });

            modelBuilder.Entity("AutonomyApi.Models.Entities.Client", b =>
                {
                    b.HasOne("AutonomyApi.Models.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_clients_users_user_id");
                });

            modelBuilder.Entity("AutonomyApi.Models.Entities.ClientDocument", b =>
                {
                    b.HasOne("AutonomyApi.Models.Entities.Client", null)
                        .WithMany("Documents")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_client_documents_clients_client_id");
                });

            modelBuilder.Entity("AutonomyApi.Models.Entities.Budget", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("AutonomyApi.Models.Entities.Client", b =>
                {
                    b.Navigation("Documents");
                });
#pragma warning restore 612, 618
        }
    }
}
