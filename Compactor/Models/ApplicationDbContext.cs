using System.Data.Entity;
using Compactor.Models.Domain;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Compactor.Models
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<UserData> UserDatas { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<EquipmentGroup> EquipmentGroups { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationPosition> ReservationPositions { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.Reservations)
                .WithRequired(x => x.User)
                .HasForeignKey(x => x.IdUser)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.UserDatas)
                .WithRequired(x => x.User)
                .HasForeignKey(x => x.IdUser)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Address>()
                .HasMany(x => x.Users)
                .WithRequired(x => x.Address)
                .HasForeignKey(x => x.IdAddress)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Address>()
                .HasMany(x => x.UserDatas)
                .WithRequired(x => x.Address)
                .HasForeignKey(x => x.IdAddress)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Equipment>()
                .HasMany(x => x.ReservationPositions)
                .WithRequired(x => x.Equipment)
                .HasForeignKey(x => x.IdEquipment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EquipmentGroup>()
                .HasMany(x => x.ReservationPositions)
                .WithRequired(x => x.Group)
                .HasForeignKey(x => x.IdGroup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EquipmentGroup>()
                .HasMany(x => x.Equipments)
                .WithRequired(x => x.Group)
                .HasForeignKey(x => x.IdGroup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Reservation>()
                .HasMany(x => x.ReservationPositions)
                .WithRequired(x => x.Reservation)
                .HasForeignKey(x => x.IdReservation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserData>()
                .HasMany(x => x.Reservations)
                .WithRequired(x => x.UserData)
                .HasForeignKey(x => x.IdUserData)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

    }
}