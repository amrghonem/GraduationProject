using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GraduationProject.DataAccess.Migrations
{
    public partial class questionsAndAnswersDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Questions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "QuestionsAnswers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "QuestionsAnswers");
        }
    }
}
