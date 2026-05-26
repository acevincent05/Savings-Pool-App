using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SchedType",
                columns: table => new
                {
                    SchedTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SchedType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchedType", x => x.SchedTypeId);
                });

            migrationBuilder.CreateTable(
                name: "StatusContribution",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StatusName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusContribution", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "SavingsPool",
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
                    table.PrimaryKey("PK_SavingsPool", x => x.SavingsPoolsId);
                    table.ForeignKey(
                        name: "FK_SavingsPool_SchedType_SchedTypeId",
                        column: x => x.SchedTypeId,
                        principalTable: "SchedType",
                        principalColumn: "SchedTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PoolContributor",
                columns: table => new
                {
                    ContributorId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    StatusContributionStatusId = table.Column<int>(type: "integer", nullable: false),
                    SavingsPoolsId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoolContributor", x => x.ContributorId);
                    table.ForeignKey(
                        name: "FK_PoolContributor_SavingsPool_SavingsPoolsId",
                        column: x => x.SavingsPoolsId,
                        principalTable: "SavingsPool",
                        principalColumn: "SavingsPoolsId");
                    table.ForeignKey(
                        name: "FK_PoolContributor_StatusContribution_StatusContributionStatus~",
                        column: x => x.StatusContributionStatusId,
                        principalTable: "StatusContribution",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PoolContributor_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PoolContributor_SavingsPoolsId",
                table: "PoolContributor",
                column: "SavingsPoolsId");

            migrationBuilder.CreateIndex(
                name: "IX_PoolContributor_StatusContributionStatusId",
                table: "PoolContributor",
                column: "StatusContributionStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PoolContributor_UserId",
                table: "PoolContributor",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SavingsPool_SchedTypeId",
                table: "SavingsPool",
                column: "SchedTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PoolContributor");

            migrationBuilder.DropTable(
                name: "SavingsPool");

            migrationBuilder.DropTable(
                name: "StatusContribution");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "SchedType");
        }
    }
}
