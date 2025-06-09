using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace test2.Migrations
{
    /// <inheritdoc />
    public partial class AddSampleData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Concert",
                columns: new[] { "ConcertId", "AvailableTickets", "Date", "Name" },
                values: new object[,]
                {
                    { 1, 0, new DateTime(2025, 6, 7, 9, 0, 0, 0, DateTimeKind.Unspecified), "Concert 1" },
                    { 14, 0, new DateTime(2025, 6, 10, 9, 0, 0, 0, DateTimeKind.Unspecified), "Concert 14" }
                });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "CustomerId", "FirstName", "LastName", "PhoneNumber" },
                values: new object[] { 1, "John", "Doe", null });

            migrationBuilder.InsertData(
                table: "Ticket",
                columns: new[] { "TicketId", "SeatNumber", "SerialNumber" },
                values: new object[] { 2, 330, "TK2027/S4831/133" });

            migrationBuilder.InsertData(
                table: "TicketConcert",
                columns: new[] { "TicketConcertId", "ConcertId", "Price", "TicketId" },
                values: new object[,]
                {
                    { 1, 1, 33.4m, 1 },
                    { 2, 14, 48.4m, 2 }
                });

            migrationBuilder.InsertData(
                table: "PurchasedTicket",
                columns: new[] { "CustomerId", "TicketConcertId", "PurchaseDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 6, 3, 9, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 1, 2, new DateTime(2025, 6, 3, 9, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PurchasedTicket",
                keyColumns: new[] { "CustomerId", "TicketConcertId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "PurchasedTicket",
                keyColumns: new[] { "CustomerId", "TicketConcertId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "CustomerId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TicketConcert",
                keyColumn: "TicketConcertId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TicketConcert",
                keyColumn: "TicketConcertId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Concert",
                keyColumn: "ConcertId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Concert",
                keyColumn: "ConcertId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "TicketId",
                keyValue: 2);
        }
    }
}
