using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DropPulse.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IrrigationEvents");

            migrationBuilder.CreateTable(
                name: "Irrigation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WasIrrigationActivated = table.Column<bool>(type: "bit", nullable: false),
                    SensorDataId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Irrigati__3214EC07B7681282", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Irrigation_SensorData_SensorDataId",
                        column: x => x.SensorDataId,
                        principalTable: "SensorData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Irrigation_SensorDataId",
                table: "Irrigation",
                column: "SensorDataId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Irrigation");

            migrationBuilder.CreateTable(
                name: "IrrigationEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DurationSeconds = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TriggerReason = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Irrigati__3214EC07B7681282", x => x.Id);
                });
        }
    }
}
