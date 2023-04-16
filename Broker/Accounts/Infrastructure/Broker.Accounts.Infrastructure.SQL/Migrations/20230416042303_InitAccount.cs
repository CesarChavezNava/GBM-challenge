using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Broker.Accounts.Infrastructure.SQL.Migrations
{
    /// <inheritdoc />
    public partial class InitAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ACCOUNT",
                columns: table => new
                {
                    User_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Balance = table.Column<decimal>(type: "decimal(12,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ACCOUNT", x => x.User_Id);
                });

            migrationBuilder.CreateTable(
                name: "ACCOUNT_ISSUER",
                columns: table => new
                {
                    User_Id = table.Column<int>(type: "int", nullable: false),
                    Issuer_Name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Total_Shares = table.Column<int>(type: "int", nullable: false),
                    Share_Price = table.Column<decimal>(type: "decimal(12,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ACCOUNT_ISSUER", x => new { x.User_Id, x.Issuer_Name });
                    table.ForeignKey(
                        name: "FK_ACCOUNT_ISSUER_ACCOUNT_User_Id",
                        column: x => x.User_Id,
                        principalTable: "ACCOUNT",
                        principalColumn: "User_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ACCOUNT_ORDER",
                columns: table => new
                {
                    User_Id = table.Column<int>(type: "int", nullable: false),
                    Order_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<long>(type: "bigint", nullable: false),
                    Operation = table.Column<int>(type: "int", maxLength: 15, nullable: false),
                    Issuer_Name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Total_Shares = table.Column<int>(type: "int", nullable: false),
                    Share_Price = table.Column<decimal>(type: "decimal(12,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ACCOUNT_ORDER", x => new { x.User_Id, x.Order_Id });
                    table.ForeignKey(
                        name: "FK_ACCOUNT_ORDER_ACCOUNT_User_Id",
                        column: x => x.User_Id,
                        principalTable: "ACCOUNT",
                        principalColumn: "User_Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ACCOUNT_ISSUER");

            migrationBuilder.DropTable(
                name: "ACCOUNT_ORDER");

            migrationBuilder.DropTable(
                name: "ACCOUNT");
        }
    }
}
