using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentsProgress.Web.Data.Migrations
{
    public partial class ChangedGroutNameType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Groups",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "Groups",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
