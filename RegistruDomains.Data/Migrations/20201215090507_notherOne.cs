using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RegistruDomains.Data.Migrations
{
    public partial class notherOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResponseInfos");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.AddColumn<string>(
                name: "AnsweredById",
                table: "Faqs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AskedById",
                table: "Faqs",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPrivate",
                table: "Faqs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Faqs_AnsweredById",
                table: "Faqs",
                column: "AnsweredById");

            migrationBuilder.CreateIndex(
                name: "IX_Faqs_AskedById",
                table: "Faqs",
                column: "AskedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Faqs_AspNetUsers_AnsweredById",
                table: "Faqs",
                column: "AnsweredById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Faqs_AspNetUsers_AskedById",
                table: "Faqs",
                column: "AskedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faqs_AspNetUsers_AnsweredById",
                table: "Faqs");

            migrationBuilder.DropForeignKey(
                name: "FK_Faqs_AspNetUsers_AskedById",
                table: "Faqs");

            migrationBuilder.DropIndex(
                name: "IX_Faqs_AnsweredById",
                table: "Faqs");

            migrationBuilder.DropIndex(
                name: "IX_Faqs_AskedById",
                table: "Faqs");

            migrationBuilder.DropColumn(
                name: "AnsweredById",
                table: "Faqs");

            migrationBuilder.DropColumn(
                name: "AskedById",
                table: "Faqs");

            migrationBuilder.DropColumn(
                name: "IsPrivate",
                table: "Faqs");

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestorId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Requests_AspNetUsers_RequestorId1",
                        column: x => x.RequestorId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResponseInfos",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestForInfoID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RespondedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ResponserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponseInfos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ResponseInfos_Requests_RequestForInfoID",
                        column: x => x.RequestForInfoID,
                        principalTable: "Requests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResponseInfos_AspNetUsers_RespondedById",
                        column: x => x.RespondedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RequestorId1",
                table: "Requests",
                column: "RequestorId1");

            migrationBuilder.CreateIndex(
                name: "IX_ResponseInfos_RequestForInfoID",
                table: "ResponseInfos",
                column: "RequestForInfoID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResponseInfos_RespondedById",
                table: "ResponseInfos",
                column: "RespondedById");
        }
    }
}
