using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeApp.Persistence.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataPoint",
                columns: table => new
                {
                    Id = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RoomId = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false),
                    Discriminator = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataPoint", x => new {x.Id, x.RoomId });
                    table.ForeignKey(
                        name: "FK_DataPoint_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataPoint_RoomId",
                table: "DataPoint",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_DataPoint_Discriminator",
                table: "DataPoint",
                column: "Discriminator");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataPoint");

            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
