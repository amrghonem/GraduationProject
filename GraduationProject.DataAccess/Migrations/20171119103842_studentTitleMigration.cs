using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GraduationProject.DataAccess.Migrations
{
    public partial class studentTitleMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Friends",
                table: "Friends");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Students",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Friends",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Friends",
                table: "Friends",
                columns: new[] { "Id", "FriendOneId", "FriendTwoId" });

            migrationBuilder.CreateIndex(
                name: "IX_Friends_FriendOneId",
                table: "Friends",
                column: "FriendOneId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Friends",
                table: "Friends");

            migrationBuilder.DropIndex(
                name: "IX_Friends_FriendOneId",
                table: "Friends");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Friends",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Friends",
                table: "Friends",
                columns: new[] { "FriendOneId", "FriendTwoId" });
        }
    }
}
