using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StreamNetServer.DomainEntities.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Music",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 30, nullable: false),
                    Subtitle = table.Column<string>(maxLength: 50, nullable: true),
                    Rating = table.Column<int>(nullable: false),
                    Comments = table.Column<string>(maxLength: 255, nullable: true),
                    ContributingArtists = table.Column<string>(maxLength: 255, nullable: true),
                    AlbumArtist = table.Column<string>(maxLength: 30, nullable: true),
                    Album = table.Column<string>(maxLength: 50, nullable: true),
                    Year = table.Column<DateTime>(nullable: false),
                    Genre = table.Column<string>(maxLength: 25, nullable: true),
                    Length = table.Column<TimeSpan>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    FolderPath = table.Column<string>(maxLength: 255, nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Size = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Music", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 30, nullable: false),
                    Subtitle = table.Column<string>(maxLength: 50, nullable: true),
                    Rating = table.Column<int>(nullable: false),
                    Comments = table.Column<string>(nullable: true),
                    Length = table.Column<TimeSpan>(nullable: false),
                    FrameWidth = table.Column<int>(nullable: false),
                    FrameHeight = table.Column<int>(nullable: false),
                    ContributingArtists = table.Column<string>(nullable: true),
                    Year = table.Column<DateTime>(nullable: false),
                    Genre = table.Column<string>(nullable: true),
                    Directors = table.Column<string>(nullable: true),
                    Producers = table.Column<string>(nullable: true),
                    Writers = table.Column<string>(nullable: true),
                    Publisher = table.Column<string>(nullable: true),
                    ParentalRating = table.Column<int>(nullable: false),
                    ParentalRatingReason = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    FolderPath = table.Column<string>(maxLength: 255, nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Size = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Music");

            migrationBuilder.DropTable(
                name: "Videos");
        }
    }
}
