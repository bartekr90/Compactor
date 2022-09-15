namespace Compactor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePropertyNames : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.UserDatas", name: "IdAddress", newName: "AddressID");
            RenameColumn(table: "dbo.AspNetUsers", name: "IdAddress", newName: "AddressID");
            RenameColumn(table: "dbo.Reservations", name: "IdUserData", newName: "UserDataID");
            RenameColumn(table: "dbo.UserDatas", name: "IdUser", newName: "UserID");
            RenameColumn(table: "dbo.ReservationPositions", name: "IdReservation", newName: "ReservationID");
            RenameColumn(table: "dbo.Reservations", name: "IdUser", newName: "UserID");
            RenameColumn(table: "dbo.ReservationPositions", name: "IdEquipment", newName: "EquipmentID");
            RenameColumn(table: "dbo.Equipments", name: "IdGroup", newName: "TypeID");
            RenameColumn(table: "dbo.ReservationPositions", name: "IdGroup", newName: "TypeID");
            RenameIndex(table: "dbo.UserDatas", name: "IX_IdUser", newName: "IX_UserID");
            RenameIndex(table: "dbo.UserDatas", name: "IX_IdAddress", newName: "IX_AddressID");
            RenameIndex(table: "dbo.Reservations", name: "IX_IdUser", newName: "IX_UserID");
            RenameIndex(table: "dbo.Reservations", name: "IX_IdUserData", newName: "IX_UserDataID");
            RenameIndex(table: "dbo.ReservationPositions", name: "IX_IdEquipment", newName: "IX_EquipmentID");
            RenameIndex(table: "dbo.ReservationPositions", name: "IX_IdGroup", newName: "IX_TypeID");
            RenameIndex(table: "dbo.ReservationPositions", name: "IX_IdReservation", newName: "IX_ReservationID");
            RenameIndex(table: "dbo.Equipments", name: "IX_IdGroup", newName: "IX_TypeID");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_IdAddress", newName: "IX_AddressID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_AddressID", newName: "IX_IdAddress");
            RenameIndex(table: "dbo.Equipments", name: "IX_TypeID", newName: "IX_IdGroup");
            RenameIndex(table: "dbo.ReservationPositions", name: "IX_ReservationID", newName: "IX_IdReservation");
            RenameIndex(table: "dbo.ReservationPositions", name: "IX_TypeID", newName: "IX_IdGroup");
            RenameIndex(table: "dbo.ReservationPositions", name: "IX_EquipmentID", newName: "IX_IdEquipment");
            RenameIndex(table: "dbo.Reservations", name: "IX_UserDataID", newName: "IX_IdUserData");
            RenameIndex(table: "dbo.Reservations", name: "IX_UserID", newName: "IX_IdUser");
            RenameIndex(table: "dbo.UserDatas", name: "IX_AddressID", newName: "IX_IdAddress");
            RenameIndex(table: "dbo.UserDatas", name: "IX_UserID", newName: "IX_IdUser");
            RenameColumn(table: "dbo.ReservationPositions", name: "TypeID", newName: "IdGroup");
            RenameColumn(table: "dbo.Equipments", name: "TypeID", newName: "IdGroup");
            RenameColumn(table: "dbo.ReservationPositions", name: "EquipmentID", newName: "IdEquipment");
            RenameColumn(table: "dbo.Reservations", name: "UserID", newName: "IdUser");
            RenameColumn(table: "dbo.ReservationPositions", name: "ReservationID", newName: "IdReservation");
            RenameColumn(table: "dbo.UserDatas", name: "UserID", newName: "IdUser");
            RenameColumn(table: "dbo.Reservations", name: "UserDataID", newName: "IdUserData");
            RenameColumn(table: "dbo.AspNetUsers", name: "AddressID", newName: "IdAddress");
            RenameColumn(table: "dbo.UserDatas", name: "AddressID", newName: "IdAddress");
        }
    }
}
