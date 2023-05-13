using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tamak.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<int>(type: "int", nullable: false),
                    Campus = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Assortiments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assortiments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assortiments_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Img = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Available = table.Column<bool>(type: "bit", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Avatar = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    AssortimentId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Assortiments_AssortimentId",
                        column: x => x.AssortimentId,
                        principalTable: "Assortiments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Campus", "City", "Email", "Name", "Password", "Role" },
                values: new object[] { 1L, 0, 0, "Test@edu.hse.ru", "Test1", "8a863b145dc6e4ed7ac41c08f7536c476ebac7509e028ed2b49f8bd5a3562b9f", 2 });

            migrationBuilder.InsertData(
                table: "Assortiments",
                columns: new[] { "Id", "UserId" },
                values: new object[] { 1L, 1L });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AssortimentId", "Available", "Avatar", "Category", "CategoryId", "Description", "Img", "Name", "Price" },
                values: new object[] { 1L, 1L, true, null, 0, 0, "Свежий кофе с молочным оттенком", null, "Латте", 200m });

            migrationBuilder.CreateIndex(
                name: "IX_Assortiments_UserId",
                table: "Assortiments",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_AssortimentId",
                table: "Products",
                column: "AssortimentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Assortiments");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
