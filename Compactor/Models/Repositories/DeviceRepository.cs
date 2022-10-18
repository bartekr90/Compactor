using Compactor.Models;
using Compactor.Models.Domain;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Compactor.Controllers
{
    public class DeviceRepository
    {
        public Device GetFirstEqWithIdOf(int typeId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Devices
                    .Include(eq => eq.Type)
                    .FirstOrDefault(eq => eq.TypeID == typeId && eq.IsRented == false);
            }
        }

        public void UpdateStates(ICollection<ReservationPosition> reservationPositions)
        {
            using (var context = new ApplicationDbContext())
            {
                foreach (var item in reservationPositions)
                {
                    var eqToUpdate = context.Devices
                        .Single(x => x.ID == item.DeviceID);

                    eqToUpdate.IsRented = item.IsActiv;
                }
                context.SaveChanges();
            }
        }
    }
}