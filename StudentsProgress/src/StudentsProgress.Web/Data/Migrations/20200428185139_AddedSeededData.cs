using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentsProgress.Web.Data.Migrations
{
    public partial class AddedSeededData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Faculty", "GroupId", "UserId" },
                values: new object[] { 1, "AMI", 1, "138ea16f-0bbf-487b-b0f2-c824095d2634" });

            migrationBuilder.InsertData(
                table: "Attendances",
                columns: new[] { "Id", "PassesCount", "StudentId", "SubjectId" },
                values: new object[,]
                {
                    { 1, 0, 1, 1 },
                    { 2, 0, 1, 2 },
                    { 3, 0, 1, 3 },
                    { 4, 0, 1, 4 },
                    { 5, 0, 1, 5 },
                    { 6, 0, 1, 6 }
                });

            migrationBuilder.InsertData(
                table: "UserRatings",
                columns: new[] { "Id", "SemestrPoints", "StudentId", "SubjectId", "SumPoints" },
                values: new object[,]
                {
                    { 1, 0, 1, 1, 0 },
                    { 2, 0, 1, 2, 0 },
                    { 3, 0, 1, 3, 0 },
                    { 4, 0, 1, 4, 0 },
                    { 5, 0, 1, 5, 0 },
                    { 6, 0, 1, 6, 0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "UserRatings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserRatings",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserRatings",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UserRatings",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "UserRatings",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "UserRatings",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
