using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataRegionAndDifficulty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("70cb9776-534c-495f-b738-f35b93d5442a"), "Easy" },
                    { new Guid("80cb9776-534c-495f-b738-f35b93d5442b"), "Medium" },
                    { new Guid("90cb9776-534c-495f-b738-f35b93d5442c"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("24b26d15-6332-4900-a5c0-bd01caffbbbc"), "GP", "Gauteng", "http://images/GP" },
                    { new Guid("48bb5b6d-7fd4-414f-8b92-e05159683814"), "MP", "Mpumalanga", "http://images/MP" },
                    { new Guid("7f23c3e1-4218-4023-8b21-26b596d5f9e3"), "LP", "Limpompo", "http://images/LP" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("70cb9776-534c-495f-b738-f35b93d5442a"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("80cb9776-534c-495f-b738-f35b93d5442b"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("90cb9776-534c-495f-b738-f35b93d5442c"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("24b26d15-6332-4900-a5c0-bd01caffbbbc"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("48bb5b6d-7fd4-414f-8b92-e05159683814"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("7f23c3e1-4218-4023-8b21-26b596d5f9e3"));
        }
    }
}
