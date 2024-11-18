using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace semester3_real_estate_back_end.Migrations
{
    /// <inheritdoc />
    public partial class Aaa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "28a11887-8723-4f56-a83f-f8d43529dfb9",
                column: "ConcurrencyStamp",
                value: "43009a98-f17b-42e3-b973-261cd950ba2d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ffe56d39-3939-4c5c-ade9-8f147dca9591",
                column: "ConcurrencyStamp",
                value: "26022a8f-66f5-4537-adbf-2e68b9cc2e61");

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: "ffe56d39-3939-4c5c-ade9-8f147dca9592",
                column: "UpdatedAt",
                value: new DateTime(2024, 11, 18, 21, 2, 1, 20, DateTimeKind.Local).AddTicks(6075));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: "ffe56d39-3939-4c5c-ade9-8f147dca9593",
                column: "UpdatedAt",
                value: new DateTime(2024, 11, 18, 21, 2, 1, 20, DateTimeKind.Local).AddTicks(6088));

            migrationBuilder.UpdateData(
                table: "Direction",
                keyColumn: "DirectionId",
                keyValue: "ffe56d39-3939-4c5c-ade9-8f147dca9593",
                column: "Name",
                value: "North");

            migrationBuilder.InsertData(
                table: "Direction",
                columns: new[] { "DirectionId", "Name" },
                values: new object[] { "ffe56d39-3939-4c5c-ade9-8f147dca9594", "South" });

            migrationBuilder.UpdateData(
                table: "Property",
                keyColumn: "PropertyId",
                keyValue: "ffe56d39-3939-4c5c-ade9-8f147dca9581",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 18, 21, 2, 1, 20, DateTimeKind.Local).AddTicks(6410), new DateTime(2024, 11, 18, 21, 2, 1, 20, DateTimeKind.Local).AddTicks(6411) });

            migrationBuilder.UpdateData(
                table: "Property",
                keyColumn: "PropertyId",
                keyValue: "ffe56d39-3939-4c5c-ade9-8f147dca9582",
                columns: new[] { "CreatedAt", "DirectionId", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 18, 21, 2, 1, 20, DateTimeKind.Local).AddTicks(6414), "ffe56d39-3939-4c5c-ade9-8f147dca9594", new DateTime(2024, 11, 18, 21, 2, 1, 20, DateTimeKind.Local).AddTicks(6415) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Direction",
                keyColumn: "DirectionId",
                keyValue: "ffe56d39-3939-4c5c-ade9-8f147dca9594");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "28a11887-8723-4f56-a83f-f8d43529dfb9",
                column: "ConcurrencyStamp",
                value: "fd3b0b8d-e962-43f4-a2a8-e68c024380a2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ffe56d39-3939-4c5c-ade9-8f147dca9591",
                column: "ConcurrencyStamp",
                value: "ead87de4-a487-4fe9-b986-b05b318abbbd");

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: "ffe56d39-3939-4c5c-ade9-8f147dca9592",
                column: "UpdatedAt",
                value: new DateTime(2024, 11, 18, 20, 49, 19, 615, DateTimeKind.Local).AddTicks(4599));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: "ffe56d39-3939-4c5c-ade9-8f147dca9593",
                column: "UpdatedAt",
                value: new DateTime(2024, 11, 18, 20, 49, 19, 615, DateTimeKind.Local).AddTicks(4610));

            migrationBuilder.UpdateData(
                table: "Direction",
                keyColumn: "DirectionId",
                keyValue: "ffe56d39-3939-4c5c-ade9-8f147dca9593",
                column: "Name",
                value: "Direction 1");

            migrationBuilder.UpdateData(
                table: "Property",
                keyColumn: "PropertyId",
                keyValue: "ffe56d39-3939-4c5c-ade9-8f147dca9581",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 18, 20, 49, 19, 615, DateTimeKind.Local).AddTicks(4917), new DateTime(2024, 11, 18, 20, 49, 19, 615, DateTimeKind.Local).AddTicks(4918) });

            migrationBuilder.UpdateData(
                table: "Property",
                keyColumn: "PropertyId",
                keyValue: "ffe56d39-3939-4c5c-ade9-8f147dca9582",
                columns: new[] { "CreatedAt", "DirectionId", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 18, 20, 49, 19, 615, DateTimeKind.Local).AddTicks(4922), "ffe56d39-3939-4c5c-ade9-8f147dca9593", new DateTime(2024, 11, 18, 20, 49, 19, 615, DateTimeKind.Local).AddTicks(4922) });
        }
    }
}
