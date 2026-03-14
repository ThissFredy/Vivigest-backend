using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vivigest_backend.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_DocumentTypes_DocumentTypeIdDocumentType",
                table: "Persons");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Persons_PersonIdPerson",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PersonIdPerson",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Persons_DocumentTypeIdDocumentType",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "PersonIdPerson",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DocumentTypeIdDocumentType",
                table: "Persons");

            migrationBuilder.CreateTable(
                name: "Complexes",
                columns: table => new
                {
                    IdComplex = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameComplex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complexes", x => x.IdComplex);
                });

            migrationBuilder.CreateTable(
                name: "CorrespondenceStates",
                columns: table => new
                {
                    IdCorrespondenceState = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameCorrespondenceState = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorrespondenceStates", x => x.IdCorrespondenceState);
                });

            migrationBuilder.CreateTable(
                name: "CorrespondenceTypes",
                columns: table => new
                {
                    IdCorrespondenceType = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameCorrespondenceType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorrespondenceTypes", x => x.IdCorrespondenceType);
                });

            migrationBuilder.CreateTable(
                name: "IncidentTypes",
                columns: table => new
                {
                    IdIncidentType = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameIncidentType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncidentTypes", x => x.IdIncidentType);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    IdPaymentMethod = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameMethod = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.IdPaymentMethod);
                });

            migrationBuilder.CreateTable(
                name: "PaymentPeriods",
                columns: table => new
                {
                    IdPaymentPeriod = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NamePeriod = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentPeriods", x => x.IdPaymentPeriod);
                });

            migrationBuilder.CreateTable(
                name: "RelationshipTypes",
                columns: table => new
                {
                    IdRelationshipType = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameRelationshipType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelationshipTypes", x => x.IdRelationshipType);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    IdState = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TableState = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.IdState);
                });

            migrationBuilder.CreateTable(
                name: "Visitors",
                columns: table => new
                {
                    IdVisitor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPerson = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitors", x => x.IdVisitor);
                });

            migrationBuilder.CreateTable(
                name: "Towers",
                columns: table => new
                {
                    IdTower = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdComplex = table.Column<int>(type: "int", nullable: false),
                    NameTower = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Towers", x => x.IdTower);
                    table.ForeignKey(
                        name: "FK_Towers_Complexes_IdComplex",
                        column: x => x.IdComplex,
                        principalTable: "Complexes",
                        principalColumn: "IdComplex",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Incidents",
                columns: table => new
                {
                    IdIncident = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdIncidentType = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlEvidence = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdUserReportee = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incidents", x => x.IdIncident);
                    table.ForeignKey(
                        name: "FK_Incidents_IncidentTypes_IdIncidentType",
                        column: x => x.IdIncidentType,
                        principalTable: "IncidentTypes",
                        principalColumn: "IdIncidentType",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Incidents_Users_IdUserReportee",
                        column: x => x.IdUserReportee,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AuthorizedPersons",
                columns: table => new
                {
                    IdAuthorized = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdResidentUser = table.Column<int>(type: "int", nullable: false),
                    IdPerson = table.Column<int>(type: "int", nullable: false),
                    IdRelationshipType = table.Column<int>(type: "int", nullable: false),
                    VehiclePlate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdState = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorizedPersons", x => x.IdAuthorized);
                    table.ForeignKey(
                        name: "FK_AuthorizedPersons_Persons_IdPerson",
                        column: x => x.IdPerson,
                        principalTable: "Persons",
                        principalColumn: "IdPerson",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuthorizedPersons_RelationshipTypes_IdRelationshipType",
                        column: x => x.IdRelationshipType,
                        principalTable: "RelationshipTypes",
                        principalColumn: "IdRelationshipType",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorizedPersons_States_IdState",
                        column: x => x.IdState,
                        principalTable: "States",
                        principalColumn: "IdState",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorizedPersons_Users_IdResidentUser",
                        column: x => x.IdResidentUser,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    IdUnit = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTower = table.Column<int>(type: "int", nullable: false),
                    CodeUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FloorUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AreaM2Unit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IdState = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.IdUnit);
                    table.ForeignKey(
                        name: "FK_Units_States_IdState",
                        column: x => x.IdState,
                        principalTable: "States",
                        principalColumn: "IdState",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Units_Towers_IdTower",
                        column: x => x.IdTower,
                        principalTable: "Towers",
                        principalColumn: "IdTower",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Correspondences",
                columns: table => new
                {
                    IdCorrespondence = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUnit = table.Column<int>(type: "int", nullable: false),
                    IdCorrespondenceType = table.Column<int>(type: "int", nullable: false),
                    Sender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateReceipt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdCorrespondenceState = table.Column<int>(type: "int", nullable: false),
                    DateNotified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateDelivered = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeliveredTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdRegisteredBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Correspondences", x => x.IdCorrespondence);
                    table.ForeignKey(
                        name: "FK_Correspondences_CorrespondenceStates_IdCorrespondenceState",
                        column: x => x.IdCorrespondenceState,
                        principalTable: "CorrespondenceStates",
                        principalColumn: "IdCorrespondenceState",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Correspondences_CorrespondenceTypes_IdCorrespondenceType",
                        column: x => x.IdCorrespondenceType,
                        principalTable: "CorrespondenceTypes",
                        principalColumn: "IdCorrespondenceType",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Correspondences_Units_IdUnit",
                        column: x => x.IdUnit,
                        principalTable: "Units",
                        principalColumn: "IdUnit",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Correspondences_Users_IdRegisteredBy",
                        column: x => x.IdRegisteredBy,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    IdPayment = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUnit = table.Column<int>(type: "int", nullable: false),
                    IdPaymentPeriod = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IdPaymentMethod = table.Column<int>(type: "int", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdRegisterdBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.IdPayment);
                    table.ForeignKey(
                        name: "FK_Payment_PaymentMethods_IdPaymentMethod",
                        column: x => x.IdPaymentMethod,
                        principalTable: "PaymentMethods",
                        principalColumn: "IdPaymentMethod",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payment_PaymentPeriods_IdPaymentPeriod",
                        column: x => x.IdPaymentPeriod,
                        principalTable: "PaymentPeriods",
                        principalColumn: "IdPaymentPeriod",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payment_Units_IdUnit",
                        column: x => x.IdUnit,
                        principalTable: "Units",
                        principalColumn: "IdUnit",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payment_Users_IdRegisterdBy",
                        column: x => x.IdRegisterdBy,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Residences",
                columns: table => new
                {
                    IdResidence = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    IdUnit = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Residences", x => x.IdResidence);
                    table.ForeignKey(
                        name: "FK_Residences_Units_IdUnit",
                        column: x => x.IdUnit,
                        principalTable: "Units",
                        principalColumn: "IdUnit",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Residences_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Visits",
                columns: table => new
                {
                    IdVisit = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdVisitor = table.Column<int>(type: "int", nullable: false),
                    IdUnit = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehiclePlate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdUserRegister = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visits", x => x.IdVisit);
                    table.ForeignKey(
                        name: "FK_Visits_Units_IdUnit",
                        column: x => x.IdUnit,
                        principalTable: "Units",
                        principalColumn: "IdUnit",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visits_Users_IdUserRegister",
                        column: x => x.IdUserRegister,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Visits_Visitors_IdVisitor",
                        column: x => x.IdVisitor,
                        principalTable: "Visitors",
                        principalColumn: "IdVisitor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdPerson",
                table: "Users",
                column: "IdPerson",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_IdDocumentType",
                table: "Persons",
                column: "IdDocumentType");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorizedPersons_IdPerson",
                table: "AuthorizedPersons",
                column: "IdPerson");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorizedPersons_IdRelationshipType",
                table: "AuthorizedPersons",
                column: "IdRelationshipType");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorizedPersons_IdResidentUser",
                table: "AuthorizedPersons",
                column: "IdResidentUser");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorizedPersons_IdState",
                table: "AuthorizedPersons",
                column: "IdState");

            migrationBuilder.CreateIndex(
                name: "IX_Correspondences_IdCorrespondenceState",
                table: "Correspondences",
                column: "IdCorrespondenceState");

            migrationBuilder.CreateIndex(
                name: "IX_Correspondences_IdCorrespondenceType",
                table: "Correspondences",
                column: "IdCorrespondenceType");

            migrationBuilder.CreateIndex(
                name: "IX_Correspondences_IdRegisteredBy",
                table: "Correspondences",
                column: "IdRegisteredBy");

            migrationBuilder.CreateIndex(
                name: "IX_Correspondences_IdUnit",
                table: "Correspondences",
                column: "IdUnit");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_IdIncidentType",
                table: "Incidents",
                column: "IdIncidentType");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_IdUserReportee",
                table: "Incidents",
                column: "IdUserReportee");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_IdPaymentMethod",
                table: "Payment",
                column: "IdPaymentMethod");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_IdPaymentPeriod",
                table: "Payment",
                column: "IdPaymentPeriod");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_IdRegisterdBy",
                table: "Payment",
                column: "IdRegisterdBy");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_IdUnit",
                table: "Payment",
                column: "IdUnit");

            migrationBuilder.CreateIndex(
                name: "IX_Residences_IdUnit",
                table: "Residences",
                column: "IdUnit");

            migrationBuilder.CreateIndex(
                name: "IX_Residences_IdUser",
                table: "Residences",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Towers_IdComplex",
                table: "Towers",
                column: "IdComplex");

            migrationBuilder.CreateIndex(
                name: "IX_Units_IdState",
                table: "Units",
                column: "IdState");

            migrationBuilder.CreateIndex(
                name: "IX_Units_IdTower",
                table: "Units",
                column: "IdTower");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_IdUnit",
                table: "Visits",
                column: "IdUnit");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_IdUserRegister",
                table: "Visits",
                column: "IdUserRegister");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_IdVisitor",
                table: "Visits",
                column: "IdVisitor");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_DocumentTypes_IdDocumentType",
                table: "Persons",
                column: "IdDocumentType",
                principalTable: "DocumentTypes",
                principalColumn: "IdDocumentType",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Persons_IdPerson",
                table: "Users",
                column: "IdPerson",
                principalTable: "Persons",
                principalColumn: "IdPerson",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_DocumentTypes_IdDocumentType",
                table: "Persons");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Persons_IdPerson",
                table: "Users");

            migrationBuilder.DropTable(
                name: "AuthorizedPersons");

            migrationBuilder.DropTable(
                name: "Correspondences");

            migrationBuilder.DropTable(
                name: "Incidents");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Residences");

            migrationBuilder.DropTable(
                name: "Visits");

            migrationBuilder.DropTable(
                name: "RelationshipTypes");

            migrationBuilder.DropTable(
                name: "CorrespondenceStates");

            migrationBuilder.DropTable(
                name: "CorrespondenceTypes");

            migrationBuilder.DropTable(
                name: "IncidentTypes");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "PaymentPeriods");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "Visitors");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "Towers");

            migrationBuilder.DropTable(
                name: "Complexes");

            migrationBuilder.DropIndex(
                name: "IX_Users_IdPerson",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Persons_IdDocumentType",
                table: "Persons");

            migrationBuilder.AddColumn<int>(
                name: "PersonIdPerson",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DocumentTypeIdDocumentType",
                table: "Persons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PersonIdPerson",
                table: "Users",
                column: "PersonIdPerson");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_DocumentTypeIdDocumentType",
                table: "Persons",
                column: "DocumentTypeIdDocumentType");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_DocumentTypes_DocumentTypeIdDocumentType",
                table: "Persons",
                column: "DocumentTypeIdDocumentType",
                principalTable: "DocumentTypes",
                principalColumn: "IdDocumentType",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Persons_PersonIdPerson",
                table: "Users",
                column: "PersonIdPerson",
                principalTable: "Persons",
                principalColumn: "IdPerson",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
