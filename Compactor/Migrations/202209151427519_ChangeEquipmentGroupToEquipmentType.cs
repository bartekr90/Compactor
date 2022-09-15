namespace Compactor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeEquipmentGroupToEquipmentType : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.EquipmentGroups", newName: "EquipmentTypes");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.EquipmentTypes", newName: "EquipmentGroups");
        }
    }
}
