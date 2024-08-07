﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace testaufgabe.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherData",
                columns: table => new
                {
                    Station = table.Column<int>(type: "INTEGER", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AirTemperature_Unit = table.Column<string>(type: "TEXT", nullable: true),
                    AirTemperature_Value = table.Column<double>(type: "REAL", nullable: true),
                    BarometricPressure_Unit = table.Column<string>(type: "TEXT", nullable: true),
                    BarometricPressure_Value = table.Column<double>(type: "REAL", nullable: true),
                    Humidity_Unit = table.Column<string>(type: "TEXT", nullable: true),
                    Humidity_Value = table.Column<double>(type: "REAL", nullable: true),
                    WaterTemperature_Unit = table.Column<string>(type: "TEXT", nullable: true),
                    WaterTemperature_Value = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherData", x => new { x.Station, x.Timestamp });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherData");
        }
    }
}
