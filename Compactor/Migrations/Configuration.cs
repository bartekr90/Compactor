namespace Compactor.Migrations
{
    using Compactor.Models.Domain;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Compactor.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Compactor.Models.ApplicationDbContext context)
        {
            context.EquipmentTypes.AddOrUpdate(
                new EquipmentType { ID = 1, Name = "Wiertarka", Price = 20, TotalNumber = 2, BorrowedNumber = 0},
                new EquipmentType { ID = 2, Name = "Ubijarka", Price = 40, TotalNumber = 2, BorrowedNumber = 0 },
                new EquipmentType { ID = 3, Name = "Kosiarka", Price = 15, TotalNumber = 2, BorrowedNumber = 0 },
                new EquipmentType { ID = 4, Name = "Piła", Price = 10, TotalNumber = 2, BorrowedNumber = 0 }
                );
            context.Devices.AddOrUpdate(
                new Device { ID = 1, Name = "BOSH WD-2", TypeID = 1, IsRented = false, SerialNr = "DD-33-2FCA"},
                new Device { ID = 2, Name = "Makita 23F", TypeID = 1, IsRented = false, SerialNr = "343-373-2CA"},
                new Device { ID = 3, Name = "Stanley HP102", TypeID = 2, IsRented = false, SerialNr = "FD3-3JF3-2FCA"},
                new Device { ID = 4, Name = "Makita RWD3", TypeID = 2, IsRented = false, SerialNr = "547-453-7DD-573"},
                new Device { ID = 5, Name = "SOLO 98RX", TypeID = 3, IsRented = false, SerialNr = "YRDD-HN33-2FCA"},
                new Device { ID = 6, Name = "Viking D4", TypeID = 3, IsRented = false, SerialNr = "8764-DD33-2FCA-56"},
                new Device { ID = 7, Name = "STIHL SAW67", TypeID = 4, IsRented = false, SerialNr = "23-JU-33-2F-CA"},
                new Device { ID = 8, Name = "MAXPRO 232", TypeID = 4, IsRented = false, SerialNr = "98-32-45-66-34DD"}
                );
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
