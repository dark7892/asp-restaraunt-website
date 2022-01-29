using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrderNo = table.Column<string>(type: "varchar(50)", nullable: true),
                    CustomerId = table.Column<int>(nullable: false),
                    PaymentMethod = table.Column<string>(type: "varchar(50)", nullable: true),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderDetailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrderId = table.Column<int>(nullable: false),
                    ItemId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.OrderDetailId);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            //Predefined Data to be inserted into DB.
            //Section Customers
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new []{ "CustomerId", "Name" },
                values: new object[] {1, "Pawel Maslak"}
            );

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Name" },
                values: new object[] { 2, "Monica Kossek" }
            );

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Name" },
                values: new object[] { 3, "Shawn Wildermuth" }
            );

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Name" },
                values: new object[] { 4, "Scott Allen" }
            );

            //Section Items
            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "ItemId", "Name", "Price" },
                values: new object[] { 1, "Margeritha", 5.50 }
            );

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "ItemId", "Name", "Price" },
                values: new object[] { 2, "Capriciosa", 6.50 }
            );

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "ItemId", "Name", "Price" },
                values: new object[] { 3, "Salame", 7.50 }
            );

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "ItemId", "Name", "Price" },
                values: new object[] { 4, "Marina", 7.50 }
            );

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "ItemId", "Name", "Price" },
                values: new object[] { 5, "Speciale", 8.50 }
            );

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "ItemId", "Name", "Price" },
                values: new object[] { 6, "Speciale", 8.50 }
            );

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "ItemId", "Name", "Price" },
                values: new object[] { 7, "Marco Polo", 6.50 }
            );

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "ItemId", "Name", "Price" },
                values: new object[] { 8, "Vesuvio", 9.50 }
            );

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "ItemId", "Name", "Price" },
                values: new object[] { 9, "Bambino", 4.50 }
            );

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "ItemId", "Name", "Price" },
                values: new object[] { 10, "Figa", 6.50 }
            );

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ItemId",
                table: "OrderDetails",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
