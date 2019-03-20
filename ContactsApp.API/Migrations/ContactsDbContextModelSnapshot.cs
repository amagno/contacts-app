﻿// <auto-generated />
using System;
using ContactsAPI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ContactsAPI.Migrations
{
    [DbContext(typeof(ContactsDbContext))]
    partial class ContactsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ContactsAPI.Models.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("Gender")
                        .IsRequired();

                    b.Property<bool>("IsFavorite");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.ToTable("contacts");
                });

            modelBuilder.Entity("ContactsAPI.Models.ContactInfo", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Address");

                    b.Property<string>("Avatar");

                    b.Property<string>("Comments");

                    b.Property<string>("Company")
                        .IsRequired();

                    b.Property<string>("Phone");

                    b.HasKey("Id");

                    b.ToTable("contacts_info");
                });

            modelBuilder.Entity("ContactsAPI.Models.ContactInfo", b =>
                {
                    b.HasOne("ContactsAPI.Models.Contact")
                        .WithOne("Info")
                        .HasForeignKey("ContactsAPI.Models.ContactInfo", "Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
