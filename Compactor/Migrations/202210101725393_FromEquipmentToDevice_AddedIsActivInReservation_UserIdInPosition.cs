namespace Compactor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FromEquipmentToDevice_AddedIsActivInReservation_UserIdInPosition : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Equipments", newName: "Devices");
            RenameColumn(table: "dbo.ReservationPositions", name: "EquipmentID", newName: "DeviceID");
            RenameIndex(table: "dbo.ReservationPositions", name: "IX_EquipmentID", newName: "IX_DeviceID");
            AddColumn("dbo.Reservations", "IsActiv", c => c.Boolean(nullable: false));
            AddColumn("dbo.ReservationPositions", "UserID", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.ReservationPositions", "UserID");
            AddForeignKey("dbo.ReservationPositions", "UserID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReservationPositions", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.ReservationPositions", new[] { "UserID" });
            DropColumn("dbo.ReservationPositions", "UserID");
            DropColumn("dbo.Reservations", "IsActiv");
            RenameIndex(table: "dbo.ReservationPositions", name: "IX_DeviceID", newName: "IX_EquipmentID");
            RenameColumn(table: "dbo.ReservationPositions", name: "DeviceID", newName: "EquipmentID");
            RenameTable(name: "dbo.Devices", newName: "Equipments");
        }
    }
}
