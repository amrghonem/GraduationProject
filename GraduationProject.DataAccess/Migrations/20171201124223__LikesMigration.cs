using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GraduationProject.DataAccess.Migrations
{
    public partial class _LikesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionsLikes_AspNetUsers_UserId",
                table: "QuestionsLikes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionsLikes",
                table: "QuestionsLikes");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "QuestionsLikes",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionsLikes",
                table: "QuestionsLikes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionsLikes_AspNetUsers_UserId",
                table: "QuestionsLikes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionsLikes_AspNetUsers_UserId",
                table: "QuestionsLikes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionsLikes",
                table: "QuestionsLikes");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "QuestionsLikes",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionsLikes",
                table: "QuestionsLikes",
                columns: new[] { "Id", "QuestionId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionsLikes_AspNetUsers_UserId",
                table: "QuestionsLikes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
