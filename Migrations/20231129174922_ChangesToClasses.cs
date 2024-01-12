using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class ChangesToClasses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Schedule",
                table: "StudySessions");

            migrationBuilder.RenameColumn(
                name: "Subject",
                table: "StudySessions",
                newName: "SubjectId");

            migrationBuilder.AddColumn<string>(
                name: "EducationalBackground",
                table: "Tutors",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DeliveryMode",
                table: "StudySessions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Availability",
                table: "StudySessions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    SubjectCode = table.Column<string>(type: "TEXT", nullable: true),
                    Category = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudySessions_SubjectId",
                table: "StudySessions",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudySessions_Subject_SubjectId",
                table: "StudySessions",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudySessions_Subject_SubjectId",
                table: "StudySessions");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropIndex(
                name: "IX_StudySessions_SubjectId",
                table: "StudySessions");

            migrationBuilder.DropColumn(
                name: "EducationalBackground",
                table: "Tutors");

            migrationBuilder.DropColumn(
                name: "Availability",
                table: "StudySessions");

            migrationBuilder.RenameColumn(
                name: "SubjectId",
                table: "StudySessions",
                newName: "Subject");

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryMode",
                table: "StudySessions",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "Schedule",
                table: "StudySessions",
                type: "TEXT",
                nullable: true);
        }
    }
}
