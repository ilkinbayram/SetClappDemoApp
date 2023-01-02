using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class first_dbCreateMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OperationClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Created_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "System Manager"),
                    Modified_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "System Manager"),
                    Created_at = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValue: new DateTime(2023, 1, 2, 7, 44, 47, 278, DateTimeKind.Local).AddTicks(5780)),
                    Modified_at = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValue: new DateTime(2023, 1, 2, 7, 44, 47, 278, DateTimeKind.Local).AddTicks(6199)),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(1000)", maxLength: 1000, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(1000)", maxLength: 1000, nullable: false),
                    UserType = table.Column<int>(type: "int", nullable: false),
                    ChiefId = table.Column<int>(type: "int", nullable: false),
                    SecurityToken = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, defaultValue: "6fd83d539a"),
                    SecurityCount = table.Column<int>(type: "int", nullable: false),
                    ConfirmationStatus = table.Column<bool>(type: "bit", nullable: false),
                    Created_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "System Manager"),
                    Modified_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "System Manager"),
                    Created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2023, 1, 2, 7, 44, 47, 279, DateTimeKind.Local).AddTicks(729)),
                    Modified_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2023, 1, 2, 7, 44, 47, 279, DateTimeKind.Local).AddTicks(1143)),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkerRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentNumber = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    AdditionalDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartFrom = table.Column<DateTime>(type: "date", nullable: false),
                    FinishDate = table.Column<DateTime>(type: "date", nullable: false),
                    RequestType = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AssignedWorkerId = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Created_by = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Modified_by = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserOperationClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    OperationClaimId = table.Column<int>(type: "int", nullable: false),
                    Created_by = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Modified_by = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOperationClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserOperationClaims_OperationClaims_OperationClaimId",
                        column: x => x.OperationClaimId,
                        principalTable: "OperationClaims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserOperationClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserWorkerRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IsRequestOwner = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RequestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWorkerRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserWorkerRequests_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserWorkerRequests_WorkerRequests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "WorkerRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserOperationClaims_OperationClaimId",
                table: "UserOperationClaims",
                column: "OperationClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOperationClaims_UserId",
                table: "UserOperationClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWorkerRequests_RequestId",
                table: "UserWorkerRequests",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWorkerRequests_UserId",
                table: "UserWorkerRequests",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserOperationClaims");

            migrationBuilder.DropTable(
                name: "UserWorkerRequests");

            migrationBuilder.DropTable(
                name: "OperationClaims");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "WorkerRequests");
        }
    }
}
