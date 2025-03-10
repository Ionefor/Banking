﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Banking.Accounts.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Banking.Accounts.Infrastructure.Migrations
{
    [DbContext(typeof(AccountsWriteDbContext))]
    [Migration("20250310192207_Initial_W")]
    partial class Initial_W
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Accounts")
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Banking.Accounts.Domain.CorporateAccount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.ComplexProperty<Dictionary<string, object>>("Address", "Banking.Accounts.Domain.CorporateAccount.Address#Address", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("city");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("country");

                            b1.Property<string>("HouseNumber")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("house_number");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("street");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("CompanyName", "Banking.Accounts.Domain.CorporateAccount.CompanyName#Name", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("company_name");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("ContactEmail", "Banking.Accounts.Domain.CorporateAccount.ContactEmail#Email", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("contact_email");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("ContactPhone", "Banking.Accounts.Domain.CorporateAccount.ContactPhone#PhoneNumber", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("contact_phone");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("TaxId", "Banking.Accounts.Domain.CorporateAccount.TaxId#TaxId", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("tax_id");
                        });

                    b.HasKey("Id")
                        .HasName("pk_corporate_accounts");

                    b.ToTable("corporate_accounts", "Accounts");
                });

            modelBuilder.Entity("Banking.Accounts.Domain.IndividualAccount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.ComplexProperty<Dictionary<string, object>>("Address", "Banking.Accounts.Domain.IndividualAccount.Address#Address", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("city");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("country");

                            b1.Property<string>("HouseNumber")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("house_number");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("street");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("DateOfBirth", "Banking.Accounts.Domain.IndividualAccount.DateOfBirth#DateOfBirth", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<DateOnly>("Value")
                                .HasColumnType("date")
                                .HasColumnName("date_of_birth");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Email", "Banking.Accounts.Domain.IndividualAccount.Email#Email", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("email");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("FullName", "Banking.Accounts.Domain.IndividualAccount.FullName#FullName", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("character varying(20)")
                                .HasColumnName("firstName");

                            b1.Property<string>("LastName")
                                .HasMaxLength(20)
                                .HasColumnType("character varying(20)")
                                .HasColumnName("lastName");

                            b1.Property<string>("MiddleName")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("character varying(20)")
                                .HasColumnName("middleName");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("PhoneNumber", "Banking.Accounts.Domain.IndividualAccount.PhoneNumber#PhoneNumber", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("phone_number");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Photo", "Banking.Accounts.Domain.IndividualAccount.Photo#FilePath", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("photo");
                        });

                    b.HasKey("Id")
                        .HasName("pk_individual_accounts");

                    b.ToTable("individual_accounts", "Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}
