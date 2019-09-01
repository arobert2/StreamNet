using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StreamNetServer.DomainEntities.Migrations
{
    public partial class addedprofilepic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FolderPath",
                table: "Videos",
                newName: "MediaType");

            migrationBuilder.RenameColumn(
                name: "FolderPath",
                table: "Music",
                newName: "MediaType");

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Videos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Music",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "UserProfilePicture",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Music");

            migrationBuilder.DropColumn(
                name: "UserProfilePicture",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "MediaType",
                table: "Videos",
                newName: "FolderPath");

            migrationBuilder.RenameColumn(
                name: "MediaType",
                table: "Music",
                newName: "FolderPath");
        }
    }
}
