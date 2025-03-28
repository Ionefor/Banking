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
    [DbContext(typeof(AccountsReadDbContext))]
    [Migration("20250310192220_Initial_R")]
    partial class Initial_R
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

            modelBuilder.Entity("Banking.Accounts.Contracts.Dto.Models.CorporateAccountDto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("company_name");

                    b.Property<string>("ContactEmail")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("contact_email");

                    b.Property<string>("ContactPhone")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("contact_phone");

                    b.Property<string>("TaxId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("tax_id");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.ComplexProperty<Dictionary<string, object>>("Address", "Banking.Accounts.Contracts.Dto.Models.CorporateAccountDto.Address#AddressDto", b1 =>
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

                    b.HasKey("Id")
                        .HasName("pk_corporate_accounts");

                    b.ToTable("corporate_accounts", "Accounts");
                });

            modelBuilder.Entity("Banking.Accounts.Contracts.Dto.Models.IndividualAccountDto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date")
                        .HasColumnName("date_of_birth");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("phone_number");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("photo");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.ComplexProperty<Dictionary<string, object>>("Address", "Banking.Accounts.Contracts.Dto.Models.IndividualAccountDto.Address#AddressDto", b1 =>
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

                    b.ComplexProperty<Dictionary<string, object>>("FullName", "Banking.Accounts.Contracts.Dto.Models.IndividualAccountDto.FullName#FullNameDto", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("firstName");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("lastName");

                            b1.Property<string>("MiddleName")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("middleName");
                        });

                    b.HasKey("Id")
                        .HasName("pk_individual_accounts");

                    b.ToTable("individual_accounts", "Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}
