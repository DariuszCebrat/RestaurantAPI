using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantAPI.Migrations
{
    public partial class seeder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "PostalCode", "Street" },
                values: new object[] { 1, "Kraków", "30-001", "Długa 10" });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "PostalCode", "Street" },
                values: new object[] { 2, "Karków", "30-001", "Miejska 10" });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "AddressId", "Category", "ContactEmail", "ContactNumber", "Description", "HasDelivery", "Name" },
                values: new object[] { 1, 1, "fast food", "kfc@gmail.com", "222333444", "KFC american fast food restaurant", true, "KFC" });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "AddressId", "Category", "ContactEmail", "ContactNumber", "Description", "HasDelivery", "Name" },
                values: new object[] { 2, 2, "fast food", "mcdonald@gmail.com", "555333444", "McDonald american fast food restaurant", false, "McDonald" });

            migrationBuilder.InsertData(
                table: "Dishes",
                columns: new[] { "Id", "Description", "Name", "Price", "RestaurantId" },
                values: new object[,]
                {
                    { 1, "", "burger", 8.30m, 1 },
                    { 2, "", "Cheeseburger", 10.30m, 1 },
                    { 3, "", "Chiken Nuggets", 12.50m, 1 },
                    { 4, "", "salad", 11.30m, 2 },
                    { 5, "", "BigBurger", 10.30m, 2 },
                    { 6, "", "Chiken Nuggets", 13.50m, 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
