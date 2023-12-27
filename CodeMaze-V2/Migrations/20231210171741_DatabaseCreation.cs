using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeMaze_V2.Migrations
{
    public partial class DatabaseCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companys_V2",
                columns: table => new
                {
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companys_V2", x => x.CompanyId);
                });

            migrationBuilder.CreateTable(
                name: "Employees_V2",
                columns: table => new
                {
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees_V2", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employees_V2_Companys_V2_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companys_V2",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Companys_V2",
                columns: new[] { "CompanyId", "Address", "Country", "Name" },
                values: new object[] { new Guid("3d490a70-94ce-4d15-9494-5248280c2ce4"), "312 Forest Avenue, BF 923", "USA", "Admin_Solution Ltd" });

            migrationBuilder.InsertData(
                table: "Companys_V2",
                columns: new[] { "CompanyId", "Address", "Country", "Name" },
                values: new object[] { new Guid("c9d4c053-49b6-4f3f-9a8c-8f6d5a24f19a"), "583 Wall Dr. Gwynn Oak, MD 21207", "USA", "IT_Solutions Ltd" });

            migrationBuilder.InsertData(
                table: "Employees_V2",
                columns: new[] { "EmployeeId", "Age", "CompanyId", "Name", "Position" },
                values: new object[] { new Guid("2dc4c053-49b6-4f3f-9a8c-8f6d5a24f19a"), 30, new Guid("c9d4c053-49b6-4f3f-9a8c-8f6d5a24f19a"), "Jana McLeaf", "Software developer" });

            migrationBuilder.InsertData(
                table: "Employees_V2",
                columns: new[] { "EmployeeId", "Age", "CompanyId", "Name", "Position" },
                values: new object[] { new Guid("3ac4c053-49b6-4f3f-9a8c-8f6d5a24f19a"), 35, new Guid("3d490a70-94ce-4d15-9494-5248280c2ce4"), "Kane Miller", "Administrator" });

            migrationBuilder.InsertData(
                table: "Employees_V2",
                columns: new[] { "EmployeeId", "Age", "CompanyId", "Name", "Position" },
                values: new object[] { new Guid("75c4c053-49b6-4f3f-9a8c-8f6d5a24f19a"), 26, new Guid("c9d4c053-49b6-4f3f-9a8c-8f6d5a24f19a"), "Sam Raiden", "Software developer" });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_V2_CompanyId",
                table: "Employees_V2",
                column: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees_V2");

            migrationBuilder.DropTable(
                name: "Companys_V2");
        }
    }
}
