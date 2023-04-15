﻿// <auto-generated />
using Broker.Accounts.SQLRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Broker.Accounts.SQLRepository.Migrations
{
    [DbContext(typeof(AccountContext))]
    [Migration("20230414233035_InitAccountConection")]
    partial class InitAccountConection
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Broker.Accounts.SQLRepository.Schemas.AccountSchema", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("User_Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(12, 2)")
                        .HasColumnName("Balance");

                    b.HasKey("UserId");

                    b.ToTable("ACCOUNT");
                });
#pragma warning restore 612, 618
        }
    }
}
