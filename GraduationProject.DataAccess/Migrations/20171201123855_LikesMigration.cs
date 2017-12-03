using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GraduationProject.DataAccess.Migrations
{
    public partial class LikesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.CreateTable(
                name: "QuestionsLikes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    QuestionId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    State = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionsLikes", x => new { x.Id, x.QuestionId, x.UserId });
                    table.ForeignKey(
                        name: "FK_QuestionsLikes_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionsLikes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "QuestionsAnswers");

            //migrationBuilder.DropTable(
            //    name: "Friends");

            //migrationBuilder.DropTable(
            //    name: "QuestionsLikes");

            //migrationBuilder.DropTable(
            //    name: "SignalrConnections");

            //migrationBuilder.DropTable(
            //    name: "Students");

            //migrationBuilder.DropTable(
            //    name: "StudentCourse");

            //migrationBuilder.DropTable(
            //    name: "StudentExam");

            //migrationBuilder.DropTable(
            //    name: "StudentSkill");

            //migrationBuilder.DropTable(
            //    name: "AspNetRoleClaims");

            //migrationBuilder.DropTable(
            //    name: "AspNetUserClaims");

            //migrationBuilder.DropTable(
            //    name: "AspNetUserLogins");

            //migrationBuilder.DropTable(
            //    name: "AspNetUserRoles");

            //migrationBuilder.DropTable(
            //    name: "AspNetUserTokens");

            //migrationBuilder.DropTable(
            //    name: "Questions");

            //migrationBuilder.DropTable(
            //    name: "Course");

            //migrationBuilder.DropTable(
            //    name: "Exam");

            //migrationBuilder.DropTable(
            //    name: "Skills");

            //migrationBuilder.DropTable(
            //    name: "AspNetRoles");

            //migrationBuilder.DropTable(
            //    name: "AspNetUsers");

            //migrationBuilder.DropTable(
            //    name: "SubCategories");

            //migrationBuilder.DropTable(
            //    name: "MainCategories");
        }
    }
}
