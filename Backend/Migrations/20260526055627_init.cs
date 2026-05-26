using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SchedTypes",
                columns: table => new
                {
                    SchedTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchedTypes", x => x.SchedTypeId);
                });

            migrationBuilder.CreateTable(
                name: "StatusContributions",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StatusName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusContributions", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "SavingsPools",
                columns: table => new
                {
                    SavingsPoolsId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    TargetAmount = table.Column<int>(type: "integer", nullable: false),
                    CurrentAmount = table.Column<int>(type: "integer", nullable: false),
                    SchedTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavingsPools", x => x.SavingsPoolsId);
                    table.ForeignKey(
                        name: "FK_SavingsPools_SchedTypes_SchedTypeId",
                        column: x => x.SchedTypeId,
                        principalTable: "SchedTypes",
                        principalColumn: "SchedTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PoolContributors",
                columns: table => new
                {
                    ContributorId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SavingsPoolId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    ContributionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoolContributors", x => x.ContributorId);
                    table.ForeignKey(
                        name: "FK_PoolContributors_SavingsPools_SavingsPoolId",
                        column: x => x.SavingsPoolId,
                        principalTable: "SavingsPools",
                        principalColumn: "SavingsPoolsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PoolContributors_StatusContributions_StatusId",
                        column: x => x.StatusId,
                        principalTable: "StatusContributions",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PoolContributors_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "SchedTypes",
                columns: new[] { "SchedTypeId", "Name" },
                values: new object[,]
                {
                    { 1, "Daily" },
                    { 2, "Weekly" },
                    { 3, "Bi-Weekly" },
                    { 4, "Monthly" },
                    { 5, "Quarterly" },
                    { 6, "Yearly" }
                });

            migrationBuilder.InsertData(
                table: "StatusContributions",
                columns: new[] { "StatusId", "StatusName" },
                values: new object[,]
                {
                    { 1, "Pending" },
                    { 2, "Completed" },
                    { 3, "Failed" },
                    { 4, "Refunded" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Name" },
                values: new object[,]
                {
                    { 1, "Alice Johnson" },
                    { 2, "Bob Smith" },
                    { 3, "Charlie Brown" },
                    { 4, "Diana Prince" },
                    { 5, "Evan Wright" }
                });

            migrationBuilder.InsertData(
                table: "SavingsPools",
                columns: new[] { "SavingsPoolsId", "CurrentAmount", "SchedTypeId", "TargetAmount", "Title" },
                values: new object[,]
                {
                    { 1, 2500, 4, 5000, "Summer Vacation Fund" },
                    { 2, 1200, 2, 3000, "New Laptop Group Buy" },
                    { 3, 4500, 4, 10000, "Emergency Rainy Day Fund" },
                    { 4, 800, 1, 800, "Office Party Budget" },
                    { 5, 6000, 5, 15000, "Quarterly Investment Pool" }
                });

            migrationBuilder.InsertData(
                table: "PoolContributors",
                columns: new[] { "ContributorId", "Amount", "ContributionDate", "SavingsPoolId", "StatusId", "UserId" },
                values: new object[,]
                {
                    { 1, 500, new DateTime(2026, 1, 15, 10, 30, 0, 0, DateTimeKind.Utc), 1, 2, 1 },
                    { 2, 500, new DateTime(2026, 1, 16, 14, 0, 0, 0, DateTimeKind.Utc), 1, 2, 2 },
                    { 3, 500, new DateTime(2026, 1, 17, 9, 0, 0, 0, DateTimeKind.Utc), 1, 1, 3 },
                    { 4, 500, new DateTime(2026, 1, 18, 16, 45, 0, 0, DateTimeKind.Utc), 1, 2, 4 },
                    { 5, 500, new DateTime(2026, 1, 19, 11, 20, 0, 0, DateTimeKind.Utc), 1, 2, 5 },
                    { 6, 300, new DateTime(2026, 2, 1, 8, 0, 0, 0, DateTimeKind.Utc), 2, 2, 1 },
                    { 7, 300, new DateTime(2026, 2, 8, 8, 0, 0, 0, DateTimeKind.Utc), 2, 2, 2 },
                    { 8, 300, new DateTime(2026, 2, 15, 8, 0, 0, 0, DateTimeKind.Utc), 2, 2, 3 },
                    { 9, 300, new DateTime(2026, 2, 22, 8, 0, 0, 0, DateTimeKind.Utc), 2, 1, 4 },
                    { 10, 1500, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, 2, 1 },
                    { 11, 1500, new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, 2, 2 },
                    { 12, 1500, new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, 2, 3 },
                    { 13, 200, new DateTime(2026, 5, 20, 12, 0, 0, 0, DateTimeKind.Utc), 4, 2, 1 },
                    { 14, 200, new DateTime(2026, 5, 21, 12, 0, 0, 0, DateTimeKind.Utc), 4, 2, 2 },
                    { 15, 200, new DateTime(2026, 5, 22, 12, 0, 0, 0, DateTimeKind.Utc), 4, 2, 3 },
                    { 16, 200, new DateTime(2026, 5, 23, 12, 0, 0, 0, DateTimeKind.Utc), 4, 2, 4 },
                    { 17, 2000, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, 2, 1 },
                    { 18, 2000, new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, 2, 2 },
                    { 19, 2000, new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, 1, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PoolContributors_SavingsPoolId",
                table: "PoolContributors",
                column: "SavingsPoolId");

            migrationBuilder.CreateIndex(
                name: "IX_PoolContributors_StatusId",
                table: "PoolContributors",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PoolContributors_UserId",
                table: "PoolContributors",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SavingsPools_SchedTypeId",
                table: "SavingsPools",
                column: "SchedTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PoolContributors");

            migrationBuilder.DropTable(
                name: "SavingsPools");

            migrationBuilder.DropTable(
                name: "StatusContributions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "SchedTypes");
        }
    }
}
