using System;
using Microsoft.EntityFrameworkCore.Migrations;
using GamePlatform.Repository;

#nullable disable

namespace GamePlatform.Repository.Migrations
{
    /// <inheritdoc />
    public partial class init_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfileName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Developers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DevName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DevDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearFormed = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Developers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HighScores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HighScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HighScores_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GameDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateReleased = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GameImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    DeveloperId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Developers_DeveloperId",
                        column: x => x.DeveloperId,
                        principalTable: "Developers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GameInCarts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ShoppingCartId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameInCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameInCarts_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GameInCarts_ShoppingCarts_ShoppingCartId",
                        column: x => x.ShoppingCartId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GameInOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameInOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameInOrders_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GameInOrders_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GameScoreRaltions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    HighScoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameScoreRaltions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameScoreRaltions_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GameScoreRaltions_HighScores_HighScoreId",
                        column: x => x.HighScoreId,
                        principalTable: "HighScores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameInCarts_GameId",
                table: "GameInCarts",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameInCarts_ShoppingCartId",
                table: "GameInCarts",
                column: "ShoppingCartId");

            migrationBuilder.CreateIndex(
                name: "IX_GameInOrders_GameId",
                table: "GameInOrders",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameInOrders_OrderId",
                table: "GameInOrders",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_DeveloperId",
                table: "Games",
                column: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_GameScoreRaltions_GameId",
                table: "GameScoreRaltions",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameScoreRaltions_HighScoreId",
                table: "GameScoreRaltions",
                column: "HighScoreId");

            migrationBuilder.CreateIndex(
                name: "IX_HighScores_UserId",
                table: "HighScores",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_UserId",
                table: "ShoppingCarts",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameInCarts");

            migrationBuilder.DropTable(
                name: "GameInOrders");

            migrationBuilder.DropTable(
                name: "GameScoreRaltions");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "HighScores");

            migrationBuilder.DropTable(
                name: "Developers");

            migrationBuilder.DropColumn(
                name: "ProfileName",
                table: "AspNetUsers");
        }
    }
}
