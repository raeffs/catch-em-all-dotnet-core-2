using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Raefftec.CatchEmAll.DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HighPriorityQuota = table.Column<int>(nullable: false),
                    IsDefault = table.Column<bool>(nullable: false),
                    LowPriotityQuota = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NormalPriotiryQuota = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: false),
                    IsAdmin = table.Column<bool>(nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: false),
                    Username = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Queries",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AutoFilterDeletedDuplicates = table.Column<bool>(nullable: false),
                    CategoryId = table.Column<long>(nullable: false),
                    DesiredPrice = table.Column<decimal>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsLocked = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    NotificationMode = table.Column<int>(nullable: false),
                    Priority = table.Column<int>(nullable: false),
                    Updated = table.Column<DateTimeOffset>(nullable: false),
                    UseDescription = table.Column<bool>(nullable: false),
                    WithAllTheseWords = table.Column<string>(nullable: true),
                    WithExactlyTheseWords = table.Column<string>(nullable: true),
                    WithNoneOfTheseWords = table.Column<string>(nullable: true),
                    WithOneOfTheseWords = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Queries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Queries_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BidPrice = table.Column<decimal>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Ends = table.Column<DateTimeOffset>(nullable: true),
                    ExternalId = table.Column<long>(nullable: false),
                    FinalPrice = table.Column<decimal>(nullable: true),
                    IsClosed = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsFavorite = table.Column<bool>(nullable: false),
                    IsHidden = table.Column<bool>(nullable: false),
                    IsNew = table.Column<bool>(nullable: false),
                    IsNotified = table.Column<bool>(nullable: false),
                    IsSold = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    PurchasePrice = table.Column<decimal>(nullable: true),
                    QueryId = table.Column<long>(nullable: false),
                    Updated = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Results_Queries_QueryId",
                        column: x => x.QueryId,
                        principalTable: "Queries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_UserId_Number",
                table: "Categories",
                columns: new[] { "UserId", "Number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Queries_CategoryId",
                table: "Queries",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_QueryId",
                table: "Results",
                column: "QueryId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "Queries");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
