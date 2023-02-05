using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateSurvey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ServiceId",
                table: "Surveys",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5fb7097c-335c-4d07-b4fd-000004e2d28c"),
                column: "CreatedOn",
                value: new DateTime(2023, 1, 28, 21, 37, 51, 838, DateTimeKind.Local).AddTicks(7683));

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_ServiceId",
                table: "Surveys",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_Services_ServiceId",
                table: "Surveys",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_Services_ServiceId",
                table: "Surveys");

            migrationBuilder.DropIndex(
                name: "IX_Surveys_ServiceId",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "Surveys");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5fb7097c-335c-4d07-b4fd-000004e2d28c"),
                column: "CreatedOn",
                value: new DateTime(2023, 1, 28, 14, 55, 44, 316, DateTimeKind.Local).AddTicks(7934));
        }
    }
}
