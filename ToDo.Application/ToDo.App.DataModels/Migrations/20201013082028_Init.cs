using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDo.App.DataModels.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Username = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    IsCompleted = table.Column<bool>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubTask",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    TaskDescription = table.Column<string>(maxLength: 250, nullable: false),
                    IsCompleted = table.Column<bool>(nullable: false),
                    TodoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubTask_Tasks_TodoId",
                        column: x => x.TodoId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "Username" },
                values: new object[] { 1, "Dimitar", "Risteski", "{??o?j???ds?[??", "dimris" });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "DateCreated", "IsCompleted", "Title", "UserId" },
                values: new object[] { 1, new DateTime(2020, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Calisthenics Workout", 1 });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "DateCreated", "IsCompleted", "Title", "UserId" },
                values: new object[] { 2, new DateTime(2020, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Working", 1 });

            migrationBuilder.InsertData(
                table: "SubTask",
                columns: new[] { "Id", "IsCompleted", "TaskDescription", "Title", "TodoId" },
                values: new object[,]
                {
                    { 1, true, "5x5 pull-aps", "pullups", 1 },
                    { 2, true, "5x10 dips", "dips", 1 },
                    { 3, false, "Making portfolio analysis", "Quarterly Reports", 2 },
                    { 4, false, "Studing WebApi at SEDC", "Web Api", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubTask_TodoId",
                table: "SubTask",
                column: "TodoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_UserId",
                table: "Tasks",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubTask");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
