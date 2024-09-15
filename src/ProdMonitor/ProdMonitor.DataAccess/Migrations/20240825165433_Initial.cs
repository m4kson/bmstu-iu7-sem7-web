using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProdMonitor.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssemblyLines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Length = table.Column<float>(type: "real", nullable: false),
                    Height = table.Column<float>(type: "real", nullable: false),
                    Width = table.Column<float>(type: "real", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Production = table.Column<int>(type: "integer", nullable: false),
                    DownTime = table.Column<int>(type: "integer", nullable: false),
                    InspectionsPerYear = table.Column<int>(type: "integer", nullable: false),
                    LastInspection = table.Column<DateOnly>(type: "date", nullable: false),
                    NextInspection = table.Column<DateOnly>(type: "date", nullable: false),
                    DefectRate = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssemblyLines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Details",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Length = table.Column<int>(type: "integer", nullable: false),
                    Height = table.Column<int>(type: "integer", nullable: false),
                    Width = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Details", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tractors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Model = table.Column<string>(type: "text", nullable: false),
                    ReleaseYear = table.Column<int>(type: "integer", nullable: false),
                    EngineType = table.Column<string>(type: "text", nullable: false),
                    EnginePower = table.Column<string>(type: "text", nullable: false),
                    FrontTireSize = table.Column<int>(type: "integer", nullable: false),
                    BackTireSize = table.Column<int>(type: "integer", nullable: false),
                    WheelsAmount = table.Column<int>(type: "integer", nullable: false),
                    TankCapacity = table.Column<int>(type: "integer", nullable: false),
                    EcologicalStandard = table.Column<string>(type: "text", nullable: false),
                    Length = table.Column<float>(type: "real", nullable: false),
                    Width = table.Column<float>(type: "real", nullable: false),
                    CabinHeight = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tractors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Patronymic = table.Column<string>(type: "text", nullable: false),
                    Department = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "bytea", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "bytea", nullable: false),
                    BirthDay = table.Column<DateOnly>(type: "date", nullable: false),
                    Sex = table.Column<int>(type: "integer", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssemblyLineDbDetailDb",
                columns: table => new
                {
                    AssemblyLinesId = table.Column<Guid>(type: "uuid", nullable: false),
                    DetailsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssemblyLineDbDetailDb", x => new { x.AssemblyLinesId, x.DetailsId });
                    table.ForeignKey(
                        name: "FK_AssemblyLineDbDetailDb_AssemblyLines_AssemblyLinesId",
                        column: x => x.AssemblyLinesId,
                        principalTable: "AssemblyLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssemblyLineDbDetailDb_Details_DetailsId",
                        column: x => x.DetailsId,
                        principalTable: "Details",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssemblyLineDbTractorDb",
                columns: table => new
                {
                    AssemblyLinesId = table.Column<Guid>(type: "uuid", nullable: false),
                    TractorsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssemblyLineDbTractorDb", x => new { x.AssemblyLinesId, x.TractorsId });
                    table.ForeignKey(
                        name: "FK_AssemblyLineDbTractorDb_AssemblyLines_AssemblyLinesId",
                        column: x => x.AssemblyLinesId,
                        principalTable: "AssemblyLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssemblyLineDbTractorDb_Tractors_TractorsId",
                        column: x => x.TractorsId,
                        principalTable: "Tractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetailOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    TotalPrice = table.Column<float>(type: "real", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetailOrders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LineId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ServiceReportId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceRequests_AssemblyLines_LineId",
                        column: x => x.LineId,
                        principalTable: "AssemblyLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceRequests_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DetailId = table.Column<Guid>(type: "uuid", nullable: false),
                    DetailOrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    DetailsAmount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_DetailOrders_DetailOrderId",
                        column: x => x.DetailOrderId,
                        principalTable: "DetailOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Details_DetailId",
                        column: x => x.DetailId,
                        principalTable: "Details",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LineId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RequestId = table.Column<Guid>(type: "uuid", nullable: false),
                    OpenDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CloseDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Price = table.Column<float>(type: "real", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceReports_AssemblyLines_LineId",
                        column: x => x.LineId,
                        principalTable: "AssemblyLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceReports_ServiceRequests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "ServiceRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceReports_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssemblyLineDbDetailDb_DetailsId",
                table: "AssemblyLineDbDetailDb",
                column: "DetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_AssemblyLineDbTractorDb_TractorsId",
                table: "AssemblyLineDbTractorDb",
                column: "TractorsId");

            migrationBuilder.CreateIndex(
                name: "IX_AssemblyLines_Id",
                table: "AssemblyLines",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetailOrders_Id",
                table: "DetailOrders",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetailOrders_UserId",
                table: "DetailOrders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Details_Id",
                table: "Details",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_DetailId",
                table: "OrderDetails",
                column: "DetailId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_DetailOrderId",
                table: "OrderDetails",
                column: "DetailOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_Id",
                table: "OrderDetails",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceReports_Id",
                table: "ServiceReports",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceReports_LineId",
                table: "ServiceReports",
                column: "LineId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceReports_RequestId",
                table: "ServiceReports",
                column: "RequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceReports_UserId",
                table: "ServiceReports",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_Id",
                table: "ServiceRequests",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_LineId",
                table: "ServiceRequests",
                column: "LineId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_UserId",
                table: "ServiceRequests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tractors_Id",
                table: "Tractors",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Id",
                table: "Users",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssemblyLineDbDetailDb");

            migrationBuilder.DropTable(
                name: "AssemblyLineDbTractorDb");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "ServiceReports");

            migrationBuilder.DropTable(
                name: "Tractors");

            migrationBuilder.DropTable(
                name: "DetailOrders");

            migrationBuilder.DropTable(
                name: "Details");

            migrationBuilder.DropTable(
                name: "ServiceRequests");

            migrationBuilder.DropTable(
                name: "AssemblyLines");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
