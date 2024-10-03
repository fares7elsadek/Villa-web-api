using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Villa.Migrations
{
    /// <inheritdoc />
    public partial class addusertotable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VillaNumber_villas_VillaId",
                table: "VillaNumber");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VillaNumber",
                table: "VillaNumber");

            migrationBuilder.RenameTable(
                name: "VillaNumber",
                newName: "villaNumber");

            migrationBuilder.RenameIndex(
                name: "IX_VillaNumber_VillaId",
                table: "villaNumber",
                newName: "IX_villaNumber_VillaId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "villaNumber",
                type: "date",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldDefaultValueSql: "getdate");

            migrationBuilder.AddPrimaryKey(
                name: "PK_villaNumber",
                table: "villaNumber",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_villaNumber_villas_VillaId",
                table: "villaNumber",
                column: "VillaId",
                principalTable: "villas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_villaNumber_villas_VillaId",
                table: "villaNumber");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_villaNumber",
                table: "villaNumber");

            migrationBuilder.RenameTable(
                name: "villaNumber",
                newName: "VillaNumber");

            migrationBuilder.RenameIndex(
                name: "IX_villaNumber_VillaId",
                table: "VillaNumber",
                newName: "IX_VillaNumber_VillaId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "VillaNumber",
                type: "date",
                nullable: false,
                defaultValueSql: "getdate",
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VillaNumber",
                table: "VillaNumber",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VillaNumber_villas_VillaId",
                table: "VillaNumber",
                column: "VillaId",
                principalTable: "villas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
