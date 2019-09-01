﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using StreamNetServer.DomainEntities.Data;
using System;

namespace StreamNetServer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190812004920_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("StreamNetServer.Models.AudioMetaData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Album")
                        .HasMaxLength(50);

                    b.Property<string>("AlbumArtist")
                        .HasMaxLength(30);

                    b.Property<string>("Comments")
                        .HasMaxLength(255);

                    b.Property<string>("ContributingArtists")
                        .HasMaxLength(255);

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("FolderPath")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("Genre")
                        .HasMaxLength(25);

                    b.Property<TimeSpan>("Length");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<int>("Rating");

                    b.Property<double>("Size");

                    b.Property<string>("Subtitle")
                        .HasMaxLength(50);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<DateTime>("Year");

                    b.HasKey("Id");

                    b.ToTable("Music");
                });

            modelBuilder.Entity("StreamNetServer.Models.VideoMetaData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comments");

                    b.Property<string>("ContributingArtists");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("Directors");

                    b.Property<string>("FolderPath")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<int>("FrameHeight");

                    b.Property<int>("FrameWidth");

                    b.Property<string>("Genre");

                    b.Property<TimeSpan>("Length");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<int>("ParentalRating");

                    b.Property<string>("ParentalRatingReason");

                    b.Property<string>("Producers");

                    b.Property<string>("Publisher");

                    b.Property<int>("Rating");

                    b.Property<double>("Size");

                    b.Property<string>("Subtitle")
                        .HasMaxLength(50);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Writers");

                    b.Property<DateTime>("Year");

                    b.HasKey("Id");

                    b.ToTable("Videos");
                });
#pragma warning restore 612, 618
        }
    }
}
