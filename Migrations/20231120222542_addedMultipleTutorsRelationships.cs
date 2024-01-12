using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class addedMultipleTutorsRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudySessions_Tutors_TutorId",
                table: "StudySessions");

            migrationBuilder.DropIndex(
                name: "IX_StudySessions_TutorId",
                table: "StudySessions");

            migrationBuilder.DropColumn(
                name: "TutorId",
                table: "StudySessions");

            migrationBuilder.CreateTable(
                name: "StudySessionTutor",
                columns: table => new
                {
                    StudySessionsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TutorsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudySessionTutor", x => new { x.StudySessionsId, x.TutorsId });
                    table.ForeignKey(
                        name: "FK_StudySessionTutor_StudySessions_StudySessionsId",
                        column: x => x.StudySessionsId,
                        principalTable: "StudySessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudySessionTutor_Tutors_TutorsId",
                        column: x => x.TutorsId,
                        principalTable: "Tutors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudySessionTutor_TutorsId",
                table: "StudySessionTutor",
                column: "TutorsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudySessionTutor");

            migrationBuilder.AddColumn<Guid>(
                name: "TutorId",
                table: "StudySessions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudySessions_TutorId",
                table: "StudySessions",
                column: "TutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudySessions_Tutors_TutorId",
                table: "StudySessions",
                column: "TutorId",
                principalTable: "Tutors",
                principalColumn: "Id");
        }
    }
}
