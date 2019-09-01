using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StreamNetServer.DomainEntities.Migrations
{
    public partial class forgetid3itsdumb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "ContributingArtists",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "Directors",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "FrameHeight",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "FrameWidth",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "ParentalRating",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "ParentalRatingReason",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "Producers",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "Subtitle",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "Album",
                table: "Music");

            migrationBuilder.DropColumn(
                name: "AlbumArtist",
                table: "Music");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Music");

            migrationBuilder.DropColumn(
                name: "ContributingArtists",
                table: "Music");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Music");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "Music");

            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Music");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "Music");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Music");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Music");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Music");

            migrationBuilder.DropColumn(
                name: "Subtitle",
                table: "Music");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Music");

            migrationBuilder.RenameColumn(
                name: "Writers",
                table: "Videos",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Publisher",
                table: "Videos",
                newName: "Artists");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Videos",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "FolderPath",
                table: "Videos",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.AddColumn<byte[]>(
                name: "CoverArt",
                table: "Videos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Videos",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Music",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "FolderPath",
                table: "Music",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.AddColumn<string>(
                name: "Artists",
                table: "Music",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "CoverArt",
                table: "Music",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Music",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Music",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverArt",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "Artists",
                table: "Music");

            migrationBuilder.DropColumn(
                name: "CoverArt",
                table: "Music");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Music");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Music");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Videos",
                newName: "Writers");

            migrationBuilder.RenameColumn(
                name: "Artists",
                table: "Videos",
                newName: "Publisher");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Videos",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "FolderPath",
                table: "Videos",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "Videos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContributingArtists",
                table: "Videos",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Videos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "Videos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Directors",
                table: "Videos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FrameHeight",
                table: "Videos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FrameWidth",
                table: "Videos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Videos",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Length",
                table: "Videos",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Videos",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ParentalRating",
                table: "Videos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ParentalRatingReason",
                table: "Videos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Producers",
                table: "Videos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Videos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Size",
                table: "Videos",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Subtitle",
                table: "Videos",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Year",
                table: "Videos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Music",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "FolderPath",
                table: "Music",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Album",
                table: "Music",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AlbumArtist",
                table: "Music",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "Music",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContributingArtists",
                table: "Music",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Music",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "Music",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Music",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Length",
                table: "Music",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Music",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Music",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Size",
                table: "Music",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Subtitle",
                table: "Music",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Year",
                table: "Music",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
