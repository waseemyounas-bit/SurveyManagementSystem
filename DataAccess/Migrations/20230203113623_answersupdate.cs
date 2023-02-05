using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class answersupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Answers",
                newName: "Value");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "Answers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SurveyId",
                table: "Answers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5fb7097c-335c-4d07-b4fd-000004e2d28c"),
                column: "CreatedOn",
                value: new DateTime(2023, 2, 3, 14, 36, 23, 597, DateTimeKind.Local).AddTicks(2776));

            migrationBuilder.CreateIndex(
                name: "IX_Answers_CustomerId",
                table: "Answers",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_SurveyId",
                table: "Answers",
                column: "SurveyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Customers_CustomerId",
                table: "Answers",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Surveys_SurveyId",
                table: "Answers",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Customers_CustomerId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Surveys_SurveyId",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_CustomerId",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_SurveyId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "SurveyId",
                table: "Answers");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Answers",
                newName: "Text");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5fb7097c-335c-4d07-b4fd-000004e2d28c"),
                column: "CreatedOn",
                value: new DateTime(2023, 2, 1, 14, 21, 46, 270, DateTimeKind.Local).AddTicks(1153));
        }
    }
}
