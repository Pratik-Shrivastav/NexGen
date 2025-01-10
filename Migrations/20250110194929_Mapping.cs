using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexGen.Migrations
{
    /// <inheritdoc />
    public partial class Mapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayTransactionTable_PlayStationTable_PlayStationId",
                table: "PlayTransactionTable");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayTransactionTable_UserTable_UserId",
                table: "PlayTransactionTable");

            migrationBuilder.DropIndex(
                name: "IX_PlayTransactionTable_PlayStationId",
                table: "PlayTransactionTable");

            migrationBuilder.DropColumn(
                name: "PlayStationId",
                table: "PlayTransactionTable");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "PlayTransactionTable",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentMethod",
                table: "PlayTransactionTable",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PlayStation",
                table: "PlayTransactionTable",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayTransactionTable_UserTable_UserId",
                table: "PlayTransactionTable",
                column: "UserId",
                principalTable: "UserTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayTransactionTable_UserTable_UserId",
                table: "PlayTransactionTable");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "PlayTransactionTable");

            migrationBuilder.DropColumn(
                name: "PlayStation",
                table: "PlayTransactionTable");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "PlayTransactionTable",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PlayStationId",
                table: "PlayTransactionTable",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayTransactionTable_PlayStationId",
                table: "PlayTransactionTable",
                column: "PlayStationId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayTransactionTable_PlayStationTable_PlayStationId",
                table: "PlayTransactionTable",
                column: "PlayStationId",
                principalTable: "PlayStationTable",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayTransactionTable_UserTable_UserId",
                table: "PlayTransactionTable",
                column: "UserId",
                principalTable: "UserTable",
                principalColumn: "Id");
        }
    }
}
