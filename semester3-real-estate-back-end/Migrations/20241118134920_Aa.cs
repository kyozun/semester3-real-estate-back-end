using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace semester3_real_estate_back_end.Migrations
{
    /// <inheritdoc />
    public partial class Aa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Property",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

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
                table: "Property",
                keyColumn: "PropertyId",
                keyValue: "ffe56d39-3939-4c5c-ade9-8f147dca9581",
                columns: new[] { "CreatedAt", "UpdatedAt", "UserId" },
                values: new object[] { new DateTime(2024, 11, 18, 20, 49, 19, 615, DateTimeKind.Local).AddTicks(4917), new DateTime(2024, 11, 18, 20, 49, 19, 615, DateTimeKind.Local).AddTicks(4918), "70acc54a-44e0-4d31-8b07-52b5b82e9e55" });

            migrationBuilder.UpdateData(
                table: "Property",
                keyColumn: "PropertyId",
                keyValue: "ffe56d39-3939-4c5c-ade9-8f147dca9582",
                columns: new[] { "CreatedAt", "UpdatedAt", "UserId" },
                values: new object[] { new DateTime(2024, 11, 18, 20, 49, 19, 615, DateTimeKind.Local).AddTicks(4922), new DateTime(2024, 11, 18, 20, 49, 19, 615, DateTimeKind.Local).AddTicks(4922), "70acc54a-44e0-4d31-8b07-52b5b82e9e55" });

            migrationBuilder.CreateIndex(
                name: "IX_Property_UserId",
                table: "Property",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Property_AspNetUsers_UserId",
                table: "Property",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Property_AspNetUsers_UserId",
                table: "Property");

            migrationBuilder.DropIndex(
                name: "IX_Property_UserId",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Property");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "28a11887-8723-4f56-a83f-f8d43529dfb9",
                column: "ConcurrencyStamp",
                value: "466cb5ae-fc31-4573-b1d7-e3a1e15a5010");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ffe56d39-3939-4c5c-ade9-8f147dca9591",
                column: "ConcurrencyStamp",
                value: "239fb660-9b66-4c31-a4d3-e2112da27b41");

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: "ffe56d39-3939-4c5c-ade9-8f147dca9592",
                column: "UpdatedAt",
                value: new DateTime(2024, 11, 18, 17, 19, 7, 989, DateTimeKind.Local).AddTicks(7028));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: "ffe56d39-3939-4c5c-ade9-8f147dca9593",
                column: "UpdatedAt",
                value: new DateTime(2024, 11, 18, 17, 19, 7, 989, DateTimeKind.Local).AddTicks(7040));

            migrationBuilder.UpdateData(
                table: "Property",
                keyColumn: "PropertyId",
                keyValue: "ffe56d39-3939-4c5c-ade9-8f147dca9581",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 18, 17, 19, 7, 989, DateTimeKind.Local).AddTicks(7287), new DateTime(2024, 11, 18, 17, 19, 7, 989, DateTimeKind.Local).AddTicks(7288) });

            migrationBuilder.UpdateData(
                table: "Property",
                keyColumn: "PropertyId",
                keyValue: "ffe56d39-3939-4c5c-ade9-8f147dca9582",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 18, 17, 19, 7, 989, DateTimeKind.Local).AddTicks(7292), new DateTime(2024, 11, 18, 17, 19, 7, 989, DateTimeKind.Local).AddTicks(7292) });
        }
    }
}
