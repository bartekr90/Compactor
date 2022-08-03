namespace Compactor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Street = c.String(nullable: false),
                        Number = c.String(nullable: false),
                        City = c.String(nullable: false),
                        PostalCode = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        IdUser = c.String(nullable: false, maxLength: 128),
                        IdAddress = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.IdUser)
                .ForeignKey("dbo.Addresses", t => t.IdAddress)
                .Index(t => t.IdUser)
                .Index(t => t.IdAddress);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Comments = c.String(),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RentDate = c.DateTime(nullable: false),
                        ReturnDate = c.DateTime(nullable: false),
                        IdUser = c.String(nullable: false, maxLength: 128),
                        IdUserData = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.IdUser)
                .ForeignKey("dbo.UserDatas", t => t.IdUserData)
                .Index(t => t.IdUser)
                .Index(t => t.IdUserData);
            
            CreateTable(
                "dbo.ReservationPositions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsActiv = c.Boolean(nullable: false),
                        SequenceNumber = c.Int(nullable: false),
                        RentQuantity = c.Int(nullable: false),
                        IdEquipment = c.Int(nullable: false),
                        IdGroup = c.Int(nullable: false),
                        IdReservation = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EquipmentGroups", t => t.IdGroup)
                .ForeignKey("dbo.Equipments", t => t.IdEquipment)
                .ForeignKey("dbo.Reservations", t => t.IdReservation)
                .Index(t => t.IdEquipment)
                .Index(t => t.IdGroup)
                .Index(t => t.IdReservation);
            
            CreateTable(
                "dbo.Equipments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        SerialNr = c.String(nullable: false),
                        IsRented = c.Boolean(nullable: false),
                        IdGroup = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EquipmentGroups", t => t.IdGroup)
                .Index(t => t.IdGroup);
            
            CreateTable(
                "dbo.EquipmentGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        BorrowedNumber = c.Int(nullable: false),
                        TotalNumber = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        IdAddress = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.IdAddress)
                .Index(t => t.IdAddress)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUsers", "IdAddress", "dbo.Addresses");
            DropForeignKey("dbo.UserDatas", "IdAddress", "dbo.Addresses");
            DropForeignKey("dbo.Reservations", "IdUserData", "dbo.UserDatas");
            DropForeignKey("dbo.UserDatas", "IdUser", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reservations", "IdUser", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ReservationPositions", "IdReservation", "dbo.Reservations");
            DropForeignKey("dbo.ReservationPositions", "IdEquipment", "dbo.Equipments");
            DropForeignKey("dbo.ReservationPositions", "IdGroup", "dbo.EquipmentGroups");
            DropForeignKey("dbo.Equipments", "IdGroup", "dbo.EquipmentGroups");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "IdAddress" });
            DropIndex("dbo.Equipments", new[] { "IdGroup" });
            DropIndex("dbo.ReservationPositions", new[] { "IdReservation" });
            DropIndex("dbo.ReservationPositions", new[] { "IdGroup" });
            DropIndex("dbo.ReservationPositions", new[] { "IdEquipment" });
            DropIndex("dbo.Reservations", new[] { "IdUserData" });
            DropIndex("dbo.Reservations", new[] { "IdUser" });
            DropIndex("dbo.UserDatas", new[] { "IdAddress" });
            DropIndex("dbo.UserDatas", new[] { "IdUser" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.EquipmentGroups");
            DropTable("dbo.Equipments");
            DropTable("dbo.ReservationPositions");
            DropTable("dbo.Reservations");
            DropTable("dbo.UserDatas");
            DropTable("dbo.Addresses");
        }
    }
}
