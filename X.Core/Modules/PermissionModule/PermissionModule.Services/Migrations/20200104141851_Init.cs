using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PermissionModule.Services.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "permission");

            migrationBuilder.CreateTable(
                name: "PermissionGroup",
                schema: "permission",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    CreatedById = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedById = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleType",
                schema: "permission",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    IsMultipleAllowed = table.Column<bool>(nullable: false),
                    PermissionHierarchyLevel = table.Column<short>(nullable: true),
                    IsAllowedAssigningToUsers = table.Column<bool>(nullable: false),
                    CreatedById = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    ModifiedById = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsAllowedAssigningPermissionToRole = table.Column<bool>(nullable: false, defaultValueSql: "((1))"),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    IsAllowedMatchingOnSameLevel = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                schema: "permission",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: false),
                    IsAllowed = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OperationType = table.Column<string>(maxLength: 100, nullable: true),
                    OwnerPermission = table.Column<string>(maxLength: 500, nullable: false),
                    PermissionGroupId = table.Column<short>(nullable: false),
                    CreatedById = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    ModifiedById = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_permission.Permission_permission.PermissionGroup_PermissionGroupId",
                        column: x => x.PermissionGroupId,
                        principalSchema: "permission",
                        principalTable: "PermissionGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "permission",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    OwnerPermission = table.Column<string>(maxLength: 500, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    RoleTypeId = table.Column<short>(nullable: false),
                    CreatedById = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    ModifiedById = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                    table.ForeignKey(
                        name: "FK_permission.Role_permission.RoleType_RoleTypeId",
                        column: x => x.RoleTypeId,
                        principalSchema: "permission",
                        principalTable: "RoleType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RolePermission",
                schema: "permission",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(nullable: false),
                    PermissionId = table.Column<int>(nullable: false),
                    IsAllowed = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedById = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    ModifiedById = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_permission.RolePermission_permission.Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "permission",
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_permission.RolePermission_permission.Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "permission",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleRelations",
                schema: "permission",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(nullable: false),
                    ParentRoleId = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedById = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    ModifiedById = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_permission.RoleRelations_permission.Role_ParentRoleId",
                        column: x => x.ParentRoleId,
                        principalSchema: "permission",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_permission.RoleRelations_permission.Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "permission",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "permission",
                table: "PermissionGroup",
                columns: new[] { "Id", "CreatedById", "CreatedOn", "ModifiedById", "ModifiedOn", "Name" },
                values: new object[] { (short)1, "1", new DateTime(2020, 1, 4, 15, 18, 51, 45, DateTimeKind.Local).AddTicks(5317), "1", new DateTime(2020, 1, 4, 15, 18, 51, 45, DateTimeKind.Local).AddTicks(5906), "System" });

            migrationBuilder.InsertData(
                schema: "permission",
                table: "RoleType",
                columns: new[] { "Id", "Code", "CreatedById", "CreatedOn", "IsAllowedAssigningPermissionToRole", "IsAllowedAssigningToUsers", "IsAllowedMatchingOnSameLevel", "IsMultipleAllowed", "ModifiedById", "ModifiedOn", "Name", "PermissionHierarchyLevel" },
                values: new object[] { (short)1, "USER_TYPE", "1", new DateTime(2020, 1, 4, 15, 18, 51, 36, DateTimeKind.Local).AddTicks(9018), true, true, true, false, "1", new DateTime(2020, 1, 4, 15, 18, 51, 41, DateTimeKind.Local).AddTicks(843), "User type", (short)1 });

            migrationBuilder.InsertData(
                schema: "permission",
                table: "Permission",
                columns: new[] { "Id", "CreatedById", "CreatedOn", "Description", "IsAllowed", "IsDeleted", "ModifiedById", "ModifiedOn", "Name", "OperationType", "OwnerPermission", "PermissionGroupId" },
                values: new object[,]
                {
                    { 1, "1", new DateTime(2020, 1, 4, 15, 18, 51, 46, DateTimeKind.Local).AddTicks(2692), "Root permission", false, false, "1", new DateTime(2020, 1, 4, 15, 18, 51, 46, DateTimeKind.Local).AddTicks(3251), "*", null, "PermissionModule.Permission.Insert", (short)1 },
                    { 2, "1", new DateTime(2020, 1, 4, 15, 18, 51, 46, DateTimeKind.Local).AddTicks(4176), "Permission - view data", false, false, "1", new DateTime(2020, 1, 4, 15, 18, 51, 46, DateTimeKind.Local).AddTicks(4197), "PermissionModule.Permission.*", "View", "PermissionModule.Permission.Insert", (short)1 },
                    { 3, "1", new DateTime(2020, 1, 4, 15, 18, 51, 46, DateTimeKind.Local).AddTicks(4242), "Permission - edit data", false, false, "1", new DateTime(2020, 1, 4, 15, 18, 51, 46, DateTimeKind.Local).AddTicks(4246), "PermissionModule.Permission.*", "Edit", "PermissionModule.Permission.Insert", (short)1 },
                    { 4, "1", new DateTime(2020, 1, 4, 15, 18, 51, 46, DateTimeKind.Local).AddTicks(4271), "Permission - delete data", false, false, "1", new DateTime(2020, 1, 4, 15, 18, 51, 46, DateTimeKind.Local).AddTicks(4275), "PermissionModule.Permission.*", "Delete", "PermissionModule.Permission.Insert", (short)1 }
                });

            migrationBuilder.InsertData(
                schema: "permission",
                table: "Role",
                columns: new[] { "Id", "CreatedById", "CreatedOn", "Description", "ModifiedById", "ModifiedOn", "Name", "OwnerPermission", "RoleTypeId" },
                values: new object[,]
                {
                    { 1, "1", new DateTime(2020, 1, 4, 15, 18, 51, 44, DateTimeKind.Local).AddTicks(7672), "The one who manages the system", "1", new DateTime(2020, 1, 4, 15, 18, 51, 44, DateTimeKind.Local).AddTicks(8442), "Super Admin", "*", (short)1 },
                    { 2, "1", new DateTime(2020, 1, 4, 15, 18, 51, 44, DateTimeKind.Local).AddTicks(9847), "User with limited permissions", "1", new DateTime(2020, 1, 4, 15, 18, 51, 44, DateTimeKind.Local).AddTicks(9870), "User", "*", (short)1 },
                    { 3, "1", new DateTime(2020, 1, 4, 15, 18, 51, 44, DateTimeKind.Local).AddTicks(9942), "Admin with somewhat limited functionalities", "1", new DateTime(2020, 1, 4, 15, 18, 51, 44, DateTimeKind.Local).AddTicks(9946), "Admin", "*", (short)1 }
                });

            migrationBuilder.InsertData(
                schema: "permission",
                table: "RolePermission",
                columns: new[] { "Id", "CreatedById", "CreatedOn", "IsAllowed", "IsDeleted", "ModifiedById", "ModifiedOn", "PermissionId", "RoleId" },
                values: new object[] { 1, "1", new DateTime(2020, 1, 4, 15, 18, 51, 46, DateTimeKind.Local).AddTicks(8656), true, false, "1", new DateTime(2020, 1, 4, 15, 18, 51, 46, DateTimeKind.Local).AddTicks(9585), 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_PermissionGroupId",
                schema: "permission",
                table: "Permission",
                column: "PermissionGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleTypeId",
                schema: "permission",
                table: "Role",
                column: "RoleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionId",
                schema: "permission",
                table: "RolePermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleId",
                schema: "permission",
                table: "RolePermission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ParentRoleId",
                schema: "permission",
                table: "RoleRelations",
                column: "ParentRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleId",
                schema: "permission",
                table: "RoleRelations",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolePermission",
                schema: "permission");

            migrationBuilder.DropTable(
                name: "RoleRelations",
                schema: "permission");

            migrationBuilder.DropTable(
                name: "Permission",
                schema: "permission");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "permission");

            migrationBuilder.DropTable(
                name: "PermissionGroup",
                schema: "permission");

            migrationBuilder.DropTable(
                name: "RoleType",
                schema: "permission");
        }
    }
}
