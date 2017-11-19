using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GraduationProject.DataAccess.Migrations
{
    public partial class UniSchoolYearsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SchholYearFrom",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SchholYearTo",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UniverstyYearFrom",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UniverstyYearTo",
                table: "Students",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SchholYearFrom",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "SchholYearTo",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "UniverstyYearFrom",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "UniverstyYearTo",
                table: "Students");
        }
    }
}
