using Microsoft.EntityFrameworkCore.Migrations;

namespace Workshop.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowInfos_Books_BookId",
                table: "BorrowInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_BorrowInfos_Persons_PersonId",
                table: "BorrowInfos");

            migrationBuilder.DropIndex(
                name: "IX_BorrowInfos_BookId",
                table: "BorrowInfos");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "BorrowInfos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "BorrowInfos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowInfos_Persons_PersonId",
                table: "BorrowInfos",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowInfos_Persons_PersonId",
                table: "BorrowInfos");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "BorrowInfos",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "BorrowInfos",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowInfos_BookId",
                table: "BorrowInfos",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowInfos_Books_BookId",
                table: "BorrowInfos",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowInfos_Persons_PersonId",
                table: "BorrowInfos",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
