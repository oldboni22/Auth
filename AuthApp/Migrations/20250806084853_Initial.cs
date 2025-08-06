using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthApp.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    password_hash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password_salt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    last_updated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.user_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_user_name",
                table: "users",
                column: "user_name",
                unique: true);
            
            CreateUserLastUpdatedTrigger(migrationBuilder);
            CreateUserSetCreatedTrigger(migrationBuilder);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            DropUserLastUpdatedTrigger(migrationBuilder);
            DropUserSetCreatedTrigger(migrationBuilder);
            
            migrationBuilder.DropTable(
                name: "users");
        }


        private void CreateUserSetCreatedTrigger(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql
            (
                $@"CREATE TRIGGER {User.SetCreatedTrigger}
                ON dbo.users
                INSTEAD OF INSERT
                AS
                    BEGIN
                        SET NOCOUNT ON;

                        INSERT INTO dbo.users(user_name, password_hash, password_salt, last_updated, created)
                        SELECT
                            i.user_name,
                            i.password_hash,
                            i.password_salt,
                            ISNULL(i.last_updated, GETDATE()),
                            ISNULL(i.created, GETDATE())
                        FROM inserted i;
                    END;"
            );
        }
        
        private void DropUserSetCreatedTrigger(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@$"DROP TRIGGER {User.SetCreatedTrigger}");
        }
        
        private void CreateUserLastUpdatedTrigger(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql
                (
                    $@"CREATE TRIGGER {User.LastUpdatedTrigger}
                    ON dbo.users
                    AFTER UPDATE
                    AS
                        BEGIN
                            SET NOCOUNT ON;

                            UPDATE t
                                SET last_updated = GETDATE()
                                FROM dbo.users t
                            INNER JOIN inserted i ON t.user_id = i.user_id;
                        END;"
                );
        }
        
        private void DropUserLastUpdatedTrigger(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@$"DROP TRIGGER {User.LastUpdatedTrigger}");
        }
        
        
    }
}
